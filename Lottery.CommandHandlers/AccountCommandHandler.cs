using ENode.Commanding;
using Lottery.Commands.UserInfos;
using Lottery.Core.Domain.UserInfos;
using Lottery.Core.Domain.UserTicket;
using Lottery.Infrastructure.Enums;

namespace Lottery.CommandHandlers
{
    public class AccountCommandHandler :
        ICommandHandler<AddAccessTokenCommand>,
        ICommandHandler<UpdateAccessTokenCommand>,
        ICommandHandler<InvalidAccessTokenCommand>,
        ICommandHandler<AddUserInfoCommand>,
        ICommandHandler<UpdateLastLoginTimeCommand>,
        ICommandHandler<BindUserEmailCommand>,
        ICommandHandler<BindUserPhoneCommand>

    {
        public void Handle(ICommandContext context, AddAccessTokenCommand command)
        {
             context.Add(new UserTicket(command.AggregateRootId,command.UserId,command.AccessToken,command.CreateBy));
        }

        public void Handle(ICommandContext context, UpdateAccessTokenCommand command)
        {
            context.Get<UserTicket>(command.AggregateRootId).UpdateAccessToken(command.UserId,command.AccessToken,command.UpdateBy);
        }

        public void Handle(ICommandContext context, InvalidAccessTokenCommand command)
        {
            context.Get<UserTicket>(command.AggregateRootId).InvalidAccessToken();
        }
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

    }
}