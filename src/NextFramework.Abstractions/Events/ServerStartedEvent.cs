using System;
using System.Collections.Generic;
using System.Text;

namespace NextFramework.Events
{
    public class ServerStartedEvent : IEvent
    {
        public bool IsPropagationStopped { get; set; }
    }
}
