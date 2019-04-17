using System;
using System.Collections.Generic;
using System.Text;

namespace NextFramework.Events
{
    public class EntityDestroyedEvent : IEvent
    {
        public EntityDestroyedEvent(IEntity entity)
        {
            Entity = entity;
            IsPropagationStopped = false;
        }

        public IEntity Entity { get; }

        public bool IsPropagationStopped { get; set; }
    }
}
