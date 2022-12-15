using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApplication6.Data;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    [Authorize]
    public class ArticuloesController : Controller
    {
        private readonly LoginRolesContext _context;

        public ArticuloesController(LoginRolesContext context)
        {
            _context = context;
        }

        // GET: Articulos
        public async Task<IActionResult> Add(int id)
        {
            List<Articulo> listaArticulos = new List<Articulo>();

            //Obtiene el articulo de BD
            Articulo? vo = await _context.Articulos.FindAsync(id);

            //Lista de articulos agregados por el usuario
            if (HttpContext.Session.GetString("listaArticulos") != null)
            {
                listaArticulos = JsonConvert.DeserializeObject<List<Articulo>>(HttpContext.Session.GetString("listaArticulos"));
            }

            listaArticulos.Add(vo);

            //Agregar el nuevo articulo
            HttpContext.Session.SetString("listaArticulos", JsonConvert.SerializeObject(listaArticulos));

            foreach (Articulo articulo in listaArticulos)
            {
                Console.WriteLine(articulo.Id);
            }

            return Redirect(Request.Headers["Referer"].ToString());
        }

        // POST: Facturas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        public async Task<IActionResult> GuardarFactura()
        {

            Factura vo = new Factura();
            vo.Cliente = "ken@campos.com";
            vo.Total = 40900;
            _context.Add(vo);

            List<Articulo> listaArticulos = JsonConvert.DeserializeObject<List<Articulo>>(HttpContext.Session.GetString("listaArticulos"));

            foreach(Articulo art in listaArticulos)
            {
                _context.Add(art);
            }
            await _context.SaveChangesAsync();
            return Redirect("~/Home/PagoHecho");
        }


        public async Task<IActionResult> SeguirComprando()
        {
            List<Articulo> listaArticulos = new List<Articulo>();
            //Agregar el nuevo articulo
            HttpContext.Session.SetString("listaArticulos", JsonConvert.SerializeObject(listaArticulos));
            return Redirect("~/Articuloes/Index");
        }


        // GET: Articuloes
        public async Task<IActionResult> Index()
        {
              return View(await _context.Articulos.ToListAsync());
        }

        // GET: Articuloes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Articulos == null)
            {
                return NotFound();
            }

            var articulo = await _context.Articulos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (articulo == null)
            {
                return NotFound();
            }

            return View(articulo);
        }

        // GET: Articuloes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Articuloes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,Imagen,Precio,CategoriaId")] Articulo articulo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(articulo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(articulo);
        }

        // GET: Articuloes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Articulos == null)
            {
                return NotFound();
            }

            var articulo = await _context.Articulos.FindAsync(id);
            if (articulo == null)
            {
                return NotFound();
            }
            return View(articulo);
        }

        // POST: Articuloes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion,Imagen,Precio,CategoriaId")] Articulo articulo)
        {
            if (id != articulo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(articulo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticuloExists(articulo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(articulo);
        }

        // GET: Articuloes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Articulos == null)
            {
                return NotFound();
            }

            var articulo = await _context.Articulos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (articulo == null)
            {
                return NotFound();
            }

            return View(articulo);
        }

        // POST: Articuloes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Articulos == null)
            {
                return Problem("Entity set 'LoginRolesContext.Articulos'  is null.");
            }
            var articulo = await _context.Articulos.FindAsync(id);
            if (articulo != null)
            {
                _context.Articulos.Remove(articulo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticuloExists(int id)
        {
          return _context.Articulos.Any(e => e.Id == id);
        }
    }
}
