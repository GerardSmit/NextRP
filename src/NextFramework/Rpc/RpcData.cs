using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NextFramework.Attributes;
using NextFramework.Extensions;

namespace NextFramework.Rpc
{
    public class RpcData
    {
        public RpcData(string name, Type type, MethodInfo methodInfo)
        {
            Name = name;
            ClassType = type;
            MethodInfo = methodInfo;
            Method = methodInfo.GetFriendlyName(showParameters: false);
            IsTask = methodInfo.ReturnParameter.ParameterType.IsTask();
        }

        /// <summary>
        ///     The types that the event is listening to.
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     The RPC listener.
        /// </summary>
        public Type ClassType { get; }

        /// <summary>
        ///     The method.
        /// </summary>
        public MethodInfo MethodInfo { get; }

        /// <summary>
        ///     The method name.
        /// </summary>
        public string Method { get; }

        /// <summary>
        ///     True if the method returns a task.
        /// </summary>
        public bool IsTask { get; }

        public static IEnumerable<RpcData> FromType(Type classType)
        {
            return classType.GetMethods(BindingFlags.Instance | BindingFlags.Public)
                .Where(m => m.GetCustomAttributes(typeof(RpcCommandAttribute), false).Any())
                .Select(m => FromMethod(classType, m));
        }

        private static RpcData FromMethod(Type classType, MethodInfo methodType)
        {
            // Register the event.
            var attribute = methodType.GetCustomAttribute<RpcCommandAttribute>(false);

            return new RpcData(attribute.Name ?? methodType.Name, classType, methodType);
        }
    }
}
