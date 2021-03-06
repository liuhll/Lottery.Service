﻿using Lottery.Infrastructure;
using System.Data;
using System.Data.SqlClient;

namespace Lottery.QueryServices.Dapper
{
    public abstract class BaseQueryService
    {
        static BaseQueryService()
        {
            DataConfigSettings.Initialize();
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