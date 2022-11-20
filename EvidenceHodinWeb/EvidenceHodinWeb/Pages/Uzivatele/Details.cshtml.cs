using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EvidenceHodinWeb.Data;
using EvidenceHodinWeb.Models;

namespace EvidenceHodinWeb.Pages.Uzivatele
{
    public class DetailsModel : PageModel
    {
        private readonly EvidenceHodinWeb.Data.ApplicationDbContext _context;

        public DetailsModel(EvidenceHodinWeb.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Uzivatel Uzivatel { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var uzivatel = await _context.User.FirstOrDefaultAsync(m => m.UzivatelId == id);
            if (uzivatel == null)
            {
                return NotFound();
            }
            else 
            {
                Uzivatel = uzivatel;
            }
            return Page();
        }
    }
}
