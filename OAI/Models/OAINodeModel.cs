using OAI.Queues.Changes;

namespace OAI.Models
{
    public class OAINodeModel : OAIModel
    {
        private string _Node;
        public string Node
        {
            get
            {
                return _Node;
            }

            set
            {
                _Node = value;

                    // Trigger Node update notification
                OAINodeChangeQueue.Relay().Line = _Node;
            }
        }

        private int _State;
        public int State
        {
            get
            {
                return _State;
            }

            set
            {
                if (_State != value)
                {
                    _State = value;

                    // Trigger Node update notification
                    OAINodeChangeQueue.Relay().Line = _Node;
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
                if (_Status != value)
                {
                    _Status = value;

                    // Trigger Node update notification
                    OAINodeChangeQueue.Relay().Line = _Node;
                }
            }
        }
    }
}
