using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LosProfes.Data;
using LosProfes.Models;

namespace LosProfes.Controllers
{
    public class PreciosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PreciosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Precios
        public async Task<IActionResult> Index()
        {
            return View(await _context.Precio.ToListAsync());
        }

        // GET: Precios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var precio = await _context.Precio
                .FirstOrDefaultAsync(m => m.Id == id);
            if (precio == null)
            {
                return NotFound();
            }

            return View(precio);
        }

        // GET: Precios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Precios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Valor")] Precio precio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(precio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(precio);
        }

        // GET: Precios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var precio = await _context.Precio.FindAsync(id);
            if (precio == null)
            {
                return NotFound();
            }
            return View(precio);
        }

        // POST: Precios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Valor")] Precio precio)
        {
            if (id != precio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(precio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrecioExists(precio.Id))
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
            return View(precio);
        }

        // GET: Precios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var precio = await _context.Precio
                .FirstOrDefaultAsync(m => m.Id == id);
            if (precio == null)
            {
                return NotFound();
            }

            return View(precio);
        }

        // POST: Precios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var precio = await _context.Precio.FindAsync(id);
            _context.Precio.Remove(precio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrecioExists(int id)
        {
            return _context.Precio.Any(e => e.Id == id);
        }
    }
}
