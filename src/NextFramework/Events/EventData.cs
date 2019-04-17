using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NextFramework.Attributes;
using NextFramework.Extensions;

namespace NextFramework.Events
{
    internal class EventData
    {
        public EventData(Type[] types, IEventListener listener, MethodInfo methodInfo, EventListenerAttribute attribute)
        {
            Types = types;
            Listener = listener;
            MethodInfo = methodInfo;
            Priority = attribute.Priority;
            IgnoreCancelled = attribute.IgnoreCancelled;
            Method = methodInfo.GetFriendlyName(showParameters: false);
            IsTask = methodInfo.ReturnParameter.ParameterType.IsTask();
        }

        /// <summary>
        ///     The types that the event is listening to.
        /// </summary>
        public Type[] Types { get; }

        /// <summary>
        ///     The event listener.
        /// </summary>
        public IEventListener Listener { get; }

        /// <summary>
        ///     The method.
        /// </summary>
        public MethodInfo MethodInfo { get; }

        /// <summary>
        ///     The priority of the event listener.
        /// </summary>
        public EventPriority Priority { get; }

        /// <summary>
        ///     If set to true, the listener will be called regardless if the event was cancelled.
        /// </summary>
        public bool IgnoreCancelled { get; }

        /// <summary>
        ///     The method name.
        /// </summary>
        public string Method { get; }

        /// <summary>
        ///     True if the method returns a task.
        /// </summary>
        public bool IsTask { get; }

        private static ConcurrentDictionary<Type, EventData[]> _eventDataCache = new ConcurrentDictionary<Type, EventData[]>();

        public static IEnumerable<EventData> FromInstance(IEventListener instance)
        {
            bool IsValidMethod(MethodInfo m)
            {
                // If the method has the EventListener attribute it's always an event.
                if (m.GetCustomAttributes(typeof(EventListenerAttribute), false).Any())
                {
                    return true;
                }

                // If the first parameter is the type IEvent register it as well.
                var parameter = m.GetParameters().FirstOrDefault();
                if (parameter != null && typeof(IEvent).IsAssignableFrom(parameter.ParameterType) && parameter.ParameterType.IsClass)
                {
                    return true;
                }

                return false;
            }

            return _eventDataCache.GetOrAdd(instance.GetType(),
                type => type.GetMethods(BindingFlags.Instance | BindingFlags.Public)
                    .Where(IsValidMethod)
                    .Select(m => FromMethod(instance, m))
                    .ToArray());
        }

        private static EventData FromMethod(IEventListener instance, MethodInfo methodType)
        {
            // Get the return type.
            var returnType = methodType.ReturnType;

            if (returnType != typeof(void) && !returnType.IsTask())
            {
                throw new InvalidOperationException($"The method {methodType.GetFriendlyName()} does not return void or Task.");
            }

            var parameter = methodType.GetParameters().FirstOrDefault();

            if (parameter == null || !typeof(IEvent).IsAssignableFrom(parameter.ParameterType))
            {
                throw new InvalidOperationException($"The first parameter of the method {methodType.GetFriendlyName()} should be the type {nameof(IEvent)}.");
            }

            // Register the event.
            var attribute = methodType.GetCustomAttribute<EventListenerAttribute>(false) ?? new EventListenerAttribute();
            var eventTypes = attribute.Events.Length == 0
                ? new[] { methodType.GetParameters()[0].ParameterType }
                : attribute.Events;

            return new EventData(eventTypes, instance, methodType, attribute);
        }
    }
}
