using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OAI.Models;
using OAI.Controllers;

namespace OAI.Packets.Events.Gateway
{
    /**
     * Link Status Event – LS
     * 
     * Generates link status messages to the application. These messages allow the 
     * application to inform users that connections to various OAI ports have been lost.
     * 
     * LS,<Resync_Code>,<Node_Number>,<Link_Status>,<Reason_Code>
     */
    public class OAILinkStatusEvent : OAIGateway
    {
        public const string EVENT = "LS";

        public OAILinkStatusEvent(string[] parts) : base(parts) { }
        public OAILinkStatusEvent(byte[] bytes) : base(bytes) { }

        /**
         * 3 - Node_Number
         * 
         * Indicates the number of the node reporting the status.
         */
        public string NodeNumber()
        {
            return Part(3);
        }

        /**
         * 4 - Link_Status
         * 
         * Indicates if the link is up (1) or down (0).
         */
        public int LinkStatus()
        {
            return IntPart(4);
        }

        /**
         * 5 - Reason_Code
         * 
         * Identifies why the connection state changed. This code 
         * can be one of the following:
         *  0 = Unknown
         *  1 = RS232 Connection Failure (Axxess only)
         *  2 = RS232 Reconnect (Axxess only)
         *  3 = TCP/IP Connection Failure
         *  4 = TCP/IP Reconnect
         *  5 = Added Node
         *  6 = Deleted Node
         *  7 = Timed Out
         */
        public int ReasonCode()
        {
            return IntPart(5);
        }

        public new void Process()
        {
            string node = NodeNumber();

            OAINodeModel model = OAINodeController.Relay().Peek(node);
            bool exists = true;

            if (null == model)
            {
                exists = false;
                model = new OAINodeModel();
            }

            model.Node = node;
            model.Status = LinkStatus();
            model.State = ReasonCode();

            if (!exists)
            {
                OAINodeController.Relay().Push(node, model);
            }
        }
    }
}
