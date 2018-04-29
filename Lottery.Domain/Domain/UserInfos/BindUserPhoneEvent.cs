using ENode.Eventing;

namespace Lottery.Core.Domain.UserInfos
{
    public class BindUserPhoneEvent : DomainEvent<string>
    {
        private BindUserPhoneEvent()
        {
        }

        public BindUserPhoneEvent(string phone)
        {
            Phone = phone;
        }

        public string Phone { get; private set; }
    }
}