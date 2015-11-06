using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

using OAI.Packets.Commands;
using OAI.Queues;

using OAI.Controllers;
using OAI.Models;

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
                        Station(i);
                        continue;
                    }

                    // 3 Hunt Group List
                    case OAIQueryListExtended.LIST_TYPE_HUNT_GROUP_LIST:
                    {
                        HuntGroup(i);
                        continue;
                    }

                    // 8 Feature List 
                    case OAIQueryListExtended.LIST_TYPE_FEATURE_LIST:
                    {
                        Feature(i);
                        continue;
                    }

                    // 10 DND Message List
                    case OAIQueryListExtended.LIST_TYPE_DND_LIST:
                    {
                        DND(i);
                        continue;
                    }

                    // 17 ACD Agent List
                    case OAIQueryListExtended.LIST_TYPE_ACD_AGENT:
                    {
                        Agent(i);
                        continue;
                    }
                }
            }
        }

        public void Feature(int index)
        {
            string feature = Part(index++);

            OAIFeatureModel model = GetFeature(feature);

            if (null == model)
            {
                model = new OAIFeatureModel();
                model.FeatureCode = feature;
            }

            int mask = EntityFieldMask();

            if (OAIQueryListExtended.MASK_FEATURE_LIST_FEAT_NUM ==
                (OAIQueryListExtended.MASK_FEATURE_LIST_FEAT_NUM & mask))
            {
                model.FeatureNumber = Part(index++);
            }

            if (OAIQueryListExtended.MASK_FEATURE_LIST_FEAT_NAME ==
                (OAIQueryListExtended.MASK_FEATURE_LIST_FEAT_NAME & mask))
            {
                model.FeatureName = Part(index++);
            }

            if (OAIQueryListExtended.MASK_FEATURE_LIST_IS_ADMIN ==
                (OAIQueryListExtended.MASK_FEATURE_LIST_IS_ADMIN & mask))
            {
                model.IsAdministratorFeature = IntPart(index++);
            }

            if (OAIQueryListExtended.MASK_FEATURE_LIST_IS_DIR ==
                (OAIQueryListExtended.MASK_FEATURE_LIST_IS_DIR & mask))
            {
                model.IsDirectoryFeature = IntPart(index++);
            }

            if (OAIQueryListExtended.MASK_FEATURE_LIST_IS_DIAG ==
                (OAIQueryListExtended.MASK_FEATURE_LIST_IS_DIAG & mask))
            {
                model.IsDiagnosticFeature = IntPart(index++);
            }

            if (OAIQueryListExtended.MASK_FEATURE_LIST_IS_TOGGLE ==
                (OAIQueryListExtended.MASK_FEATURE_LIST_IS_TOGGLE & mask))
            {
                model.IsToggleableFeature = IntPart(index++);
            }

            OAIFeatureController
                .Relay()
                .Push(feature, model);
        }

        public void DND(int index)
        {
            string dnd = Part(index++);

            OAIDNDModel model = GetDND(dnd);

            if (null == model)
            {
                model = new OAIDNDModel();
                model.DNDMessageNumber = dnd;
            }

            int mask = EntityFieldMask();

            if (OAIQueryListExtended.MASK_DND_LIST_MESSAGE ==
                (OAIQueryListExtended.MASK_DND_LIST_MESSAGE & mask))
            {
                model.DNDMessageText = Part(index++);
            }

            OAIDNDController
                .Relay()
                .Push(dnd, model);
        }

        public void HuntGroup(int index)
        {
            string huntGroup = Part(index++);

            OAIHuntGroupModel model = GetHuntGroup(huntGroup);

            if (null == model)
            {
                model = new OAIHuntGroupModel();
                model.HuntGroup = huntGroup;
            }

            int mask = EntityFieldMask();

            if (OAIQueryListExtended.MASK_HUNT_GROUP_LIST_USER ==
                (OAIQueryListExtended.MASK_HUNT_GROUP_LIST_USER & mask))
            {
                model.Username = Part(index++);
            }

            if (OAIQueryListExtended.MASK_HUNT_GROUP_LIST_DESC ==
                (OAIQueryListExtended.MASK_HUNT_GROUP_LIST_DESC & mask))
            {
                model.Description = Part(index++);
            }

            if (OAIQueryListExtended.MASK_HUNT_GROUP_LIST_HUNT_GROUP ==
                (OAIQueryListExtended.MASK_HUNT_GROUP_LIST_HUNT_GROUP & mask))
            {
                model.HuntGroupType = IntPart(index++);
            }

            if (OAIQueryListExtended.MASK_HUNT_GROUP_LIST_MEMBERS ==
                (OAIQueryListExtended.MASK_HUNT_GROUP_LIST_MEMBERS & mask))
            {
                model.NumberOfMembers = IntPart(index++);
            }

            if (OAIQueryListExtended.MASK_HUNT_GROUP_LIST_MAILBOX ==
                (OAIQueryListExtended.MASK_HUNT_GROUP_LIST_MAILBOX & mask))
            {
                model.MailboxNodeNumber = IntPart(index++);
            }

            OAIHuntGroupsController
                .Relay()
                .Push(huntGroup, model);
        }

        public void Agent(int index)
        {
            string agent = Part(index++);

            OAIAgentModel model = GetAgent(agent);

            if (null == model)
            {
                model = new OAIAgentModel();
                model.Agent = agent;
            }

            int mask = EntityFieldMask();

            if (OAIQueryListExtended.MASK_ACD_AGENT_DESC ==
                (OAIQueryListExtended.MASK_ACD_AGENT_DESC & mask))
            {
                model.Description = Part(index++);
            }

            OAIAgentsController
                .Relay()
                .Push(agent, model);
        }

        public void Station(int index)
        {
            string extension = Part(index++);

            OAIDeviceModel model = GetDevice(extension);

            if (null == model)
            {
                model = new OAIDeviceModel();
                model.Extension = extension;
            }

            int mask = EntityFieldMask();


            if (OAIQueryListExtended.MASK_STATION_LIST_USER == 
                (OAIQueryListExtended.MASK_STATION_LIST_USER & mask))
            {
                model.Username = Part(index++);
            }

            if (OAIQueryListExtended.MASK_STATION_LIST_DESC ==
                (OAIQueryListExtended.MASK_STATION_LIST_DESC & mask))
            {
                model.Description = Part(index++);
            }

            if (OAIQueryListExtended.MASK_STATION_LIST_ATTEND ==
                (OAIQueryListExtended.MASK_STATION_LIST_ATTEND & mask))
            {
                model.Attendant = IntPart(index++);
            }

            if (OAIQueryListExtended.MASK_STATION_LIST_IS_ADMIN ==
                (OAIQueryListExtended.MASK_STATION_LIST_IS_ADMIN & mask))
            {
                model.IsAnAdministrator = IntPart(index++);
            }

            if (OAIQueryListExtended.MASK_STATION_LIST_IS_ATTEND ==
                (OAIQueryListExtended.MASK_STATION_LIST_IS_ATTEND & mask))
            {
                model.IsAnAttendant = IntPart(index++);
            }

            if (OAIQueryListExtended.MASK_STATION_LIST_DAY_FLAGS ==
                (OAIQueryListExtended.MASK_STATION_LIST_DAY_FLAGS & mask))
            {
                model.DayCOSFlags = IntPart(index++);
            }

            if (OAIQueryListExtended.MASK_STATION_LIST_NIGHT_FLAGS ==
                (OAIQueryListExtended.MASK_STATION_LIST_NIGHT_FLAGS & mask))
            {
                model.NightCOSFlags = IntPart(index++);
            }

            if (OAIQueryListExtended.MASK_STATION_LIST_VOICE_MAIL ==
                (OAIQueryListExtended.MASK_STATION_LIST_VOICE_MAIL & mask))
            {
                model.VoiceMailExtension = IntPart(index++);
            }

            if (OAIQueryListExtended.MASK_STATION_LIST_DEVICE_TYPE ==
                (OAIQueryListExtended.MASK_STATION_LIST_DEVICE_TYPE & mask))
            {
                model.DeviceType = IntPart(index++);
            }

            if (OAIQueryListExtended.MASK_STATION_LIST_MAILBOX_NODE ==
                (OAIQueryListExtended.MASK_STATION_LIST_MAILBOX_NODE & mask))
            {
                model.MailboxNodeNumber = IntPart(index++);
            }

            if (OAIQueryListExtended.MASK_STATION_LIST_PHYSICAL_DEVICE ==
                (OAIQueryListExtended.MASK_STATION_LIST_PHYSICAL_DEVICE & mask))
            {
                model.PhysicalDeviceType = IntPart(index++);
            }

            OAIDevicesController
                .Relay()
                .Push(extension, model);
        }
    }
}
