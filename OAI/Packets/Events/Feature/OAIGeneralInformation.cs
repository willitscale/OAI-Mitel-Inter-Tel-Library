using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Packets.Events.Feature
{
    /**
     * General Information – GI
     * 
     * Occurs when a station is on a transfer announcement call and is 
     * waiting at voice mail.
     * 
     * GI,<Resync_Code>,<Mon_Cross_Ref_ID>,<Affected_Ext>,<Call_ID>,
     * <Dest_Extension>,<Reason>
     */
    public class OAIGeneralInformation : OAIFeature
    {
        public const string EVENT = "GI";

        public OAIGeneralInformation(string[] parts) : base(parts) { }
        public OAIGeneralInformation(byte[] bytes) : base(bytes) { }

        /**
         * 4 - Affected_Ext
         */
        public string AffectedExt()
        {
            return Part(4);
        }

        /**
         * 5 - Call_ID
         */
        public string CallID()
        {
            return Part(5);
        }
        
        /**
         * 6 - Dest_Extension
         * 
         * Identifies the extension number of the device where the 
         * <Affected_Ext> is waiting.
         */
        public string DestExtension()
        {
            return Part(6);
        }

        /**
         * 7 - Reason
         * 
         * Indicates why the event occurred (1 = Prompting for Mailbox; 
         * 2 = Waiting to Complete Transfer).
         */
        public int Reason()
        {
            return IntPart(7);
        }

        public new void Process()
        {
            // TODO
        }
    }
}
