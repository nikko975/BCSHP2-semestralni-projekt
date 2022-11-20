using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EvidenceHodinWeb.Authorization;
using Microsoft.EntityFrameworkCore;
using EvidenceHodinWeb.Data;
using EvidenceHodinWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ContactManager.Pages.Contacts;

namespace EvidenceHodinWeb.Pages.Uzivatele
{
    public class EditModel : DI_BasePageModel
    {
        private readonly EvidenceHodinWeb.Data.ApplicationDbContext _context;

        public EditModel(
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

            Uzivatel? contact = await Context.User.FirstOrDefaultAsync(
                                                         m => m.UzivatelId == id);
            if (contact == null)
            {
                return NotFound();
            }

            Uzivatel = contact;

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                      User, Uzivatel,
                                                      UserOperations.Update);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            return Page();

        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }





            _context.Attach(Uzivatel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UzivatelExists(Uzivatel.UzivatelId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
                throw;
            }

            return RedirectToPage("./Index");

            // TODO

            //// Fetch Contact from DB to get OwnerID.
            //var contact = await Context
            //    .Contact.AsNoTracking()
            //    .FirstOrDefaultAsync(m => m.ContactId == id);

            //if (contact == null)
            //{
            //    return NotFound();
            //}

            //var isAuthorized = await AuthorizationService.AuthorizeAsync(
            //                                         User, contact,
            //                                         ContactOperations.Update);
            //if (!isAuthorized.Succeeded)
            //{
            //    return Forbid();
            //}

            //Contact.OwnerID = contact.OwnerID;

            //Context.Attach(Contact).State = EntityState.Modified;

            //if (Contact.Status == ContactStatus.Approved)
            //{
            //    // If the contact is updated after approval, 
            //    // and the user cannot approve,
            //    // set the status back to submitted so the update can be
            //    // checked and approved.
            //    var canApprove = await AuthorizationService.AuthorizeAsync(User,
            //                            Contact,
            //                            ContactOperations.Approve);

            //    if (!canApprove.Succeeded)
            //    {
            //        Contact.Status = ContactStatus.Submitted;
            //    }
            //}

            //await Context.SaveChangesAsync();

            //return RedirectToPage("./Index");
        }
    

        private bool UzivatelExists(int id)
        {
          return (_context.User?.Any(e => e.UzivatelId == id)).GetValueOrDefault();
        }
    }
}
