﻿using ECommon.Components;
using Lottery.Infrastructure.Mail.Smtp;
using MailKit.Net.Smtp;

namespace Lottery.Infrastructure.Mail.MailKit
{
    [Component]
    public class DefaultMailKitSmtpBuilder : IMailKitSmtpBuilder
    {
        private readonly ISmtpEmailSenderConfiguration _smtpEmailSenderConfiguration;

        public DefaultMailKitSmtpBuilder(ISmtpEmailSenderConfiguration smtpEmailSenderConfiguration)
        {
            _smtpEmailSenderConfiguration = smtpEmailSenderConfiguration;
        }

        public virtual SmtpClient Build()
        {
            var client = new SmtpClient();

            try
            {
                ConfigureClient(client);
                return client;
            }
            catch
            {
                client.Dispose();
                throw;
            }
        }

        protected virtual void ConfigureClient(SmtpClient client)
        {
            client.Connect(
                _smtpEmailSenderConfiguration.Host,
                _smtpEmailSenderConfiguration.Port,
                _smtpEmailSenderConfiguration.EnableSsl
            );

            if (_smtpEmailSenderConfiguration.UseDefaultCredentials)
            {
                return;
            }

            client.Authenticate(
                _smtpEmailSenderConfiguration.UserName,
                _smtpEmailSenderConfiguration.Password
            );
        }
    }
}