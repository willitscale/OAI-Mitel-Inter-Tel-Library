using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Packets.Events.Feature
{
    /**
     * Call Info – CI
     * 
     * Occurs when a user account code is collected for a party on the 
     * call. This includes all account code types (e.g., forced, standard, 
     * optional, etc.). The CO party name/number includes the ANI/Caller ID 
     * information.
     * 
     * CI,<Resync_Code>,<Mon_Cross_Ref_ID>,<Call_ID>,<Device_Ext>,
     * <Account_Code>,<Outside_Caller_Name>,<Outside_Caller_Number>
     */
    public class OAICallInfo : OAIFeature
    {
        public const string EVENT = "CI";

        public OAICallInfo(string[] parts) : base(parts) { }
        public OAICallInfo(byte[] bytes) : base(bytes) { }

        /**
         * 4 - Call_ID
         * 
         */
        public string CallID()
        {
            return Part(4);
        }

        /**
         * 5 - Device_Ext
         * 
         * Displays the extension of the station that entered the account code.
         */
        public string DeviceExt()
        {
            return Part(5);
        }

        /**
         * 6 - Account_Code
         * 
         * Identifies the account code for the call.
         */
        public string AccountCode()
        {
            return Part(6);
        }

        /**
         * 7 - Outside_Caller_Name
         * 
         * Identifies the Caller ID. This is a || delimited field.
         */
        public string OutsideCallerName()
        {
            return Part(7);
        }

        /**
         * 8 - Outside_Caller_Number
         * 
         * Identifies the outside number.
         */
        public string OutsideCallerNumber()
        {
            return Part(8);
        }

        public new void Process()
        {
            // TODO
        }
    }
}
