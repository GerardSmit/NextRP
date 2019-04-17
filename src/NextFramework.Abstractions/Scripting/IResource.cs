using System.Threading.Tasks;

namespace NextFramework.Scripting
{
    public interface IResource
    {
        Task OnStartAsync();
        Task OnStopAsync();
    }
}
