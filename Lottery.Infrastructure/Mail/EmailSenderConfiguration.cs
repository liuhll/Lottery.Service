namespace Lottery.Infrastructure.Mail
{
    /// <summary>
    /// Implementation of <see cref="IEmailSenderConfiguration"/>
    /// </summary>

    public abstract class EmailSenderConfiguration : IEmailSenderConfiguration
    {
        public virtual string DefaultFromAddress
        {
            get { return EmailSettingConfigs.DefaultFromAddress; }
        }

        public virtual string DefaultFromDisplayName
        {
            get { return EmailSettingConfigs.DefaultFromDisplayName; }
        }
    }
}