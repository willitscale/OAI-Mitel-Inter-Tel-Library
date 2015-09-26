using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

using OAI.Packets.Commands;
using OAI.Structures.Queries;
using OAI.Structures;
using OAI.Queues;
using OAI.Bus;

namespace OAI.Packets.Events.Commands
{
    public class OAIQueryListExtendedCF : OAIConfirmation
    {
        public const string COMMAND = OAIQueryListExtended.CMD;

        public OAIQueryListExtendedCF(string[] parts) : base(parts) {}
        public OAIQueryListExtendedCF(byte[] bytes) : base(bytes) {}

        public int ListType()
        {
            return IntPart(5);
        }

        public int EntityFieldMask()
        {
            return int.Parse(Part(6),
                NumberStyles.HexNumber);
        }

        public int MSGIndicator()
        {
            return IntPart(7);
        }

        public int EntityCount()
        {
            return IntPart(8);
        }

        public int SegmentCount()
        {
            return ( Parts.Length - EntityOffset() ) /
                EntityCount();
        }

        public int EntityOffset()
        {
            return 9;
        }

        public new void Process()
        {
            int offset = EntityOffset();
            int segments = SegmentCount();
            int count = SegmentCount() * EntityCount();

            for (int i = offset; i < offset+count; i += segments)
            {
                switch (ListType())
                {
                    // 1 Station List
                    case OAIQueryListExtended.LIST_TYPE_STATION_LIST:
                    {
                        OAIDeviceBus.Relay().Push(StationStructure(i));
                        continue;
                    }

                    // 3 Hunt Group List
                    case OAIQueryListExtended.LIST_TYPE_HUNT_GROUP_LIST:
                    {
                        OAIHuntGroupBus.Relay().Push(HuntGroupStructure(i));
                        continue;
                    }

                    // 8 Feature List 
                    case OAIQueryListExtended.LIST_TYPE_FEATURE_LIST:
                    {
                        OAIFeatureBus.Relay().Push(FeatureStructure(i));
                        continue;
                    }

                    // 10 DND Message List
                    case OAIQueryListExtended.LIST_TYPE_DND_LIST:
                    {
                        OAIDNDBus.Relay().Push(DNDStructure(i));
                        continue;
                    }

                    // 17 ACD Agent List
                    case OAIQueryListExtended.LIST_TYPE_ACD_AGENT:
                    {
                        OAIAgentBus.Relay().Push(AgentStructure(i));
                        continue;
                    }
                }
            }
        }

        public OAIQueryExtendedFeature FeatureStructure(int index)
        {
            OAIQueryExtendedFeature entity = new OAIQueryExtendedFeature();

            int mask = EntityFieldMask();

            entity.Feature_Code = Part(index++);

            if (OAIQueryListExtended.MASK_FEATURE_LIST_FEAT_NUM ==
                (OAIQueryListExtended.MASK_FEATURE_LIST_FEAT_NUM & mask))
            {
                entity.Feature_Number = Part(index++);
            }

            if (OAIQueryListExtended.MASK_FEATURE_LIST_FEAT_NAME ==
                (OAIQueryListExtended.MASK_FEATURE_LIST_FEAT_NAME & mask))
            {
                entity.Feature_Name = Part(index++);
            }

            if (OAIQueryListExtended.MASK_FEATURE_LIST_IS_ADMIN ==
                (OAIQueryListExtended.MASK_FEATURE_LIST_IS_ADMIN & mask))
            {
                entity.Is_Administrator_Feature = IntPart(index++);
            }

            if (OAIQueryListExtended.MASK_FEATURE_LIST_IS_DIR ==
                (OAIQueryListExtended.MASK_FEATURE_LIST_IS_DIR & mask))
            {
                entity.Is_Directory_Feature = IntPart(index++);
            }

            if (OAIQueryListExtended.MASK_FEATURE_LIST_IS_DIAG ==
                (OAIQueryListExtended.MASK_FEATURE_LIST_IS_DIAG & mask))
            {
                entity.Is_Diagnostic_Feature = IntPart(index++);
            }

            if (OAIQueryListExtended.MASK_FEATURE_LIST_IS_TOGGLE ==
                (OAIQueryListExtended.MASK_FEATURE_LIST_IS_TOGGLE & mask))
            {
                entity.Is_Toggleable_Feature = IntPart(index++);
            }

            return entity;
        }

        public OAIQueryExtendedDND DNDStructure(int index)
        {
            OAIQueryExtendedDND entity = new OAIQueryExtendedDND();

            int mask = EntityFieldMask();

            entity.DND_Message_Number = Part(index++);

            if (OAIQueryListExtended.MASK_DND_LIST_MESSAGE ==
                (OAIQueryListExtended.MASK_DND_LIST_MESSAGE & mask))
            {
                entity.DND_Message_Text = Part(index++);
            }

            return entity;
        }

