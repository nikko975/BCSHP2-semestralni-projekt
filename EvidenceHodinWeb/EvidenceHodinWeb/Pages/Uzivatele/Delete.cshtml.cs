using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EvidenceHodinWeb.Data;
using EvidenceHodinWeb.Models;
using ContactManager.Pages.Contacts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace EvidenceHodinWeb.Pages.Uzivatele
{
    public class DeleteModel : DI_BasePageModel
    {
        private readonly EvidenceHodinWeb.Data.ApplicationDbContext _context;

        public DeleteModel(
        ApplicationDbContext context,
        IAuthorizationService authorizationService,
        UserManager<IdentityUser> userManager)
        : base(context, authorizationService, userManager)
        {
            _context = base.Context; // TODO je toto nutne?
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }
            var uzivatel = await _context.User.FindAsync(id);

            if (uzivatel != null)
            {
                Uzivatel = uzivatel;
                _context.User.Remove(Uzivatel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
