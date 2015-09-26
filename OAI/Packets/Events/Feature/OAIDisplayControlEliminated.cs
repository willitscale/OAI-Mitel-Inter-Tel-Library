using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Packets.Events.Feature
{
    /**
     * Display Control Eliminated – DCE
     * 
     * Occurs whenever a station loses Display Control.
     * 
     * DCE,<Resync_Code>,<Mon_Cross_Ref_ID>,<Subject_Ext>,<Reason>,
     * <Display_Control_Type>
     */
    public class OAIDisplayControlEliminated : OAIFeature
    {
        public const string EVENT = "DCE";

        public OAIDisplayControlEliminated(string[] parts) : base(parts) { }
        public OAIDisplayControlEliminated(byte[] bytes) : base(bytes) { }

        /**
         * 4 - Subject_Ext
         * 
         * Specifies the device sending the event.
         */
        public string SubjectExt()
        {
            return Part(4);
        }

        /**
         * 5 - Reason
         * 
         * Indicates why the device is losing Display Control. Possible values include:
         *  0 = Application stopped control
         *  1 = Call changed state
         *  2 = Switch stopped monitor
         *  3 = Network communication failure
         *  4 = Max allowed features
         *  5 = Feature code used
         */
        public int Reason()
        {
            return IntPart(5);
        }

        /**
         * 6 - Display_Control_Type
         * 
         * Specifies the type of Display Control the application has (1 = full connected 
         * control, which is the only value currently available).
         */
        public int DisplayControlType()
        {
            return IntPart(6);
        }

        public new void Process()
        {
            // TODO
        }
    }
}
