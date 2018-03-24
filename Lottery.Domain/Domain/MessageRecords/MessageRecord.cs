using System;
using ENode.Domain;

namespace Lottery.Core.Domain.MessageRecords
{
    public class MessageRecord : AggregateRoot<string>
    {
        public MessageRecord(string id,string sender,string receiver,string title,
            string content,int messageType,int snderPlatform, string createBy,
            string updateBy)
            : base(id)
        {
            Sender = sender;
            Receiver = receiver;
            Title = title;
            Content = content;
            MessageType = messageType;
            SenderPlatform = snderPlatform;
            CreateBy = createBy;
            UpdateBy = updateBy;
            CreateTime = DateTime.Now;
            UpdateTime = DateTime.Now;
            ApplyEvent(new AddMessageRecordEvent(sender,receiver,title,content,messageType,SenderPlatform,createBy));
        }

        public string Sender { get; private set; }

        public string Receiver { get; private set; }

        public string Title { get; private set; }

        public string Content { get; private set; }

        public int MessageType { get; private set; }

        public int SenderPlatform { get; private set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; private set; }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime? CreateTime { get; private set; }

        public string UpdateBy { get; private set; }

        public DateTime? UpdateTime { get; private set; }

        #region Handle Methods

        private void Handle(AddMessageRecordEvent evnt)
        {
            Sender = evnt.Sender;
            Receiver = evnt.Receiver;
            Title = evnt.Title;
            Content = evnt.Content;
            MessageType = evnt.MessageType;
            SenderPlatform = evnt.SenderPlatform;
            CreateBy = evnt.CreateBy;
        }

        #endregion
    }
}