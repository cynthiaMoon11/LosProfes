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
    public class AdminEstudiantesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminEstudiantesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdminEstudiantes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Estudiante.Include(e => e.Colegio).Include(e => e.Grado).Include(e => e.Idioma).Include(e => e.Usuario);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AdminEstudiantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudiante = await _context.Estudiante
                .Include(e => e.Colegio)
                .Include(e => e.Grado)
                .Include(e => e.Idioma)
                .Include(e => e.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estudiante == null)
            {
                return NotFound();
            }

            return View(estudiante);
        }

        // GET: AdminEstudiantes/Create
        public IActionResult Create()
        {
            ViewData["ColegioId"] = new SelectList(_context.Colegio, "Id", "Id");
            ViewData["GradoId"] = new SelectList(_context.Grado, "Id", "Id");
            ViewData["IdiomaId"] = new SelectList(_context.Idioma, "Id", "Id");
            ViewData["UsuarioId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: AdminEstudiantes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UsuarioId,NombreEstudiante,FechaNacimiento,ColegioId,IdiomaId,GradoId")] Estudiante estudiante)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estudiante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ColegioId"] = new SelectList(_context.Colegio, "Id", "Id", estudiante.ColegioId);
            ViewData["GradoId"] = new SelectList(_context.Grado, "Id", "Id", estudiante.GradoId);
            ViewData["IdiomaId"] = new SelectList(_context.Idioma, "Id", "Id", estudiante.IdiomaId);
            ViewData["UsuarioId"] = new SelectList(_context.Users, "Id", "Id", estudiante.UsuarioId);
            return View(estudiante);
        }

        // GET: AdminEstudiantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudiante = await _context.Estudiante.FindAsync(id);
            if (estudiante == null)
            {
                return NotFound();
            }
            ViewData["ColegioId"] = new SelectList(_context.Colegio, "Id", "Id", estudiante.ColegioId);
            ViewData["GradoId"] = new SelectList(_context.Grado, "Id", "Id", estudiante.GradoId);
            ViewData["IdiomaId"] = new SelectList(_context.Idioma, "Id", "Id", estudiante.IdiomaId);
            ViewData["UsuarioId"] = new SelectList(_context.Users, "Id", "Id", estudiante.UsuarioId);
            return View(estudiante);
        }

        // POST: AdminEstudiantes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UsuarioId,NombreEstudiante,FechaNacimiento,ColegioId,IdiomaId,GradoId")] Estudiante estudiante)
        {
            if (id != estudiante.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estudiante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstudianteExists(estudiante.Id))
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
            ViewData["ColegioId"] = new SelectList(_context.Colegio, "Id", "Id", estudiante.ColegioId);
            ViewData["GradoId"] = new SelectList(_context.Grado, "Id", "Id", estudiante.GradoId);
            ViewData["IdiomaId"] = new SelectList(_context.Idioma, "Id", "Id", estudiante.IdiomaId);
            ViewData["UsuarioId"] = new SelectList(_context.Users, "Id", "Id", estudiante.UsuarioId);
            return View(estudiante);
        }

        // GET: AdminEstudiantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudiante = await _context.Estudiante
                .Include(e => e.Colegio)
                .Include(e => e.Grado)
                .Include(e => e.Idioma)
                .Include(e => e.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estudiante == null)
            {
                return NotFound();
            }

            return View(estudiante);
        }

        // POST: AdminEstudiantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estudiante = await _context.Estudiante.FindAsync(id);
            _context.Estudiante.Remove(estudiante);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstudianteExists(int id)
        {
            return _context.Estudiante.Any(e => e.Id == id);
        }
    }
}
