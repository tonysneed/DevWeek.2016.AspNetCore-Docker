using System.Collections.Generic;
using System.Threading.Tasks;
using DockerComposeDemo.Models;
using DockerComposeDemo.Repositories;
using Microsoft.AspNet.Mvc;

namespace DockerComposeDemo.Controllers
{    
    public class HomeController : Controller
    {
        private readonly ICustomersRepository _customersRepo;
        
        public HomeController(ICustomersRepository customersRepo)
        {
            _customersRepo = customersRepo;
        }
        
        public async Task<IActionResult> Index()
        {
            IEnumerable<Customer> model = await _customersRepo.GetCustomers();
            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
