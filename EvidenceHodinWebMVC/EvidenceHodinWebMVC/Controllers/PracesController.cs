using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EvidenceHodinWebMVC.Data;
using EvidenceHodinWebMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace EvidenceHodinWebMVC.Controllers
{
    [Authorize(Roles = "Admin, Manager, Member")]
    public class PracesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PracesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Praces
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var applicationDbContext = _context.Prace.Where(x => x.Aktivita == 100).Include(p => p.Cinnost).Include(p => p.Projekt).Include(p => p.Zakaznik).Include(p => p.Uzivatel).Where(x => x.Uzivatel.Id == user.Id);
            if (User.IsInRole("Manager"))
            {
                applicationDbContext = _context.Prace.Where(x => x.Aktivita == 100).Include(p => p.Cinnost).Include(p => p.Projekt).Include(p => p.Zakaznik).Include(p => p.Uzivatel);

            }
            if (User.IsInRole("Admin"))
            {
                applicationDbContext = _context.Prace.Include(p => p.Cinnost).Include(p => p.Projekt).Include(p => p.Zakaznik).Include(p => p.Uzivatel);
            }
            
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Praces/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Prace == null)
            {
                return NotFound();
            }

            var prace = await _context.Prace
                .Include(p => p.Cinnost)
                .Include(p => p.Projekt)
                .Include(p => p.Zakaznik)
                .FirstOrDefaultAsync(m => m.PraceId == id);
            if (prace == null)
            {
                return NotFound();
            }

            return View(prace);
        }

        // GET: Praces/Create
        public IActionResult Create()
        {
            ViewData["CinnostId"] = new SelectList(_context.Cinnost.Where(x => x.Aktivita == 100), "CinnostId", "Nazev");
            ViewData["ProjektId"] = new SelectList(_context.Projekt.Where(x => x.Aktivita == 100), "ProjektId", "Nazev");
            ViewData["ZakaznikId"] = new SelectList(_context.Zakaznik.Where(x => x.Aktivita == 100), "ZakaznikId", "Nazev");
            return View();
        }


        // POST: Praces/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PraceId,ZakaznikId,ProjektId,CinnostId,datum,time")] Prace prace)
        {
            prace.Aktivita = 100;
            
            var user = await _userManager.GetUserAsync(User);
            prace.Uzivatel = user;

            if (ModelState.IsValid)
            {
                _context.Add(prace);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CinnostId"] = new SelectList(_context.Cinnost, "CinnostId", "Nazev", prace.CinnostId);
            ViewData["ProjektId"] = new SelectList(_context.Projekt, "ProjektId", "Nazev", prace.ProjektId);
            ViewData["ZakaznikId"] = new SelectList(_context.Zakaznik, "ZakaznikId", "Nazev", prace.ZakaznikId);
            return View(prace);
        }

        // GET: Praces/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Prace == null)
            {
                return NotFound();
            }

            var prace = await _context.Prace.FindAsync(id);
            if (prace == null)
            {
                return NotFound();
            }
            ViewData["CinnostId"] = new SelectList(_context.Cinnost, "CinnostId", "Nazev", prace.CinnostId);
            ViewData["ProjektId"] = new SelectList(_context.Projekt, "ProjektId", "Nazev", prace.ProjektId);
            ViewData["ZakaznikId"] = new SelectList(_context.Zakaznik, "ZakaznikId", "Nazev", prace.ZakaznikId);
            return View(prace);
        }

        // POST: Praces/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PraceId,ZakaznikId,ProjektId,CinnostId,datum,time,Aktivita")] Prace prace)
        {
            if (id != prace.PraceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prace);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PraceExists(prace.PraceId))
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
            ViewData["CinnostId"] = new SelectList(_context.Cinnost, "CinnostId", "Nazev", prace.CinnostId);
            ViewData["ProjektId"] = new SelectList(_context.Projekt, "ProjektId", "Nazev", prace.ProjektId);
            ViewData["ZakaznikId"] = new SelectList(_context.Zakaznik, "ZakaznikId", "Nazev", prace.ZakaznikId);
            return View(prace);
        }

        // GET: Praces/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Prace == null)
            {
                return NotFound();
            }

            var prace = await _context.Prace
                .Include(p => p.Cinnost)
                .Include(p => p.Projekt)
                .Include(p => p.Zakaznik)
                .FirstOrDefaultAsync(m => m.PraceId == id);
            if (prace == null)
            {
                return NotFound();
            }

            return View(prace);
        }

        // POST: Praces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Prace == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Prace'  is null.");
            }
            var prace = await _context.Prace.FindAsync(id);
            if (prace != null)
            {
                prace.Aktivita = 900;
                _context.Update(prace);                
                //_context.Prace.Remove(prace);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PraceExists(int id)
        {
          return (_context.Prace?.Any(e => e.PraceId == id)).GetValueOrDefault();
        }
    }
}
