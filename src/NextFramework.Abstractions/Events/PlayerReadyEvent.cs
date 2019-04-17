using System;
using System.Collections.Generic;
using System.Text;

namespace NextFramework.Events
{
    public class PlayerReadyEvent : IEvent
    {
        public PlayerReadyEvent(IPlayer player)
        {
            Player = player;
        }

        public IPlayer Player { get; }

        public bool IsPropagationStopped { get; set; }
    }
}
