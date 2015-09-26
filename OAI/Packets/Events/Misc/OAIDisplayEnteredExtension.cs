using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Packets.Events.Misc
{
    /**
     * Display Entered Extension – DEE
     * 
     * Occurs whenever an extension is entered as a result of a 
     * PROMPT_EXTENSION prompt, regardless of the validity of the 
     * extension.
     * 
     * DEE,<Resync_Code>,<Mon_Cross_Ref_ID>,<Subject_Ext>,
     * <Display_Control_Type>,<Extension_Entered>,<Extension_Valid>
     */
    public class OAIDisplayEnteredExtension : OAIMisc
    {
        public const string EVENT = "DEE";

        public OAIDisplayEnteredExtension(string[] parts) : base(parts) { }
        public OAIDisplayEnteredExtension(byte[] bytes) : base(bytes) { }

        /**
         * 4 - Subject_Ext
         * 
         * Specifies the device sending the event.
         */
        public string Subject_Ext()
        {
            return Part(4);
        }

        /**
         * 5 - Display_Control_Type
         * 
         * Specifies the type of Display Control the application has (1 = full
         * connected control, which is the only value currently available).
         */
        public string Display_Control_Type()
        {
            return Part(5);
        }

        /**
         * 6 - Extension_Entered
         * 
         * Specifies the extension entered at the station as a response to the
         * PROMPT_EXTENSION prompt.
         */
        public string Extension_Entered()
        {
            return Part(6);
        }

        /**
         * 7 - Extension_Valid
         * 
         * Indicates whether the extension number is valid (1) or invalid (0) in the
         * communications system.
         */
        public string Extension_Valid()
        {
            return Part(7);
        }

        public new void Process()
        {
            // TODO
        }
    }
}
