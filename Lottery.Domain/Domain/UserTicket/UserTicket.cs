using System;
using ENode.Domain;
using Lottery.Core.Domain.UserInfos;

namespace Lottery.Core.Domain.UserTicket
{
    public class UserTicket : AggregateRoot<string>
    {
        public UserTicket(string id,string userId,string accessToken,string createBy) : base(id)
        {
            UserId = userId;
            AccessToken = accessToken;
            CreateBy = createBy;
            CreateTime = DateTime.Now;

            ApplyEvent(new AddUserTicketEvent(UserId,AccessToken,CreateBy));
            ApplyEvent(new UpdateLastLoginTimeEvent(UserId));
        }

        public string UserId { get; private set; }

        public string AccessToken { get; private set; }

        public string CreateBy { get; private set; }

        public DateTime CreateTime { get; private set; }

        public string UpdateBy { get; private set; }

        public DateTime UpdateTime { get; private set; }

        public void UpdateAccessToken(string userId, string accessToken, string updateBy)
        {
            ApplyEvent(new UpdateUserTicketEvent(userId,accessToken,updateBy));
            ApplyEvent(new UpdateLastLoginTimeEvent(userId));
        }

        public void InvalidAccessToken()
        {
            ApplyEvent(new InvalidAccessTokenEvent(UserId));
        }

        #region hander event

        private void Handle(AddUserTicketEvent evt)
        {
            UserId = evt.UserId;
            AccessToken = evt.AccessToken;
            CreateBy = evt.CreateBy;
            CreateTime = evt.Timestamp;
        }

        private void Handle(UpdateUserTicketEvent evt)
        {
            UserId = evt.UserId;
            AccessToken = evt.AccessToken;
            UpdateBy = evt.UpdateBy;
            UpdateTime = evt.Timestamp;
        }

        private void Handle(InvalidAccessTokenEvent evt)
        {
            AccessToken = evt.AccessToken;  
        }

        private void Handle(UpdateLastLoginTimeEvent evt)
        {
        }

        #endregion


    }
}