using ENode.Eventing;

namespace Lottery.Core.Domain.UserInfos
{
    public class BindUserEmailEvent : DomainEvent<string>
    {
        private BindUserEmailEvent()
        {
        }

        public BindUserEmailEvent(string email)
        {
            Email = email;
        }

        public string Email { get; private set; }
    }
}