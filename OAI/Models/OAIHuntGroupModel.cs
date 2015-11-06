using System.Collections.Generic;

using OAI.Queues.Changes;

namespace OAI.Models
{
    public class OAIHuntGroupModel : OAIModel
    {
        /**
         * Hunt Group List
         * 
         * <Extension> Extension of the hunt group.
         * <|Username|> Username assigned to the hunt group.
         * <|Description|> Description assigned to the hunt group.
         */

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

        /**
         * <Hunt_Group_Type> 
         *      0 = UCD Hunt Group
         *      1 = ACD Hunt Group without ACD Agent IDs
         *      2 = ACD Hunt Group with ACD Agent IDs
         *      3 = Personal Router Device Hunt Group
         */
        private int _HuntGroupType;
        public int HuntGroupType
        {
            get
            {
                return _HuntGroupType;
            }

            set
            {
                // Only update/notify if a change has actually been made!
                if (value != _HuntGroupType)
                {
                    _HuntGroupType = value;

                    // Trigger Hunt Group update notification
                    OAIHuntGroupChangeQueue.Relay().Line = _HuntGroup;
                }
            }
        }

        /** 
         * <Number_Of_Members> Total number of members or agent IDs for the Hunt Group.
         */
        private int _NumberOfMembers;
        public int NumberOfMembers
        {
            get
            {
                return _NumberOfMembers;
            }

            set
            {
                // Only update/notify if a change has actually been made!
                if (value != _NumberOfMembers)
                {
                    _NumberOfMembers = value;

                    // Trigger Hunt Group update notification
                    OAIHuntGroupChangeQueue.Relay().Line = _HuntGroup;
                }
            }
        }

        /** 
         * <Mailbox_Node_Number> The node containing the hunt group’s associated mailbox. If
         *      zero, the hunt group does not have an associated mailbox on
         *      any node. Extension lists count as one member, regardless of how
         *      many extensions are in the list. Each time an extension occurs in 
         *      the hunt path, it counts as a member. For example, if the hunt group 
         *      has members of 1000, 1001, and 1000 again, it is counted as 3 members. 
         */
        private int _MailboxNodeNumber;
        public int MailboxNodeNumber
        {
            get
            {
                return _MailboxNodeNumber;
            }

            set
            {
                // Only update/notify if a change has actually been made!
                if (value != _MailboxNodeNumber)
                {
                    _MailboxNodeNumber = value;

                    // Trigger Hunt Group update notification
                    OAIHuntGroupChangeQueue.Relay().Line = _HuntGroup;
                }
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
                // Only update/notify if a change has actually been made!
                if (null == value || 0 != value.CompareTo(_Username))
                {
                    _Username = value;

                    // Trigger Hunt Group update notification
                    OAIHuntGroupChangeQueue.Relay().Line = _HuntGroup;
                }
            }
        }

        private string _Description;
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

                    // Trigger Hunt Group update notification
                    OAIHuntGroupChangeQueue.Relay().Line = _HuntGroup;
                }
            }
        }
    }
}
