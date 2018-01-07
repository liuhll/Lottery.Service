using System;
using ENode.Eventing;
using Lottery.Infrastructure.Enums;

namespace Lottery.Core.Domain.UserInfos
{
    public class AddUserInfoEvent : DomainEvent<string>
    {
        private AddUserInfoEvent()
        {
        }

        public AddUserInfoEvent(string userName,
            string email,
            string phone,
            string password,
            bool isActive,
            ClientRegistType clientRegistType,
            bool isDelete,
            AccountRegistType accountRegistType)
        {
            UserName = userName;
            Email = email;
            Phone = phone;
            Password = password;
            IsActive = isActive;
            ClientRegistType = clientRegistType;
            CreateTime = DateTime.Now;
            IsDelete = isDelete;
            AccountRegistType = accountRegistType;

        }

        public string UserName { get; private set; }

        public string Email { get; private set; }

        public string Phone { get; private set; }

        public string Password { get; private set; }

        public bool IsActive { get; private set; }

        public DateTime CreateTime { get; private set; }

        public bool IsDelete { get; private set; }

        public ClientRegistType ClientRegistType { get; private set; }

        public AccountRegistType AccountRegistType { get; private set; }

    }
}