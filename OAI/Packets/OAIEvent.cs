using System;
using System.Linq;

using OAI.Queues;
using OAI.Tools;

using OAI.Models;
using OAI.Controllers;

namespace OAI.Packets
{
    public abstract class OAIEvent : OAIPacket
    {
        public string[] events = new string[]
        {
            // Agent Status Event Reports
            "LF", // Logoff
            "LN", // Logon
            "NRY", // Not Ready
            "RY", // Ready
            "WNR", // Work Not Ready

            // Call Event Reports
            "CC", // Call Cleared
            "CO", // Conferenced
            "XC", // Connection Cleared
            "DE", // Delivered
            "DI", // Diverted
            "ER", // Established Routing
            "ES", // Established
            "FA", // Failed
            "HE", // Held
            "NT", // Network Reached
            "OR", // Originated
            "QU", // Queued
            "RE", // Retrieved
            "SI", // Service Initiated
            "TR", // Transferred

            // Feature Event Reports
            "CB", // Callback 
            "CI", // Call Info 
            "OL", // Device Offline 
            "DCE", // Display Control Eliminated 
            "DLC", // Display Language Changed 
            "DND", // Do-Not-Disturb 
            "ET", // Elapsed Time For CO Call 
            "FE", // Feature Status 
            "FW", // Forwarded 
            "GI", // General Information 
            "MU", // Keyset Mute 
            "MW", // MSG Waiting 
            "OE", // Offering Ended 

            // System Event Reports
            "AG", // Alarm Generation 
            "DC", // Database Change
            "EC", // Extension Change 
            "NM", // Night Mode Status 

            // Miscellaneous Event Reports
            "DSC", // Display Station Change 
            "DEE", // Display Entered Extension 
            "ME", // Monitor Ended 
            "OF", // Offered 
            "PE", // Ports Exceeded 
            "RD", // Resync Ended 
            "RS", // Resync Response 
            "SMR", // Station Message Recording 

            // Gateway-Specific Events
            "CS", // Connection Status Event 
            "GDA", // Gateway Data from Application 
            "LS", // Link Status Event 
        };

        public OAIEvent(string[] parts)
        {
            this.Parts = parts;
            OAIDebuggerQueue.Relay().Line = ToString();
        }

        public OAIEvent(byte[] bytes)
        {
            string packet = OAIUtils.GetString(bytes);
            this.Parts = OAIUtils.PBXPacketSplit(packet);
            OAIDebuggerQueue.Relay().Line = ToString();
        }

        public string[] Parts { get; set; }

        public string Part(int part)
        {
            if (null == this.Parts || part >= this.Parts.Count())
            {
                return null;
            }

            if (null != this.Parts[part])
            {
                return this.Parts[part].Replace("|", "");
            }

            return null;
        }

        public int IntPart(int part)
        {
            string str = Part(part);

            if (null == str)
            {
                return -1;
            }
            try
            {
                return Int32.Parse(str);
            }
            catch(Exception exception)
            {
                OAIDebuggerQueue.Relay().Line = exception.Message;
                OAIDebuggerQueue.Relay().Line = exception.StackTrace;
            }

            return 0;
        }

        public string SequenceNumber()
        {
            return Part(0);
        }

        public string Event()
        {
            return Part(1);
        }

        /**
         * 2 - Resync_Code
         * 
         * This code is a parameter in almost every event. However, this field is 
         * blank if the event is not part of a resynchronization; otherwise, it 
         * contains a single character identifying the reason the resync occurred. 
         * 
         * The value associated with this code can be one of the following: 
         *      0 = Communications system power up or System OAI port reset.
         *      1 = Response to Resync Request (RR or _RR) or Resysnc Request 
         *          command with an <InvokeID> of 1 (RR,1 or _RR,1).
         *      # = Response to Resync Request command with an <InvokeID> of # 
         *          (RR,# or _RR,#), where “#” indicates any valid InvokeID.
         */
        public string ResyncCode()
        {
            return Part(2);
        }

