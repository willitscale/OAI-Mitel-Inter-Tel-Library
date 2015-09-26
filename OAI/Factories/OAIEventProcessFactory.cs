using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OAI.Packets;

using OAI.Packets.Events;
using OAI.Packets.Events.Commands;
using OAI.Packets.Events.Agent;
using OAI.Packets.Events.Call;
using OAI.Packets.Events.Feature;
using OAI.Packets.Events.Gateway;
using OAI.Packets.Events.Misc;
using OAI.Packets.Events.System;

using OAI.Queues;

namespace OAI.Factories
{
    class OAIEventProcessFactory
    {
        public static void ProcessEvent(OAIEvent packet)
        {
            if (null == packet)
            {
                return;
            }

            // Confirmation Packets
            if (packet is OAIConfirmation)
            {
                ProcessConfirmation((OAIConfirmation)packet);
            }

            // Agent Packets
            if (packet is OAIAgentEvent)
            {
                ProcessAgentEvent((OAIAgentEvent)packet);
            }

            // Call Packets
            if (packet is OAICall)
            {
                ProcessCallEvent((OAICall)packet);
            }

            // Feature Packets
            if (packet is OAIFeature)
            {
                ProcessCallFeature((OAIFeature)packet);
            }

            // Gateway Packets
            if (packet is OAIGateway)
            {
                ProcessGateway((OAIGateway)packet);
            }

            // Gateway Packets
            if (packet is OAIMisc)
            {
                ProcessMisc((OAIMisc)packet);
            }

            // System Packets
            if (packet is OAISystem)
            {
                ProcessSystem((OAISystem)packet);
            }
        }

        public static void ProcessConfirmation(OAIConfirmation packet)
        {
            // Make Call Confirm
            if (packet.GetType() == typeof(OAIMakeCallCF))
            {
                ((OAIMakeCallCF)packet).Process();
            }

            // Clear Connection Confirm
            if (packet.GetType() == typeof(OAIClearConnectionCF))
            {
                ((OAIClearConnectionCF)packet).Process();
            }

            // Clear Call Confirm
            if (packet.GetType() == typeof(OAIClearCallCF))
            {
                ((OAIClearCallCF)packet).Process();
            }
            
            // Query Hunt Group Confirm
            if (packet.GetType() == typeof(OAIQueryHuntGroupCF))
            {
                ((OAIQueryHuntGroupCF)packet).Process();
            }

            // Query List Extended Confirm
            if (packet.GetType() == typeof(OAIQueryListExtendedCF))
            {
                ((OAIQueryListExtendedCF)packet).Process();
            }

            // No Operation Confirm
            if (packet.GetType() == typeof(OAINoOperationCF))
            {
                ((OAINoOperationCF)packet).Process();
            }

            // Resync Request Confirm
            if (packet.GetType() == typeof(OAIResyncRequestCF))
            {
                ((OAIResyncRequestCF)packet).Process();
            }
        }

        public static void ProcessAgentEvent(OAIAgentEvent packet)
        {
            Type type = packet.GetType();

            // Log off
            if (type == typeof(OAILogoff))
            {
                ((OAILogoff)packet).Process();
            }

            // Logon
            if (type == typeof(OAILogon))
            {
                ((OAILogon)packet).Process();
            }

            // Not Ready
            if (type == typeof(OAINotReady))
            {
                ((OAINotReady)packet).Process();
            }

            // Ready
            if (type == typeof(OAIReady))
            {
                ((OAIReady)packet).Process();
            }

            // Work Not Ready
            if (type == typeof(OAIWorkNotReady))
            {
                ((OAIWorkNotReady)packet).Process();
            }
        }

        public static void ProcessCallEvent(OAICall packet)
        {
            Type type = packet.GetType();

            // Call Cleared
            if (type == typeof(OAICallCleared))
            {
                ((OAICallCleared)packet).Process();
            }

            // Conferenced
            if (type == typeof(OAIConferenced))
            {
                ((OAIConferenced)packet).Process();
            }

            // Connection Cleared
            if (type == typeof(OAIConnectionCleared))
            {
                ((OAIConnectionCleared)packet).Process();
            }

            // Delivered
            if (type == typeof(OAIDelivered))
            {
                ((OAIDelivered)packet).Process();
            }

            // Diverted
            if (type == typeof(OAIDiverted))
            {
                ((OAIDiverted)packet).Process();
            }

            // Established
            if (type == typeof(OAIEstablished))
            {
                ((OAIEstablished)packet).Process();
            }

            // Established Routing
            if (type == typeof(OAIEstablishedRouting))
            {
                ((OAIEstablishedRouting)packet).Process();
            }

            // Failed
            if (type == typeof(OAIFailed))
            {
                ((OAIFailed)packet).Process();
            }

            // Held
            if (type == typeof(OAIHeld))
            {
                ((OAIHeld)packet).Process();
            }

            // Network Reached
            if (type == typeof(OAINetworkReached))
            {
                ((OAINetworkReached)packet).Process();
            }

            // Originated
            if (type == typeof(OAIOriginated))
            {
                ((OAIOriginated)packet).Process();
            }

            // Queued
            if (type == typeof(OAIQueued))
            {
                ((OAIQueued)packet).Process();
            }

            // Retrieved
            if (type == typeof(OAIRetrieved))
            {
                ((OAIRetrieved)packet).Process();
            }

            // Service Initiated
            if (type == typeof(OAIServiceInitiated))
            {
                ((OAIServiceInitiated)packet).Process();
            }

            // Transferred
            if (type == typeof(OAITransferred))
            {
                ((OAITransferred)packet).Process();
            }
        }

