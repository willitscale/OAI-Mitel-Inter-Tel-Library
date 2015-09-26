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
using OAI.Packets.Events.Unknown;

using OAI.Queues;
using OAI.Tools;

namespace OAI.Factories
{
    public class OAIEventFactory
    {
        public static OAIEvent Production(byte[] bytes)
        {
            if (0 == bytes.Length)
            {
                return null;
            }

            String packet = OAIUtils.GetString(bytes);

            if (null == packet)
            {
                return null;
            }

            string[] parts = OAIUtils.PBXPacketSplit(packet);

            if (0 == parts.Length)
            {
                return null;
            }

            return ProcessEvent(parts);
        }

        public static OAIEvent ProcessEvent(string[] parts)
        {
            // CF - Confirmation
            if (0 == OAIConfirmation.EVENT.CompareTo(parts[1]))
            {
                return ConfirmationLine(parts);
            }

            OAIEvent evt = null;

            // Agent Events
            if (null != (evt = AgentEvent(parts)))
            {
                return evt;
            }

            // Call Events
            if (null != (evt = CallEvent(parts)))
            {
                return evt;
            }

            // Feature Events
            if (null != (evt = FeatureEvent(parts)))
            {
                return evt;
            }

            // Gateway Events
            if (null != (evt = GatewayEvent(parts)))
            {
                return evt;
            }

            // Misc Events
            if (null != (evt = MiscEvent(parts)))
            {
                return evt;
            }

            // System Events
            if (null != (evt = SystemEvent(parts)))
            {
                return evt;
            }

            return new OAIUnknownEvent(parts);
        }

        public static OAIAgentEvent AgentEvent(string[] parts)
        {
            // LF - Logoff
            if (0 == OAILogoff.EVENT.CompareTo(parts[1]))
            {
                return new OAILogoff(parts);
            }

            // LN - Logon
            if (0 == OAILogon.EVENT.CompareTo(parts[1]))
            {
                return new OAILogon(parts);
            }

            // NRY - Not Ready
            if (0 == OAINotReady.EVENT.CompareTo(parts[1]))
            {
                return new OAINotReady(parts);
            }

            // RY - Ready
            if (0 == OAIReady.EVENT.CompareTo(parts[1]))
            {
                return new OAIReady(parts);
            }

            // WNR - Work Not Ready
            if (0 == OAIWorkNotReady.EVENT.CompareTo(parts[1]))
            {
                return new OAIWorkNotReady(parts);
            }

            return null;
        }

        public static OAIEvent CallEvent(string[] parts)
        {
            // CC - Call Cleared
            if (0 == OAICallCleared.EVENT.CompareTo(parts[1]))
            {
                return new OAICallCleared(parts);
            }

            // CO - Conferenced
            if (0 == OAIConferenced.EVENT.CompareTo(parts[1]))
            {
                return new OAIConferenced(parts);
            }

            // XC - Connection Cleared
            if (0 == OAIConnectionCleared.EVENT.CompareTo(parts[1]))
            {
                return new OAIConnectionCleared(parts);
            }

            // DE - Delivered
            if (0 == OAIDelivered.EVENT.CompareTo(parts[1]))
            {
                return new OAIDelivered(parts);
            }

            // DI - Diverted
            if (0 == OAIDiverted.EVENT.CompareTo(parts[1]))
            {
                return new OAIDiverted(parts);
            }

            // ES - Established
            if (0 == OAIEstablished.EVENT.CompareTo(parts[1]))
            {
                return new OAIEstablished(parts);
            }

            // ER - Established Routing
            if (0 == OAIEstablishedRouting.EVENT.CompareTo(parts[1]))
            {
                return new OAIEstablishedRouting(parts);
            }

            // FA - Failed
            if (0 == OAIFailed.EVENT.CompareTo(parts[1]))
            {
                return new OAIFailed(parts);
            }

            // HE - Held
            if (0 == OAIHeld.EVENT.CompareTo(parts[1]))
            {
                return new OAIHeld(parts);
            }

            // NT - Network Reached
            if (0 == OAINetworkReached.EVENT.CompareTo(parts[1]))
            {
                return new OAINetworkReached(parts);
            }

            // OR - Originated
            if (0 == OAIOriginated.EVENT.CompareTo(parts[1]))
            {
                return new OAIOriginated(parts);
            }

            // QU - Queued
            if (0 == OAIQueued.EVENT.CompareTo(parts[1]))
            {
                return new OAIQueued(parts);
            }

            // RE - Retrieved
            if (0 == OAIRetrieved.EVENT.CompareTo(parts[1]))
            {
                return new OAIRetrieved(parts);
            }

            // SI - Service Initiated
            if (0 == OAIServiceInitiated.EVENT.CompareTo(parts[1]))
            {
                return new OAIServiceInitiated(parts);
            }

            // TR - Transferred
            if (0 == OAITransferred.EVENT.CompareTo(parts[1]))
            {
                return new OAITransferred(parts);
            }

            return null;
        }

