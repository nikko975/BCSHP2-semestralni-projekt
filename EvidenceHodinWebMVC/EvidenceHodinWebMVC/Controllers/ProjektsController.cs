using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EvidenceHodinWebMVC.Data;
using EvidenceHodinWebMVC.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace EvidenceHodinWebMVC.Controllers
{
    [Authorize(Roles = "Admin, Manager, Uzivatel")]
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
            var applicationDbContext = _context.Projekt.Where(x => x.Aktivita == 100).Include(p => p.Zakaznik);

            if (User.IsInRole("Admin"))
            {
                applicationDbContext = _context.Projekt.Include(p => p.Zakaznik);
            }
            
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Projekts/Details/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Projekt == null)
            {
                return NotFound();
            }

            //var projekt = await _context.Projekt.FindAsync(id);
            var projekt = await _context.Projekt.Include(i => i.CinnostVazba).ThenInclude(i => i.Cinnost)
                .AsNoTracking().FirstOrDefaultAsync(m => m.ProjektId == id);
            if (projekt == null)
            {
                return NotFound();
            }
            PopulateAssignedTasks(projekt);
            ViewData["ZakaznikId"] = new SelectList(_context.Zakaznik, "ZakaznikId", "Nazev", projekt.ZakaznikId);
            return View(projekt);
        }

        private void PopulateAssignedTasks(Projekt projekt)
        {
            var allTasks = _context.Cinnost;
            var projektCinnost = new HashSet<int>(projekt.CinnostVazba.Select(c => c.CinnostId));
            var viewModel = new List<PraceCinnostData>();
            foreach (var task in allTasks)
            {
                if (task.Aktivita == 100)
                {
                    viewModel.Add(new PraceCinnostData
                    {
                        CinnostId = task.CinnostId,
                        Nazev = task.Nazev,
                        Assigned = projektCinnost.Contains(task.CinnostId)
                    });
                }
                
            }
            ViewData["Cinnosti"] = viewModel;

        }

        // POST: Projekts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("ProjektId,Nazev,MaxMinut,ZakaznikId,Aktivita")] Projekt projekt, string[] vybraneCinnosti)
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

                    var projektN = await _context.Projekt.Include(i => i.CinnostVazba).ThenInclude(i => i.Cinnost).AsNoTracking().FirstOrDefaultAsync(m => m.ProjektId == id);
                    UpdateVazbaProjektCinnost(projektN, vybraneCinnosti);
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

        private void UpdateVazbaProjektCinnost(Projekt projekt, string[] vybraneCinnosti)
        {
            if (vybraneCinnosti == null)
            {
                projekt.CinnostVazba = new List<ProjektCinnostVazba>();
                return;
            }
            var vybraneCinnostiHS = new HashSet<string>(vybraneCinnosti);
            var projektoveCinnosti = new HashSet<int>(projekt.CinnostVazba.Select(c => c.Cinnost.CinnostId));

            foreach (var cinnost in _context.Cinnost)
            {
                if (vybraneCinnostiHS.Contains(cinnost.CinnostId.ToString()))
                {
                    if (!projektoveCinnosti.Contains(cinnost.CinnostId))
                    {
                        //projekt.CinnostVazba.Add(new ProjektCinnostVazba { ProjektId = projekt.ProjektId, CinnostId = cinnost.CinnostId });
                        _context.Add(new ProjektCinnostVazba { ProjektId = projekt.ProjektId, CinnostId = cinnost.CinnostId });
                    }
                }
                else
                {

                    if (projektoveCinnosti.Contains(cinnost.CinnostId))
                    {
                        ProjektCinnostVazba cinnostToRemove = projekt.CinnostVazba.FirstOrDefault(i => i.CinnostId == cinnost.CinnostId);
                        _context.Remove(cinnostToRemove);
                    }
                }
            }

        }

        // GET: Projekts/Delete/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Projekt == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Projekt'  is null.");
            }
            var projekt = await _context.Projekt.FindAsync(id);
            if (projekt != null)
            {
                projekt.Aktivita = 900;
                _context.Update(projekt);
                //_context.Projekt.Remove(projekt);
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
