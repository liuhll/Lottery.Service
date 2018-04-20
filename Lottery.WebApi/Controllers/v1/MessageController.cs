using ENode.Commanding;
using Lottery.AppService.Account;
using Lottery.AppService.IdentifyCode;
using Lottery.Commands.IdentifyCodes;
using Lottery.Commands.Messages;
using Lottery.Dtos.IdentifyCodes;
using Lottery.Infrastructure.Enums;
using Lottery.Infrastructure.Exceptions;
using Lottery.Infrastructure.Extensions;
using Lottery.Infrastructure.Mail;
using Lottery.Infrastructure.Sms;
using Lottery.Infrastructure.Tools;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Lottery.WebApi.Controllers.v1
{
    [RoutePrefix("v1/message")]
    public class MessageController : BaseApiV1Controller
    {
        private readonly IIdentifyCodeAppService _identifyCodeAppService;
        private readonly ISmsSender _smsSender;
        private readonly IEmailSender _emailSender;
        private readonly IUserManager _userManager;

        public MessageController(ICommandService commandService,
            IIdentifyCodeAppService identifyCodeAppService,
            ISmsSender smsSender,
            IEmailSender emailSender,
            IUserManager userManager)
            : base(commandService)
        {
            _identifyCodeAppService = identifyCodeAppService;
            _smsSender = smsSender;
            _emailSender = emailSender;
            _userManager = userManager;
        }

        /// <summary>
        /// 通过账号获取验证码
        /// </summary>
        /// <param name="account">手机|Email</param>
        /// <param name="identifyCodeType"></param>
        /// <returns>是否成功</returns>
        [Route("identifycode1")]
        [HttpGet]
        [AllowAnonymous]
        public string IdentifyCode1(string account, IdentifyCodeType identifyCodeType = IdentifyCodeType.Register)
        {
            var accountType = AccountHelper.JudgeAccountRegType(account);
            if (accountType == AccountRegistType.UserName)
            {
                throw new LotteryException("只能通过手机号码或Email获取验证码");
            }
            var identifyCode = _identifyCodeAppService.GenerateIdentifyCode(account, accountType);

            switch (accountType)
            {
                case AccountRegistType.Email:
                    SendIdentifyCodeByEmail(account, identifyCode, identifyCodeType);
                    break;

                case AccountRegistType.Phone:
                    SendIdentifyCodeByPhone(account, identifyCode, identifyCodeType);
                    break;
            }

            return "验证码获取成功,请注意查收";
        }

        /// <summary>
        /// 通过账号获取验证码
        /// </summary>
        /// <param name="account">手机|Email</param>
        /// <param name="identifyCodeType">验证码类型</param>
        /// <returns>是否成功</returns>
        [Route("identifycode2")]
        [HttpGet]
        [AllowAnonymous]
        public string IdentifyCode2(string account, IdentifyCodeType identifyCodeType)
        {
            var accountType = AccountHelper.JudgeAccountRegType(account);
            if (accountType == AccountRegistType.UserName)
            {
                throw new LotteryException("只能通过手机号码或Email获取验证码");
            }
            var identifyCode = _identifyCodeAppService.GenerateIdentifyCode(account, accountType);

            switch (accountType)
            {
                case AccountRegistType.Email:
                    SendIdentifyCodeByEmail(account, identifyCode, identifyCodeType);
                    break;

                case AccountRegistType.Phone:
                    SendIdentifyCodeByPhone(account, identifyCode, identifyCodeType);
                    break;
            }

            return "验证码获取成功,请注意查收";
        }

        /// <summary>
        /// 验证验证码
        /// </summary>
        /// <remarks>通过账号验证验证码是否正确</remarks>
        /// <param name="input"></param>
        /// <returns>是否成功</returns>
        [Route("identifycode")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<string> IdentifyCode3(IdentifyCodeValidInput input)
        {
            if (input.Account.IsNullOrEmpty())
            {
                throw new LotteryException("账号不允许为空");
            }
            var accountType = AccountHelper.JudgeAccountRegType(input.Account);
            if (accountType == AccountRegistType.UserName)
            {
                throw new LotteryException("账号不正确");
            }
            if (input.IdentifyCode.IsNullOrEmpty())
            {
                throw new LotteryException("验证码不允许为空");
            }

            if (input.IsValidAccountExist && !(await _userManager.IsExistAccount(input.Account)))
            {
                throw new LotteryException($"不存在账号为的{input.Account}用户");
            }

            var validIdentifyCodeOutput = _identifyCodeAppService.ValidIdentifyCode(input.Account, input.IdentifyCode);

            if (validIdentifyCodeOutput.IsOvertime)
            {
                await SendCommandAsync(new InvalidIdentifyCodeCommand(validIdentifyCodeOutput.IdentifyCodeId, input.Account, input.Account));
                throw new LotteryDataException("验证码超时,请重新获取验证码");
            }
            if (!validIdentifyCodeOutput.IsValid)
            {
                // await SendCommandAsync(new InvalidIdentifyCodeCommand(validIdentifyCodeOutput.IdentifyCodeId, user.Account, _lotterySession.UserId));
                throw new LotteryDataException("您输入的验证码错误,请重新输入");
            }
            await SendCommandAsync(new InvalidIdentifyCodeCommand(validIdentifyCodeOutput.IdentifyCodeId, input.Account, input.Account));
            return "验证成功";
        }

        private void SendIdentifyCodeByEmail(string email, IdentifyCodeOutput identifyCode, IdentifyCodeType identifyCodeType)
        {
            var templetParams = new Dictionary<string, string>()
            {
                { "code", identifyCode.Code }
            };
            var emailContent = string.Empty;
            var emailTitle = string.Empty;
            switch (identifyCodeType)
            {
                case IdentifyCodeType.Register:
                    emailTitle = "注册用户";
                    emailContent = EmailTempletHelper.ReadContent("RegisterTemplet", templetParams);
                    break;

                case IdentifyCodeType.RetrievePwd:
                    emailTitle = "找回密码";
                    emailContent = EmailTempletHelper.ReadContent("RetrievePwd", templetParams);
                    break;
            }
            SendCommandAsync(new AddMessageRecordCommand(Guid.NewGuid().ToString(), null, email, emailTitle,
                emailContent, (int)identifyCodeType, (int)AccountRegistType.Email, _lotterySession.UserId));
            if (identifyCode.IsNew)
            {
                SendCommandAsync(new AddIdentifyCodeCommand(identifyCode.IdentifyCodeId, email, identifyCode.Code,
                    (int)identifyCodeType, (int)AccountRegistType.Email,
                    identifyCode.ExpirationDate, _lotterySession.UserId));
            }
            else
            {
                SendCommandAsync(new UpdateIdentifyCodeCommand(identifyCode.IdentifyCodeId, identifyCode.Code, email,
                    identifyCode.ExpirationDate, _lotterySession.UserId));
            }
            _emailSender.Send(email, emailTitle, emailContent, false);
        }

        private void SendIdentifyCodeByPhone(string phone, IdentifyCodeOutput identifyCode, IdentifyCodeType identifyCodeType)
        {
            string smsTempleteCode;
            string title = "";
            switch (identifyCodeType)
            {
                case IdentifyCodeType.Register:
                    smsTempleteCode = "SMS_128575019";
                    title = "用户注册";
                    break;

                case IdentifyCodeType.RetrievePwd:
                    smsTempleteCode = "SMS_128545037";
                    title = "找回密码";
                    break;

                default:
                    throw new LotteryException("无法获取该种类型的验证码");
            }

            var templateParam = "{\"code\":\"" + identifyCode.Code + "\"}";
            SendCommandAsync(new AddMessageRecordCommand(Guid.NewGuid().ToString(), null, phone, title,
                templateParam, (int)identifyCodeType, (int)AccountRegistType.Phone, _lotterySession.UserId));
            if (identifyCode.IsNew)
            {
                SendCommandAsync(new AddIdentifyCodeCommand(identifyCode.IdentifyCodeId, phone, identifyCode.Code,
                    (int)identifyCodeType, (int)AccountRegistType.Phone,
                    identifyCode.ExpirationDate, _lotterySession.UserId));
            }
            else
            {
                SendCommandAsync(new UpdateIdentifyCodeCommand(identifyCode.IdentifyCodeId, identifyCode.Code, phone,
                    identifyCode.ExpirationDate, _lotterySession.UserId));
            }
            _smsSender.Send(phone, templateParam, smsTempleteCode);
        }
    }
}