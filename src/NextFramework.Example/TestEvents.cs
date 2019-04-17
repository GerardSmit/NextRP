using System;
using System.Threading.Tasks;
using NextFramework.Events;

namespace NextFramework.Example
{
    public class ServerEvents : IEventListener
    {
        public async Task OnCommand(PlayerCommandEvent e)
        {
            const string prefix = "set ";

            if (e.Raw.StartsWith(prefix))
            {
                await e.Player.CallBrowserAsync("setTitle", e.Raw.Substring(prefix.Length));
            }
        }
    }
}
