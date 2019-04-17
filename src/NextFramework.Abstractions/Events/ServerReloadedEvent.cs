using System;
using System.Collections.Generic;
using System.Text;

namespace NextFramework.Events
{
    public class ServerReloadedEvent : IEvent
    {
        public bool IsPropagationStopped { get; set; }
    }
}
