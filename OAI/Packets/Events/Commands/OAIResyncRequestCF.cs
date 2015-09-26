using OAI.Packets.Commands;
using OAI.Recovery;

namespace OAI.Packets.Events.Commands
{
    class OAIResyncRequestCF : OAIConfirmation
    {
        public const string COMMAND = OAIResyncRequest.CMD;

        public OAIResyncRequestCF(string[] parts) : base(parts) { }
        public OAIResyncRequestCF(byte[] bytes) : base(bytes) { }

        public new void Process()
        {
            OAIResyncRequest cmd = (OAIResyncRequest)Cmd;

            if (null == cmd)
            {
                return;
            }

            // Command failed
            if (0 != Outcome())
            {
                return;
            }

            // Clear the resync request upon confirmation
            OAIState.ResyncRequested = false;
        }
    }
}
