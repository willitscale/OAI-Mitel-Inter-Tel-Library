using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using OAI.Packets;

namespace OAI.Queues
{
    public abstract class OAIQueue<T> where T : class
    {
        // Events waiting to be processed
        protected LinkedList<T> Events = new LinkedList<T>();

        // Events stashed from the queue
        protected LinkedList<T> Stash = new LinkedList<T>();

        protected static int Limit = 0;

        protected bool Enabled = true;

        public void Disable()
        {
            Enabled = false;
        }

        public void Enable()
        {
            Enabled = true;
        }

        /**
         * We may need one event to take presidence
         */
        public void Priority(T evt)
        {
            lock (Events)
            {
                Events.AddFirst(evt);
            }
        }

        /**
         * Stash queue is for preserving a queue in the event of
         * a queue processing failure where a new queue may need
         * to be created and the previous queue needs to be invoked
         * after a certain event has occured.
         */
        public void StashQueue()
        {
            lock (Events)
            {
                while (0 < Events.Count)
                {
                    Stash.AddLast(Events.First);
                    Events.RemoveFirst();
                }
            }
        }

        /**
         * Restore the stashed queue events
         */
        public void ApplyStash()
        {
            lock (Events)
            {
                while(0 < Stash.Count)
                {
                    Events.AddFirst(Stash.First);
                    Stash.RemoveFirst();
                }
            }
        }

        public T Line
        {
            get
            {
                T data = null;

                lock (Events)
                {
                    data = Events.First();
                    Events.RemoveFirst();
                }
                return data;
            }
            set
            {
                // Don't add queue items if the queue is disabled
                if (!Enabled)
                {
                    return;
                }

                lock (Events)
                {
                    if (0 < Limit && Events.Count > Limit)
                    {
                        Events.RemoveFirst();
                    }

                    Events.AddLast(value);
                }
            }
        }

        public bool Available()
        {
            /**
             * Rather than trying to push through event that won't
             * be delayed we need to delay the entire queue.
             * 
             * This is important to prevent loss of synchronization 
             * and to preserve event ordering which may be required.
             */
            if (Delay() && Delayed())
            {
                return false;
            }

            bool available = false;

            lock (Events)
            {
                available = (0 < Events.Count);
            }

            return available;
        }

        public int Count()
        {
            int count = 0;

            lock (Events)
            {
                count = Events.Count;
            }

            return count;
        }

        protected abstract bool Delay();

        protected abstract bool Delayed();
    }
}
