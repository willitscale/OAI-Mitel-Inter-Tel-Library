using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OAI.Packets.Commands;
using OAI.Structures.Queries;
using OAI.Queues;
using OAI.Bus;

namespace OAI.Packets.Events.Commands
{
    public class OAIQueryHuntGroupCF : OAIConfirmation
    {
        public const string COMMAND = OAIQueryHuntGroup.CMD;

        public OAIQueryHuntGroupCF(string[] parts) : base(parts) {}
        public OAIQueryHuntGroupCF(byte[] bytes) : base(bytes) { }

        /**
         * 5 <Number_of_Entries>
         * Identifies the number of <Agent_ID> entries that follow this
         * parameter. If this field is zero (0), the hunt group type is 
         * Personal Router Device.
         */
        public int EntityCount()
        {
            return IntPart(5);
        }

        /**
         * 9 <InInitState>
         * If set to (0) the Hunt group is not initializing, if set to (1) 
         * it is initializing.
         */
        public int InInitState()
        {
            return IntPart(Parts.Length - 2);
        }

        /**
         * 10 <Hunt_Group_Type>
         * Hunt_Group_Type: Identifies the type of the hunt group. 
         * Hunt group types include:
         *      0 = UCD Hunt Group
         *      1 = ACD Hunt Group without Agent IDs
         *      2 = ACD Hunt Group with Agent IDs
         *      3 = Personal Router Device Hunt Group
         */
        public int HuntGroupType()
        {
            return IntPart(Parts.Length - 1);
        }

        public int EntityOffset()
        {
            return 6;
        }

        public int SegmentCount()
        {
            return 3;
        }

        public new void Process()
        {
            int offset = EntityOffset();
            int count = EntityCount() * SegmentCount();
            int segments = SegmentCount();

            for (int i = offset; i < offset+count; i += segments)
            {
                OAIAgentHuntGroupBus.Relay().Push(Structure(i));
            }
        }

        public OAIQQueryHuntGroup Structure(int index)
        {
            // LOGGED OUT - 5610,,0
            // LOGGED IN AND FREE - 5488,1883,1
            // LOGGED IN AND BUSY - 5054,1876,2
            OAIQQueryHuntGroup entity = new OAIQQueryHuntGroup();

            entity.Agent_ID = Part(index);
            entity.Device_Ext = Part(index+1);
            entity.ACD_UCD_Mode = IntPart(index+2);

            return entity;
        }
    }
}
