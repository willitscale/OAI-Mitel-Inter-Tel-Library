using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OAI.Models;
using OAI.Controllers;

namespace OAI.Packets.Events.Feature
{
    public abstract class OAIFeature : OAIEvent
    {
        public OAIFeature(string[] parts) : base(parts) { }
        public OAIFeature(byte[] bytes) : base(bytes) { }
    }
}
