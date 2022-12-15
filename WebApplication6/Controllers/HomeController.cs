using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        

        public IActionResult PagoHecho()
        {
            if (HttpContext.Session.GetString("listaArticulos") == null)
            {
                return View();
            }

            List<Articulo> listaArticulos = JsonConvert.DeserializeObject<List<Articulo>>(HttpContext.Session.GetString("listaArticulos"));
            return View(listaArticulos);
        }

        public IActionResult Pago()
        {
            List<Articulo> listaArticulos = new List<Articulo>();
            if (HttpContext.Session.GetString("listaArticulos") != null)
            {
                listaArticulos = JsonConvert.DeserializeObject<List<Articulo>>(HttpContext.Session.GetString("listaArticulos"));
            }

            return View(listaArticulos);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}