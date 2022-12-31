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
    public class ZakazniksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ZakazniksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Zakazniks
        public async Task<IActionResult> Index()
        {
            var data = _context.Zakaznik.Where(x => x.Aktivita == 100).Include(p => p.Projekty);

            if (User.IsInRole("Manager"))
            {
                
            }
            if (User.IsInRole("Admin"))
            {
                data = _context.Zakaznik.Include(p => p.Projekty);
            }

            return _context.Zakaznik != null ? 
                          View(await data.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Zakaznik'  is null.");
        }

        // GET: Zakazniks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Zakaznik == null)
            {
                return NotFound();
            }

            var zakaznik = await _context.Zakaznik
                .FirstOrDefaultAsync(m => m.ZakaznikId == id);
            if (zakaznik == null)
            {
                return NotFound();
            }

            return View(zakaznik);
        }

        // GET: Zakazniks/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Zakazniks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("ZakaznikId,Zkratka,Nazev")] Zakaznik zakaznik)
        {
            zakaznik.Aktivita = 100;
            if (ModelState.IsValid)
            {
                _context.Add(zakaznik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(zakaznik);
        }

        // GET: Zakazniks/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Zakaznik == null)
            {
                return NotFound();
            }

            var zakaznik = await _context.Zakaznik.FindAsync(id);
            if (zakaznik == null)
            {
                return NotFound();
            }
            return View(zakaznik);
        }

        // POST: Zakazniks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("ZakaznikId,Zkratka,Nazev,Aktivita")] Zakaznik zakaznik)
        {
            if (id != zakaznik.ZakaznikId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zakaznik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZakaznikExists(zakaznik.ZakaznikId))
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
            return View(zakaznik);
        }

        // GET: Zakazniks/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Zakaznik == null)
            {
                return NotFound();
            }

            var zakaznik = await _context.Zakaznik
                .FirstOrDefaultAsync(m => m.ZakaznikId == id);
            if (zakaznik == null)
            {
                return NotFound();
            }

            return View(zakaznik);
        }

        // POST: Zakazniks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Zakaznik == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Zakaznik'  is null.");
            }
            var zakaznik = await _context.Zakaznik.FindAsync(id);
            if (zakaznik != null)
            {
                zakaznik.Aktivita = 100;
                _context.Update(zakaznik);
                //_context.Zakaznik.Remove(zakaznik);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZakaznikExists(int id)
        {
          return (_context.Zakaznik?.Any(e => e.ZakaznikId == id)).GetValueOrDefault();
        }
    }
}
