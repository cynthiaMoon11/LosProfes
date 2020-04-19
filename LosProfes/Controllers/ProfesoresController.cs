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
    public class ProfesoresController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Usuario> _userManager;

        public ProfesoresController(ApplicationDbContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Profesors
        public async Task<IActionResult> Index()
        {
            Usuario usuario = await _userManager.GetUserAsync(User);
            if (usuario == null || User.IsInRole("admin"))
            {
                var applicationDbContext = _context.Profesor
               .Include(p => p.Genero)
               .Include(p => p.Usuario)
               .Include(pf => pf.ProfesoresFormaciones)
               .ThenInclude(f => f.Formacion)
               .Include(pi => pi.ProfesoresIdiomas)
               .ThenInclude(i => i.Idioma)
               .Include(pm => pm.ProfesoresMaterias)
               .ThenInclude(m => m.Materia);
                return View(await applicationDbContext.ToListAsync());
            }
           
            else
            {
                var applicationDbContext = _context.Profesor
                    .Where(e => e.UsuarioId == usuario.Id)
                    .Include(p => p.Genero)
                    .Include(p => p.Usuario)
                    .Include(pf => pf.ProfesoresFormaciones)
                    .ThenInclude(f => f.Formacion)
                    .Include(pi => pi.ProfesoresIdiomas)
                    .ThenInclude(i => i.Idioma)
                    .Include(pm => pm.ProfesoresMaterias)
                    .ThenInclude(m => m.Materia);
                return View(await applicationDbContext.ToListAsync());
            }
        }

        // GET: Profesors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesor = await _context.Profesor
               .Include(p => p.Genero)
                .Include(p => p.Usuario)
                .Include(pf => pf.ProfesoresFormaciones)
                .ThenInclude(f => f.Formacion)
                .Include(pi => pi.ProfesoresIdiomas)
                .ThenInclude(i => i.Idioma)
                .Include(pm => pm.ProfesoresMaterias)
                .ThenInclude(m => m.Materia)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profesor == null)
            {
                return NotFound();
            }

            return View(profesor);
        }

        // GET: Profesors/Create
        public IActionResult Create()
        {
            ViewData["ProfesoresMateriasId"] = new SelectList(_context.Set<Materia>(), "Id", "Nombre");
            ViewData["ProfesoresIdiomasId"] = new SelectList(_context.Set<Idioma>(), "Id", "Nombre");
            ViewData["ProfesoresFormacionesId"] = new SelectList(_context.Set<Formacion>(), "Id", "Nombre");
            ViewData["GeneroId"] = new SelectList(_context.Set<Genero>(), "Id", "Nombre");
            return View();
        }

        // POST: Profesors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GeneroId,Nombre,Apellidos,Imagen")] Profesor profesor, 
            List<int> formaciones, List<int> idiomas, List<int> materias)
        {
            Usuario usuario = await _userManager.GetUserAsync(User);
            profesor.UsuarioId = usuario.Id;

            profesor.ProfesoresFormaciones = new List<ProfesorFormacion>();
            profesor.ProfesoresIdiomas = new List<ProfesorIdioma>();
            profesor.ProfesoresMaterias = new List<ProfesorMateria>();

            foreach (int idIdioma in idiomas)
            {
                ProfesorIdioma profesorIdioma = new ProfesorIdioma
                {
                    ProfesorId = profesor.Id,
                    IdiomaId = idIdioma
                };
                profesor.ProfesoresIdiomas.Add(profesorIdioma);
            }

            foreach (int idFormacion in formaciones)
            {
                ProfesorFormacion profesorFormacion = new ProfesorFormacion
                {
                    ProfesorId = profesor.Id,
                    FormacionId = idFormacion
                };
                profesor.ProfesoresFormaciones.Add(profesorFormacion);
            }

            foreach (int idMateria in materias)
            {
                ProfesorMateria profesorMateria = new ProfesorMateria
                {
                    ProfesorId = profesor.Id,
                    MateriaId = idMateria
                };
                profesor.ProfesoresMaterias.Add(profesorMateria);
            }

            if (ModelState.IsValid)
            {
                _context.Add(profesor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProfesoresMateriasId"] = new SelectList(_context.Set<Materia>(), "Id", "Nombre", profesor.ProfesoresMaterias );
            ViewData["ProfesoresIdiomasId"] = new SelectList(_context.Set<Idioma>(), "Id", "Nombre", profesor.ProfesoresIdiomas);
            ViewData["ProfesoresFormacionesId"] = new SelectList(_context.Set<Formacion>(), "Id", "Nombre", profesor.ProfesoresFormaciones);
            ViewData["GeneroId"] = new SelectList(_context.Set<Genero>(), "Id", "Nombre", profesor.GeneroId);
            return View(profesor);
        }

        // GET: Profesors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesor = await _context.Profesor.FindAsync(id);
            if (profesor == null)
            {
                return NotFound();
            }
            ViewData["ProfesoresMateriasId"] = new SelectList(_context.Set<Materia>(), "Id", "Nombre", profesor.ProfesoresMaterias);
            ViewData["ProfesoresIdiomasId"] = new SelectList(_context.Set<Idioma>(), "Id", "Nombre", profesor.ProfesoresIdiomas);
            ViewData["ProfesoresFormacionesId"] = new SelectList(_context.Set<Formacion>(), "Id", "Nombre", profesor.ProfesoresFormaciones);
            ViewData["GeneroId"] = new SelectList(_context.Set<Genero>(), "Id", "Nombre", profesor.GeneroId);
            return View(profesor);
        }

        // POST: Profesors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GeneroId,Nombre,Apellidos,Imagen")] Profesor profesor,
            List<int> formaciones, List<int> idiomas, List<int> materias)
        {
            if (id != profesor.Id)
            {
                return NotFound();
            }

            List<ProfesorFormacion> profesorFormaciones = await _context.ProfesorFormacion
                .Where(pf => pf.ProfesorId == profesor.Id)
                .ToListAsync();

            foreach (ProfesorFormacion formacion in profesorFormaciones)
            {
                _context.Remove(formacion);
            }

            List<ProfesorIdioma> profesorIdiomas = await _context.ProfesorIdioma
               .Where(pi => pi.ProfesorId == profesor.Id)
               .ToListAsync();

            foreach (ProfesorIdioma idioma in profesorIdiomas)
            {
                _context.Remove(idioma);
            }

            Usuario usuario = await _userManager.GetUserAsync(User);
            profesor.UsuarioId = usuario.Id;

            profesor.ProfesoresFormaciones = new List<ProfesorFormacion>();

            profesor.ProfesoresIdiomas = new List<ProfesorIdioma>();


            foreach (int idIdioma in idiomas)
            {
                ProfesorIdioma profesorIdioma = new ProfesorIdioma
                {
                    ProfesorId = profesor.Id,
                    IdiomaId = idIdioma
                };
                profesor.ProfesoresIdiomas.Add(profesorIdioma);
            }

            foreach (int idFormacion in formaciones)
            {
                ProfesorFormacion profesorFormacion = new ProfesorFormacion
                {
                    ProfesorId = profesor.Id,
                    FormacionId = idFormacion
                };
                profesor.ProfesoresFormaciones.Add(profesorFormacion);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profesor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfesorExists(profesor.Id))
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
            ViewData["ProfesoresMateriasId"] = new SelectList(_context.Set<Materia>(), "Id", "Nombre", profesor.ProfesoresMaterias);
            ViewData["ProfesoresIdiomasId"] = new SelectList(_context.Set<Idioma>(), "Id", "Nombre", profesor.ProfesoresIdiomas);
            ViewData["ProfesoresFormacionesId"] = new SelectList(_context.Set<Formacion>(), "Id", "Nombre", profesor.ProfesoresFormaciones);
            ViewData["GeneroId"] = new SelectList(_context.Set<Genero>(), "Id", "Nombre", profesor.GeneroId);
            return View(profesor);
        }

        // GET: Profesors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesor = await _context.Profesor
                .Include(p => p.Genero)
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profesor == null)
            {
                return NotFound();
            }

            return View(profesor);
        }

        // POST: Profesors/Delete/5 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profesor = await _context.Profesor.FindAsync(id);
            _context.Profesor.Remove(profesor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfesorExists(int id)
        {
            return _context.Profesor.Any(e => e.Id == id);
        }
    }
}
