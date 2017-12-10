namespace Lottery.Infrastructure
{
    public class SqlConstants
    {
        public const string CreateDBSql = "CREATE DATABASE {0};";

        public const string CreatePredictTableSql = @"
/*==============================================================*/
/* Table: {0}                             */
/*==============================================================*/
create table {0} (
   Id                   varchar(36)          not null,
   NormConfigId         varchar(36)          null,
   CurrentPredictPeriod int                  not null,
   StartPeriod          int                  null,
   EndPeriod            int                  null,
   MinorCycle           int                  null,
   PredictedData        varchar(200)         null,
   PredictedResult      int                  null,
   CurrentScore         decimal(18,2)        null,
   CreateBy             varchar(36)          null,
   CreateTime           datetime             null,
   UpdateBy             varchar(36)          null,
   UpdateTime           datetime             null,
   constraint PK_LA_LOTTERYPREDICTDATA_{1} primary key (Id)
)
";
    }
}