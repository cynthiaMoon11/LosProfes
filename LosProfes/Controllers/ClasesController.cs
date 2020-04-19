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

namespace LosProfes.Controllers
{
    public class ClasesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Usuario> _userManager;

        public ClasesController(ApplicationDbContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Clases
        public async Task<IActionResult> Index()
        {
            Usuario usuario = await _userManager.GetUserAsync(User);
            if (User.IsInRole("profe"))
            {
                 var applicationDbContext = _context.Clase
                    .Where(c => c.Profesor.Usuario.Id == usuario.Id)
                    .Include(c => c.Estudiante)
                    .Include(c => c.Profesor);
                return View(await applicationDbContext.ToListAsync());
            }
            else if (User.IsInRole("admin"))
            {
                var applicationDbContext = _context.Clase                   
                   .Include(c => c.Estudiante)
                   .Include(c => c.Profesor);
                return View(await applicationDbContext.ToListAsync());
            }
            else
            {                
                 var applicationDbContext = _context.Clase
                    .Where(c => c.Estudiante.Usuario.Id == usuario.Id)
                    .Include(c => c.Estudiante)
                    .Include(c => c.Profesor);
                return View(await applicationDbContext.ToListAsync());
            }
            
        }

        // GET: Clases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clase = await _context.Clase
                .Include(c => c.Estudiante)
                .Include(c => c.Profesor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clase == null)
            {
                return NotFound();
            }

            return View(clase);
        }

        // GET: Clases/Create
        public IActionResult Create()
        {           
            ViewData["EstudianteId"] = new SelectList(_context.Estudiante, "Id", "NombreEstudiante");
            ViewData["ProfesorId"] = new SelectList(_context.Profesor, "Id", "Nombre");
            return View();
        }

        // POST: Clases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProfesorId,EstudianteId,FechaClase")] Clase clase, int estudiante, int profesor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EstudianteId"] = new SelectList(_context.Estudiante, "Id", "NombreEstudiante", clase.EstudianteId);
            ViewData["ProfesorId"] = new SelectList(_context.Profesor, "Id", "Nombre", clase.ProfesorId);
            return View(clase);
        }

        // GET: Clases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clase = await _context.Clase.FindAsync(id);
            if (clase == null)
            {
                return NotFound();
            }
            ViewData["EstudianteId"] = new SelectList(_context.Estudiante, "Id", "NombreEstudiante", clase.EstudianteId);
            ViewData["ProfesorId"] = new SelectList(_context.Profesor, "Id", "Nombre", clase.ProfesorId);
            return View(clase);
        }

        // POST: Clases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProfesorId,EstudianteId,FechaClase")] Clase clase)
        {
            if (id != clase.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClaseExists(clase.Id))
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
            ViewData["EstudianteId"] = new SelectList(_context.Estudiante, "Id", "NombreEstudiante", clase.EstudianteId);
            ViewData["ProfesorId"] = new SelectList(_context.Profesor, "Id", "Nombre", clase.ProfesorId);
            return View(clase);
        }

        // GET: Clases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clase = await _context.Clase
                .Include(c => c.Estudiante)
                .Include(c => c.Profesor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clase == null)
            {
                return NotFound();
            }

            return View(clase);
        }

        // POST: Clases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clase = await _context.Clase.FindAsync(id);
            _context.Clase.Remove(clase);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClaseExists(int id)
        {
            return _context.Clase.Any(e => e.Id == id);
        }
    }
}
