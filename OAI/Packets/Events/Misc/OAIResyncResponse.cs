using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Packets.Events.Misc
{
    /**
     * Resync Response – RS
     * 
     * Occurs in response to the Resync Request (RR, not _RR) command or 
     * at power-up of the system. Following this event, the system sends 
     * a complete set of events providing information on all monitored 
     * devices unless a specific station was requested. If the resync 
     * request message requested a resync of a specific station, the 
     * system generates the events pertaining only to that station.
     * 
     * RS,<Resync_Code>,<Time>,<Date>,<Protocol_Version>,<KSU_SW_Version>,
     * <Premium_Feature_Status>,<Network_Node_Number>,
     * <Networking_Enabled/Disabled>,<Country_Code>,<TCPIP_Flag>
     */
    public class OAIResyncResponse : OAIMisc
    {
        public const string EVENT = "RS";

        public OAIResyncResponse(string[] parts) : base(parts) { }
        public OAIResyncResponse(byte[] bytes) : base(bytes) { }

        /**
         * 3 - Time
         * 
         * Displays the time information from the communications system, allowing the 
         * attached computer to synchronize clocks. This is displayed in 24-hour 
         * format (e.g., 13:30 = 1:30PM).
         */
        public string Time()
        {
            return Part(3);
        }

        /**
         * 4 - Date
         * 
         * Displays the date information from the communications system, using the MMDDYY
         * format.
         */
        public string Date()
        {
            return Part(4);
        }

        /**
         * 5 - Protocol_Version
         * 
         * Identifies the version number of the System OAI protocol in use. This is
         * displayed in the format Vxx.yyy where xx is the major version and yy is 
         * the revision number
         */
        public string ProtocolVersion()
        {
            return Part(5);
        }

        /**
         * 6 - KSU_SW_Version
         * 
         * Indicates the version number of the equipment cabinet (KSU) software
         * in use. This is displayed in the format Vxx.yyy where xx is the major 
         * version and yyy is the revision number.
         * 
         * If the Gateway sends this event as a response to a node-specific Resync 
         * Time command (RT), the <KSU_SW_Version> parameter contains the equipment 
         * cabinet version of the node specified in the command. In all other cases, 
         * the CT Gateway populates this field with the lowest cabinet version of 
         * all of the communications system nodes.
         */
        public string KSUSWVersion()
        {
            return Part(6);
        }

        /**
         * 7 - Premium_Feature_Status
         * 
         * Displays the enabled premium features. The options are:
         *      0 = There is no common feature status across nodes (see the note below)
         *      1 = Only System OAI Events are enabled
         *      2 = Only Third-Party Call Control commands are enabled
         *      3 = Both of the premium features are enabled 
         */
        public int PremiumFeatureStatus()
        {
            return IntPart(7);
        }

        /**
         * 8 - Network_Node_Number
         * 
         * Indicates the node number of the connected communications system. 
         * If the system is not networked, this will be “1.” When this event 
         * is sent to an application by the CT Gateway, this field specifies 
         * the node number of the communications system that originally generated 
         * the event.
         * 
         * The Gateway reports network node number “0” for power-up Resync 
         * responses and resync responses it generates due to a system-wide 
         * Resync Request (i.e., RR or _RR with a blank extension parameter). 
         * In all other cases, this field indicates the specific node that 
         * generated the Resync Response.
         */
        public int NetworkNodeNumber()
        {
            return IntPart(8);
        }

        /**
         * 9 - Networking Enabled/Disabled
         * 
         * Indicates whether or not the networking premium feature is 
         * enabled on the communications system (0 = Disabled; 1 = Enabled). 
         * This parameter indicates only whether or not the premium 
         * feature for networking has been enabled. It does not guarantee 
         * that the communications system is actually networked to another node.
         */
        public int NetworkingEnabledDisabled()
        {
            return IntPart(9);
        }

        /**
         * 10 - Country_Code
         * 
         * Identifies the country code associated with the system. The options are:
         *      1 = North America (United States and Canada)
         *      2 = Japan
         *      3 = United Kingdom
         *      4 = Indonesia
         *      5 = Mexico
         */
        public int CountryCode()
        {
            return IntPart(10);
        }

        /**
         * 11 - TCPIP_Flag
         * 
         * Indicates the type of interface being used for OAI. Possible values are:
         *      0 = OAI data via RS232 (Axxess only)
         *      1 = OAI data via TCP/IP
         * 
         * The CT Gateway reports “1” for power-up Resync responses and resync 
         * responses it generates due to a system-wide Resync Request (i.e., RR 
         * or _RR with a blank extension parameter). In all other cases, this 
         * field indicates the connection type between the CT Gateway and the 
         * specific node that generated the Resync Response.
         */
        public int TCPIPFlag()
        {
            return IntPart(11);
        }

        public new void Process()
        {
            // TODO
        }
    }
}
