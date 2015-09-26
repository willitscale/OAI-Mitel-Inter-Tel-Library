using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Packets.Events.Misc
{
    /**
     * Display Station Change – DSC
     * 
     * Returns the input entered at the station over which the application 
     * currently has full control. This is dependent on the station state 
     * at any particular time.
     * 
     * DSC,<Resync_Code>,<Mon_Cross_Ref_ID>,<Subject_Ext>,
     * <Display_Control_Type>,<Display_Event_Type>,
     * <Display_Event_Action>,<Input>
     */
    public class OAIDisplayStationChange : OAIMisc
    {
        public const string EVENT = "DSC";

        public OAIDisplayStationChange(string[] parts) : base(parts) { }
        public OAIDisplayStationChange(byte[] bytes) : base(bytes) { }

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
         * 5 - Display_Control_Type
         * 
         * Specifies the type of Display Control the application has (1 = full
         * connected control, which is the only value currently available).
         */
        public int DisplayControlType()
        {
            return IntPart(5);
        }

        /**
         * 6 - Display_Event_Type
         * 
         * Specifies the type of display event that was generated. Possible
         * values include:
         *      0 = Keypad digit
         *      1 = Menu key
         *      2 = Action
         */
        public int DisplayEventType()
        {
            return IntPart(6);
        }

        /**
         * 7 - Display_Event_Action
         * 
         * Specifies the action that occurred. This is only returned if
         * <Display_Event_Type> is Action (2). Possible values include:
         *      0 = Terminate
         *      1 = Cursor Left
         *      2 = Cursor Right
         *      3 = Next
         *      4 = Previous
         *      5 = Clear
         *      6 = Cancel
         */
        public int DisplayEventAction()
        {
            return IntPart(7);
        }

        /**
         * 8 - Input
         * 
         * Indicates the input generated at the station.
         */
        public string Input()
        {
            return Part(8);
        }
        
        public new void Process()
        {
            // TODO
        }
    }
}
