using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Packets.Events.System
{
    public abstract class OAISystem : OAIEvent
    {
        public OAISystem(string[] parts) : base(parts) { }
        public OAISystem(byte[] bytes) : base(bytes) { }
    }
}
