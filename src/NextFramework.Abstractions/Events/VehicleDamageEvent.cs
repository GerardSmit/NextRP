using System;
using System.Collections.Generic;
using System.Text;

namespace NextFramework.Events
{
    public class VehicleDamageEvent : IEvent
    {
        public VehicleDamageEvent(IVehicle vehicle, float bodyHealthLoss, float engineHealthLoss)
        {
            Vehicle = vehicle;
            BodyHealthLoss = bodyHealthLoss;
            EngineHealthLoss = engineHealthLoss;
        }

        public IVehicle Vehicle { get; }

        public float BodyHealthLoss { get; }

        public float EngineHealthLoss { get; }

        public bool IsPropagationStopped { get; set; }
    }
}
