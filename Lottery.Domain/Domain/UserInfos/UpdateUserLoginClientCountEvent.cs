using ENode.Eventing;

namespace Lottery.Core.Domain.UserInfos
{
    public class UpdateUserLoginClientCountEvent :DomainEvent<string>
    {
        private UpdateUserLoginClientCountEvent()
        {
        }

        public UpdateUserLoginClientCountEvent(int loginClientCount)
        {
            LoginClientCount = loginClientCount;
        }

        public int LoginClientCount { get; private set; }
    }
}