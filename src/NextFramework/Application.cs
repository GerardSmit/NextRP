using System;
using System.Runtime.CompilerServices;
using System.Runtime.Loader;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NextFramework.Data.Collections;
using NextFramework.Events;
using NextFramework.Helpers;
using NextFramework.Scripting;

[assembly:RuntimeCompatibility(WrapNonExceptionThrows = true)]

namespace NextFramework
{
    public static class Application
    {
        private static IWebHost _host;

        public static IEventManager EventManager { get; internal set; }

        public static AssemblyLoadContext PluginLoadContext { get; internal set; }

        public static void Main()
        {
        }

        private static void Start(bool isReload = false)
        {
            PluginLoader.LoadSettings();

            _host = WebHost
                .CreateDefaultBuilder()
                .UseKestrel()
                .UseUrls("http://*:5006")
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                })
                .UseStartup<Startup>()
                .Build();

            Task.Run(async () =>
            {
                await _host.StartAsync();
                await EventManager.CallAsync(new ServerStartedEvent());
                if (isReload)
                {
                    await EventManager.CallAsync(new ServerReloadedEvent());
                }
                await _host.WaitForShutdownAsync();
            });
        }

        public static async Task StopAsync()
        {
            EventManager = null;

            await _host.StopAsync();
            _host.Dispose();

#if NETCOREAPP3_0
            PluginLoadContext.Unload();
            PluginLoadContext = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
#endif
        }

        public static async Task ReloadAsync()
        {
            await StopAsync();
            Start(isReload: true);
        }

        public static void Initialize(IntPtr plugin)
        {
            TickScheduler.Initialize();
            Collections.Initialize(plugin);

            Start();
        }
    }
}
