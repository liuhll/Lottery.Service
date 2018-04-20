using Lottery.Infrastructure.Enums;
using System;

namespace Lottery.Dtos.Auths
{
    public class UserAuthDto
    {
        public string AuthOrderNo { get; set; }

        public string SaleRecordId { get; set; }

        public SellType AuthType { get; set; }

        public DateTime AuthTime { get; set; }

        public DateTime InvalidDate { get; set; }

        public string Notes { get; set; }

        //  public AuthStatus Status { get; set; }
    }
}