using System;
using System.Collections.Generic;
using System.Text;

namespace NextFramework.Events
{
    public class PlayerStreamInEvent : IEvent
    {
        public PlayerStreamInEvent(IPlayer player, IPlayer streamedPlayer)
        {
            Player = player;
            StreamedPlayer = streamedPlayer;
        }

        public IPlayer Player { get; }

        public IPlayer StreamedPlayer { get; }

        public bool IsPropagationStopped { get; set; }
    }
}
