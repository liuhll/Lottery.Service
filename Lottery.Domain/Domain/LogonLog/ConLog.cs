using System;
using ENode.Domain;
using Lottery.Core.Domain.UserInfos;

namespace Lottery.Core.Domain.LogonLog
{
    public class ConLog : AggregateRoot<string>
    {
        public ConLog(string id,int clientNo,string systemTypeId,string ip,string userId,DateTime invalidDateTime,string createBy) : base(id)
        {
            UserId = userId;
            UpdateTokenCount = 0;
            CreateBy = createBy;
            ClientNo = clientNo;
            Ip = ip;
            SystemTypeId = systemTypeId;
            InvalidDateTime = invalidDateTime;
            CreateTime = DateTime.Now;
            ApplyEvent(new AddConLogEvent(UserId,ClientNo, SystemTypeId, Ip,InvalidDateTime, CreateBy));
            ApplyEvent(new UpdateLastLoginTimeEvent(UserId));
        }

        public string UserId { get; private set; }

        public string SystemTypeId { get; private set; }

        public int UpdateTokenCount { get; private set; }

        public string CreateBy { get; private set; }

        public DateTime CreateTime { get; private set; }

        public DateTime InvalidDateTime { get; private set; }

        public int ClientNo { get; private set; }

        public string Ip { get; private set; }

        public DateTime LoginTime { get; set; }

        public DateTime LogoutTime { get; set; }

        public string UpdateBy { get; private set; }

        public DateTime UpdateTime { get; private set; }

        public void UpdateToken(string userId, DateTime updateTokenTime, string updateBy)
        {
            ApplyEvent(new UpdateTokenEvent(userId, updateTokenTime, updateBy));         
        }

        public void Logout()
        {
            ApplyEvent(new LogoutEvent(UserId,LoginTime));
        }

        #region hander event

        private void Handle(AddConLogEvent evt)
        {
            UserId = evt.UserId;
            ClientNo = evt.ClientNo;
            Ip = evt.Ip;
            SystemTypeId = evt.SystemTypeId;
            LoginTime = evt.Timestamp;
            InvalidDateTime = evt.InvalidTime;
            CreateBy = evt.CreateBy;
            CreateTime = evt.Timestamp;
        }

        private void Handle(UpdateTokenEvent evt)
        {
            UserId = evt.UserId;
            UpdateTime = evt.UpdateTokenTime;
            UpdateBy = evt.UpdateBy;
            UpdateTime = evt.Timestamp;
        }

        private void Handle(LogoutEvent evt)
        {
            LoginTime = evt.Timestamp;
        }


        private void Handle(UpdateLastLoginTimeEvent evt)
        {
        }

        #endregion


    }
}