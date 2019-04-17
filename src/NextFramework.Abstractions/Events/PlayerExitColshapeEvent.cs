﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NextFramework.Events
{
    public class PlayerExitColshapeEvent : IEvent
    {
        public PlayerExitColshapeEvent(IPlayer player, IColshape colshape)
        {
            Player = player;
            Colshape = colshape;
        }

        public IPlayer Player { get; }

        public IColshape Colshape { get; }

        public bool IsPropagationStopped { get; set; }
    }
}
