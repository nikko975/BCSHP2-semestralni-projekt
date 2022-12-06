using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EvidenceHodinWebMVC.Data;
using EvidenceHodinWebMVC.Models;

namespace EvidenceHodinWebMVC.Controllers
{
    public class ProjektsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjektsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Projekts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Projekt.Include(p => p.Zakaznik).OrderBy(z => z.Zakaznik.Nazev);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Projekts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Projekt == null)
            {
                return NotFound();
            }

            var projekt = await _context.Projekt
                .Include(p => p.Zakaznik)
                .FirstOrDefaultAsync(m => m.ProjektId == id);
            if (projekt == null)
            {
                return NotFound();
            }

            return View(projekt);
        }

        // GET: Projekts/Create
        public IActionResult Create()
        {
            ViewData["ZakaznikId"] = new SelectList(_context.Zakaznik, "ZakaznikId", "Nazev");
            return View();
        }

        // POST: Projekts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjektId,Nazev,MaxMinut,ZakaznikId")] Projekt projekt)
        {
            projekt.Aktivita = 100;
            if (ModelState.IsValid)
            {
                _context.Add(projekt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ZakaznikId"] = new SelectList(_context.Zakaznik, "ZakaznikId", "Nazev", projekt.ZakaznikId);
            return View(projekt);
        }

        // GET: Projekts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Projekt == null)
            {
                return NotFound();
            }

            var projekt = await _context.Projekt.FindAsync(id);
            if (projekt == null)
            {
                return NotFound();
            }
            ViewData["ZakaznikId"] = new SelectList(_context.Zakaznik, "ZakaznikId", "Nazev", projekt.ZakaznikId);
            return View(projekt);
        }

        // POST: Projekts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjektId,Nazev,MaxMinut,ZakaznikId,Aktivita")] Projekt projekt)
        {
            if (id != projekt.ProjektId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projekt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjektExists(projekt.ProjektId))
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
            ViewData["ZakaznikId"] = new SelectList(_context.Zakaznik, "ZakaznikId", "Nazev", projekt.ZakaznikId);
            return View(projekt);
        }

        // GET: Projekts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Projekt == null)
            {
                return NotFound();
            }

            var projekt = await _context.Projekt
                .Include(p => p.Zakaznik)
                .FirstOrDefaultAsync(m => m.ProjektId == id);
            if (projekt == null)
            {
                return NotFound();
            }

            return View(projekt);
        }

        // POST: Projekts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Projekt == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Projekt'  is null.");
            }
            var projekt = await _context.Projekt.FindAsync(id);
            if (projekt != null)
            {
                _context.Projekt.Remove(projekt);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // TODO nedostava to ID
        // POST: Projekts/Delete/5
        [HttpPost, ActionName("Deactivate")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeactivateConfirmed(int id)
        {
            if (_context.Projekt == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Projekt'  is null.");
            }
            var projekt = await _context.Projekt.FindAsync(id);
            if (projekt != null)
            {
                projekt.Aktivita = 900;
                _context.Projekt.Update(projekt);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjektExists(int id)
        {
          return (_context.Projekt?.Any(e => e.ProjektId == id)).GetValueOrDefault();
        }
    }
}
