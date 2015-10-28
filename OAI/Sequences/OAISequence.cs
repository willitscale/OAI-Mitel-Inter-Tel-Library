using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OAI.Packets;
using OAI.Packets.Commands;
using OAI.Queues;
using OAI.Configuration;

namespace OAI.Sequences
{
    public abstract class OAISequence
    {
        public static bool Connected = false;
        public static bool DataBuilt = false;

        // Stages
        public const int STAGE_START = 0x00;
        public const int STAGE_NODE_AWARE = 0x01;
        public const int STAGE_STATION_MONITOR = 0x02;
        public const int STAGE_SYSTEM_MONITOR = 0x04;
        public const int STAGE_SYSTEM_QUERY_AGENTS = 0x08;
        public const int STAGE_SYSTEM_QUERY_STATIONS = 0x10;
        public const int STAGE_SYSTEM_QUERY_HUNTS = 0x20;
        public const int STAGE_SYSTEM_QUERY_FEATURES = 0x40;
        public const int STAGE_SYSTEM_QUERY_AGENT = 0x80;
        public const int STAGE_FINISHED = 0x100;

        // Segments
        public const int STAGE_SEGMENT_NULL = 0x00;
        public const int STAGE_START_SEGMENT_RS = 0x01;
        public const int STAGE_START_SEGMENT_RD = 0x02;

        // Instance states
        protected int Stage = STAGE_START;
        protected int StageSegment = STAGE_SEGMENT_NULL;

        protected OAICommand LastPacket;

        public abstract void Step(OAIEvent oaiEvent);

        protected OAIConfig Config;

        public OAISequence(OAIConfig config)
        {
            Config = config;
        }

        public static bool isConnected()
        {
            return Connected;
        }

        public static bool isDataBuilt()
        {
            return DataBuilt;
        }

        /**
         * Generic Instance Connected Method for
         * when the Sequence is being used abstractly
         */
        public bool iConnected()
        {
            return Connected;
        }

        public bool iDataBuilt()
        {
            return DataBuilt;
        }

        public void Reset()
        {
            Stage = STAGE_START;
            StageSegment = STAGE_SEGMENT_NULL;
            LastPacket = null;
            // Preserve any packets waiting to be written
            OAIWriteQueue.Relay().StashQueue();
        }

        public void NodeAware(OAIEvent evt, int next)
        {
            if (STAGE_SEGMENT_NULL == this.StageSegment &&
                0 == "RS".CompareTo(evt.Event()))
            {
                this.StageSegment |= STAGE_START_SEGMENT_RS;
            }
            else if (STAGE_START_SEGMENT_RS == this.StageSegment &&
                0 == "RD".CompareTo(evt.Event()))
            {
                this.StageSegment = STAGE_SEGMENT_NULL;
                this.Stage = next;
                OAIWriteQueue.Relay().Line = (LastPacket = new OAINodeAware());
            }
        }

        public void StationMonitor(OAIEvent evt, int next)
        {
            if (LastPacket.Confirmation(evt))
            {
                this.Stage = next;
                OAIWriteQueue.Relay().Line = (LastPacket = new OAIMonitorStart(
                    OAIMonitorStart.MONITOR_TYPE_ALL_STATIONS,
                    new string[3] { "A:FFFF", "C:FFFF", "F:FFFF" }));
            }
        }

        public void SystemMonitor(OAIEvent evt, int next)
        {
            if (LastPacket.Confirmation(evt))
            {
                this.Stage = next;
                OAIWriteQueue.Relay().Line = (LastPacket = new OAIMonitorStart(
                    OAIMonitorStart.MONITOR_TYPE_SYSTEM,
                    new string[1] { "S:FFFF" }));
            }
        }

        public void QueryAgents(OAIEvent evt, int next)
        {
            if (LastPacket.Confirmation(evt))
            {
                this.Stage = next;
                OAIWriteQueue.Relay().Line = (LastPacket = new OAIQueryListExtended(
                    OAIQueryListExtended.LIST_TYPE_ACD_AGENT,
                    OAIQueryListExtended.MASK_ACD_AGENT_DESC));
            }
        }

        public void QueryStations(OAIEvent evt, int next)
        {
            if (LastPacket.Confirmation(evt))
            {
                this.Stage = next;
                OAIWriteQueue.Relay().Line = (LastPacket = new OAIQueryListExtended(
                    OAIQueryListExtended.LIST_TYPE_STATION_LIST,
                    OAIQueryListExtended.MASK_STATION_LIST_USER |
                    OAIQueryListExtended.MASK_STATION_LIST_IS_ADMIN |
                    OAIQueryListExtended.MASK_STATION_LIST_DEVICE_TYPE));
            }
        }

        /**
         * 
         */
        public void QueryHuntGroups(OAIEvent evt, int next)
        {
            if (LastPacket.Confirmation(evt))
            {
                this.Stage = next;
                OAIWriteQueue.Relay().Line = (LastPacket = new OAIQueryListExtended(
                    OAIQueryListExtended.LIST_TYPE_HUNT_GROUP_LIST,
                    OAIQueryListExtended.MASK_STATION_LIST_USER |
                    OAIQueryListExtended.MASK_HUNT_GROUP_LIST_DESC));
            }
        }

        /**
         * 
         */
        public void QueryFeatures(OAIEvent evt, int next)
        {
            if (LastPacket.Confirmation(evt))
            {
                this.Stage = next;
                OAIWriteQueue.Relay().Line = (LastPacket = new OAIQueryListExtended(
                    OAIQueryListExtended.LIST_TYPE_FEATURE_LIST,
                    OAIQueryListExtended.MASK_FEATURE_LIST_FEAT_NUM |
                    OAIQueryListExtended.MASK_FEATURE_LIST_FEAT_NAME |
                    OAIQueryListExtended.MASK_FEATURE_LIST_IS_ADMIN |
                    OAIQueryListExtended.MASK_FEATURE_LIST_IS_DIR |
                    OAIQueryListExtended.MASK_FEATURE_LIST_IS_DIAG |
                    OAIQueryListExtended.MASK_FEATURE_LIST_IS_TOGGLE));
            }
        }

        /**
         * 
         */
        public void QueryDND(OAIEvent evt, int next)
        {
            if (LastPacket.Confirmation(evt))
            {
                this.Stage = next;
                OAIWriteQueue.Relay().Line = (LastPacket = new OAIQueryListExtended(
                    OAIQueryListExtended.LIST_TYPE_DND_LIST,
                    OAIQueryListExtended.MASK_DND_LIST_MESSAGE));
            }
        }

        /**
         * 
         */
        public void QueryAgent(OAIEvent evt, int next)
        {
            if (LastPacket.Confirmation(evt))
            {
                this.Stage = next;
                OAIWriteQueue.Relay().Line = (LastPacket = new OAIQueryHuntGroup(Config.MasterHuntGroup));
            }
        }

        /**
         * 
         */
        public void Finished(OAIEvent evt)
        {
            if (LastPacket.Confirmation(evt))
            {
                // Restore any packets which we may have had 
                // left from a disconnect event
                OAIWriteQueue.Relay().ApplyStash();
                Connected = true;
            }
        }
    }
}
