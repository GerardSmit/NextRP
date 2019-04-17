using System;
using System.Collections.Generic;
using System.Text;

namespace NextFramework.Events
{
    public class VehicleHornToggleEvent : IEvent
    {
        public VehicleHornToggleEvent(IVehicle vehicle, bool toggle)
        {
            Vehicle = vehicle;
            Toggle = toggle;
        }

        public IVehicle Vehicle { get; }

        public bool Toggle { get; }

        public bool IsPropagationStopped { get; set; }
    }
}
