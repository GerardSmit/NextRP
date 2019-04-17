using System;
using System.Threading.Tasks;
using NextFramework.Attributes;

namespace NextFramework.Events
{
    public interface IEventManager
    {
        /// <summary>
        ///     Call all the event listeners for the type <see cref="TEvent"/>.
        /// </summary>
        /// <param name="eventBase">The event argument.</param>
        /// <param name="provider">The service provider.</param>
        Task<TEvent> CallAsync<TEvent>(TEvent eventBase, IServiceProvider provider) where TEvent : IEvent;

        /// <summary>
        ///     Call all the event listeners for the type <see cref="TEvent"/>.
        /// </summary>
        /// <param name="eventBase">The event argument.</param>
        Task<TEvent> CallAsync<TEvent>(TEvent eventBase) where TEvent : IEvent;
    }
}
