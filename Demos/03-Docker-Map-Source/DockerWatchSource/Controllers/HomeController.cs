using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc;

namespace DockerMapSource
{
    [Route("")]
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _env;
        
        public HomeController(IHostingEnvironment env)
        {
            _env = env;
        }
        
        [Route(""), HttpGet]
        public IActionResult Index()
        {
            return Content($"Hello ASP.NET Core on {_env.EnvironmentName} with Docker!");
        }
    }
}