        public static OAIEvent FeatureEvent(string[] parts)
        {
            // CB - Callback
            if (0 == OAICallback.EVENT.CompareTo(parts[1]))
            {
                return new OAICallback(parts);
            }

            // CI - Call Info
            if (0 == OAICallInfo.EVENT.CompareTo(parts[1]))
            {
                return new OAICallInfo(parts);
            }

            // OL - Device Offline
            if (0 == OAIDeviceOffline.EVENT.CompareTo(parts[1]))
            {
                return new OAIDeviceOffline(parts);
            }

            // DCE - Display Control Eliminated
            if (0 == OAIDisplayControlEliminated.EVENT.CompareTo(parts[1]))
            {
                return new OAIDisplayControlEliminated(parts);
            }

            // DLC - Display Language Changed
            if (0 == OAIDisplayLanguageChanged.EVENT.CompareTo(parts[1]))
            {
                return new OAIDisplayLanguageChanged(parts);
            }

            // DND - Do-Not-Disturb
            if (0 == OAIDoNotDisturb.EVENT.CompareTo(parts[1]))
            {
                return new OAIDoNotDisturb(parts);
            }

            // ET - Elapsed Time For CO Call
            if (0 == OAIElapsedTimeForCOCall.EVENT.CompareTo(parts[1]))
            {
                return new OAIElapsedTimeForCOCall(parts);
            }

            // FE - Feature Status
            if (0 == OAIFeatureStatus.EVENT.CompareTo(parts[1]))
            {
                return new OAIFeatureStatus(parts);
            }

            // FW - Forwarded
            if (0 == OAIForwarded.EVENT.CompareTo(parts[1]))
            {
                return new OAIForwarded(parts);
            }

            // GI - General Information
            if (0 == OAIGeneralInformation.EVENT.CompareTo(parts[1]))
            {
                return new OAIGeneralInformation(parts);
            }

            // MU - Keyset Mute
            if (0 == OAIKeysetMute.EVENT.CompareTo(parts[1]))
            {
                return new OAIKeysetMute(parts);
            }

            // MW - MSG Waiting
            if (0 == OAIMSGWaiting.EVENT.CompareTo(parts[1]))
            {
                return new OAIMSGWaiting(parts);
            }

            // OE - Offering Ended
            if (0 == OAIOfferingEnded.EVENT.CompareTo(parts[1]))
            {
                return new OAIOfferingEnded(parts);
            }

            return null;
        }

        public static OAIEvent GatewayEvent(string[] parts)
        {
            // CS - Connection Status Event
            if (0 == OAIConnectionStatusEvent.EVENT.CompareTo(parts[1]))
            {
                return new OAIConnectionStatusEvent(parts);
            }

            // GDA - Gateway Data from Application
            if (0 == OAIGatewayDataFromApplication.EVENT.CompareTo(parts[1]))
            {
                return new OAIGatewayDataFromApplication(parts);
            }

            // LS - Link Status Event
            if (0 == OAILinkStatusEvent.EVENT.CompareTo(parts[1]))
            {
                return new OAILinkStatusEvent(parts);
            }

            return null;
        }

