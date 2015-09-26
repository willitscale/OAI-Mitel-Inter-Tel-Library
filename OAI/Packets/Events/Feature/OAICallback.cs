using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Packets.Events.Feature
{
    /**
     * Callback – CB
     * 
     * Occurs when a device registers or removes a register for a callback. Currently, 
     * only the device registering or deregistering the callback request issues this 
     * event. Because future System OAI versions, might issue this event for other 
     * devices and/or calls, applications should use all parameters within this event 
     * to understand the event meaning.
     * 
     * CB,<Resync_Code>,<Mon_Cross_Ref_ID>,<On/Off>,<Affected_Ext>,
     * <Target_Ext>,<|Target_Username|>,<|Target_PC_Description|>,
     * <Account_Code>,<Outside_Number>
     */
    public class OAICallback : OAIFeature
    {
        public const string EVENT = "CB";

        public OAICallback(string[] parts) : base(parts) { }
        public OAICallback(byte[] bytes) : base(bytes) { }

        /**
         * 4 - On/Off
         * 
         * Indicates whether the callback register was initiated (1) or removed (0).
         */
        public int OnOff()
        {
            return IntPart(4);
        }

        /**
         * 5 - Affected_Ext
         * 
         * Specifies the device requesting the callback (i.e., the requestor).
         */
        public string Affected_Ext()
        {
            return Part(5);
        }

        /**
         * 6 - Target_Ext
         * 
         * Indicates the device extension for which the <Affected_Ext> requested a
         * callback.
         */
        public string Target_Ext()
        {
            return Part(6);
        }

        /**
         * 7 - Target_Username
         * 
         * Identifies the username programmed for the <Target_Ext>. This field is
         * delimited by vertical slashes ( | ).
         */
        public string Target_Username()
        {
            return Part(7);
        }

        /**
         * 8 - Target_PC_Description
         * 
         * Indicates the <Target_Ext> PC description. This field is delimited
         * by vertical slashes ( | ).
         */
        public string Target_PC_Description()
        {
            return Part(8);
        }

        /**
         * 9 - Account_Code
         * 
         * Specifies the account code if Call Processing collected an account code
         * before the callback request occurred. If no account code was collected, 
         * this field is blank.
         */
        public string Account_Code()
        {
            return Part(9);
        }

        /**
         * 10 - Outside_Number
         * 
         * Indicates the outside number that Call Processing collected before the
         * callback request occurred. If Call Processing modified the number, this 
         * field will not match the exact number the user dialed. If the call is an 
         * IC call or if call processing has not collected the outside number, this 
         * field is blank
         */
        public string Outside_Number()
        {
            return Part(10);
        }

        public new void Process()
        {
            // TODO
        }
    }
}
