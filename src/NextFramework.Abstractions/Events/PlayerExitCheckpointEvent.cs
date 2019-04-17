using System;
using System.Collections.Generic;
using System.Text;

namespace NextFramework.Events
{
    public class PlayerExitCheckpointEvent : IEvent
    {
        public PlayerExitCheckpointEvent(IPlayer player, ICheckpoint checkpoint)
        {
            Player = player;
            Checkpoint = checkpoint;
        }

        public IPlayer Player { get; }

        public ICheckpoint Checkpoint { get; }

        public bool IsPropagationStopped { get; set; }
    }
}
