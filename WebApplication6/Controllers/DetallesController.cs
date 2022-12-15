using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication6.Data;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class DetallesController : Controller
    {
        private readonly LoginRolesContext _context;

        public DetallesController(LoginRolesContext context)
        {
            _context = context;
        }

        // GET: Detalles
        public async Task<IActionResult> Index()
        {
            var loginRolesContext = _context.Detalles.Include(d => d.Articulo).Include(d => d.Factura);
            return View(await loginRolesContext.ToListAsync());
        }

        // GET: Detalles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Detalles == null)
            {
                return NotFound();
            }

            var detalle = await _context.Detalles
                .Include(d => d.Articulo)
                .Include(d => d.Factura)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detalle == null)
            {
                return NotFound();
            }

            return View(detalle);
        }

        // GET: Detalles/Create
        public IActionResult Create()
        {
            ViewData["ArticuloId"] = new SelectList(_context.Articulos, "Id", "Id");
            ViewData["FacturaId"] = new SelectList(_context.Facturas, "Id", "Id");
            return View();
        }

        // POST: Detalles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FacturaId,ArticuloId")] Detalle detalle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detalle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArticuloId"] = new SelectList(_context.Articulos, "Id", "Id", detalle.ArticuloId);
            ViewData["FacturaId"] = new SelectList(_context.Facturas, "Id", "Id", detalle.FacturaId);
            return View(detalle);
        }

        // GET: Detalles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Detalles == null)
            {
                return NotFound();
            }

            var detalle = await _context.Detalles.FindAsync(id);
            if (detalle == null)
            {
                return NotFound();
            }
            ViewData["ArticuloId"] = new SelectList(_context.Articulos, "Id", "Id", detalle.ArticuloId);
            ViewData["FacturaId"] = new SelectList(_context.Facturas, "Id", "Id", detalle.FacturaId);
            return View(detalle);
        }

        // POST: Detalles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FacturaId,ArticuloId")] Detalle detalle)
        {
            if (id != detalle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalleExists(detalle.Id))
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
            ViewData["ArticuloId"] = new SelectList(_context.Articulos, "Id", "Id", detalle.ArticuloId);
            ViewData["FacturaId"] = new SelectList(_context.Facturas, "Id", "Id", detalle.FacturaId);
            return View(detalle);
        }

        // GET: Detalles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Detalles == null)
            {
                return NotFound();
            }

            var detalle = await _context.Detalles
                .Include(d => d.Articulo)
                .Include(d => d.Factura)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detalle == null)
            {
                return NotFound();
            }

            return View(detalle);
        }

        // POST: Detalles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Detalles == null)
            {
                return Problem("Entity set 'LoginRolesContext.Detalles'  is null.");
            }
            var detalle = await _context.Detalles.FindAsync(id);
            if (detalle != null)
            {
                _context.Detalles.Remove(detalle);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetalleExists(int id)
        {
          return _context.Detalles.Any(e => e.Id == id);
        }
    }
}
