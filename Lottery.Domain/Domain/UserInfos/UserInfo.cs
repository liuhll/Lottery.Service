using System;
using ENode.Domain;

namespace Lottery.Core.Domain.UserInfos
{
   public class UserInfo : AggregateRoot<string>
   {
      public UserInfo(
        string id,
        string userName,
        string surName,
        string email,
        string phone,
        string password,
        bool isActive,
        string tokenId,
        DateTime? lastLoginTime,
        string qQCode,
        string wechat,
        string wechatOpenId,
        int? userRegistType,
        DateTime? updateBy,
        string createby,
        bool? isDelete
        ) : base(id)
      {
            UserName = userName;
            SurName = surName;
            Email = email;
            Phone = phone;
            Password = password;
            IsActive = isActive;
            TokenId = tokenId;
            LastLoginTime = lastLoginTime;
            QQCode = qQCode;
            Wechat = wechat;
            WechatOpenId = wechatOpenId;
            UserRegistType = userRegistType;
            UpdateBy = updateBy;
            Createby = createby;
            CreateTime = DateTime.Now;
            IsDelete = isDelete;
       
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
      
      /// <summary>
      /// 票据Id
      /// </summary>
      public string TokenId { get; private set; }
      
      /// <summary>
      /// 最后登录时间
      /// </summary>
      public DateTime? LastLoginTime { get; private set; }
      
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
      public int? UserRegistType { get; private set; }
      
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
      public bool? IsDelete { get; private set; }
      
      
   }   
}
