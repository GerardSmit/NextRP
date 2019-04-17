using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace NextFramework.Events
{
    public class PlayerCreateWaypointEvent : IEvent
    {
        public PlayerCreateWaypointEvent(IPlayer player, Vector3 position)
        {
            Player = player;
            Position = position;
        }

        public IPlayer Player { get; }

        public Vector3 Position { get; }

        public bool IsPropagationStopped { get; set; }
    }
}
