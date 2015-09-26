using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Packets.Events.Feature
{
    /**
     * Offering Ended – OE
     * 
     * Occurs when offering control is terminated at the device (see page 83 for 
     * information about the Offered event). This event indicates that the 
     * application has lost control of the device due to one of the following:
     *      - The application failed to respond to an Offered (OF) event, and the <Affected_Ext> had
     *        reached its limit of failures.
     *      - The application terminated the device monitor it has on the station device.
     *      - System OAI terminated the device monitor.
     *      - The station user manually terminated offering control over the device.
     * 
     * OE,<Resync_Code>,<Mon_Cross_Ref_ID>,<Affected_Ext>,<Reason>
     */
    public class OAIOfferingEnded : OAIFeature
    {
        public const string EVENT = "OE";

        public OAIOfferingEnded(string[] parts) : base(parts) { }
        public OAIOfferingEnded(byte[] bytes) : base(bytes) { }

        /**
         * 4 - Affected_Ext
         * 
         * Indicates the station where the application lost offering control.
         */
        public string AffectedExt()
        {
            return Part(4);
        }

        /**
         * 5 - Reason
         * 
         * Indicates the reason that the application lost control. Possible values include:
         *  0 = The application terminated control either by issuing the Offering Eliminate 
         *      Control (_OEC) command or by terminating the device monitor.
         *  2 = System OAI stopped the device monitor for the <Affected_Ext> (i.e., an 
         *      extension change, unequip, or application failed to issue a command within 
         *      the valid command timer interval, which defaults to 4.5 minutes; see page 
         *      18 for details).
         *  3 = There was a network communication failure. This occurs if the CT Gateway 
         *      loses communication with the system node.
         *  4 = The application exceeded the number of allowed failures (see page 83 for 
         *      details).
         *  5 = The station user manually terminated control by entering the Routing Off 
         *      feature code (default code is 304; logical number is 125).
         */
        public int Reason()
        {
            return IntPart(5);
        }

        public new void Process()
        {
            // TODO
        }
    }
}
