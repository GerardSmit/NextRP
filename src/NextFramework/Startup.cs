using System.Runtime.Loader;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NextFramework.Data.Collections;
using NextFramework.Events;
using NextFramework.Rpc;
using NextFramework.Scripting;
using NextFramework.Scripting.ScriptingClasses;
#if NETCOREAPP3_0
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IWebHostEnvironment;
#endif

namespace NextFramework
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .AddApplicationPart(typeof(Startup).Assembly);

            services.AddSingleton<IEventManager, EventManager>();

            services.AddSingleton<IPlayerCollection>(Collections.PlayerCollection);
            services.AddSingleton<IVehicleCollection>(Collections.VehicleCollection);
            services.AddSingleton<IBlipCollection>(Collections.BlipCollection);
            services.AddSingleton<ICheckpointCollection>(Collections.CheckpointCollection);
            services.AddSingleton<IColshapeCollection>(Collections.ColshapeCollection);
            services.AddSingleton<IMarkerCollection>(Collections.MarkerCollection);
            services.AddSingleton<IObjectCollection>(Collections.ObjectCollection);
            services.AddSingleton<ITextLabelCollection>(Collections.TextLabelCollection);
            services.AddSingleton<IWorld>(Collections.World);
            services.AddSingleton<IConfig>(Collections.Config);

            services.AddScoped<IEventListener, RpcEventHandler>();

            services.AddHostedService<EventScripting>();
            services.AddSingleton<IApplication, ApplicationWrapper>();
            services.AddSingleton<AssemblyLoadContext>(PluginLoader.CreateAssemblyLoadContext(services));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IEventManager eventManager, AssemblyLoadContext assemblyLoadContext)
        {
            Application.EventManager = eventManager;
            Application.PluginLoadContext = assemblyLoadContext;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseWebSockets();
            app.UseMvc();
        }
    }
}
