using ENode.Domain;
using System;

namespace Lottery.Core.Domain.OpinionRecords
{
    public class OpinionRecord : AggregateRoot<string>
    {
        public OpinionRecord(string id, int opinionType, string content, int platform,
            string contactWay, string createBy) : base(id)
        {
            OpinionType = opinionType;
            Content = content;
            Platform = platform;
            ContactWay = contactWay;
            CreateBy = createBy;
            CreateTime = DateTime.Now;
            Status = 0;
            ApplyEvent(new AddOpinionRecordEvent(opinionType, content, platform, contactWay, createBy, Status));
        }

        public int OpinionType { get; private set; }

        public string Content { get; private set; }

        public int Platform { get; private set; }

        public string ContactWay { get; private set; }

        public bool IsAccept { get; private set; }

        public string ReplyContent { get; private set; }

        public int ReplyWay { get; private set; }

        public int Status { get; private set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; private set; }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime CreateTime { get; private set; }

        public string UpdateBy { get; private set; }

        public DateTime? UpdateTime { get; private set; }

        #region handle

        private void Handle(AddOpinionRecordEvent evt)
        {
            OpinionType = evt.OpinionType;
            Content = evt.Content;
            Platform = evt.Platform;
            ContactWay = evt.ContactWay;
            CreateBy = evt.CreateBy;
            CreateTime = evt.Timestamp;
            Status = evt.Status;
        }

        #endregion handle
    }
}