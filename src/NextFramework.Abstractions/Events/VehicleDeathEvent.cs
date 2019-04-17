using System;
using System.Collections.Generic;
using System.Text;
using NextFramework.Enums;

namespace NextFramework.Events
{
    public class VehicleDeathEvent : IEvent
    {
        public VehicleDeathEvent(IVehicle vehicle, IPlayer killer, DeathReason reason)
        {
            Vehicle = vehicle;
            Killer = killer;
            Reason = reason;
        }

        public IVehicle Vehicle { get; }

        public IPlayer Killer { get; }

        public DeathReason Reason { get; }

        public bool IsPropagationStopped { get; set; }
    }
}
