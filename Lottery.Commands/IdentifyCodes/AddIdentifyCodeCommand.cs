using ENode.Commanding;
using System;

namespace Lottery.Commands.IdentifyCodes
{
    public class AddIdentifyCodeCommand : Command<string>
    {
        private AddIdentifyCodeCommand()
        {
        }

        public AddIdentifyCodeCommand(string id, string receiver,
            string code, int identifyCodeType, int messageType, DateTime expirationDate, string createBy) : base(id)
        {
            Receiver = receiver;
            Code = code;
            IdentifyCodeType = identifyCodeType;
            MessageType = messageType;
            CreateBy = createBy;
            ExpirationDate = expirationDate;
            Status = 0;
        }

        public string Code { get; private set; }
        public string Receiver { get; private set; }

        public int IdentifyCodeType { get; private set; }

        public int MessageType { get; private set; }

        public DateTime ExpirationDate { get; private set; }

        public int Status { get; private set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; private set; }
    }
}