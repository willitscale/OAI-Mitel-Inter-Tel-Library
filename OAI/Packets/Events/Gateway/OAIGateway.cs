using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Packets.Events.Gateway
{
    public abstract class OAIGateway : OAIEvent
    {
        public OAIGateway(string[] parts) : base(parts) { }
        public OAIGateway(byte[] bytes) : base(bytes) { }
    }
}
