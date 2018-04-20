using System;

namespace Lottery.Dtos.ConLog
{
    public class ConLogDto
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string SystemTypeId { get; set; }

        public DateTime LoginTime { get; set; }

        public DateTime? InvalidTime { get; set; }

        public DateTime? LogoutTime { get; set; }

        public int OnlineTime { get; set; }

        public int ClientNo { get; set; }

        public string Ip { get; set; }

        public int UpdateTokenCount { get; set; }
    }
}