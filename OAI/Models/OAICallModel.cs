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
                _Call = value;
                OAICallChangeQueue.Relay().Line = (null == _Call) ? value : _Call;
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
                _Extension = value;
                OAICallChangeQueue.Relay().Line = _Call;
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
                _AccountCode = value;
                OAICallChangeQueue.Relay().Line = _Call;
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
                _DDI = value;
                OAICallChangeQueue.Relay().Line = _Call;
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
                _CLI = value;
                OAICallChangeQueue.Relay().Line = _Call;
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
                OAICallChangeQueue.Relay().Line = _Call;
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
                _Trunk = value;
                OAICallChangeQueue.Relay().Line = _Call;
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
                _Status = value;
                OAICallChangeQueue.Relay().Line = _Call;
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
                _Hold = value;
                OAICallChangeQueue.Relay().Line = _Call;
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
                _Caller = value;
                OAICallChangeQueue.Relay().Line = _Call;
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
                _CNX = value;
                OAICallChangeQueue.Relay().Line = _Call;
            }
        }
    }
}
