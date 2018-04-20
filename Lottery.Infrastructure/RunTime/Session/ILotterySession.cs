using Lottery.Infrastructure.Enums;

namespace Lottery.Infrastructure.RunTime.Session
{
    public interface ILotterySession
    {
        /// <summary>
        /// Gets current UserId or null.
        /// It can be null if no user logged in.
        /// </summary>
        string UserId { get; }

        //string TicketId { get; }

        string UserName { get; }

        string Email { get; }

        string Phone { get; }

        int ClientNo { get; }

        string SystemTypeId { get; }

        SystemType SystemType { get; }

        MemberRank MemberRank { get; }
    }
}