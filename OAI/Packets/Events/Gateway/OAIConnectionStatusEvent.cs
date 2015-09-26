using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Packets.Events.Gateway
{
    /**
     * Connection Status Event – CS
     * 
     * Informs connecting applications that the specified communications system node is not
     * connected to the CT Gateway. When a CT Gateway initially starts up, it may not be able to
     * communicate with all communications system nodes. The CT Gateway waits for 60 seconds
     * while the communications system nodes respond with a Power Up Resync Response. The CT
     * Gateway then allows applications to connect either when it has received successful responses
     * from all communications system nodes or when the 60 second timer has expired. If the CT
     * Gateway has not received successful responses from the communications system nodes (i.e.,
     * the 60 seconds has expired), a CS event is sent for every connection that has not responded.
     * This event is applicable when the CT Gateway is first starting up and accessing node
     * configurations from the registry. Once the node has connected, any subsequent connection
     * failures are sent using the Link Status (LS) event.
     * 
     * CS,<Resync_Code>,<Connection_Number>,<Connection_State>,
     * <Previous_Node_Number>,<Current_Node_Number>
     */
    public class OAIConnectionStatusEvent : OAIGateway
    {
        public const string EVENT = "CS";

        public OAIConnectionStatusEvent(string[] parts) : base(parts) { }
        public OAIConnectionStatusEvent(byte[] bytes) : base(bytes) { }

        /**
         * 3 - Connection_Number
         * 
         * Indicates the Gateway connection number of the uninitialized
         * communications system node.
         */
        public string ConnectionNumber()
        {
            return Part(3);
        }

        /**
         * 4 - Connection_State
         * 
         * Indicates the state of the connection. Possible values are:
         * 0 = Connection Down
         * 1 = Connection Up
         * 2 = Connection Duplicate
         * 3 = Connection Delete
         */
        public int ConnectionState()
        {
            return IntPart(3);
        }

        /**
         * 5 - Previous_Node_Number
         * 
         * Specifies the node number received from a previous successful connection to the Gateway. 
         * The value of this field may differ, based on the following:
         * o If the CT Gateway was never able to communicate with this node, this field is left blank.
         * o If a previous instance of the CT Gateway successfully communicated with the node,
         *      this field contains the node number associated with that connection.
         * o If the CT Gateway had been previously connected to the node, but the connection was
         *      failing and another connection was established with this node, this field is left blank.
         * o If the CT Gateway had been previously connected to this node (and the connection
         *      was successful) but another connection was established with this node, this field
         *      displays the old node number. The <Connection_State> field also displays a value of 3
         *      (Connection Duplicate).
         */
        public string PreviousNodeNumber()
        {
            return Part(5);
        }

        /**
         * 6 - Current_Node_Number
         * 
         * Indicates the node with which the CT Gateway is communicating.
         * If blank, the CT Gateway is not communicating with the node.
         */
        public string CurrentNodeNumber()
        {
            return Part(6);
        }

        public new void Process()
        {
            // TODO
        }
    }
}
