using ENode.Eventing;

namespace Lottery.Core.Domain.UserInfos
{
    public class UpdateLastLoginTimeEvent : DomainEvent<string>
    {
        private UpdateLastLoginTimeEvent()
        {
        }

        public UpdateLastLoginTimeEvent(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; private set; }
    }
}