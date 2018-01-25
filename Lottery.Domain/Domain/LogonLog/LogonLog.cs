using System;
using ENode.Domain;
using Lottery.Core.Domain.UserInfos;

namespace Lottery.Core.Domain.LogonLog
{
    public class LogonLog : AggregateRoot<string>
    {
        public LogonLog(string id,string userId,string createBy) : base(id)
        {
            UserId = userId;
            UpdateTokenCount = 0;
            CreateBy = createBy;
            CreateTime = DateTime.Now;
            UpdateTime = DateTime.Now;
            ApplyEvent(new AddLogonLogEvent(UserId,CreateBy));
            ApplyEvent(new UpdateLastLoginTimeEvent(UserId));
        }

        public string UserId { get; private set; }

        public int UpdateTokenCount { get; private set; }

        public string CreateBy { get; private set; }

        public DateTime CreateTime { get; private set; }

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

        private void Handle(AddLogonLogEvent evt)
        {
            UserId = evt.UserId;
            LoginTime = evt.Timestamp;
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