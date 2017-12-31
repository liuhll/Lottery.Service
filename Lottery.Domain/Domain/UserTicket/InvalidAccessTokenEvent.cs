using ENode.Eventing;

namespace Lottery.Core.Domain.UserTicket
{
    public class InvalidAccessTokenEvent : DomainEvent<string>
    {
        private InvalidAccessTokenEvent()
        {
           
        }

        public InvalidAccessTokenEvent(string userId)
        {
            UserId = userId;
            AccessToken = null;
        }

        public string AccessToken { get; private set; }

        public string UserId { get; private set; }
    }
}