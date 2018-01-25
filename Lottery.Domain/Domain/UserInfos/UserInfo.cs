using System;
using ENode.Domain;
using Lottery.Infrastructure.Enums;

namespace Lottery.Core.Domain.UserInfos
{
   public class UserInfo : AggregateRoot<string>
   {
      public UserInfo(
        string id,
        string userName,
        string email,
        string phone,
        string password,
        bool isActive,
        ClientRegistType clientRegistType,
        AccountRegistType accountRegistType,
        int points
        ) : base(id)
      {
            UserName = userName;
            Email = email;
            Phone = phone;
            Password = password;
            IsActive = isActive;
            ClientRegistType = clientRegistType;
            CreateTime = DateTime.Now;
            IsDelete = false;
            AccountRegistType = accountRegistType;
            Points = points;

            ApplyEvent(new AddUserInfoEvent(UserName, Email, Phone, Password, IsActive, ClientRegistType, IsDelete, AccountRegistType,Points));
      }         
 
      /// <summary>
      /// 用户名
      /// </summary>
      public string UserName { get; private set; }
      
      /// <summary>
      /// 昵称
      /// </summary>
      public string SurName { get; private set; }
      
      /// <summary>
      /// Email电子邮件
      /// </summary>
      public string Email { get; private set; }
      
      /// <summary>
      /// 手机号码
      /// </summary>
      public string Phone { get; private set; }
      
      /// <summary>
      /// 密码
      /// </summary>
      public string Password { get; private set; }
      

       /// <summary>
      /// 是否激活:1,有效；0.冻结
      /// </summary>
       public bool IsActive { get; private set; }

       public AccountRegistType AccountRegistType { get; private set; }

       public int Balance { get; private set; }

       public int Points { get; private set; }

       public int TotalRecharge { get; private set; }

       public int TotalConsumeAccount { get; private set; }

       public int PointCount { get; private set; }

       public int AmountCount { get; set; }

       /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime? LastLoginTime { get; private set; }

        /// <summary>
        /// 用户登录的客户端个数
        /// </summary>
       public int LoginClientCount { get; private set; }

       /// <summary>
       /// qq
       /// </summary>
       public string QQCode { get; private set; }
      
      /// <summary>
      /// 微信
      /// </summary>
      public string Wechat { get; private set; }
      
      /// <summary>
      /// 微信OpenId
      /// </summary>
      public string WechatOpenId { get; private set; }
      
      /// <summary>
      /// 用户注册来源
      /// </summary>
      public ClientRegistType ClientRegistType { get; private set; }
      
      /// <summary>
      /// 最后修改人
      /// </summary>
      public DateTime? UpdateBy { get; private set; }
      
      /// <summary>
      /// 最后修改时间
      /// </summary>
      public DateTime? UpdateTime { get; private set; }
      
      /// <summary>
      /// 账号创建人
      /// </summary>
      public string Createby { get; private set; }
      
      /// <summary>
      /// 创建时间
      /// </summary>
      public DateTime CreateTime { get; private set; }
      
      /// <summary>
      /// 
      /// </summary>
      public bool IsDelete { get; private set; }



       #region public method

       public void BindUserEmail(string email)
       {
           ApplyEvent(new BindUserEmailEvent(email));
       }

       public void BindUserPhone(string phone)
       {
           ApplyEvent(new BindUserPhoneEvent(phone));
       }
       public void UpdateLastLoginTime()
       {
           ApplyEvent(new UpdateLoginTimeEvent());
       }
        #endregion

        #region Handle Method

        private void Handle(AddUserInfoEvent evnt)
        {
           Email = evnt.Email;
           Password = evnt.Password;
           Phone = evnt.Phone;
           AccountRegistType = evnt.AccountRegistType;
           ClientRegistType = evnt.ClientRegistType;
           CreateTime = evnt.CreateTime;
           IsActive = evnt.IsActive;
           IsDelete = evnt.IsDelete;
         
        }

       private void Handle(BindUserEmailEvent evnt)
       {
           Email = evnt.Email;
       }
       private void Handle(BindUserPhoneEvent evnt)
       {
           Phone = evnt.Phone;
       }

       private void Handle(UpdateLoginTimeEvent evnt)
       {
           LastLoginTime = evnt.Timestamp;
       }
        #endregion



    }   
}
