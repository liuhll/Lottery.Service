using System;
using ENode.Domain;
using Lottery.Core.Domain.UserInfos;

namespace Lottery.Core.Domain.LogonLog
{
    public class ConLog : AggregateRoot<string>
    {
        public ConLog(string id,int clientNo,string systemTypeId,string ip,string userId,DateTime invalidTime,string createBy) : base(id)
        {
            UserId = userId;
            UpdateTokenCount = 0;
            CreateBy = createBy;
            ClientNo = clientNo;
            Ip = ip;
            SystemTypeId = systemTypeId;
            InvalidTime = invalidTime;
            CreateTime = DateTime.Now;
            ApplyEvent(new AddConLogEvent(UserId,ClientNo, SystemTypeId, Ip,InvalidTime, CreateBy));           
        }

        public string UserId { get; private set; }

        public string SystemTypeId { get; private set; }

        public int UpdateTokenCount { get; private set; }

        public string CreateBy { get; private set; }

        public DateTime CreateTime { get; private set; }

        public DateTime InvalidTime { get; private set; }

        public int ClientNo { get; private set; }

        public string Ip { get; private set; }

        public DateTime LoginTime { get; set; }

        public DateTime LogoutTime { get; set; }

        public string UpdateBy { get; private set; }

        public DateTime UpdateTime { get; private set; }

        public int OnlineTime { get; private set; }

        public void UpdateToken(DateTime invalidTime, string updateBy)
        {
            UpdateTokenCount = UpdateTokenCount + 1;
            ApplyEvent(new UpdateTokenEvent(invalidTime, UpdateTokenCount, updateBy));         
        }

        public void Logout(string updateBy)
        {
            LogoutTime = DateTime.Now;
            OnlineTime = (int)(LogoutTime - LoginTime).TotalSeconds;
            ApplyEvent(new LogoutEvent(updateBy,LogoutTime, OnlineTime));
        }

        #region hander event

        private void Handle(AddConLogEvent evt)
        {
            UserId = evt.UserId;
            ClientNo = evt.ClientNo;
            Ip = evt.Ip;
            SystemTypeId = evt.SystemTypeId;
            LoginTime = evt.Timestamp;
            InvalidTime = evt.InvalidTime;
            CreateBy = evt.CreateBy;
            CreateTime = evt.Timestamp;
            UpdateTokenCount = evt.UpdateTokenCount;
        }

        private void Handle(UpdateTokenEvent evt)
        {
            UpdateBy = evt.UpdateBy;
            InvalidTime = evt.InvalidTime;
            UpdateBy = evt.UpdateBy;
            UpdateTokenCount = evt.UpdateTokenCount;
        }

        private void Handle(LogoutEvent evt)
        {
            LogoutTime = evt.LogoutTime;
            OnlineTime = evt.OnlineTime;
            UpdateBy = evt.UserId;
            UpdateTime = evt.Timestamp;
        }

        #endregion


    }
}