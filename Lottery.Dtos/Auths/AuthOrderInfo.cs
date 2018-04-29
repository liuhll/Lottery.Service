using System;
using Lottery.Infrastructure.Enums;

namespace Lottery.Dtos.Auths
{
    public class AuthOrderInfo : UserAuthDto
    {
        public string Id { get; set; }

        public string AuthUserId { get; set; }

        public string AuthRankId { get; set; }

        public AuthStatus Status { get; set; }

        public string CreateBy { get; set; }

        public DateTime CreateTime { get; set; }

        public string UpdateBy { get; set; }

        public DateTime? UpdateTime { get; set; }

    }
}