        public static void ProcessCallFeature(OAIFeature packet)
        {
            Type type = packet.GetType();

            // Callback
            if (type == typeof(OAICallback))
            {
                ((OAICallback)packet).Process();
            }

            // Call Info
            if (type == typeof(OAICallInfo))
            {
                ((OAICallInfo)packet).Process();
            }

            // Device Offline
            if (type == typeof(OAIDeviceOffline))
            {
                ((OAIDeviceOffline)packet).Process();
            }

            // Display Control Eliminated
            if (type == typeof(OAIDisplayControlEliminated))
            {
                ((OAIDisplayControlEliminated)packet).Process();
            }

            // Display Language Changed
            if (type == typeof(OAIDisplayLanguageChanged))
            {
                ((OAIDisplayLanguageChanged)packet).Process();
            }

            // Do Not Disturb
            if (type == typeof(OAIDoNotDisturb))
            {
                ((OAIDoNotDisturb)packet).Process();
            }

            // Elapsed Time For CO Call
            if (type == typeof(OAIElapsedTimeForCOCall))
            {
                ((OAIElapsedTimeForCOCall)packet).Process();
            }

            // Feature Status
            if (type == typeof(OAIFeatureStatus))
            {
                ((OAIFeatureStatus)packet).Process();
            }

            // Forwarded
            if (type == typeof(OAIForwarded))
            {
                ((OAIForwarded)packet).Process();
            }

            // General Information
            if (type == typeof(OAIGeneralInformation))
            {
                ((OAIGeneralInformation)packet).Process();
            }

            // Keyset Mute
            if (type == typeof(OAIKeysetMute))
            {
                ((OAIKeysetMute)packet).Process();
            }

            // MSG Waiting
            if (type == typeof(OAIMSGWaiting))
            {
                ((OAIMSGWaiting)packet).Process();
            }

            // Offering Ended
            if (type == typeof(OAIOfferingEnded))
            {
                ((OAIOfferingEnded)packet).Process();
            }
        }

        public static void ProcessGateway(OAIGateway packet)
        {
            Type type = packet.GetType();

            // Connection Status Event
            if (type == typeof(OAIConnectionStatusEvent))
            {
                ((OAIConnectionStatusEvent)packet).Process();
            }

            // Gateway Data from Application
            if (type == typeof(OAIGatewayDataFromApplication))
            {
                ((OAIGatewayDataFromApplication)packet).Process();
            }

            // Link Status Event
            if (type == typeof(OAILinkStatusEvent))
            {
                ((OAILinkStatusEvent)packet).Process();
            }
        }

        public static void ProcessMisc(OAIMisc packet)
        {
            Type type = packet.GetType();

            // Display Entered Extension
            if (type == typeof(OAIDisplayEnteredExtension))
            {
                ((OAIDisplayEnteredExtension)packet).Process();
            }

            // Display Station Change
            if (type == typeof(OAIDisplayStationChange))
            {
                ((OAIDisplayStationChange)packet).Process();
            }

            // Monitor Ended
            if (type == typeof(OAIMonitorEnded))
            {
                ((OAIMonitorEnded)packet).Process();
            }

            // Offered
            if (type == typeof(OAIOffered))
            {
                ((OAIOffered)packet).Process();
            }

            // Ports Exceeded
            if (type == typeof(OAIPortsExceeded))
            {
                ((OAIPortsExceeded)packet).Process();
            }

            // Resync Ended
            if (type == typeof(OAIResyncEnded))
            {
                ((OAIResyncEnded)packet).Process();
            }

            // Resync Response
            if (type == typeof(OAIResyncResponse))
            {
                ((OAIResyncResponse)packet).Process();
            }

            // Station Message Recording
            if (type == typeof(OAIStationMessageRecording))
            {
                ((OAIStationMessageRecording)packet).Process();
            }
        }

        public static void ProcessSystem(OAISystem packet)
        {
            Type type = packet.GetType();

            // Alarm Generation
            if (type == typeof(OAIAlarmGeneration))
            {
                ((OAIAlarmGeneration)packet).Process();
            }

            // Database Change
            if (type == typeof(OAIDatabaseChange))
            {
                ((OAIDatabaseChange)packet).Process();
            }

            // Extension Change
            if (type == typeof(OAIExtensionChange))
            {
                ((OAIExtensionChange)packet).Process();
            }

            // Night Mode Status
            if (type == typeof(OAINightModeStatus))
            {
                ((OAINightModeStatus)packet).Process();
            }
        }
    }
}
