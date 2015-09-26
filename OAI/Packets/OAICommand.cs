using System;
using System.Linq;

using OAI.Packets.Events;
using OAI.Packets.Events.Commands;
using OAI.Factories;
using OAI.Tools;

namespace OAI.Packets
{
    public abstract class OAICommand : OAIPacket
    {
        public string[] Commands = new string[]
        {
            "_AOC", // Accept Offered Call
            "_AC", // Answer Call 
            "_CCB", // Cancel Callback
            "_CCT", // Control Call Timers
            "_CE", // Change Event Filter
            "_CT", // Change Timer
            "_CC", // Clear Call 
            "_CX", // Clear Connection 
            "_CN", // Conference Call 
            "_CM", // Conference Call (Multi-Party) 
            "_CS", // Consultation Call 
            "_DF", // Deflect Call 
            "_DCR", // Display Counter 
            "_DEC", // Display Eliminate Control 
            "_DGT", // Display Generic Tone 
            "_DRC", // Display Request Control 
            "_DTD", // Display Transparent Display 
            "_DPR", // Display Transparent Prompt Responses 
            "_DD", // Divert To DND 
            "_XE", // Expanded Extension
            "_EF", // Extension Feature 
            "_HC", // Hold Call 
            "_HM", // Hunt Group Merge Out 
            "_MC", // Make Call 
            "_MD", // Modify Call 
            "_MS", // Monitor Start 
            "_MA", // Monitor Start Advanced 
            "_MP", // Monitor Stop 
            "_MO", // Move Call 
            "_NO", // No Operation 
            "_OEC", // Offering Eliminate Control 
            "_ORC", // Offering Request Control 
            "_PC", // Pickup Call 
            "_PSP", // Program Station Password 
            "_PSS", // Program Station Speed-Dial Bins 
            "_QA", // Query Agent State 
            "_QC", // Query Country Code 
            "_QI", // Query Device Info 
            "_DQD", // Query Display 
            "_QD", // Query DND 
            "_QE", // Query Event Filter
            "_QF", // Query Forwarding 
            "_QH", // Query Hunt Group 
            "_QX", // Query List Extended 
            "_QM", // Query Message Waiting 
            "_QN", // Query Network Node Number 
            "_QT", // Query Night Mode 
            "_QUD", // Query User Destination 
            "_QUR", // Query User Routing 
            "_RC", // Reconnect Call 
            "_RCB", // Request Callback 
            "_RP", // Reset Port 
            "_RR", // Resync Request 
            "_RT", // Resync System Time
            "_RV", // Retrieve Call
            "_SA", // Set Agent State
            "_SD", // Set Do-Not-Disturb
            "_SF", // Set Forwarding
            "_SM", // Set Message Waiting Indication
            "_ST", // Set Night Mode 
            "_SUD", // Set User Destination 
            "_SUI", // Set User Information 
            "_SV", // Set Volume 
            "_SP", // Snapshot Device 
            "_TR", // Transfer Call 
            "_TO", // Transfer Call (One Step) 
            "_TD", // Transient Display 
            "_VP", // Verify Password 
            
            // Gateway-Specific Commands
            "_AP", // Application Parameters Programming
            "_GAD", // Gateway Attach Data to Call 
            "_GDR", // Gateway Data Destination Registration 
            "_GDD", // Gateway Delete Data to Attached Call 
            "_GMD", // Gateway Modify Data to Attached Call 
            "_GQD", // Gateway Query Data to Attached Call 
            "_GRD", // Gateway Retrieve Data to Attached Call 
            "_GSD", // Gateway Send Data 
            "_UQA", // Gateway Utility Query Applications 
            "_UQI", // Gateway Utility Query Info 
            "_UQS", // Gateway Utility Query Statistics 
            "_UQC", // Gateway Utility Query Switch Connections 
            "_UQV", // Gateway Utility Query Version 
            "_NA", // Node Aware 
            "_USS", // Set Scope Command 
            "_UAE", // Unifier Alarming Enable 
            "_UQR", // Utility Query Redundant Data 
        };

        public string Command { get; set; }
        protected string[] Arguments;
        protected int InvokeID = -1;
        protected OAIConfirmation Confirm;

        public int Invoke
        {
            get
            {
                // Only get the invoke once
                if (-1 == InvokeID)
                {
                    InvokeID = OAIInvokeID.Next();
                }
                return InvokeID;
            }
            set
            {
                InvokeID = value;
            }
        }

        public OAICommand(params string[] args)
        {
            if (null != args && 0 < args.Count())
            {
                Arguments = new string[args.Count() - 1];
                Command = args[0];

                for(int i = 1; i < args.Count(); i++)
                {
                    Arguments[i - 1] = args[i];
                }
            }
        }

        public string PacketMessage()
        {
            string message = Command + "," + Invoke;

            if (0 < Arguments.Count())
            {
                message += "," + string.Join(",", Arguments);
            }

            return message;
        }

        public void Bind(OAIConfirmation confirm)
        {
            Confirm = confirm;
        }

        public void Process()
        {
            if( null == Confirm )
            {
                return;
            }

            OAIEventProcessFactory.ProcessConfirmation(Confirm);
        }

        public bool Confirmation(OAIEvent oaiEvent)
        {
            return (Invoke == oaiEvent.Invoke() &&
                0 == Command.CompareTo(oaiEvent.Command()) &&
                0 == "CF".CompareTo(oaiEvent.Event()));
        }
    }
}
