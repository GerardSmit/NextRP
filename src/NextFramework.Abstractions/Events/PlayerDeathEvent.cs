using System;
using System.Collections.Generic;
using System.Text;
using NextFramework.Enums;

namespace NextFramework.Events
{
    public class PlayerDeathEvent : IEvent
    {
        public PlayerDeathEvent(IPlayer player, IPlayer killer, DeathReason reason)
        {
            Player = player;
            Killer = killer;
            Reason = reason;
        }

        public IPlayer Player { get; }

        public IPlayer Killer { get; }

        public DeathReason Reason { get; }

        public bool IsPropagationStopped { get; set; }
    }
}
