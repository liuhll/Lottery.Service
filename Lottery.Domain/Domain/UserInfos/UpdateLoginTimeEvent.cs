using ENode.Eventing;

namespace Lottery.Core.Domain.UserInfos
{
    public class UpdateLoginTimeEvent : DomainEvent<string>
    {

        public UpdateLoginTimeEvent()
        {
        }
    }
}