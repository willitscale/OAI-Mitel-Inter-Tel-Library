using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OAI.Packets.Delays;

namespace OAI.Packets.Commands
{
    /**
     * Make Call – _MC
     * 
     * Instructs the device to dial the specified string. Although it is 
     * intended to originate a call between two devices, it can be used 
     * to dial anything at the device, including an OfficeLink call when the
     * <Dest_Type> is specified. If the dialed string is a valid device on 
     * the communications system or a valid trunk access code and outside 
     * number, the service attempts to create a new call and establish a 
     * connection with the originating device. If the calling device 
     * (<Affected_Ext>) cannot be forced to dialtone and is not already at 
     * dialtone, the system rejects the command. This command also fails 
     * if the calling device (<Affected_Ext>) is already connected on a call 
     * or is in any other state that does not allow calls to be made. 
     * 
     * The <Affected_Ext> can be an on-hook single-line phone provided the 
     * requirements specified below are met.
     */
    public class OAIMakeCall : OAICommand
    {
        public const string CMD = "_MC";

        /** */
        public const int DEST_TYPE_DESK = 1000;

        /** */
        public const int DEST_TYPE_HOME_IP = 1001;

        /** */
        public const int DEST_TYPE_SOFTPHONE = 1002;

        /** */
        public const int DEST_TYPE_DESK_2 = 1003;

        /** */
        public const int DEST_TYPE_HOME_IP_2 = 1004;

        /** */
        public const int DEST_TYPE_SOFTPHONE_2 = 1005;

        /** */
        public const int DEST_TYPE_MOBILE = 2000;

        /** */
        public const int DEST_TYPE_HOME = 2001;

        /** */
        public const int DEST_TYPE_MOBILE_2 = 2002;

        /** */
        public const int DEST_TYPE_HOME_2 = 2003;

        protected void CallBase(string extension, string number)
        {
            Command = CMD;

            // Affected_Ext: Indicates a user’s main extension (online).
            Arguments[0] = extension;

            // Digit_String: Indicates the internal or external number to be dialed. 
            // May be up to 48 digits and may include the values shown in Table 36 
            // (below), however:
            // o The digit string cannot begin with P (pause).
            // o If the application wants to dial an outside number, it must specify 
            //      a trunk access code followed by the outside number to dial. 
            //      An account code cannot be entered in this field.
            //
            //  Value           Indication      Value               Indication
            //  0-9, *, #       Keypad Digits   - (Hyphen)          Ignored
            //  P               Pause           ( ) (Parentheses)   Ignored
            //  F               Hookflash       ; (Semicolon)       Ignored
            //  ! (Exclamation) SPCL Key        Space               Ignored
            Arguments[1] = number;
        }

        public OAIMakeCall(string extension, string number)
        {
            // _MC,<InvokeID>,<Affected_Ext>,<Digit_String>
            Arguments = new string[2];

            CallBase(extension, number);
        }

        public OAIMakeCall(string extension, string number, string accountCode,
            bool returnMessage, string callIDTransfer, string mailbox, 
            bool overrideDND, string callerIDNumber, string callerIDName,
            int destinationType, bool humanAnswerSupervision )
        {
            // _MC,<InvokeID>,<Affected_Ext>,<Digit_String>,<Account_Code>,
            // <Is_Call_Return_Message>,<Call_ID_Of_Call_To_Transfer>,<Mailbox>,
            // <Override_DND>,<Caller_ID_Number>,<Caller_ID_Name>,<Dest_Type>,
            // <OfficeLink_Human_Answer_Supervision>
            Arguments = new string[11];

            CallBase(extension, number);

            // Account_Code: (Optional) Identifies the account code the system 
            // should use for this call. This field is ignored if the call is 
            // an internal call. 
            Arguments[2] = "";

            //  Is_Call_Return_Message: (Optional) Indicates whether or not the 
            // call being made is returning a station message (0 = Is not a Return 
            // Message; 1 = Is a Return Message). If blank, the default value of 
            // Is not a Return Message (0) is used. This parameter is not used for 
            // an OfficeLink call.
            Arguments[3] = ( returnMessage ) ? "1" : "0";

            // Call_ID_Of_Call_To_Transfer: (Optional) Indicates the Call ID of 
            // a call to transfer. If this parameter is specified, the Make 
            // Call command makes a transfer announce call to the destination. 
            // The specified call must already be on some type of hold (i.e., system,
            // individual, or transfer hold).
            Arguments[4] = callIDTransfer;

            // Mailbox: (Optional) Specifies the mailbox the voice mail application 
            // should use. This field is used only when the destination for the 
            // command is voice mail.
            Arguments[5] = mailbox;

            // Override_DND: (Optional) Indicates whether or not the call should 
            // override DND if the destination extension (associated with 
            // <Digit_String>) is in DND (0 = Do not Override; 1 = Override). 
            // If blank, the default value of Do not Override (0) is used.
            Arguments[6] = ( overrideDND ) ? "1" : "0";

            // Caller_ID_Number: A digit string of up to 24 digits
            Arguments[7] = callerIDNumber;

            // Caller_ID_Name: An ASCII string of up to 32 characters 
            // delimited by |'s. For example, |Doe, John J|.
            Arguments[8] = "|" + callerIDName + "|";

            // Dest_Type: Identifies the type of destination that the OfficeLink 
            // Assistant is placing the OfficeLink call to. Includes the following 
            // locations:
            // o 1000 = Desk
            // o 1001 = Home IP
            // o 1002 = Softphone
            // o 1003 = Desk 2
            // o 1004 = Home IP 2
            // o 1005 = Softphone 2
            // o 2000 = Mobile
            // o 2001 = Home
            // o 2002 = Mobile 2
            // o 2003 = Home 2
            if (0 < destinationType)
            {
                Arguments[9] = destinationType.ToString();
            }
            else
            {
                Arguments[9] = "";
            }
            
            // OfficeLink_Human_Answer_Supervision: (Optional) If the call is 
            // an OfficeLink call and the Dest_Type specifies an external 
            // destination, such as Mobile, then this parameter indicates
            // whether or not the system uses human answer supervision 
            // (0 = is disable; 1 = enabled) for this call. If not specified, 
            // the default is 1.
            Arguments[10] = (humanAnswerSupervision) ? "1" : "0";
        }

        public string Extension()
        {
            return Arguments[0];
        }

        public override bool Delayed()
        {
            return OAIDeviceDelay.Relay().Delayed(Extension());
        }

        public override void Block()
        {
            OAIDeviceDelay.Relay()
                .Block(Extension());
        }

        public override void Release()
        {
            OAIDeviceDelay.Relay()
                .Release(Extension());
        }
    }
}
