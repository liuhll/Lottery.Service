using ENode.Eventing;

namespace Lottery.Core.Domain.UserTicket
{
    public class UpdateUserTicketEvent : DomainEvent<string>
    {
        private UpdateUserTicketEvent()
        {
        }

        public UpdateUserTicketEvent(string userId, string accessToken, string updateBy)
        {
            UserId = userId;
            AccessToken = accessToken;
            UpdateBy = updateBy;
        }

        public string UserId { get; private set; }

        public string AccessToken { get; private set; }

        public string UpdateBy { get; private set; }
    }
}