using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OAI.Packets;

namespace OAI.Queues
{
    public abstract class OAIPacketQueue<T> : OAIQueue<T> where T : OAIPacket { }
}
