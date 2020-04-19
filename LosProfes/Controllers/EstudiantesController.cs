using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LosProfes.Data;
using LosProfes.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace LosProfes.Controllers
{
    [Authorize]
    public class EstudiantesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Usuario> _userManager;

        public EstudiantesController(ApplicationDbContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Estudiantes

        public async Task<IActionResult> Index()
        {
            
            if (User.IsInRole("padre"))
            {
                Usuario usuario = await _userManager.GetUserAsync(User);
                var applicationDbContext = _context.Estudiante
                                                        .Where(e => e.UsuarioId == usuario.Id)
                                                        .Include(e => e.Colegio)
                                                        .Include(e => e.Grado)
                                                        .Include(e => e.Idioma)
                                                        .Include(e => e.Usuario);
                return View(await applicationDbContext.ToListAsync());
            }
            else if (User.IsInRole("admin"))
            {
                Usuario usuario = await _userManager.GetUserAsync(User);
                var applicationDbContext = _context.Estudiante
                                                   .Include(e => e.Colegio)
                                                   .Include(e => e.Grado)
                                                   .Include(e => e.Idioma)
                                                   .Include(e => e.Usuario);
                return View(await applicationDbContext.ToListAsync());
            }
            else
            {
                Usuario usuario = await _userManager.GetUserAsync(User);
                var applicationDbContext = _context.Estudiante
                                                   .Include(e => e.Colegio)
                                                   .Include(e => e.Grado)
                                                   .Include(e => e.Idioma)
                                                   .Include(e => e.Usuario);
                return View(await applicationDbContext.ToListAsync());
            }
        }
        // GET: Estudiantes/Details/5
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

        // GET: Estudiantes/Create
        public IActionResult Create()
        {
            ViewData["ColegioId"] = new SelectList(_context.Set<Colegio>(), "Id", "Nombre");
            ViewData["GradoId"] = new SelectList(_context.Set<Grado>(), "Id", "Anio");
            ViewData["IdiomaId"] = new SelectList(_context.Set<Idioma>(), "Id", "Nombre");
            return View();
        }

        // POST: Estudiantes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UsuarioId,NombreEstudiante,FechaNacimiento,ColegioId,IdiomaId,GradoId")] Estudiante estudiante)
        {
            Usuario usuario = await _userManager.GetUserAsync(User);
            estudiante.UsuarioId = usuario.Id;

            if (ModelState.IsValid)
            {
                _context.Add(estudiante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ColegioId"] = new SelectList(_context.Set<Colegio>(), "Id", "Nombre", estudiante.ColegioId);
            ViewData["GradoId"] = new SelectList(_context.Set<Grado>(), "Id", "Anio", estudiante.GradoId);
            ViewData["IdiomaId"] = new SelectList(_context.Set<Idioma>(), "Id", "Nombre", estudiante.IdiomaId);
            return View(estudiante);
        }

        // GET: Estudiantes/Edit/5
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
            ViewData["ColegioId"] = new SelectList(_context.Set<Colegio>(), "Id", "Nombre", estudiante.ColegioId);
            ViewData["GradoId"] = new SelectList(_context.Set<Grado>(), "Id", "Anio", estudiante.GradoId);
            ViewData["IdiomaId"] = new SelectList(_context.Set<Idioma>(), "Id", "Nombre", estudiante.IdiomaId);

            return View(estudiante);
        }

        // POST: Estudiantes/Edit/5
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
            ViewData["ColegioId"] = new SelectList(_context.Set<Colegio>(), "Id", "Nombre", estudiante.ColegioId);
            ViewData["GradoId"] = new SelectList(_context.Set<Grado>(), "Id", "Anio", estudiante.GradoId);
            ViewData["IdiomaId"] = new SelectList(_context.Set<Idioma>(), "Id", "Nombre", estudiante.IdiomaId);

            return View(estudiante);
        }

        // GET: Estudiantes/Delete/5
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

        // POST: Estudiantes/Delete/5
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
