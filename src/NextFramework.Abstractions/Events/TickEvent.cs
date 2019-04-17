using System;
using System.Collections.Generic;
using System.Text;

namespace NextFramework.Events
{
    public class TickEvent : IEvent
    {
        public ulong TickCount { get; set; }

        public bool IsPropagationStopped { get; set; }
    }
}
