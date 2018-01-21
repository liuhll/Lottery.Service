using ECommon.Components;

namespace Lottery.Infrastructure.Mail.Smtp
{
    [Component]
    public class SmtpEmailSenderConfiguration : EmailSenderConfiguration, ISmtpEmailSenderConfiguration
    {
      
        public virtual string Host {
            get { return EmailSettingConfigs.Smtp.Host; }
        }
        public virtual int Port { get { return EmailSettingConfigs.Smtp.Port; } }
        public virtual string UserName { get { return EmailSettingConfigs.Smtp.UserName; } }
        public virtual string Password { get { return EmailSettingConfigs.Smtp.Password; } }
        public virtual string Domain { get { return EmailSettingConfigs.Smtp.Domain; } }
        public virtual bool EnableSsl { get { return EmailSettingConfigs.Smtp.EnableSsl; } }
        public virtual bool UseDefaultCredentials { get { return EmailSettingConfigs.Smtp.UseDefaultCredentials; } }
    }
}