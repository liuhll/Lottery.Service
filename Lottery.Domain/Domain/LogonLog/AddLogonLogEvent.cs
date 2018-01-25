using ENode.Eventing;

namespace Lottery.Core.Domain.LogonLog
{
    public class AddLogonLogEvent : DomainEvent<string>
    {

        private AddLogonLogEvent()
        {
        }

        public AddLogonLogEvent(string userId, string createBy)
        {
            UserId = userId;
            CreateBy = createBy;
        }

        public string UserId { get; private set; }

        public string CreateBy { get; private set; }
    }
}