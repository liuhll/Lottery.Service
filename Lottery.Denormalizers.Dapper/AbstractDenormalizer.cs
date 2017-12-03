using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommon.IO;
using Lottery.Infrastructure;
using IOException = System.IO.IOException;

namespace Lottery.Denormalizers.Dapper
{
    public abstract class AbstractDenormalizer
    {
        protected async Task<AsyncTaskResult> TryInsertRecordAsync(Func<IDbConnection, Task<long>> action, IDbConnection connection = null)
        {
            try
            {
                if (connection == null)
                {
                    connection = GetLotteryConnection();
                }
                using (connection)
                {
                    connection.Open();
                    await action(connection);
                    return AsyncTaskResult.Success;
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)  //主键冲突，忽略即可；出现这种情况，是因为同一个消息的重复处理
                {
                    return AsyncTaskResult.Success;
                }
                throw new IOException("Insert record failed.", ex);
            }
        }
        protected async Task<AsyncTaskResult> TryUpdateRecordAsync(Func<IDbConnection, Task<int>> action,IDbConnection connection = null)
        {
            try
            {
                if (connection == null)
                {
                    connection = GetLotteryConnection();
                }
                using (connection)
                {
                    connection.Open();
                    await action(connection);
                    return AsyncTaskResult.Success;
                }
            }
            catch (SqlException ex)
            {
                throw new IOException("Update record failed.", ex);
            }
        }
        protected IDbConnection GetLotteryConnection()
        {
            return new SqlConnection(DataConfigSettings.LotteryConnectionString);
        }

        protected IDbConnection GetForecastLotteryConnection()
        {
            return new SqlConnection(DataConfigSettings.ForecastLotteryConnectionString);
        }
    }
}
