using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace NextFramework.Web
{
    [Route("/api/server")]
    public class PlayerController : Controller
    {
        [Route("reload")]
        public object Reload()
        {
            Task.Run(async () =>
            {
                await Task.Delay(250);
                await Application.ReloadAsync();
            });

            return new
            {
                Success = true,
                Message = "The server will restart in 250ms."
            };
        }
    }
}
