## C# Mitel 5000 CP and Inter-Tel Axxess OAI Toolkit Integration

### Requirements

I designed this around the "System OAI Toolkit Specifications Manual – Issue 10.20, January 2011", so with this in mind some modifications may be required for previous revisions of the API which I may implement in due course.

### Basic instantiation using sockets

The following example will simply show you how to get the library to connect to your PBX or 

#### Prerequisites
```csharp
using OAI.Sequences;
using OAI.Configuration;
using OAI.Threads;
```

#### Your initialisation
```csharp
// Define your config setup "HOST", PORT, "APPLICATION NAME", <OPTIONAL>"PASSWORD", <OPTIONAL> TYPE
OAIConfig config = new OAIConfig("my-pbx-or-gateway.example.com", 12345, "OAI Desktop Client");

// Specify the connection sequence you want to use when connecting
OAIGlobalSequence sequence = new OAIGlobalSequence();

// Initialise the OAI socket with your configuration and sequence
OAISocket socket = new OAISocket(config,sequence);

// Create a thread for the socket
Thread OAIClient = new Thread(new ThreadStart(socket.Run));

// Start the socket thread
OAIClient.Start();
```
### Real Time Event Listening and additional threading 

If you would like to use real time event notification then you will need additional threads to listen for the desired events.

When utilizing additional threads for the OAI library be mindful that the application needs to be in a "ACTIVE" state which can be derived from the [OAI.Activity.OAIRunning](https://github.com/willitscale/OAI-Mitel-Inter-Tel-Library/blob/master/OAI/Activity/OAIRunning.cs).Active attribute.

#### Sample Call Event Listening Thread
```csharp
using System;
using System.Threading;

using OAI.Activity;
using OAI.Queues.Changes;
using OAI.Queues;

namespace OAI.Threads
{
    public class OAICallListener
    {
        public event EventHandler<string> CallEvent;

        public void Run()
        {
            for (;;)
            {
                // Application has stopped so kill the thread!
                if (!OAIRunning.Active)
                {
                    return;
                }

                if (!OAICallChangeQueue.Relay().Available())
                {
                    Thread.Sleep(100);
                    continue;
                }

                string evt = OAICallChangeQueue.Relay().Line;

                CallEvent(this, evt);
            }
        }
    }
}

```

#### Sample Agent Event Listening Thread
```csharp
using System;
using System.Threading;

using OAI.Activity;
using OAI.Queues.Changes;

namespace OAI.Threads
{
    public class OAIAgentListener
    {
        public event EventHandler<string> AgentEvent;

        public void Run()
        {
            for (;;)
            {
                // Application has stopped so kill the thread!
                if (!OAIRunning.Active)
                {
                    return;
                }

                if (!OAIAgentChangeQueue.Relay().Available())
                {
                    Thread.Sleep(100);
                    continue;
                }

                string evt = OAIAgentChangeQueue.Relay().Line;

                AgentEvent(this, evt);
            }
        }
    }
}
```

#### Sample Device Event Listening Thread
```csharp
using System;
using System.Threading;

using OAI.Activity;
using OAI.Queues.Changes;

namespace OAI.Threads
{
    public class OAIDeviceListener
    {
        public event EventHandler<string> DeviceEvent;

        public void Run()
        {
            for (;;)
            {
                // Application has stopped so kill the thread!
                if (!OAIRunning.Active)
                {
                    return;
                }

                if (!OAIDeviceChangeQueue.Relay().Available())
                {
                    Thread.Sleep(100);
                    continue;
                }

                string evt = OAIDeviceChangeQueue.Relay().Line;

                DeviceEvent(this, evt);
            }
        }
    }
}
```


#### Sample Debugging Event Listening Thread

You can access the debugging of the library which gets written to the Debugger Queue. As a side note this debugger queue is never flushed and I will in the future add a debug flag to the config to prevent this causing a memory leak for applications that never poll the queue.

```csharp
using System;
using System.Threading;

using OAI.Activity;
using OAI.Queues;

namespace OAI.Threads
{
    public class DebuggerQueueThread
    {
        public event EventHandler<string> DebuggingEvent;

        public void Run()
        {
            for (;;)
            {
                // Application has stopped so kill the thread!
                if (!OAIRunning.Active)
                {
                    return;
                }

                if (!OAIDebuggerQueue.Relay().Available())
                {
                    Thread.Sleep(100);
                    continue;
                }

                string evt = OAIDebuggerQueue.Relay().Line;

                DebuggingEvent(this, evt);
            }
        }
    }
}
```

### Accessing the data captured by the library

Data can be accessed directly by the controllers, although it is advised that you do not modify this data outside of the library as it may have unpredicted results which could cause instability in the application and phone system.

#### Controller
The controllers are what should be used by external applications in finding out information about calls, agents, devices, nodes and trunks. You are able to access a singular instance or all of the models bound to the controller in one of two ways.

##### Controller Peeking

Controller peaking is a way of accessing a model inside the controller without removing the model from the controller.

###### Sample Prerequisites
```csharp
using OAI.Controllers;
using OAI.Models;
```

###### Sample Peeking
```csharp
String AgentID = "5555";
OAIAgentModel agent = OAIAgentsController.Relay().Peek(AgentID);

if (null == agent)
{
  // Agent doesn't exist in the controller
}
else
{
  // Agent exists within the controller
}
```

##### Controller Popping

Controller popping is a way of accessing a model inside the controller and then sequentially removing that model from the controller on call.

##### Controller Pushing

You should avoid pushing to the controller at all costs as this may cause unexpected results.

