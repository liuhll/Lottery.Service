using ENode.Eventing;
using System;

namespace Lottery.Core.Domain.OpinionRecords
{
    public class AddOpinionRecordEvent : DomainEvent<string>
    {
        private AddOpinionRecordEvent()
        {
        }

        public AddOpinionRecordEvent(int opinionType, string content, int platform,
            string contactWay, string createBy, int status)
        {
            OpinionType = opinionType;
            Content = content;
            Platform = platform;
            ContactWay = contactWay;
            CreateBy = createBy;
            CreateTime = DateTime.Now;
            Status = status;
        }

        public int OpinionType { get; private set; }

        public string Content { get; private set; }

        public int Platform { get; private set; }

        public string ContactWay { get; private set; }

        public int Status { get; private set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; private set; }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime CreateTime { get; private set; }
    }
}