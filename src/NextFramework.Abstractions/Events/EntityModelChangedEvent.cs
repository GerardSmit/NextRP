using System;
using System.Collections.Generic;
using System.Text;

namespace NextFramework.Events
{
    public class EntityModelChangedEvent : IEvent
    {
        public EntityModelChangedEvent(IEntity entity, uint oldModel)
        {
            Entity = entity;
            OldModel = oldModel;
            IsPropagationStopped = false;
        }

        public IEntity Entity { get; }

        public uint OldModel { get; }

        public bool IsPropagationStopped { get; set; }
    }
}
