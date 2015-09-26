using OAI.Structures;
using OAI.Abstraction;

namespace OAI.Bus
{
    public abstract class OAIBus<T> : OAIDictionary<T> where T : OAIStructure
    {
        public void Push(T value)
        {
            Push(value.ID(), value);
        }
    }
}
