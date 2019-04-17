using System;
using System.Collections.Generic;
using System.Text;

namespace NextFramework.Events
{
    public class EntityCreatedEvent : IEvent
    {
        public EntityCreatedEvent(IEntity entity)
        {
            Entity = entity;
            IsPropagationStopped = false;
        }

        public IEntity Entity { get; }

        public bool IsPropagationStopped { get; set; }
    }
}
