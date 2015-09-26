using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Packets.Events.Feature
{
    /**
     * Forwarded – FW
     * 
     * Occurs when a station enables or disables manual call forwarding. 
     * This does not include system forwards
     * 
     * FW,<Resync_Code>,<Mon_Cross_Ref_ID>,<Forwarding_Ext>,
     * <Destination_Ext>,<Outside_Number>,<Forward_Type>
     */
    public class OAIForwarded : OAIFeature
    {
        public const string EVENT = "FW";

        public OAIForwarded(string[] parts) : base(parts) { }
        public OAIForwarded(byte[] bytes) : base(bytes) { }

        /**
         * 4 - Forwarding_Ext
         * 
         * Identifies the extension number of the device that changed its manual 
         * forwarding type.
         */
        public string Forwarding_Ext()
        {
            return Part(4);
        }

        /**
         * 5 - Destination_Ext
         * 
         * Indicates the new manual forward’s internal destination. For a manual
         * forward to the public network, this is the extension number of the 
         * trunk access code used (i.e., ARS, the trunk, or trunk group extension). 
         * This field is blank when this event indicates that manual forwarding is 
         * disabled.
         */
        public string Destination_Ext()
        {
            return Part(5);
        }

        /**
         * 6 - Outside_Number
         * 
         * Identifies the outside number if manually forwarding to the public
         * network; otherwise, this is blank.
         */
        public string Outside_Number()
        {
            return Part(6);
        }

        /**
         * 7 - Forward_Type
         * 
         * Indicates the type of manual forward being enabled (see page 29).
         */
        public string Forward_Type()
        {
            return Part(7);
        }

        public new void Process()
        {
            // TODO
        }
    }
}
