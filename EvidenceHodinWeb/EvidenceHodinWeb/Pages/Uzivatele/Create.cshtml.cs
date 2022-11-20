using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EvidenceHodinWeb.Authorization;
using EvidenceHodinWeb.Data;
using EvidenceHodinWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ContactManager.Pages.Contacts;

namespace EvidenceHodinWeb.Pages.Uzivatele
{
    public class CreateModel : DI_BasePageModel
    {
        private readonly EvidenceHodinWeb.Data.ApplicationDbContext _context;

        public CreateModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
            _context =  base.Context; // TODO je toto nutne?
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Uzivatel Uzivatel { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.User == null || Uzivatel == null)
            {
                return Page();
            }
            Uzivatel.OwnerID = UserManager.GetUserId(User);

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                        User, Uzivatel,
                                                        UserOperations.Create);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            _context.User.Add(Uzivatel);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");

        }
    }
}
