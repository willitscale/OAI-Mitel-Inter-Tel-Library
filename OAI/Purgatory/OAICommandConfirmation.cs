using System.Collections.Generic;

using OAI.Packets;
using OAI.Packets.Events;
using OAI.Queues;

namespace OAI.Purgatory
{
    class OAICommandConfirmation
    {
        private static Dictionary<int, OAICommand> Judgement = 
            new Dictionary<int, OAICommand>();

        public static void Push(OAICommand cmd)
        {
            lock (Judgement)
            {
                if (!Judgement.ContainsKey(cmd.Invoke))
                {
                    Judgement.Add(cmd.Invoke, cmd);
                }
            }
        }

        public static OAICommand Pop(int key)
        {
            OAICommand packet = null;

            lock (Judgement)
            {
                if (Judgement.ContainsKey(key))
                {
                    packet = Judgement[key];
                    Judgement.Remove(key);
                }
            }

            return packet;
        }

        public static OAICommand Peek(int key)
        {
            OAICommand packet = null;

            lock (Judgement)
            {
                if (Judgement.ContainsKey(key))
                {
                    packet = Judgement[key];
                }
            }

            return packet;
        }

        public static void Confirmation(OAIConfirmation confirmation)
        {
            OAICommand cmd = Pop(confirmation.Invoke());

            if (null != cmd)
            {
                // We need to reference the outcome in the command
                cmd.Bind(confirmation);

                // We need to be able to reference the command in the confirmation
                confirmation.Bind(cmd);

                // Need something here to deal with processing non-generic
                cmd.Process();
            }
        }
    }
}
