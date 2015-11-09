using System.Collections.Generic;

using OAI.Queues.Changes;
using OAI.Controllers;

namespace OAI.Models
{
    public class OAIDeviceModel : OAIModel
    {
        /**
         * <Device_Type> 
         *      0 Keyset Station Device         
         *      1 SingleLine Station Device    
         *      2 ACD/Hunt Group                
         *      3 Loop Start Trunk              
         *      4 Loop Start Trunk w/Caller ID  
         *      5 Ground Start Trunk            
         *      6 Ground Start Trunk w/Caller ID 
         *      7 DID Trunk                     
         *      8 E&M Trunk                     
         *      9 ISDN Trunk (Primary or Basic Rate) 
         *      
         *      10 Trunk Group                  
         *      11 Operator 
         *      12 Voice Mail 
         *      13 Other 
         *      14 Feature Code2, 3 
         *      15 Page Zone2 
         *      16 Page Port2 
         *      17 OffNode Keyset Station Device2 
         *      18 OffNode SingleLine Station Device2 
         *      19 OffNode Hunt Group2 
         *      
         *      20 OffNode Page Port2
         *      21 OffNode Page Zone2
         *      22 OffNode Voice Mail2
         *      23 ACD Agent2
         *      24 Unassociated Mailbox2
         *      25 OffNode Unassociated Mailbox2
         *      26 BRI Station
         *      27 OffNode BRI Station2
         *      28 MFC/R2 Trunk
         *      29 Modem1
         *      
         *      30 Off Node Modem4
         *      31 MGCP Phone
         *      32 MGCP Gateway and Phone
         *      33 SIP Trunk
         *      34 Phantom
         *      35 SIP Translator
         *      36 UC Advanced Softphone
         *      37 Configuration Assistant2
         *      38 SIP Phone
         *      39 OfficeLink Assistant
         *      
         *      40 MeetMe Conference Assistant
         */
        public static string[] TYPES = new string[]
        {
            "Keyset Station Device",
            "SingleLine Station Device",
            "ACD/Hunt Group",
            "Loop Start Trunk",
            "Loop Start Trunk w/Caller ID",
            "Ground Start Trunk",
            "Ground Start Trunk w/Caller ID",
            "DID Trunk",
            "E&M Trunk ",
            "ISDN Trunk (Primary or Basic Rate)",

            "Trunk Group",
            "Operator",
            "Voice Mail",
            "Other",
            "Feature Code2, 3",
            "Page Zone2",
            "Page Port2",
            "OffNode Keyset Station Device2",
            "OffNode SingleLine Station Device2",
            "OffNode Hunt Group2",

            "OffNode Page Port2",
            "OffNode Page Zone2",
            "OffNode Voice Mail2",
            "ACD Agent2",
            "Unassociated Mailbox2",
            "OffNode Unassociated Mailbox2",
            "BRI Station",
            "OffNode BRI Station2",
            "MFC/R2 Trunk",
            "Modem1",

            "Off Node Modem4",
            "MGCP Phone",
            "MGCP Gateway and Phone",
            "SIP Trunk",
            "Phantom",
            "SIP Translator",
            "UC Advanced Softphone",
            "Configuration Assistant2",
            "SIP Phone",
            "OfficeLink Assistant",

            "MeetMe Conference Assistant"
        };

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

        private int _DeviceType;
        public int DeviceType
        {
            get
            {
                return _DeviceType;
            }

            set
            {
                if (_DeviceType != value)
                {
                    _DeviceType = value;
                    _Type = TYPES[ _DeviceType ];
                    // Trigger Device update notification
                    OAIDeviceChangeQueue.Relay().Line = _Extension;
                }
            }
        }

        private int _PhysicalDeviceType;
        public int PhysicalDeviceType
        {
            get
            {
                return _PhysicalDeviceType;
            }

            set
            {
                if (_PhysicalDeviceType != value)
                {
                    _PhysicalDeviceType = value;
                    // Trigger Device update notification
                    OAIDeviceChangeQueue.Relay().Line = _Extension;
                }
            }
        }

        private int _Attendant;
        public int Attendant
        {
            get
            {
                return _Attendant;
            }

            set
            {
                if (_Attendant != value)
                {
                    _Attendant = value;
                    // Trigger Device update notification
                    OAIDeviceChangeQueue.Relay().Line = _Extension;
                }
            }
        }

        private int _IsAnAdministrator;
        public int IsAnAdministrator
        {
            get
            {
                return _IsAnAdministrator;
            }

            set
            {
                if (_IsAnAdministrator != value)
                {
                    _IsAnAdministrator = value;
                    // Trigger Device update notification
                    OAIDeviceChangeQueue.Relay().Line = _Extension;
                }
            }
        }

        private int _IsAnAttendant;
        public int IsAnAttendant
        {
            get
            {
                return _IsAnAttendant;
            }

            set
            {
                if (_IsAnAttendant != value)
                {
                    _IsAnAttendant = value;
                    // Trigger Device update notification
                    OAIDeviceChangeQueue.Relay().Line = _Extension;
                }
            }
        }

        private int _DayCOSFlags;
        public int DayCOSFlags
        {
            get
            {
                return _DayCOSFlags;
            }

            set
            {
                if (_DayCOSFlags != value)
                {
                    _DayCOSFlags = value;
                    // Trigger Device update notification
                    OAIDeviceChangeQueue.Relay().Line = _Extension;
                }
            }
        }

        private int _NightCOSFlags;
        public int NightCOSFlags
        {
            get
            {
                return _NightCOSFlags;
            }

            set
            {
                if (_NightCOSFlags != value)
                {
                    _NightCOSFlags = value;
                    // Trigger Device update notification
                    OAIDeviceChangeQueue.Relay().Line = _Extension;
                }
            }
        }

        private int _VoiceMailExtension;
        public int VoiceMailExtension
        {
            get
            {
                return _VoiceMailExtension;
            }

            set
            {
                if (_VoiceMailExtension != value)
                {
                    _VoiceMailExtension = value;
                    // Trigger Device update notification
                    OAIDeviceChangeQueue.Relay().Line = _Extension;
                }
            }
        }

        private int _MailboxNodeNumber;
        public int MailboxNodeNumber
        {
            get
            {
                return _MailboxNodeNumber;
            }

            set
            {
                if (_MailboxNodeNumber != value)
                {
                    _MailboxNodeNumber = value;
                    // Trigger Device update notification
                    OAIDeviceChangeQueue.Relay().Line = _Extension;
                }
            }
        }

        private string _Type;
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

                    // Trigger Device update notification
                    OAIDeviceChangeQueue.Relay().Line = _Extension;
                }
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
                // Only update/notify if a change has actually been made!
                if (null == value || 0 != value.CompareTo(_Agent))
                {
                    _Agent = value;

                    // Trigger Device update notification
                    OAIDeviceChangeQueue.Relay().Line = _Extension;
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

                    // Trigger Device update notification
                    OAIDeviceChangeQueue.Relay().Line = _Extension;
                }
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
                if (_Available != value )
                {
                    _Available = value;

                    // Trigger Device update notification
                    OAIDeviceChangeQueue.Relay().Line = _Extension;
                }
            }
        }

        private string _DNDMessage;
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

        private string _DNDText;
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
