using System.Collections.Generic;

namespace OAI.Packets.Delays
{
    public abstract class OAIDelay
    {
        protected Dictionary<string, bool> Delays = new Dictionary<string, bool>();

        /**
         * Singleton design pattern
         */
        protected OAIDelay() { }

        public void Block(string key)
        {
            lock (Delays)
            {
                if (!Delays.ContainsKey(key))
                {
                    Delays.Add(key, true);
                }
            }
        }

        public bool Delayed(string key)
        {
            bool delay = false;

            if (null == key)
            {
                return delay;
            }

            lock (Delays)
            {
                delay = (Delays.ContainsKey(key) && Delays[key]);
            }

            return delay;
        }

        public void Release(string key)
        {
            lock (Delays)
            {
                if (Delays.ContainsKey(key))
                {
                    Delays.Remove(key);
                }
            }
        }
    }
}
