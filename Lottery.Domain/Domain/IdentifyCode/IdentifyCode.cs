using ENode.Domain;
using System;

namespace Lottery.Core.Domain.IdentifyCode
{
    public class IdentifyCode : AggregateRoot<string>
    {
        public IdentifyCode(string id, string receiver,
            string code, int identifyCodeType, int messageType, DateTime expirationDate, string createBy,
            string updateBy) : base(id)
        {
            Receiver = receiver;
            Code = code;
            IdentifyCodeType = identifyCodeType;
            MessageType = messageType;
            CreateBy = createBy;
            UpdateBy = updateBy;
            CreateTime = DateTime.Now;
            UpdateTime = DateTime.Now;
            ExpirationDate = expirationDate;
            ValidateDate = null;
            Status = 0;
            ApplyEvent(new AddIdentifyCodeEvent(receiver, code, identifyCodeType, messageType, expirationDate, createBy));
        }

        public string Code { get; private set; }
        public string Receiver { get; private set; }

        public int IdentifyCodeType { get; private set; }

        public int MessageType { get; private set; }

        public DateTime ExpirationDate { get; private set; }

        public DateTime? ValidateDate { get; private set; }

        public int Status { get; private set; }

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

        #region 公有方法

        public void UpdateIdentifyCode(string code, DateTime expirationData, string updateBy)
        {
            ApplyEvent(new UpdateIdentifyCodeEvent(code, Receiver, expirationData, updateBy));
        }

        public void InvalidIdentifyCode(string updateBy)
        {
            ApplyEvent(new InvalidIdentifyCodeEvent(Receiver, updateBy));
        }

        #endregion 公有方法

        #region Handle Methods

        private void Handle(AddIdentifyCodeEvent evt)
        {
            this.Code = evt.Code;
            this.Receiver = evt.Receiver;
            this.ExpirationDate = evt.ExpirationDate;
            this.CreateBy = evt.CreateBy;
            this.Status = evt.Status;
            this.IdentifyCodeType = evt.IdentifyCodeType;
        }

        private void Handle(UpdateIdentifyCodeEvent evt)
        {
            Code = evt.Code;
            Receiver = evt.Receiver;
            ExpirationDate = evt.ExpirationDate;
        }

        private void Handle(InvalidIdentifyCodeEvent evt)
        {
            ValidateDate = evt.ValidateDate;
            Status = evt.Status;
            UpdateBy = evt.UpdateBy;
            Receiver = evt.Receiver;
        }

        #endregion Handle Methods
    }
}