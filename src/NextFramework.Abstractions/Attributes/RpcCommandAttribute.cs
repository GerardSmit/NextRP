using System;
using System.Collections.Generic;
using System.Text;

namespace NextFramework.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RpcCommandAttribute : Attribute
    {
        public RpcCommandAttribute(string name = null)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
