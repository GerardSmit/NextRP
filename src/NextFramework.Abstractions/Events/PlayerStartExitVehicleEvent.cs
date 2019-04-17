using System;
using System.Collections.Generic;
using System.Text;

namespace NextFramework.Events
{
    public class PlayerStartExitVehicleEvent : IEvent
    {
        public PlayerStartExitVehicleEvent(IPlayer player, IVehicle vehicle)
        {
            Player = player;
            Vehicle = vehicle;
        }

        public IPlayer Player { get; }

        public IVehicle Vehicle { get; }

        public bool IsPropagationStopped { get; set; }
    }
}
