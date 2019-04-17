using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using NextFramework.Events;
using Standart.Hash.xxHash;

namespace NextFramework.Rpc
{
    public class RpcEventHandler : IEventListener
    {
        private uint a;
        private uint b;

        private static readonly Type VoidTask = typeof(Task<>).MakeGenericType(Type.GetType("System.Threading.Tasks.VoidTaskResult"));

        private readonly IServiceProvider _provider;
        private readonly RpcRegistry _rpcRegistry;

        public RpcEventHandler(RpcRegistry rpcRegistry, IServiceProvider provider)
        {
            _rpcRegistry = rpcRegistry;
            _provider = provider;

            var data = Encoding.UTF8.GetBytes("nf_init");
            a = (uint)xxHash64.ComputeHash(data, data.Length);
            data = Encoding.UTF8.GetBytes("nf_rpcCall");
            b = (uint)xxHash64.ComputeHash(data, data.Length);
        }

        private static async Task<object> Convert(Task task)
        {
            await task;

            if (VoidTask.IsInstanceOfType(task))
            {
                return null;
            }
               
            return task.GetType().GetProperty("Result", BindingFlags.Public | BindingFlags.Instance)?.GetValue(task);
        }

        public async Task OnPlayerRemote(PlayerRemoteEvent e)
        {
            if (e.EventNameHash == a)
            {
                var commands = _rpcRegistry.Commands.Values
                    .Select(c => new
                    {
                        name = c.Name
                    })
                    .ToArray();

                await e.Player.CallClientAsync("nf_rpcList", JsonConvert.SerializeObject(commands));
                return;
            }

            if (e.EventNameHash != b)
            {
                return;
            }

            var name = (string)e.Arguments[0];
            var id = (int)e.Arguments[1];
            var args = e.Arguments.Skip(2).Select(a => JsonConvert.DeserializeObject((string) a)).ToArray();

            try
            {
                if (!_rpcRegistry.Commands.TryGetValue(name, out var data))
                {
                    throw new MissingMethodException("The method " + name + " does not exists.");
                }

                var service = _provider.GetService(data.ClassType);
                var result = data.MethodInfo.Invoke(service, args);

                if (data.IsTask)
                {
                    result = await Convert((Task) result);
                }

                await e.Player.CallClientAsync("nf_rpcResult", id, 0, JsonConvert.SerializeObject(result));
            }
            catch (Exception ex)
            {
                await e.Player.CallClientAsync("nf_rpcResult", id, 1, ex.Message);
            }
        }
    }
}
