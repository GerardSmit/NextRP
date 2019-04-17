using System;
using System.Collections.Generic;
using System.Text;

namespace NextFramework.Events
{
    public class PlayerCommandEvent : IEvent
    {
        public PlayerCommandEvent(IPlayer player, string raw)
        {
            Player = player;
            Raw = raw;
        }

        public IPlayer Player { get; }

        public string Raw { get; }

        public bool IsPropagationStopped { get; set; }
    }
}
