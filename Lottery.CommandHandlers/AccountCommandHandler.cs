using ENode.Commanding;
using Lottery.Commands.LogonLog;
using Lottery.Commands.UserInfos;
using Lottery.Core.Domain.LogonLog;
using Lottery.Core.Domain.UserInfos;

namespace Lottery.CommandHandlers
{
    public class AccountCommandHandler :
        ICommandHandler<AddUserInfoCommand>,
        ICommandHandler<UpdateLastLoginTimeCommand>,
        ICommandHandler<BindUserEmailCommand>,
        ICommandHandler<BindUserPhoneCommand>
    {
        public void Handle(ICommandContext context, AddUserInfoCommand command)
        {
            context.Add(new UserInfo(command.AggregateRootId, command.UserName, command.Email, command.Phone, command.Password, true, command.ClientRegistType, command.AccountRegistType,command.Points));
        }


        public void Handle(ICommandContext context, UpdateLastLoginTimeCommand command)
        {
            context.Get<UserInfo>(command.AggregateRootId)
                .UpdateLastLoginTime();
        }

        public void Handle(ICommandContext context, BindUserEmailCommand command)
        {
            context.Get<UserInfo>(command.AggregateRootId)
                .BindUserEmail(command.Email);
        }

        public void Handle(ICommandContext context, BindUserPhoneCommand command)
        {
            context.Get<UserInfo>(command.AggregateRootId)
                .BindUserPhone(command.Phone);
        }

        //public void Handle(ICommandContext context, UpdateUserLoginClientCountCommand command)
        //{
        //    context.Get<UserInfo>(command.AggregateRootId)
        //        .UserLoginClientCount(command.IsLogin);
        //}

       
    }
}