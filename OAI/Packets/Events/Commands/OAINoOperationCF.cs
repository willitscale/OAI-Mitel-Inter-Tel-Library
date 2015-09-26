using OAI.Packets.Commands;
using OAI.Controllers;
using OAI.Models;

namespace OAI.Packets.Events.Commands
{
    public class OAINoOperationCF : OAIConfirmation
    {
        public const string COMMAND = OAINoOperation.CMD;

        public OAINoOperationCF(string[] parts) : base(parts) { }
        public OAINoOperationCF(byte[] bytes) : base(bytes) { }

        public int NumNodes()
        {
            return IntPart(5);
        }

        /**
         * n*2 + 6 - Node Number
         * 
         * Indicates the number of the node configured for the Gateway.
         */
        public string NodeNumber(int offset)
        {
            offset = (offset * 2) + 6;
            return Part(offset);
        }

        /**
         * n*2 + 7 - Node Status 
         * 
         * Indicates if the node responded (1) or did not respond (0) to 
         * the _NO command.
         */
        public int NodeStatus(int offset)
        {
            offset = (offset * 2) + 7;
            return IntPart(offset);
        }

        public new void Process()
        {
            OAINoOperation cmd = (OAINoOperation)Cmd;

            if (null == cmd)
            {
                return;
            }

            // Command failed
            if (0 != Outcome())
            {
                return;
            }

            // Update the status of all the nodes
            for (int i = 0; i < NumNodes(); i++)
            {
                string node = NodeNumber(i);

                OAINodeModel model = OAINodeController.Relay().Peek(node);
                bool exists = true;

                if (null == model)
                {
                    exists = false;
                    model = new OAINodeModel();
                }

                model.Node = node;
                model.Status = NodeStatus(i);

                if (!exists)
                {
                    OAINodeController.Relay().Push(node, model);
                }
            }
        }
    }
}
