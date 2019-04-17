using System;
using System.Collections.Generic;
using System.Text;

namespace NextFramework.Events
{
    public class PlayerDamageEvent : IEvent
    {
        public PlayerDamageEvent(IPlayer player, float healthLoss, float armorLoss)
        {
            Player = player;
            HealthLoss = healthLoss;
            ArmorLoss = armorLoss;
        }

        public IPlayer Player { get; }

        public float HealthLoss { get; }

        public float ArmorLoss { get; }

        public bool IsPropagationStopped { get; set; }
    }
}
