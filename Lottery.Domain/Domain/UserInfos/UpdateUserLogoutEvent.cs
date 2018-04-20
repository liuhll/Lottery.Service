using ENode.Eventing;

namespace Lottery.Core.Domain.UserInfos
{
    public class UpdateUserLogoutEvent : DomainEvent<string>
    {
        public UpdateUserLogoutEvent()
        {
        }

        public UpdateUserLogoutEvent(string userId)
        {
        }

        public string UserId { get; set; }
    }
}