using System;
using System.Collections.Generic;
using System.Text;

namespace NextFramework.Events
{
    public class EntityDimensionChangeEvent : IEvent
    {
        public EntityDimensionChangeEvent(IEntity entity, uint dimension)
        {
            Entity = entity;
            Dimension = dimension;
        }

        public IEntity Entity { get; }

        public uint Dimension { get; }

        public bool IsPropagationStopped { get; set; }
    }
}
