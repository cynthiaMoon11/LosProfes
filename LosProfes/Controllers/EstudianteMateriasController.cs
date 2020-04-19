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
    public class EstudianteMateriasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EstudianteMateriasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EstudianteMaterias
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.EstudianteMateria.Include(e => e.Estudiante).Include(e => e.Materia);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: EstudianteMaterias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudianteMateria = await _context.EstudianteMateria
                .Include(e => e.Estudiante)
                .Include(e => e.Materia)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estudianteMateria == null)
            {
                return NotFound();
            }

            return View(estudianteMateria);
        }

        // GET: EstudianteMaterias/Create
        public IActionResult Create()
        {
            ViewData["EstudianteId"] = new SelectList(_context.Estudiante, "Id", "NombreEstudiante");
            ViewData["MateriaId"] = new SelectList(_context.Set<Materia>(), "Id", "Nombre");
            return View();
        }

        // POST: EstudianteMaterias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MateriaId,EstudianteId")] EstudianteMateria estudianteMateria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estudianteMateria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EstudianteId"] = new SelectList(_context.Estudiante, "Id", "NombreEstudiante", estudianteMateria.EstudianteId);
            ViewData["MateriaId"] = new SelectList(_context.Set<Materia>(), "Id", "Nombre", estudianteMateria.MateriaId);
            return View(estudianteMateria);
        }

        // GET: EstudianteMaterias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudianteMateria = await _context.EstudianteMateria.FindAsync(id);
            if (estudianteMateria == null)
            {
                return NotFound();
            }
            ViewData["EstudianteId"] = new SelectList(_context.Estudiante, "Id", "NombreEstudiante", estudianteMateria.EstudianteId);
            ViewData["MateriaId"] = new SelectList(_context.Set<Materia>(), "Id", "Nombre", estudianteMateria.MateriaId);
            return View(estudianteMateria);
        }

        // POST: EstudianteMaterias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MateriaId,EstudianteId")] EstudianteMateria estudianteMateria)
        {
            if (id != estudianteMateria.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estudianteMateria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstudianteMateriaExists(estudianteMateria.Id))
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
            ViewData["EstudianteId"] = new SelectList(_context.Estudiante, "Id", "NombreEstudiante", estudianteMateria.EstudianteId);
            ViewData["MateriaId"] = new SelectList(_context.Set<Materia>(), "Id", "Nombre", estudianteMateria.MateriaId);
            return View(estudianteMateria);
        }

        // GET: EstudianteMaterias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudianteMateria = await _context.EstudianteMateria
                .Include(e => e.Estudiante)
                .Include(e => e.Materia)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estudianteMateria == null)
            {
                return NotFound();
            }

            return View(estudianteMateria);
        }

        // POST: EstudianteMaterias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estudianteMateria = await _context.EstudianteMateria.FindAsync(id);
            _context.EstudianteMateria.Remove(estudianteMateria);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstudianteMateriaExists(int id)
        {
            return _context.EstudianteMateria.Any(e => e.Id == id);
        }
    }
}
