using ECommon.Components;
using ECommon.IO;
using ECommon.Logging;
using Lottery.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using IOException = System.IO.IOException;

namespace Lottery.Denormalizers.Dapper
{
    public abstract class AbstractDenormalizer
    {
        protected readonly ILogger logger =
            ObjectContainer.Resolve<ILoggerFactory>().Create(typeof(AbstractDenormalizer));

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

        protected async Task<AsyncTaskResult> TryUpdateRecordAsync(Func<IDbConnection, Task<int>> action, IDbConnection connection = null)
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

        protected AsyncTaskResult TryTransactionAsync(Func<IDbConnection, IDbTransaction, ICollection<Action>> func, IDbConnection conn = null)
        {
            if (conn == null)
            {
                conn = GetLotteryConnection();
            }
            using (var connection = conn)
            {
                connection.Open();
                var transaction = connection.BeginTransaction();
                try
                {
                    var actions = func(conn, transaction);

                    foreach (var action in actions)
                    {
                        action();
                    }

                    transaction.Commit();
                    return AsyncTaskResult.Success;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        protected IDbConnection GetLotteryConnection()
        {
            return new SqlConnection(DataConfigSettings.LotteryConnectionString);
        }

        protected IDbConnection GetForecastLotteryConnection(string lotteryCode)
        {
            return new SqlConnection(string.Format(DataConfigSettings.ForecastLotteryConnectionString, lotteryCode));
        }
    }
}