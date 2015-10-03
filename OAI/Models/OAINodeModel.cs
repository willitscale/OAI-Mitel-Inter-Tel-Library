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
                // Only update/notify if a change has actually been made!
                if (null == value || 0 != value.CompareTo(_Node))
                {
                    _Node = value;

                    // Trigger Node update notification
                    OAINodeChangeQueue.Relay().Line = _Node;
                }
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
                // Only update/notify if a change has actually been made!
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
                // Only update/notify if a change has actually been made!
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
