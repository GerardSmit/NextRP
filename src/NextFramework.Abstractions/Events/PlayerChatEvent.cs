using System;
using System.Collections.Generic;
using System.Text;

namespace NextFramework.Events
{
    public class PlayerChatEvent : IEvent
    {
        public PlayerChatEvent(IPlayer player, string message)
        {
            Player = player;
            Message = message;
        }

        public IPlayer Player { get; }

        public string Message { get; }

        public bool IsPropagationStopped { get; set; }
    }
}
