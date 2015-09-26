using System.Collections.Generic;

using OAI.Queues.Changes;

namespace OAI.Models
{
    public class OAIAgentModel : OAIModel
    {
        private string _Extension;
        public string Extension
        { 
            get
            {
                return _Extension;
            }
            
            set
            {
                if (null == value)
                {
                    _Available = 0;
                }
                _Extension = value;

                // Trigger Agent update notification
                OAIAgentChangeQueue.Relay().Line = _Agent;
            }
        }

        private string _Username;
        public string Username
        {
            get
            {
                return _Username;
            }

            set
            {
                _Username = value;

                // Trigger Agent update notification
                OAIAgentChangeQueue.Relay().Line = _Agent;
            }
        }

        private string _Agent;
        public string Agent
        {
            get
            {
                return _Agent;
            }

            set
            {
                _Agent = value;

                // Trigger Agent update notification
                OAIAgentChangeQueue.Relay().Line = (null == _Agent) ? value : _Agent;
            }
        }


        private int _Available;
        public int Available
        {
            get
            {
                return _Available;
            }

            set
            {
                _Available = value;

                // Trigger Agent update notification
                OAIAgentChangeQueue.Relay().Line = _Agent;
            }
        }

        private string _DND;
        public string DND
        {
            get
            {
                return _DND;
            }

            set
            {
                _DND = value;

                // Trigger Agent update notification
                OAIAgentChangeQueue.Relay().Line = _Agent;
            }
        }

        private List<string> _Groups = new List<string>();

        public void AddGroup(string group)
        {
            if (!_Groups.Contains(group))
            {
                _Groups.Add(group);

                // Trigger Agent update notification
                OAIAgentChangeQueue.Relay().Line = _Agent;
            }
        }

        public bool RemoveGroup(string group)
        {
            if (_Groups.Contains(group))
            {
                bool removed = _Groups.Remove(group);

                // Trigger Agent update notification
                OAIAgentChangeQueue.Relay().Line = _Agent;
                return removed;
            }

            return true;
        }

        private string _ActiveCall;
        public string ActiveCall
        {
            get
            {
                return _ActiveCall;
            }

            set
            {
                _ActiveCall = value;

                // Trigger Agent update notification
                OAIAgentChangeQueue.Relay().Line = _Agent;
            }
        }

        private List<string> _Calls = new List<string>();

        public void AddCall(string call)
        {
            if (!_Calls.Contains(call))
            {
                _Calls.Add(call);
            }

            ActiveCall = call;
        }

        public void QueueCall(string call)
        {
            if (!_Calls.Contains(call))
            {
                _Calls.Add(call);
            }

            // Trigger Agent update notification
            OAIAgentChangeQueue.Relay().Line = _Agent;
        }

        public bool ValidCall(string call)
        {
            if (0 == _Calls.Count)
            {
                return false;
            }

            return _Calls.Contains(call);
        }

        public bool RemoveCall(string call)
        {
            if (_Calls.Contains(call))
            {
                bool removed = _Calls.Remove(call);

                if ( null != _ActiveCall && 0 == _ActiveCall.CompareTo(call) )
                {
                    _ActiveCall = null;
                }

                // Trigger Agent update notification
                OAIAgentChangeQueue.Relay().Line = _Agent;
                return removed;
            }

            return true;
        }

        public List<string> GetCalls()
        {
            List<string> calls = null;

            lock(_Calls)
            {
                calls = _Calls;
            }

            return calls;
        }
    }
}
