using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OAI.Packets;
using OAI.Packets.Commands;
using OAI.Queues;

namespace OAI.Sequences
{
    public class OAIDeviceSequence : OAISequence
    {
        protected int Device;

        public OAIDeviceSequence(int device)
        {
            Device = device;
        }

        public override void Step(OAIEvent oaiEvent)
        {
            // Don't initialise if already connected
            // Or if there was no device specified
            if (Connected || 0 >= Device)
            {
                return;
            }

            switch (this.Stage)
            {
                case STAGE_START:
                    {
                        // Confirm we connected to the phone system
                        // 000,RS,0,02:04,070515,V10.20,V6.0.11.113,3,0,1,3,1
                        if (STAGE_SEGMENT_NULL == this.StageSegment &&
                            0 == "RS".CompareTo(oaiEvent.Event()))
                        {
                            this.StageSegment |= STAGE_START_SEGMENT_RS;
                        }
                        // Confirm we have finished syncing
                        // 001,RD,0,
                        else if (STAGE_START_SEGMENT_RS == this.StageSegment &&
                            0 == "RD".CompareTo(oaiEvent.Event()))
                        {
                            this.StageSegment = STAGE_SEGMENT_NULL;
                            this.Stage = STAGE_NODE_AWARE;
                            OAIWriteQueue.Relay().Line = (LastPacket = new OAINodeAware());
                        }
                        break;
                }

                case STAGE_NODE_AWARE:
                {
                    // Confirming we are node aware
                    // 002,CF,_NA,1,0,1
                    if (LastPacket.Confirmation(oaiEvent))
                    {
                        this.Stage = STAGE_STATION_MONITOR;
                        OAIWriteQueue.Relay().Line = (LastPacket = new OAIMonitorStart(
                            Device, OAIMonitorStart.MONITOR_TYPE_DEVICE,
                            new string[]{ "A:FFFF", "C:FFFF", "F:FFFF" }));
                    }
                    break;
                }

                case STAGE_STATION_MONITOR:
                {
                    // Confirming we are monitoring calls by device
                    // 004,CF,_MS,3,0,0002,000243,1:,000244,2:,S:00FE
                    if (LastPacket.Confirmation(oaiEvent))
                    {
                        OAIWriteQueue.Relay().Line = (LastPacket = new OAIQueryListExtended(0,17,1));
                        Connected = true;
                    }
                    break;
                }
            }
        }
    }
}
