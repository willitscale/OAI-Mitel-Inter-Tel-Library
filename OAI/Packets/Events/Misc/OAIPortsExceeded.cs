using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Packets.Events.Misc
{
    /**
     * Ports Exceeded – PE
     * 
     * Occurs when a client connects to a TCP/IP System OAI Server 
     * that cannot allocate a System OAI port for the client. Servers, 
     * which typically support only a limited number of clients, send
     * this event and close the TCP/IP connection when additional 
     * clients attempt to connect and the ports are already occupied.
     * 
     * PE,<Resync_Code>,<Max_OAI_Ports>
     */
    public class OAIPortsExceeded : OAIMisc
    {
        public const string EVENT = "PE";

        public OAIPortsExceeded(string[] parts) : base(parts) { }
        public OAIPortsExceeded(byte[] bytes) : base(bytes) { }

        /**
         * 3 - Max_OAI_Ports
         * 
         * Where <Max_OAI_Ports> identifies the maximum number of 
         * System OAI ports that the TCP/IP connection can support.
         */
        public int Max_OAI_Ports()
        {
            return IntPart(3);
        }

        public new void Process()
        {
            // TODO
        }
    }
}
