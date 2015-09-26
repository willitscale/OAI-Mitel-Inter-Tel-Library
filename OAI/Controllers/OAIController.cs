using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using OAI.Abstraction;
using OAI.Models;

namespace OAI.Controllers
{
    public abstract class OAIController<T> : OAIDictionary<T> where T : OAIModel { }
}
