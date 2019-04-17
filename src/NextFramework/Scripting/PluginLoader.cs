using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NextFramework.Events;
using NextFramework.Rpc;

namespace NextFramework.Scripting
{
    public class XmlException : Exception
    {
        public XmlException(string message, XElement element) : base(AddLine(message, element))
        {
            
        }

        public XmlException(string message, XElement element, Exception innerException) : base(AddLine(message, element), innerException)
        {
        }

        private static string AddLine(string message, XElement element)
        {
            if (element is IXmlLineInfo info && info.HasLineInfo())
            {
                message += " at line " + info.LineNumber + 1;
            }

            return message;
        }
    }
    
    public class AssemblyInformation
    {
        public AssemblyInformation(AssemblyName assemblyName, string path, bool autoLoad)
        {
            AssemblyName = assemblyName;
            Path = path;
            AutoLoad = autoLoad;
        }

        public AssemblyName AssemblyName { get; }

        public string Path { get; }

        public bool AutoLoad { get; }
    }

    public class PluginAssemblyLoadContext : AssemblyLoadContext
    {
        private readonly List<AssemblyInformation> _assemblies;

#if NETCOREAPP3_0
        public PluginAssemblyLoadContext(List<AssemblyInformation> assemblies) : base(isCollectible: true)
#else
        public PluginAssemblyLoadContext(List<AssemblyInformation> assemblies)
#endif
        {
            _assemblies = assemblies;
        }

        protected override Assembly Load(AssemblyName assemblyName)
        {
            var info = _assemblies.FirstOrDefault(a => a.AssemblyName.Name == assemblyName.Name);

            if (info != null)
            {
                return LoadFromInfo(info);
            }

            return null;
        }

        public Assembly LoadFromInfo(AssemblyInformation info)
        {
            using (var stream = File.Open(info.Path, FileMode.Open))
            {
                return LoadFromStream(stream);
            }
        }
    }

    public class PluginLoader
    {
        public static readonly List<AssemblyInformation> Assemblies = new List<AssemblyInformation>();

        public static void LoadSettings()
        {
            Assemblies.Clear();

            var rootFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "..");
            var settingsFilePath = Path.Combine(rootFolder, "settings.xml");
            var settings = XElement.Load(settingsFilePath);

            foreach (var setting in settings.Nodes().OfType<XElement>())
            {
                switch (setting.Name.LocalName)
                {
                    case "resource":
                    {
                        var srcPath = setting.Attributes("src").FirstOrDefault()?.Value ?? throw new XmlException("Expected attribute 'src' in the resource tag", setting);
                        var metaFilePath = Path.Combine(rootFolder, srcPath, "meta.xml");

                        if (!File.Exists(metaFilePath))
                        {
                            throw new XmlException("The meta.xml file does not exists in the resource tag", setting);
                        }

                        LoadMeta(metaFilePath, setting);
                        break;
                    }
                    default:
                        throw new XmlException($"Invalid element {setting.Name.LocalName}", setting);
                }
            }
        }

        private static void LoadMeta(string metaFilePath, XElement setting)
        {
            try
            {
                var metaRootPath = Path.GetDirectoryName(metaFilePath);
                var meta = XElement.Load(metaFilePath);

                foreach (var item in meta.Nodes().OfType<XElement>())
                {
                    switch (item.Name.LocalName)
                    {
                        case "assembly":
                        {
                            var refPath = item.Attributes("ref").FirstOrDefault()?.Value ?? throw new XmlException("Expected attribute 'ref' in the assembly tag", item);
                            var refFullPath = Path.Combine(metaRootPath, refPath);
                            var refAssembly = AssemblyName.GetAssemblyName(refFullPath);
                            var info = new AssemblyInformation(refAssembly, refFullPath, true);
                            Assemblies.Add(info);
                            break;
                        }
                        default:
                            throw new XmlException($"Invalid element {item.Name.LocalName}", setting);
                    }
                }
            }
            catch (Exception e)
            {
                throw new XmlException("Invalid meta.xml file", setting, e);
            }
        }

        public static PluginAssemblyLoadContext CreateAssemblyLoadContext(IServiceCollection services)
        {
            var context = new PluginAssemblyLoadContext(Assemblies);
            var rpcCommands = new Dictionary<string, RpcData>();

            foreach (var info in Assemblies.Where(i => i.AutoLoad))
            {
                var assembly = context.LoadFromInfo(info);

                foreach (var type in assembly.GetTypes().Where(t => typeof(IEventListener).IsAssignableFrom(t)))
                {
                    services.AddScoped(typeof(IEventListener), type);
                }

                foreach (var type in assembly.GetTypes().Where(t => typeof(IRpcListener).IsAssignableFrom(t)))
                {
                    services.AddScoped(type);

                    foreach (var rpcData in RpcData.FromType(type))
                    {
                        // TODO: Check if command already exists.
                        rpcCommands.Add(rpcData.Name, rpcData);
                    }
                }
            }

            services.AddSingleton(new RpcRegistry(new ReadOnlyDictionary<string, RpcData>(rpcCommands)));

            return context;
        }
    }
}
