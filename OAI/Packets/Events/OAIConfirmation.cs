using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Packets.Events
{
    public class OAIConfirmation : OAIEvent
    {
        public const string EVENT = "CF";

        protected OAICommand Cmd;

        public OAIConfirmation(string[] parts) : base(parts) {}
        public OAIConfirmation(byte[] bytes) : base(bytes) { }

        public void Bind(OAICommand cmd)
        {
            Cmd = cmd;
        }

        public int Outcome()
        {
            return IntPart(4);
        }
    }
}
