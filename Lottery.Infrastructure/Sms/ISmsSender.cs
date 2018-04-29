namespace Lottery.Infrastructure.Sms
{
    public interface ISmsSender
    {
        void Send(string to, string templateParam);

        void Send(string to, string templateParam, string templateCode);
    }
}