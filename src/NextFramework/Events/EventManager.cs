using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NextFramework.Extensions;

namespace NextFramework.Events
{
    public class EventManager : IEventManager
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<EventManager> _logger;
        private readonly EventData[] _eventDatas;

        public EventManager(IServiceProvider serviceProvider, ILogger<EventManager> logger, IEnumerable<IEventListener> listeners)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _eventDatas = listeners.SelectMany(EventData.FromInstance).ToArray();
        }

        /// <inheritdoc />
        public async Task<TEvent> CallAsync<TEvent>(TEvent eventBase, IServiceProvider services) where TEvent : IEvent
        {
            var sw = Stopwatch.StartNew();
            var eventType = typeof(TEvent);

            foreach (var eventData in _eventDatas.Where(e => e.Types.Any(t => t.IsAssignableFrom(eventType))))
            {
                if (eventBase.IsPropagationStopped && !eventData.IgnoreCancelled)
                {
                    continue;
                }

                // Add the parameters.
                var parameters = eventData.MethodInfo.GetParameters();
                var args = new object[parameters.Length];

                args[0] = eventBase;

                for (var i = 1; i < parameters.Length; i++)
                {
                    args[i] = services.GetService(parameters[i].ParameterType);
                }

                // Execute the event.
                try
                {
                    sw.Restart();
                    var result = eventData.MethodInfo.Invoke(eventData.Listener, args);

                    if (eventData.IsTask)
                    {
                        await (Task) result;
                    }

                    if (sw.ElapsedMilliseconds > 50)
                    {
                        _logger.LogWarning($"The event handler {eventData.Method} took {sw.ElapsedMilliseconds}ms to execute.");
                    }
                }
                catch (Exception e)
                {
                    _logger.LogWarning($"An exception occured while executing the event {typeof(TEvent).GetFriendlyName()} on the handler {eventData.Method}. " + e.Message, e);
                }
            }

            return eventBase;
        }

        /// <inheritdoc />
        public async Task<T> CallAsync<T>(T eventBase) where T : IEvent
        {
            using (var scope = _serviceProvider.CreateScope()) 
            {
                return await CallAsync(eventBase, scope.ServiceProvider);
            }
        }
    }
}