        /**
         * 3 - Mon_Cross_Ref_ID
         * 
         * The <Mon_Cross_Ref_ID> (Monitor Cross-Reference ID) is currently an 
         * ASCII encoded decimal number assigned by the communications system to 
         * match unsolicited events with the Monitor Start (_MS) command (see page 130) 
         * that requested the event report. The confirmation to the Monitor Start 
         * command includes this cross-reference ID so that the attached computer
         * can match the unsolicited events with its previous monitor start request. 
         * Maximum length is three digits without the CT Gateway.
         * 
         * The Gateway implements monitors on all nodes, and each node allows 999 or 
         * 9999 monitors. Because the Gateway can support multiple nodes, it uses 
         * six-digit Monitor Cross-Reference IDs that are allocated starting at 000001 
         * and ending at 999999. As monitors are terminated, the Cross-Reference ID 
         * associated with the monitor is returned and reused. This means applications 
         * that cannot support more than 99999 monitors should still work, provided the
         * application itself imposes the restriction on the number of monitors started.
         */
        public string MonCrossRefID()
        {
            return Part(3);
        }

        public string Command()
        {
            return Part(2);
        }

        public int Invoke()
        {
            return IntPart(3);
        }

        public override string ToString()
        {
            return string.Join(",", this.Parts);
        }

        public bool Confirmation()
        {
            return (0 == "CF".CompareTo(Event()));
        }

        public void Process()
        {
        }

        public override bool Delayed()
        {
            return false;
        }

        public override void Block() { }

        public override void Release() { }


        protected OAIDeviceModel GetDevice(string extension)
        {
            if (null != extension && 0 < extension.Length)
            {
                return OAIDevicesController
                    .Relay()
                    .Peek(extension);
            }

            return null;
        }

        protected OAIAgentModel GetAgent(string agent)
        {
            if (null != agent && 0 < agent.Length)
            {
                return OAIAgentsController
                    .Relay()
                    .Peek(agent);
            }

            return null;
        }

        protected OAICallModel GetCall(string call)
        {
            if (null != call && 0 < call.Length)
            {
                return OAICallsController
                    .Relay()
                    .Peek(call);
            }

            return null;
        }

        protected OAIHuntGroupModel GetHuntGroup(string huntGroup)
        {
            if (null != huntGroup && 0 < huntGroup.Length)
            {
                return OAIHuntGroupsController
                    .Relay()
                    .Peek(huntGroup);
            }

            return null;
        }

        protected OAIDNDModel GetDND(string dnd)
        {
            if (null != dnd && 0 < dnd.Length)
            {
                return OAIDNDController
                    .Relay()
                    .Peek(dnd);
            }

            return null;
        }

        protected OAIFeatureModel GetFeature(string feature)
        {
            if (null != feature && 0 < feature.Length)
            {
                return OAIFeatureController
                    .Relay()
                    .Peek(feature);
            }

            return null;
        }

        protected void SetActiveCall(string extension, string call)
        {
            if (null != extension &&
                0 < extension.Length)
            {
                OAIDeviceModel device = GetDevice(extension);

                if (null != device)
                {
                    device.ActiveCall = call;

                    OAIAgentModel agent = GetAgent(device.Agent);

                    if (null != agent)
                    {
                        agent.ActiveCall = call;
                    }
                }
            }
        }
        protected void AddCallToExtension(string extension, string call)
        {
            if (null != extension &&
                0 < extension.Length)
            {
                OAIDeviceModel device = GetDevice(extension);

                if (null != device)
                {
                    device.AddCall(call);

                    OAIAgentModel agent = GetAgent(device.Agent);

                    if (null != agent)
                    {
                        agent.AddCall(call);
                    }
                }
            }
        }

        protected void DisconnectCallFromExtension(string extension, string call)
        {
            if (null != extension &&
                0 < extension.Length)
            {
                OAIDeviceModel device = GetDevice(extension);

                if (null != device)
                {
                    device.RemoveCall(call);

                    OAIAgentModel agent = GetAgent(device.Agent);

                    if (null != agent)
                    {
                        agent.RemoveCall(call);
                    }
                }
            }
        }

        protected void QueueCallToExtension(string extension, string call)
        {
            if (null != extension &&
                0 < extension.Length)
            {
                OAIDeviceModel device = GetDevice(extension);

                if (null != device)
                {
                    device.QueueCall(call);

                    OAIAgentModel agent = GetAgent(device.Agent);

                    if (null != agent)
                    {
                        agent.QueueCall(call);
                    }
                }
            }
        }

    }
}
