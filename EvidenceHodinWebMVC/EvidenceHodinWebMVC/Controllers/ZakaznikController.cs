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
    public class ZakaznikController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ZakaznikController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Zakaznik
        public async Task<IActionResult> Index()
        {
              return _context.Zakaznik != null ? 
                          View(await _context.Zakaznik.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Zakaznik'  is null.");
        }

        // GET: Zakaznik/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Zakaznik == null)
            {
                return NotFound();
            }

            var zakaznik = await _context.Zakaznik
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zakaznik == null)
            {
                return NotFound();
            }

            return View(zakaznik);
        }

        // GET: Zakaznik/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Zakaznik/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Zkratka,Nazev")] Zakaznik zakaznik)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zakaznik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(zakaznik);
        }

        // GET: Zakaznik/Edit/5
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

        // POST: Zakaznik/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Zkratka,Nazev")] Zakaznik zakaznik)
        {
            if (id != zakaznik.Id)
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
                    if (!ZakaznikExists(zakaznik.Id))
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

        // GET: Zakaznik/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Zakaznik == null)
            {
                return NotFound();
            }

            var zakaznik = await _context.Zakaznik
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zakaznik == null)
            {
                return NotFound();
            }

            return View(zakaznik);
        }

        // POST: Zakaznik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Zakaznik == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Zakaznik'  is null.");
            }
            var zakaznik = await _context.Zakaznik.FindAsync(id);
            if (zakaznik != null)
            {
                _context.Zakaznik.Remove(zakaznik);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZakaznikExists(int id)
        {
          return (_context.Zakaznik?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
