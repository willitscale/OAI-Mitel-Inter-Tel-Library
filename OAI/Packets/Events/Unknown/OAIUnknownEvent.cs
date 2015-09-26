using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Packets.Events.Unknown
{
    class OAIUnknownEvent : OAIEvent
    {
        public OAIUnknownEvent(string[] parts) : base(parts) {}
        public OAIUnknownEvent(byte[] bytes) : base(bytes) { }

        public new void Process() {}
    }
}
