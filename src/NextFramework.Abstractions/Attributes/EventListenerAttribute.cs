using System;
using NextFramework.Events;

namespace NextFramework.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class EventListenerAttribute : Attribute
    {
        public EventListenerAttribute(EventPriority priority = EventPriority.Normal)
        {
            Priority = priority;
            Events = new Type[0];
        }

        /// <summary>
        ///     The <see cref="Priority"/> of the event listener.
        /// </summary>
        public EventPriority Priority { get; set; }

        /// <summary>
        ///     The events that the listener is listening to.
        /// </summary>
        public Type[] Events { get; set; }

        /// <summary>
        ///     If set to true, the listener will be called regardless of the <see cref="IEvent.IsPropagationStopped"/>.
        /// </summary>
        public bool IgnoreCancelled { get; set; } = false;
    }
}
