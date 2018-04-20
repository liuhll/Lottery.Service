using ENode.Eventing;

namespace Lottery.Core.Domain.UserInfos
{
    public class UpdatePasswordEvent : DomainEvent<string>
    {
        private UpdatePasswordEvent()
        {
        }

        public UpdatePasswordEvent(string password, string updateBy)
        {
            Password = password;
            UpdateBy = updateBy;
        }

        public string Password { get; private set; }

        public string UpdateBy { get; private set; }
    }
}