        public static OAIEvent MiscEvent(string[] parts)
        {
            // DEE - Display Entered Extension
            if (0 == OAIDisplayEnteredExtension.EVENT.CompareTo(parts[1]))
            {
                return new OAIDisplayEnteredExtension(parts);
            }

            // DSC - Display Station Change
            if (0 == OAIDisplayStationChange.EVENT.CompareTo(parts[1]))
            {
                return new OAIDisplayStationChange(parts);
            }

            // ME - Monitor Ended
            if (0 == OAIMonitorEnded.EVENT.CompareTo(parts[1]))
            {
                return new OAIMonitorEnded(parts);
            }

            // OF - Offered
            if (0 == OAIOffered.EVENT.CompareTo(parts[1]))
            {
                return new OAIOffered(parts);
            }

            // PE - Ports Exceeded
            if (0 == OAIPortsExceeded.EVENT.CompareTo(parts[1]))
            {
                return new OAIPortsExceeded(parts);
            }

            // RD - Resync Ended
            if (0 == OAIResyncEnded.EVENT.CompareTo(parts[1]))
            {
                return new OAIResyncEnded(parts);
            }

            // RS - Resync Response
            if (0 == OAIResyncResponse.EVENT.CompareTo(parts[1]))
            {
                return new OAIResyncResponse(parts);
            }

            // SMR - Station Message Recording
            if (0 == OAIStationMessageRecording.EVENT.CompareTo(parts[1]))
            {
                return new OAIStationMessageRecording(parts);
            }

            return null;
        }

        public static OAIEvent SystemEvent(string[] parts)
        {
            // AG - Alarm Generation
            if (0 == OAIAlarmGeneration.EVENT.CompareTo(parts[1]))
            {
                return new OAIAlarmGeneration(parts);
            }

            // DC - Database Change
            if (0 == OAIDatabaseChange.EVENT.CompareTo(parts[1]))
            {
                return new OAIDatabaseChange(parts);
            }

            // EC - Extension Change
            if (0 == OAIExtensionChange.EVENT.CompareTo(parts[1]))
            {
                return new OAIExtensionChange(parts);
            }

            // NM - Night Mode Status
            if (0 == OAINightModeStatus.EVENT.CompareTo(parts[1]))
            {
                return new OAINightModeStatus(parts);
            }

            return null;
        }

        public static OAIConfirmation ConfirmationLine(string[] parts)
        {
            // _QX - 
            if (0 == OAIQueryListExtendedCF.COMMAND.CompareTo(parts[2]))
            {
                return new OAIQueryListExtendedCF(parts);
            }

            // _QH - 
            if (0 == OAIQueryHuntGroupCF.COMMAND.CompareTo(parts[2]))
            {
                return new OAIQueryHuntGroupCF(parts);
            }

            // _CC - 
            if (0 == OAIClearCallCF.COMMAND.CompareTo(parts[2]))
            {
                return new OAIClearCallCF(parts);
            }

            // _CX - 
            if (0 == OAIClearConnectionCF.COMMAND.CompareTo(parts[2]))
            {
                return new OAIClearConnectionCF(parts);
            }

            // _MC - 
            if (0 == OAIMakeCallCF.COMMAND.CompareTo(parts[2]))
            {
                return new OAIMakeCallCF(parts);
            }

            // _NO - 
            if (0 == OAINoOperationCF.COMMAND.CompareTo(parts[2]))
            {
                return new OAINoOperationCF(parts);
            }

            // _RR - 
            if (0 == OAIResyncRequestCF.COMMAND.CompareTo(parts[2]))
            {
                return new OAIResyncRequestCF(parts);
            }

            // Unknown confirmation
            return new OAIConfirmation(parts);
        }
    }
}
