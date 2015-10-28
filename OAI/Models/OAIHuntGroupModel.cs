using System.Collections.Generic;

using OAI.Queues.Changes;

namespace OAI.Models
{
    public class OAIHuntGroupModel : OAIModel
    {
        private string _HuntGroup;
        public string HuntGroup
        {
            get
            {
                return _HuntGroup;
            }

            set
            {
                // Only update/notify if a change has actually been made!
                if (null == value || 0 != value.CompareTo(_HuntGroup))
                {
                    _HuntGroup = value;

                    // Trigger Hunt Group update notification
                    OAIHuntGroupChangeQueue.Relay().Line = _HuntGroup;
                }
            }
        }

        private List<string> _Agents = new List<string>();
        public void AddAgent(string agent)
        {
            if (!_Agents.Contains(agent))
            {
                lock (_Agents)
                {
                    _Agents.Add(agent);
                }

                // Trigger Hunt Group update notification
                OAIHuntGroupChangeQueue.Relay().Line = _HuntGroup;
            }
        }

        public bool ContainsAgent(string agent)
        {
            return _Agents.Contains(agent);
        }

        public bool RemoveAgent(string agent)
        {
            bool removed = true;

            if (_Agents.Contains(agent))
            {
                lock (_Agents)
                {
                    removed = _Agents.Remove(agent);
                }

                // Trigger Hunt Group update notification
                OAIHuntGroupChangeQueue.Relay().Line = _HuntGroup;
                return removed;
            }

            return true;
        }

        public List<string> GetAgents()
        {
            List<string> agents = null;

            lock (_Agents)
            {
                agents = _Agents;
            }

            return agents;
        }

        private List<string> _Devices = new List<string>();
        public void AddDevice(string device)
        {
            if (!_Devices.Contains(device))
            {
                lock (_Devices)
                {
                    _Devices.Add(device);
                }

                // Trigger Hunt Group update notification
                OAIHuntGroupChangeQueue.Relay().Line = _HuntGroup;
            }
        }

        public bool RemoveDevice(string device)
        {
            bool removed = true;

            if (_Devices.Contains(device))
            {
                lock (_Devices)
                {
                    removed = _Devices.Remove(device);
                }

                // Trigger Hunt Group update notification
                OAIHuntGroupChangeQueue.Relay().Line = _HuntGroup;
                return removed;
            }

            return removed;
        }

        public bool ContainsDevice(string device)
        {
            return _Devices.Contains(device);
        }

        public List<string> GetDevices()
        {
            List<string> devices = null;

            lock (_Devices)
            {
                devices = _Devices;
            }

            return devices;
        }
    }
}
