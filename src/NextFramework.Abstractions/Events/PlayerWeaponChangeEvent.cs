using System;
using System.Collections.Generic;
using System.Text;

namespace NextFramework.Events
{
    public class PlayerWeaponChangeEvent : IEvent
    {
        public PlayerWeaponChangeEvent(IPlayer player, uint oldWeapon, uint newWeapon)
        {
            Player = player;
            OldWeapon = oldWeapon;
            NewWeapon = newWeapon;
        }

        public IPlayer Player { get; }

        public uint OldWeapon { get; }

        public uint NewWeapon { get; }

        public bool IsPropagationStopped { get; set; }
    }
}
