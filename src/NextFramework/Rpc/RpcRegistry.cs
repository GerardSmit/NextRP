using System;
using System.Collections.Generic;
using System.Text;

namespace NextFramework.Rpc
{
    public class RpcRegistry
    {
        public RpcRegistry(IReadOnlyDictionary<string, RpcData> commands)
        {
            Commands = commands;
        }

        public IReadOnlyDictionary<string, RpcData> Commands { get; }
    }
}
