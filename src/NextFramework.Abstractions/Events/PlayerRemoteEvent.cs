using System;
using System.Collections.Generic;
using System.Text;

namespace NextFramework.Events
{
    public class PlayerRemoteEvent : IEvent
    {
        public PlayerRemoteEvent(IPlayer player, uint eventNameHash, object[] arguments)
        {
            Player = player;
            Arguments = arguments;
            EventNameHash = eventNameHash;
        }

        public IPlayer Player { get; }

        public uint EventNameHash { get; }

        public object[] Arguments { get; }

        public bool IsPropagationStopped { get; set; }
    }
}
