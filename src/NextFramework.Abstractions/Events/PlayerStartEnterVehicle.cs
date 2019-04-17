using System;
using System.Collections.Generic;
using System.Text;

namespace NextFramework.Events
{
    public class PlayerStartEnterVehicle : IEvent
    {
        public PlayerStartEnterVehicle(IPlayer player, IVehicle vehicle, int seat)
        {
            Player = player;
            Vehicle = vehicle;
            Seat = seat;
        }

        public IPlayer Player { get; }

        public IVehicle Vehicle { get; }

        public int Seat { get; }

        public bool IsPropagationStopped { get; set; }
    }
}
