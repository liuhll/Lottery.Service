using ENode.Eventing;

namespace Lottery.Core.Domain.UserTicket
{
    public class AddUserTicketEvent : DomainEvent<string>
    {

        private AddUserTicketEvent()
        {
        }

        public AddUserTicketEvent(string userId, string accessToken, string createBy)
        {
            UserId = userId;
            AccessToken = accessToken;
            CreateBy = createBy;
        }

        public string UserId { get; private set; }

        public string AccessToken { get; private set; }

        public string CreateBy { get; private set; }
    }
}