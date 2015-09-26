using System.Collections.Generic;

namespace OAI.Abstraction
{
    public abstract class OAIDictionary<T>
    {
        protected Dictionary<string, T> Data = new Dictionary<string, T>();

        /**
         * Singleton design pattern
         */
        protected OAIDictionary() { }

        public void Push(string key, T value)
        {
            if (null == key)
            {
                return;
            }

            lock (Data)
            {
                if (!Data.ContainsKey(key))
                {
                    Data.Add(key, value);
                }
            }
        }

        public T Pop(string key)
        {

            T packet = default( T );

            if (null == key)
            {
                return packet;
            }

            lock (Data)
            {
                if (Data.ContainsKey(key))
                {
                    packet = Data[key];
                    Data.Remove(key);
                }
            }

            return packet;
        }

        public T Peek(string key)
        {
            T packet = default(T);

            if (null == key)
            {
                return packet;
            }

            lock (Data)
            {
                if (Data.ContainsKey(key))
                {
                    packet = Data[key];
                }
            }

            return packet;
        }

        public bool Exists(string key)
        {
            bool exists = false;

            if (null == key)
            {
                return exists;
            }

            lock (Data)
            {
                exists = Data.ContainsKey(key);
            }

            return exists;
        }

        public List<T> All()
        {
            List<T> all = new List<T>();

            lock (Data)
            {
                foreach (KeyValuePair<string, T> entry in Data)
                {
                    all.Add(entry.Value);
                }
            }

            return all;
        }
    }
}
