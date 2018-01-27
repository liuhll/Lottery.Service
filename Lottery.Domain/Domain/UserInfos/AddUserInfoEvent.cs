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
            AccountRegistType accountRegistType,int points)
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
            Points = points;

        }

        public AddUserInfoEvent(string userName, string email, string phone, string password,
            bool isActive, ClientRegistType clientRegistType, bool isDelete,
            AccountRegistType accountRegistType, int points, int balance,
            int totalRecharge, int totalConsumeAccount, int pointCount, int amountCount)
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
            Points = points;
            TotalRecharge = totalConsumeAccount;
            TotalConsumeAccount = totalConsumeAccount;
            AmountCount = amountCount;
            Balance = balance;
            PointCount = pointCount;
        }

        public int Balance { get; private set; }

        public int Points { get; private set; }

        public int TotalRecharge { get; private set; }

        public int TotalConsumeAccount { get; private set; }

        public int PointCount { get; private set; }

        public int AmountCount { get; private set; }

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