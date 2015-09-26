using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Packets.Events.Misc
{
    public abstract class OAIMisc : OAIEvent
    {
        public OAIMisc(string[] parts) : base(parts) { }
        public OAIMisc(byte[] bytes) : base(bytes) { }
    }
}
