using System;
using System.Configuration;
using Lottery.Infrastructure.Exceptions;
using Lottery.Infrastructure.Extensions;

namespace Lottery.Infrastructure.Mail
{
    /// <summary>
    /// Declares names of the settings defined by <see cref="EmailSettingConfigs"/>.
    /// </summary>
    public static class EmailSettingConfigs
    {
        private static string _defaultFromAddress;
        private static string _defaultFromDisplayName;
        private static string _host;
        private static int _port;
        private static string _userName;
        private static string _password;
        private static string _domain;
        private static bool _enableSsl;
        private static bool _useDefaultCredentials;


        public static void InitEmailSetting()
        {
            if (!ConfigurationHelper.TryGetAppSettingVal("Email.DefaultFromAddress", out _defaultFromAddress))
            {
                _defaultFromAddress = "1029765111@qq.com";
            }
            if (!ConfigurationHelper.TryGetAppSettingVal("Email.DefaultFromDisplayName", out _defaultFromDisplayName))
            {
                _defaultFromDisplayName = "乐彩工作室";
            }
            if (!ConfigurationHelper.TryGetAppSettingVal("Email.Smtp.Host", out _host))
            {
                _host = "smtp.qq.com";
            }
            if (!ConfigurationHelper.TryGetAppSettingIntVal("Email.Smtp.Port", out _port))
            {
                _port = 465;
            }
            if (!ConfigurationHelper.TryGetAppSettingVal("Email.Smtp.UserName", out _userName))
            {
                _userName = "1029765111@qq.com";
            }
            if (!ConfigurationHelper.TryGetAppSettingVal("Email.Smtp.Password", out _password))
            {
                _password = "iewflxjezduqbdcf";
            }
            if (!ConfigurationHelper.TryGetAppSettingVal("Email.Smtp.Domain", out _domain))
            {
                _domain = "";
            }
            if (!ConfigurationHelper.TryGetAppSettingBoolVal("Email.Smtp.EnableSsl", out _enableSsl))
            {
                _enableSsl = true;
            }
     
            if (!ConfigurationHelper.TryGetAppSettingBoolVal("Email.Smtp.UseDefaultCredentials", out _useDefaultCredentials))
            {
                _useDefaultCredentials = false;
            }
        
        }

        /// <summary>
        /// Lottery.Infrastructure.Mail.DefaultFromAddress
        /// </summary>
        public static string DefaultFromAddress
        {
            get
            {
                if (string.IsNullOrEmpty(_defaultFromAddress))
                {
                    throw new LotteryDataException("Email配置未初始化");
                }
                return _defaultFromAddress;
            }
        }

        /// <summary>
        /// Lottery.Infrastructure.Mail.DefaultFromDisplayName
        /// </summary>
        public static string DefaultFromDisplayName
        {
            get
            {
                if (string.IsNullOrEmpty(_defaultFromDisplayName))
                {
                    throw new LotteryDataException("Email配置未初始化");
                }
                return _defaultFromDisplayName;
            }
        }

        /// <summary>
        /// SMTP related email settings.
        /// </summary>
        public static class Smtp
        {
           
            /// <summary>
            /// Lottery.Infrastructure.Mail.Smtp.Host
            /// </summary>
            public static string Host
            {
                get
                {
                    if (string.IsNullOrEmpty(_host))
                    {
                        throw new LotteryDataException("Email配置未初始化");
                    }
                    return _host;
                }
            }

            /// <summary>
            /// Lottery.Infrastructure.Mail.Smtp.Port
            /// </summary>
            public static int Port
            {
                get
                {
                    if (_port == 0)
                    {
                        throw new LotteryDataException("Email配置未初始化");
                    }
                    return _port;
                }
            }

            /// <summary>
            /// Lottery.Infrastructure.Mail.Smtp.UserName
            /// </summary>
            public static string UserName
            {
                get
                {
                    if (string.IsNullOrEmpty(_userName))
                    {
                        throw new LotteryDataException("Email配置未初始化");
                    }
                    return _userName;
                }
            }

            /// <summary>
            /// Lottery.Infrastructure.Mail.Smtp.Password
            /// </summary>
            public static string Password
            {
                get
                {
                    if (string.IsNullOrEmpty(_password))
                    {
                        throw new LotteryDataException("Email配置未初始化");
                    }
                    return _password;
                }
            }

            /// <summary>
            /// Lottery.Infrastructure.Mail.Smtp.Domain
            /// </summary>
            public static string Domain
            {
                get
                {
                    return _domain;
                }
            }

            /// <summary>
            /// Lottery.Infrastructure.Mail.Smtp.EnableSsl
            /// </summary>
            public static bool EnableSsl
            {
                get
                {
                    
                    return _enableSsl;
                }
            }

            /// <summary>
            /// Lottery.Infrastructure.Mail.Smtp.UseDefaultCredentials
            /// </summary>
            public static bool UseDefaultCredentials
            {
                get
                {
                   
                    return _useDefaultCredentials;
                }
            }
        }
    }
}