using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EvidenceHodinWebMVC.Data;
using EvidenceHodinWebMVC.Models;
using System.Data;
using Microsoft.AspNetCore.Authorization;

namespace EvidenceHodinWebMVC.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    public class CinnostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CinnostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cinnosts
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Admin"))
            {              
                return _context.Cinnost != null ?
                          View(await _context.Cinnost.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Cinnost'  is null.");
            }
             return _context.Cinnost != null ? 
                          View(await _context.Cinnost.Where(x => x.Aktivita == 100).ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Cinnost'  is null.");
        }

        // GET: Cinnosts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cinnost == null)
            {
                return NotFound();
            }

            var cinnost = await _context.Cinnost
                .FirstOrDefaultAsync(m => m.CinnostId == id);
            if (cinnost == null)
            {
                return NotFound();
            }

            return View(cinnost);
        }

        // GET: Cinnosts/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cinnosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("CinnostId,Nazev")] Cinnost cinnost)
        {
            cinnost.Aktivita = 100;
            if (ModelState.IsValid)
            {
                _context.Add(cinnost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cinnost);
        }

        // GET: Cinnosts/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cinnost == null)
            {
                return NotFound();
            }

            var cinnost = await _context.Cinnost.FindAsync(id);
            if (cinnost == null)
            {
                return NotFound();
            }
            return View(cinnost);
        }

        // POST: Cinnosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("CinnostId,Nazev,Aktivita")] Cinnost cinnost)
        {
            if (id != cinnost.CinnostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cinnost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CinnostExists(cinnost.CinnostId))
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
            return View(cinnost);
        }

        // GET: Cinnosts/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cinnost == null)
            {
                return NotFound();
            }

            var cinnost = await _context.Cinnost
                .FirstOrDefaultAsync(m => m.CinnostId == id);
            if (cinnost == null)
            {
                return NotFound();
            }

            return View(cinnost);
        }

        // POST: Cinnosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cinnost == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Cinnost'  is null.");
            }
            var cinnost = await _context.Cinnost.FindAsync(id);
            if (cinnost != null)
            {
                cinnost.Aktivita = 900;
                _context.Update(cinnost);
                //_context.Cinnost.Remove(cinnost);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CinnostExists(int id)
        {
          return (_context.Cinnost?.Any(e => e.CinnostId == id)).GetValueOrDefault();
        }
    }
}
