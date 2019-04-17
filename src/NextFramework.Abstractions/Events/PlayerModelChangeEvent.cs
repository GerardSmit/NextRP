using System;
using System.Collections.Generic;
using System.Text;
using NextFramework.Enums;

namespace NextFramework.Events
{
    public class PlayerModelChangeEvent : IEvent
    {
        public PlayerModelChangeEvent(IPlayer player, PedHash oldModel)
        {
            Player = player;
            OldModel = oldModel;
        }

        public IPlayer Player { get; }

        public PedHash OldModel { get; }

        public bool IsPropagationStopped { get; set; }
    }
}
