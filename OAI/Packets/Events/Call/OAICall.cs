using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OAI.Models;
using OAI.Controllers;

namespace OAI.Packets.Events.Call
{
    public abstract class OAICall : OAIEvent
    {
        public OAICall(string[] parts) : base(parts) { }
        public OAICall(byte[] bytes) : base(bytes) { }

        /**
         * 4 - Call_ID
         */
        public string CallID()
        {
            return Part(4);
        }

        protected void DisconnectCallFromExtension(string extension)
        {
            DisconnectCallFromExtension(extension, CallID());
        }

        protected void AddCallToExtension(string extension)
        {
            AddCallToExtension(extension, CallID());
        }

        protected void SetActiveCall(string extension)
        {
            SetActiveCall(extension,CallID());
        }

        protected void QueueCallToExtension(string extension)
        {
            QueueCallToExtension(extension, CallID());
        }
    }
}
