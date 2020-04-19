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
    public class FormacionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FormacionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Formacions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Formacion.ToListAsync());
        }

        // GET: Formacions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formacion = await _context.Formacion
                .FirstOrDefaultAsync(m => m.Id == id);
            if (formacion == null)
            {
                return NotFound();
            }

            return View(formacion);
        }

        // GET: Formacions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Formacions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] Formacion formacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(formacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(formacion);
        }

        // GET: Formacions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formacion = await _context.Formacion.FindAsync(id);
            if (formacion == null)
            {
                return NotFound();
            }
            return View(formacion);
        }

        // POST: Formacions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] Formacion formacion)
        {
            if (id != formacion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(formacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormacionExists(formacion.Id))
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
            return View(formacion);
        }

        // GET: Formacions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formacion = await _context.Formacion
                .FirstOrDefaultAsync(m => m.Id == id);
            if (formacion == null)
            {
                return NotFound();
            }

            return View(formacion);
        }

        // POST: Formacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var formacion = await _context.Formacion.FindAsync(id);
            _context.Formacion.Remove(formacion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FormacionExists(int id)
        {
            return _context.Formacion.Any(e => e.Id == id);
        }
    }
}
