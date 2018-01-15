using ENode.Commanding;

namespace Lottery.Commands.Norms
{
    public class DeteteNormConfigCommand : Command<string>
    {
        private DeteteNormConfigCommand()
        {
        }

        public DeteteNormConfigCommand(string id) : base(id)
        {
        }
    }
}