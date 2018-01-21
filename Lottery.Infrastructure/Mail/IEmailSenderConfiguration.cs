namespace Lottery.Infrastructure.Mail
{
    public interface IEmailSenderConfiguration
    {
        /// <summary>
        /// Default from address.
        /// </summary>
        string DefaultFromAddress { get; }

        /// <summary>
        /// Default display name.
        /// </summary>
        string DefaultFromDisplayName { get; }
    }
}