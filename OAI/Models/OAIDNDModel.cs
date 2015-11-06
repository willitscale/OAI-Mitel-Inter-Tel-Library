using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OAI.Queues.Changes;

namespace OAI.Models
{
    public class OAIDNDModel : OAIModel
    {
        /**
         * <DND_Message_Number> Number (1-20) assigned to the DND message.
         */
        private string _DNDMessageNumber;
        public string DNDMessageNumber
        {
            get
            {
                return _DNDMessageNumber;
            }

            set
            {
                // Only update/notify if a change has actually been made!
                if (null == value || 0 != value.CompareTo(_DNDMessageNumber))
                {
                    _DNDMessageNumber = value;

                    // Trigger DND update notification
                    OAIDNDChangeQueue.Relay().Line = (null == _DNDMessageNumber) ? value : _DNDMessageNumber;
                }
            }
        }

        /**
         * <|DND_Message_Text|> Text assigned to the DND message number.
         */
        private string _DNDMessageText;
        public string DNDMessageText
        {
            get
            {
                return _DNDMessageText;
            }

            set
            {
                // Only update/notify if a change has actually been made!
                if (null == value || 0 != value.CompareTo(_DNDMessageText))
                {
                    _DNDMessageText = value;

                    // Trigger Feature update notification
                    OAIDNDChangeQueue.Relay().Line = _DNDMessageNumber;
                }
            }
        }
    }
}
