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
    public class ProfesorFormacionesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfesorFormacionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProfesorFormaciones
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProfesorFormacion.Include(p => p.Formacion).Include(p => p.Profesor).ThenInclude(u=>u.Usuario);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProfesorFormaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesorFormacion = await _context.ProfesorFormacion
                .Include(p => p.Formacion)
                .Include(p => p.Profesor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profesorFormacion == null)
            {
                return NotFound();
            }

            return View(profesorFormacion);
        }

        // GET: ProfesorFormaciones/Create
        public IActionResult Create()
        {
            
            ViewData["FormacionId"] = new SelectList(_context.Formacion, "Id", "Nombre");
            ViewData["ProfesorId"] = new SelectList(_context.Set<Usuario>(), "Id", "NombreUsuario" );
            return View();
        }

        // POST: ProfesorFormaciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FormacionId,ProfesorId")] ProfesorFormacion profesorFormacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(profesorFormacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FormacionId"] = new SelectList(_context.Formacion, "Id", "Nombre", profesorFormacion.FormacionId);
            ViewData["ProfesorId"] = new SelectList(_context.Profesor, "Id", "NombreUsuario", profesorFormacion.ProfesorId);
            return View(profesorFormacion);
        }

        // GET: ProfesorFormaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesorFormacion = await _context.ProfesorFormacion.FindAsync(id);
            if (profesorFormacion == null)
            {
                return NotFound();
            }
            ViewData["FormacionId"] = new SelectList(_context.Formacion, "Id", "Nombre", profesorFormacion.FormacionId);
            ViewData["ProfesorId"] = new SelectList(_context.Profesor, "Id", "NombreUsuario", profesorFormacion.ProfesorId);
            return View(profesorFormacion);
        }

        // POST: ProfesorFormaciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FormacionId,ProfesorId")] ProfesorFormacion profesorFormacion)
        {
            if (id != profesorFormacion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profesorFormacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfesorFormacionExists(profesorFormacion.Id))
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
            ViewData["FormacionId"] = new SelectList(_context.Formacion, "Id", "Nombre", profesorFormacion.FormacionId);
            ViewData["ProfesorId"] = new SelectList(_context.Profesor, "Id", "NombreUsuario", profesorFormacion.ProfesorId);
            return View(profesorFormacion);
        }

        // GET: ProfesorFormaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesorFormacion = await _context.ProfesorFormacion
                .Include(p => p.Formacion)
                .Include(p => p.Profesor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profesorFormacion == null)
            {
                return NotFound();
            }

            return View(profesorFormacion);
        }

        // POST: ProfesorFormaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profesorFormacion = await _context.ProfesorFormacion.FindAsync(id);
            _context.ProfesorFormacion.Remove(profesorFormacion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfesorFormacionExists(int id)
        {
            return _context.ProfesorFormacion.Any(e => e.Id == id);
        }
    }
}
