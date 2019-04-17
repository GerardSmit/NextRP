using System;
using System.Collections.Generic;
using System.Text;

namespace NextFramework.Events
{
    public class VehicleTrailerAttachedEvent : IEvent
    {
        public VehicleTrailerAttachedEvent(IVehicle vehicle, IVehicle trailer)
        {
            Vehicle = vehicle;
            Trailer = trailer;
        }

        public IVehicle Vehicle { get; }

        public IVehicle Trailer { get; }

        public bool IsPropagationStopped { get; set; }
    }
}
