using OAI.Queues.Changes;

namespace OAI.Models
{
    public class OAICallModel : OAIModel
    {
        private string _Call;
        public string Call
        {
            get
            {
                return _Call;
            }

            set
            {
                // Only update/notify if a change has actually been made!
                if (null == value || 0 != value.CompareTo(_Call))
                {
                    _Call = value;
                    OAICallChangeQueue.Relay().Line = (null == _Call) ? value : _Call;
                }
            }
        }

        private string _Extension;
        public string Extension
        {
            get
            {
                return _Extension;
            }

            set
            {
                // Only update/notify if a change has actually been made!
                if (null == value || 0 != value.CompareTo(_Extension))
                {
                    _Extension = value;
                    OAICallChangeQueue.Relay().Line = _Call;
                }
            }
        }

        private string _AccountCode;
        public string AccountCode
        {
            get
            {
                return _AccountCode;
            }

            set
            {
                // Only update/notify if a change has actually been made!
                if (null == value || 0 != value.CompareTo(_AccountCode))
                {
                    _AccountCode = value;
                    OAICallChangeQueue.Relay().Line = _Call;
                }
            }
        }

        private string _DDI;
        public string DDI
        {
            get
            {
                return _DDI;
            }
            set
            {
                // Only update/notify if a change has actually been made!
                if (null == value || 0 != value.CompareTo(_DDI))
                {
                    _DDI = value;
                    OAICallChangeQueue.Relay().Line = _Call;
                }
            }
        }

        private string _CLI;
        public string CLI
        {
            get
            {
                return _CLI;
            }
            set
            {
                // Only update/notify if a change has actually been made!
                if (null == value || 0 != value.CompareTo(_CLI))
                {
                    _CLI = value;
                    OAICallChangeQueue.Relay().Line = _Call;
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
                    OAICallChangeQueue.Relay().Line = _Call;
                }
            }
        }

        private string _Trunk;
        public string Trunk
        {
            get
            {
                return _Trunk;
            }

            set
            {
                // Only update/notify if a change has actually been made!
                if (null == value || 0 != value.CompareTo(_Trunk))
                {
                    _Trunk = value;
                    OAICallChangeQueue.Relay().Line = _Call;
                }
            }
        }

        private int _Status;
        public int Status
        {
            get
            {
                return _Status;
            }

            set
            {
                // Only update/notify if a change has actually been made!
                if (_Status != value)
                {
                    _Status = value;
                    OAICallChangeQueue.Relay().Line = _Call;
                }
            }
        }

        private bool _Hold;
        public bool Hold
        {
            get
            {
                return _Hold;
            }

            set
            {
                // Only update/notify if a change has actually been made!
                if (_Hold != value)
                {
                    _Hold = value;
                    OAICallChangeQueue.Relay().Line = _Call;
                }
            }
        }

        private string _Caller;
        public string Caller
        {
            get
            {
                return _Caller;
            }

            set
            {
                // Only update/notify if a change has actually been made!
                if (null == value || 0 != value.CompareTo(_Caller))
                {
                    _Caller = value;
                    OAICallChangeQueue.Relay().Line = _Call;
                }
            }
        }

        private string _CNX;
        public string CNX
        {
            get
            {
                return _CNX;
            }

            set
            {
                // Only update/notify if a change has actually been made!
                if (null == value || 0 != value.CompareTo(_CNX))
                {
                    _CNX = value;
                    OAICallChangeQueue.Relay().Line = _Call;
                }
            }
        }

        private int _Direction;
        public int Direction
        {
            get
            {
                return _Direction;
            }

            set
            {
                // Only update/notify if a change has actually been made!
                if (_Direction != value)
                {
                    _Direction = value;
                    OAICallChangeQueue.Relay().Line = _Call;
                }
            }
        }
    }
}
