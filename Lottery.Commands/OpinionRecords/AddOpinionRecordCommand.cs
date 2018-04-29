using ENode.Commanding;

namespace Lottery.Commands.OpinionRecords
{
    public class AddOpinionRecordCommand : Command<string>
    {
        private AddOpinionRecordCommand()
        {
        }

        public AddOpinionRecordCommand(string id, int opinionType, string content, int platform,
            string contactWay, string createBy) : base(id)
        {
            OpinionType = opinionType;
            Content = content;
            Platform = platform;
            ContactWay = contactWay;
            CreateBy = createBy;
        }

        public int OpinionType { get; private set; }

        public string Content { get; private set; }

        public int Platform { get; private set; }

        public string ContactWay { get; private set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; private set; }
    }
}