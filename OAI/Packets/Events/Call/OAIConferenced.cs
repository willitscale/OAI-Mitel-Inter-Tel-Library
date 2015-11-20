using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OAI.Models;
using OAI.Controllers;
using OAI.Queues;

namespace OAI.Packets.Events.Call
{
    /**
     * Conferenced – CO
     * 
     * Occurs when a conference call forms. 
     * 
     * CO,<Resync_Code>,<Mon_Cross_Ref_ID>,<Subject_Ext>,<Primary_Call_ID>,
     * <Secondary_Call_ID>,<Conf_Controller_Ext>,<Number_Of_Parties>,
     * <Party #1>,<Call_ID #1>,...<Party #N>,<Call_ID #N>,<Local_Cnx_State>,
     * <Event_Cause>
     */
    public class OAIConferenced : OAICall
    {
        public const string EVENT = "CO";

        public OAIConferenced(string[] parts) : base(parts) { }
        public OAIConferenced(byte[] bytes) : base(bytes) { }

        /**
         * 4 - Subject_Ext
         * 
         * Indicates the device sending this event.
         */
        public string SubjectExt()
        {
            return Part(4);
        }

        /**
         * 5 - Primary_Call_ID
         * 
         * Displays the ID of the main call identified by <Subject_Ext> before the
         * conference formed. If <Subject_Ext> is the device forming the conference, 
         * this is the ID of the call the device put onto conference-wait hold first. 
         * If <Subject_Ext> is not the device forming the conference, this is the 
         * ID of the call between <Subject_Ext> and the device forming the conference.
         */
        public string PrimaryCallID()
        {
            return Part(5);
        }

        /**
         * 6 - Secondary_Call_ID
         * 
         * Identifies the call used to form the conference (consultative call for
         * the device forming the conference).
         */
        public string SecondaryCallID()
        {
            return Part(6);
        }

        /**
         * 7 - Conf_Controller_Ext
         * 
         * Identifies the extension of the device controlling this conference (i.e.,
         * the device forming the conference).
         */
        public string ConfControllerExt()
        {
            return Part(7);
        }

        /**
         * 8 - Number_Of_Parties
         * 
         * Identifies the number of devices in the conference, up to eight
         * devices.
         */
        public int NumberOfParties()
        {
            return IntPart(8);
        }

        /**
         * #N - Party
         * 
         * Specifies the extension number of the nth party involved in the conference.
         */
        public string Party(int party)
        {
            int offset = (party - 1 * 2) + 9;
            return Part(offset);
        }

        /**
         * #N + 1 - Call_ID
         * 
         * Displays the system-generated ID of the call to which the nth party is connected.
         */
        public string CallID(int party)
        {
            int offset = (party - 1 * 2) + 10;
            return Part(offset);
        }

        /**
         * # + 2 - Local_Cnx_State
         * 
         * This single-character parameter defines the local connection state of 
         * the call, relative to the device sending the event, after the event 
         * occurs. The following table provides the values and descriptions 
         * associated with this parameter.
         * 
         * Value    Meaning     Description
         * N        Null        No connection to the call
         * I        Initiate    Dialtone connection
         * A        Alerting    Call is ringing at the device
         * C        Connected   Call is active at the device
         * H        Hold        Call is on-hold at the device
         * F        Failed      Device is listening to reorder tones
         * Q        Queued      Call is camped on to the device
         */
        public string LocalCnxState()
        {
            int offset = (NumberOfParties() - 1 * 2) + 11;
            return Part(offset);
        }

        /**
         * # + 3 - Event_Cause
         * 
         * This parameter provides extra information about the cause of the event. 
         * These cause values are encoded as two-digit decimal numbers.
         */
        public int EventCause()
        {
            int offset = (NumberOfParties() - 1 * 2) + 12;
            return IntPart(offset);
        }

        public new void Process()
        {
            // Set the primary call in the controller
            SetCall(PrimaryCallID(), LocalCnxState());

            // Set the secondary call in the controller
            SetCall(SecondaryCallID(), null);

            for (int i = 0; i < NumberOfParties(); i++)
            {
                // TODO
            }
        }

        protected void SetCall(string call, string cnx)
        {
            bool newCall = false;

            if (null != call &&
                0 < call.Length)
            {
                OAICallModel CallModel = GetCall(call);

                if (null == CallModel)
                {
                    newCall = true;
                    CallModel = new OAICallModel();
                }

                CallModel.Call = call;

                if (null != cnx)
                {
                    CallModel.CNX = cnx;
                }

                if (newCall)
                {
                    OAICallsController.Relay().Push(call, CallModel);
                }
            }
        }
    }
}
