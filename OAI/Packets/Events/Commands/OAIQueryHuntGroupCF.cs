using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OAI.Packets.Commands;
using OAI.Queues;

using OAI.Controllers;
using OAI.Models;

using OAI.Configuration;

namespace OAI.Packets.Events.Commands
{
    public class OAIQueryHuntGroupCF : OAIConfirmation
    {
        public const string COMMAND = OAIQueryHuntGroup.CMD;

        protected OAIHuntGroupModel Model;

        protected string Group;

        protected bool Master = false;

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
            
            OAIQueryHuntGroup cmd = (OAIQueryHuntGroup)Cmd;
            Group = cmd.GetHuntGroup();
            Master = cmd.GetMaster();

            Model = OAIHuntGroupsController
                .Relay()
                .Peek(Group);

            if (null == Model)
            {
                Model = new OAIHuntGroupModel();
            }

            for (int i = offset; i < offset+count; i += segments)
            {
                SetAttributes(i);
            }

            OAIHuntGroupsController
                .Relay()
                .Push(Group, Model);
        }

        public void SetAttributes(int index)
        {
            string agent = Part(index);
            string device = Part(index + 1);
            int available = (1 == IntPart(index + 2)) ? 1 : -1;

            Model.AddAgent(agent);
            Model.AddDevice(device);

            if (null == Group || !Master || 
                null == agent || null == device ||
                0 >= agent.Length || 0 >= device.Length )
            {
                return;
            }

            SetAgent(agent, device, available);
            SetDevice(device, agent, available);
        }

        protected void SetAgent(string agent, string device, int available)
        {
            OAIAgentModel model = GetAgent(agent);

            if (null == model)
            {
                model = new OAIAgentModel();
                model.Agent = agent;
            }

            model.Extension = device;
            model.Available = available;

            OAIAgentsController
                .Relay()
                .Push(agent, model);
        }

        protected void SetDevice(string device, string agent, int available)
        {
            OAIDeviceModel model = GetDevice(device);

            if (null == model)
            {
                model = new OAIDeviceModel();
                model.Extension = device;
            }

            model.Agent = agent;
            model.Available = available;

            OAIDevicesController
                .Relay()
                .Push(device, model);
        }
    }
}
