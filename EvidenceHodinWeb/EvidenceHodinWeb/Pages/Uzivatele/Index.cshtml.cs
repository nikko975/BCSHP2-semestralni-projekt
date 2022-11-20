using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EvidenceHodinWeb.Data;
using EvidenceHodinWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ContactManager.Pages.Contacts;
using EvidenceHodinWeb.Authorization;

namespace EvidenceHodinWeb.Pages.Uzivatele
{
    public class IndexModel : DI_BasePageModel
    {
        private readonly EvidenceHodinWeb.Data.ApplicationDbContext _context;

        public IndexModel(
        ApplicationDbContext context,
        IAuthorizationService authorizationService,
        UserManager<IdentityUser> userManager)
                : base(context, authorizationService, userManager)
        {
            _context = base.Context; // TODO je toto nutne?
        }
        public IList<Uzivatel> Uzivatel { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.User != null)
            {
                Uzivatel = await _context.User.ToListAsync();
            }


            var contacts = from c in _context.User
                           select c;

            var isAuthorized = User.IsInRole(Constants.ContactManagersRole) ||
                               User.IsInRole(Constants.ContactAdministratorsRole);

            var currentUserId = UserManager.GetUserId(User);

            // Only approved contacts are shown UNLESS you're authorized to see them
            // or you are the owner.
            if (!isAuthorized)
            {
                //contacts = contacts.Where(c => c.Status == ContactStatus.Approved
                //                            || c.OwnerID == currentUserId);
            }

            Uzivatel = await contacts.ToListAsync();


        }
    }
}
