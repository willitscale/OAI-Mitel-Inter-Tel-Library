using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Packets.Events.System
{
    /**
     * Night Mode Status – NM
     * 
     * Occurs when the system’s night mode status changes.
     * 
     * NM,<Resync_Code>,<Mon_Cross_Ref_ID>,<Network_Node_Number>,<On/Off>
     */
    public class OAINightModeStatus : OAISystem
    {
        public const string EVENT = "NM";

        public OAINightModeStatus(string[] parts) : base(parts) { }
        public OAINightModeStatus(byte[] bytes) : base(bytes) { }

        /**
         * 4 - Network_Node_Number
         * 
         * Indicates the node number of the connected communications system. 
         * If the system is not networked, this will be “1.” When this event 
         * is sent to an application by the CT Gateway, this field specifies 
         * the node number of the communications system that originally generated 
         * the event.
         */
        public string NetworkNodeNumber()
        {
            return Part(4);
        }

        /**
         * 5 - On/Off
         * 
         * Indicates the night mode status of the system (1 = Enabled; 
         * 0 = Disabled).
         */
        public int OnOff()
        {
            return IntPart(5);
        }

        public new void Process()
        {
            // TODO
        }
    }
}