        public OAIQueryExtendedHuntGroup HuntGroupStructure(int index)
        {
            OAIQueryExtendedHuntGroup entity = new OAIQueryExtendedHuntGroup();

            int mask = EntityFieldMask();

            entity.Extension = Part(index++);

            if (OAIQueryListExtended.MASK_HUNT_GROUP_LIST_USER ==
                (OAIQueryListExtended.MASK_HUNT_GROUP_LIST_USER & mask))
            {
                entity.Username = Part(index++);
            }

            if (OAIQueryListExtended.MASK_HUNT_GROUP_LIST_DESC ==
                (OAIQueryListExtended.MASK_HUNT_GROUP_LIST_DESC & mask))
            {
                entity.Description = Part(index++);
            }

            if (OAIQueryListExtended.MASK_HUNT_GROUP_LIST_HUNT_GROUP ==
                (OAIQueryListExtended.MASK_HUNT_GROUP_LIST_HUNT_GROUP & mask))
            {
                entity.Hunt_Group_Type = IntPart(index++);
            }

            if (OAIQueryListExtended.MASK_HUNT_GROUP_LIST_MEMBERS ==
                (OAIQueryListExtended.MASK_HUNT_GROUP_LIST_MEMBERS & mask))
            {
                entity.Number_Of_Members = IntPart(index++);
            }

            if (OAIQueryListExtended.MASK_HUNT_GROUP_LIST_MAILBOX ==
                (OAIQueryListExtended.MASK_HUNT_GROUP_LIST_MAILBOX & mask))
            {
                entity.Mailbox_Node_Number = IntPart(index++);
            }


            return entity;
        }

        public OAIQueryExtendedAgent AgentStructure(int index)
        {
            OAIQueryExtendedAgent entity = new OAIQueryExtendedAgent();

            int mask = EntityFieldMask();

            entity.Agent_ID = Part(index++);

            if (OAIQueryListExtended.MASK_ACD_AGENT_DESC ==
                (OAIQueryListExtended.MASK_ACD_AGENT_DESC & mask))
            {
                entity.Description = Part(index++);
            }

            return entity;
        }

        public OAIQueryExtendedStation StationStructure(int index)
        {
            OAIQueryExtendedStation entity = new OAIQueryExtendedStation();

            int mask = EntityFieldMask();

            entity.Extension = Part(index++);

            if (OAIQueryListExtended.MASK_STATION_LIST_USER == 
                (OAIQueryListExtended.MASK_STATION_LIST_USER & mask))
            {
                entity.Username = Part(index++);
            }

            if (OAIQueryListExtended.MASK_STATION_LIST_DESC ==
                (OAIQueryListExtended.MASK_STATION_LIST_DESC & mask))
            {
                entity.Description = Part(index++);
            }

            if (OAIQueryListExtended.MASK_STATION_LIST_ATTEND ==
                (OAIQueryListExtended.MASK_STATION_LIST_ATTEND & mask))
            {
                entity.Attendant = IntPart(index++);
            }

            if (OAIQueryListExtended.MASK_STATION_LIST_IS_ADMIN ==
                (OAIQueryListExtended.MASK_STATION_LIST_IS_ADMIN & mask))
            {
                entity.Is_An_Administrator = IntPart(index++);
            }

            if (OAIQueryListExtended.MASK_STATION_LIST_IS_ATTEND ==
                (OAIQueryListExtended.MASK_STATION_LIST_IS_ATTEND & mask))
            {
                entity.Is_An_Attendant = IntPart(index++);
            }

            if (OAIQueryListExtended.MASK_STATION_LIST_DAY_FLAGS ==
                (OAIQueryListExtended.MASK_STATION_LIST_DAY_FLAGS & mask))
            {
                entity.Day_COS_Flags = IntPart(index++);
            }

            if (OAIQueryListExtended.MASK_STATION_LIST_NIGHT_FLAGS ==
                (OAIQueryListExtended.MASK_STATION_LIST_NIGHT_FLAGS & mask))
            {
                entity.Night_COS_Flags = IntPart(index++);
            }

            if (OAIQueryListExtended.MASK_STATION_LIST_VOICE_MAIL ==
                (OAIQueryListExtended.MASK_STATION_LIST_VOICE_MAIL & mask))
            {
                entity.Voice_Mail_Extension = IntPart(index++);
            }

            if (OAIQueryListExtended.MASK_STATION_LIST_DEVICE_TYPE ==
                (OAIQueryListExtended.MASK_STATION_LIST_DEVICE_TYPE & mask))
            {
                entity.Device_Type = IntPart(index++);
            }

            if (OAIQueryListExtended.MASK_STATION_LIST_MAILBOX_NODE ==
                (OAIQueryListExtended.MASK_STATION_LIST_MAILBOX_NODE & mask))
            {
                entity.Mailbox_Node_Number = IntPart(index++);
            }

            if (OAIQueryListExtended.MASK_STATION_LIST_PHYSICAL_DEVICE ==
                (OAIQueryListExtended.MASK_STATION_LIST_PHYSICAL_DEVICE & mask))
            {
                entity.Physical_Device_Type = IntPart(index++);
            }

            return entity;
        }
    }
}
