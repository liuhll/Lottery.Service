using ENode.Commanding;

namespace Lottery.Commands.Messages
{
    public class AddMessageRecordCommand : Command<string>
    {
        private AddMessageRecordCommand()
        {
        }

        public AddMessageRecordCommand(string id, string sender, string receiver, string title, string content,
            int messageType, int snderPlatform, string createBy) : base(id)
        {
            Sender = sender;
            Receiver = receiver;
            Title = title;
            Content = content;
            MessageType = messageType;
            SenderPlatform = snderPlatform;
            CreateBy = createBy;
        }

        public string Sender { get; private set; }

        public string Receiver { get; private set; }

        public string Title { get; private set; }

        public string Content { get; private set; }

        public int MessageType { get; private set; }

        public int SenderPlatform { get; private set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; private set; }
    }
}