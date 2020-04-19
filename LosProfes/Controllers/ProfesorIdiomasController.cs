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
    public class ProfesorIdiomasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfesorIdiomasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProfesorIdiomas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProfesorIdioma.Include(p => p.Idioma).Include(p => p.Profesor);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProfesorIdiomas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesorIdioma = await _context.ProfesorIdioma
                .Include(p => p.Idioma)
                .Include(p => p.Profesor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profesorIdioma == null)
            {
                return NotFound();
            }

            return View(profesorIdioma);
        }

        // GET: ProfesorIdiomas/Create
        public IActionResult Create()
        {
            ViewData["IdiomaId"] = new SelectList(_context.Idioma, "Id", "Id");
            ViewData["ProfesorId"] = new SelectList(_context.Profesor, "Id", "Id");
            return View();
        }

        // POST: ProfesorIdiomas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProfesorId,IdiomaId")] ProfesorIdioma profesorIdioma)
        {
            if (ModelState.IsValid)
            {
                _context.Add(profesorIdioma);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdiomaId"] = new SelectList(_context.Idioma, "Id", "Id", profesorIdioma.IdiomaId);
            ViewData["ProfesorId"] = new SelectList(_context.Profesor, "Id", "Id", profesorIdioma.ProfesorId);
            return View(profesorIdioma);
        }

        // GET: ProfesorIdiomas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesorIdioma = await _context.ProfesorIdioma.FindAsync(id);
            if (profesorIdioma == null)
            {
                return NotFound();
            }
            ViewData["IdiomaId"] = new SelectList(_context.Idioma, "Id", "Id", profesorIdioma.IdiomaId);
            ViewData["ProfesorId"] = new SelectList(_context.Profesor, "Id", "Id", profesorIdioma.ProfesorId);
            return View(profesorIdioma);
        }

        // POST: ProfesorIdiomas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProfesorId,IdiomaId")] ProfesorIdioma profesorIdioma)
        {
            if (id != profesorIdioma.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profesorIdioma);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfesorIdiomaExists(profesorIdioma.Id))
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
            ViewData["IdiomaId"] = new SelectList(_context.Idioma, "Id", "Id", profesorIdioma.IdiomaId);
            ViewData["ProfesorId"] = new SelectList(_context.Profesor, "Id", "Id", profesorIdioma.ProfesorId);
            return View(profesorIdioma);
        }

        // GET: ProfesorIdiomas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesorIdioma = await _context.ProfesorIdioma
                .Include(p => p.Idioma)
                .Include(p => p.Profesor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profesorIdioma == null)
            {
                return NotFound();
            }

            return View(profesorIdioma);
        }

        // POST: ProfesorIdiomas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profesorIdioma = await _context.ProfesorIdioma.FindAsync(id);
            _context.ProfesorIdioma.Remove(profesorIdioma);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfesorIdiomaExists(int id)
        {
            return _context.ProfesorIdioma.Any(e => e.Id == id);
        }
    }
}
