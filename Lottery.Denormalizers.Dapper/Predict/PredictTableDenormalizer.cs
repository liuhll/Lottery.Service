using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using ECommon.Components;
using ECommon.IO;
using ENode.Infrastructure;
using Lottery.Core.Domain.LotteryPredictDatas;
using Lottery.Infrastructure;

namespace Lottery.Denormalizers.Dapper
{
    [Component]
    public class PredictTableDenormalizer : AbstractDenormalizer, IMessageHandler<InitPredictTableEvent>
        
    {
        public async Task<AsyncTaskResult> HandleAsync(InitPredictTableEvent evnt)
        {


            using (var conn = GetLotteryConnection())
            {
                //// 判断是否存在数据库
                var sql = $"SELECT * FROM sys.sysdatabases WHERE name='{evnt.PredictDbName}'";
                var queryResult = conn.QueryFirstOrDefault(sql);
                if (queryResult == null)
                {
                    var createDbSql = string.Format(SqlConstants.CreateDBSql, evnt.PredictDbName);
                    await conn.ExecuteAsync(createDbSql);
                }
            }

            using (var conn = GetForecastLotteryConnection())
            {
                conn.Open();
                var trans = conn.BeginTransaction();
                try
                {
                    var i = 1;
                    foreach (var tableName in evnt.PredictTableNames)
                    {
                        var createTableSql = string.Format(SqlConstants.CreatePredictTableSql, tableName,i);
                        conn.Execute(createTableSql,transaction:trans);
                        i++;
                    }
                    trans.Commit();
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    throw;
                }
            }

            return AsyncTaskResult.Success;
           
        }
    }
}