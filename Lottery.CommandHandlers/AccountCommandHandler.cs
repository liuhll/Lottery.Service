using System.Threading.Tasks;
using ECommon.IO;
using ENode.Commanding;
using ENode.Infrastructure;
using Lottery.Commands.LotteryDatas;
using Lottery.Commands.UserInfos;
using Lottery.Core.Domain.UserTicket;

namespace Lottery.CommandHandlers
{
    public class AccountCommandHandler :
        ICommandHandler<AddAccessTokenCommand>,
        ICommandHandler<UpdateAccessTokenCommand>,
        ICommandHandler<InvalidAccessTokenCommand>
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
    }
}