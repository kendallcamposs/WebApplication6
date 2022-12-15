using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
    {
        List<Articulo> listaArticulos = JsonConvert.DeserializeObject<List<Articulo>>(HttpContext.Session.GetString("listaArticulos"));
        return View(listaArticulos);
    }
}
}
