using System;
using System.Collections.Generic;
using System.Text;
using NextFramework.Enums;

namespace NextFramework.Events
{
    public class PlayerQuitEvent : IEvent
    {
        public PlayerQuitEvent(IPlayer player, string message, DisconnectReason reason)
        {
            Player = player;
            Message = message;
            Reason = reason;
        }

        public IPlayer Player { get; }

        public string Message { get; }

        public DisconnectReason Reason { get; }

        public bool IsPropagationStopped { get; set; }
    }
}
