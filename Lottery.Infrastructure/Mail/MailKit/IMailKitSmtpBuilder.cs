using MailKit.Net.Smtp;

namespace Lottery.Infrastructure.Mail.MailKit
{
    public interface IMailKitSmtpBuilder
    {
        SmtpClient Build();
    }
}