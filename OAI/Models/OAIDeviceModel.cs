using System.Collections.Generic;

using OAI.Queues.Changes;
using OAI.Controllers;

namespace OAI.Models
{
    public class OAIDeviceModel : OAIModel
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
                
                // Only update/notify if a change has actually been made!
                if (null == value || 0 != value.CompareTo(_Extension))
                {
                    _Extension = value;

                    // Trigger Device update notification
                    OAIDeviceChangeQueue.Relay().Line = (null == _Extension) ? value : _Extension;
                }
            }
        }

        public string _Type;
        public string Type
        {
            get
            {
                return _Type;
            }

            set
            {
                // Only update/notify if a change has actually been made!
                if (null == value || 0 != value.CompareTo(_Type))
                {
                    _Type = value;

                    // Trigger Device update notification
                    OAIDeviceChangeQueue.Relay().Line = _Extension;
                }
            }
        }

        public string _Agent;
        public string Agent
        {
            get
            {
                return _Agent;
            }

            set
            {
                // Only update/notify if a change has actually been made!
                if (null == value || 0 != value.CompareTo(_Agent))
                {
                    _Agent = value;

                    // Trigger Device update notification
                    OAIDeviceChangeQueue.Relay().Line = _Extension;
                }
            }
        }

        public string _Description;
        public string Description
        {
            get
            {
                return _Description;
            }

            set
            {
                // Only update/notify if a change has actually been made!
                if (null == value || 0 != value.CompareTo(_Description))
                {
                    _Description = value;

                    // Trigger Device update notification
                    OAIDeviceChangeQueue.Relay().Line = _Extension;
                }
            }
        }

        public int _Available;
        public int Available
        {
            get
            {
                return _Available;
            }

            set
            {
                if (_Available != value )
                {
                    _Available = value;

                    // Trigger Device update notification
                    OAIDeviceChangeQueue.Relay().Line = _Extension;
                }
            }
        }

        public string _DNDMessage;
        public string DNDMessage
        {
            get
            {
                return _DNDMessage;
            }

            set
            {
                // Only update/notify if a change has actually been made!
                if (null == value || 0 != value.CompareTo(_DNDMessage))
                {
                    _DNDMessage = value;

                    // Trigger Device update notification
                    OAIDeviceChangeQueue.Relay().Line = _Extension;
                }
            }
        }

        public string _DNDText;
        public string DNDText
        {
            get
            {
                return _DNDText;
            }

            set
            {
                _DNDText = value;
                if (null == _ActiveCall || 0 == _ActiveCall.Length)
                {
                    _Available = 2;
                    OAIAgentModel model = OAIAgentsController.Relay().Peek(_Agent);

                    if(null != model)
                    {
                        model.Available = _Available;
                    }
                }

                // Trigger Device update notification
                OAIDeviceChangeQueue.Relay().Line = _Extension;
            }
        }

        private List<string> _Groups = new List<string>();

        public void AddGroup(string group)
        {
            if (!_Groups.Contains(group))
            {
                _Groups.Add(group);

                // Trigger Device update notification
                OAIDeviceChangeQueue.Relay().Line = _Extension;
            }
        }

        public bool RemoveGroup(string group)
        {
            if (_Groups.Contains(group))
            {
                bool removed = _Groups.Remove(group);

                // Trigger Device update notification
                OAIDeviceChangeQueue.Relay().Line = _Extension;
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
                // Only update/notify if a change has actually been made!
                if (null == value || 0 != value.CompareTo(_ActiveCall))
                {
                    _ActiveCall = value;

                    // Trigger Device update notification
                    OAIDeviceChangeQueue.Relay().Line = _Extension;
                }
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

            // Trigger Device update notification
            OAIDeviceChangeQueue.Relay().Line = _Extension;
        }

        public bool RemoveCall(string call)
        {
            if (_Calls.Contains(call))
            {
                bool removed = _Calls.Remove(call);

                if (null != _ActiveCall && 0 == _ActiveCall.CompareTo(call))
                {
                    _ActiveCall = null;
                }

                // Trigger Device update notification
                OAIDeviceChangeQueue.Relay().Line = _Extension;
                return removed;
            }

            return true;
        }

        public List<string> GetCalls()
        {
            List<string> calls = null;

            lock (_Calls)
            {
                calls = _Calls;
            }

            return calls;
        }
    }
}
