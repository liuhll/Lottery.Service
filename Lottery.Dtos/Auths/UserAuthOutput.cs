using System;
using Lottery.Infrastructure.Enums;

namespace Lottery.Dtos.Auths
{
    public class UserAuthOutput
    {
        public string AuthOrderNo { get; set; }

        public string SalesOrderNo { get; set; }

        public SellType AuthType { get; set; }

        public DateTime? AuthTime { get; set; }

        public DateTime? InvalidDate { get; set; }

        public string Notes { get; set; }

    }
}