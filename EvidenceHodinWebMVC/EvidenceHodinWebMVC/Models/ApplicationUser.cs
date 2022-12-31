using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EvidenceHodinWebMVC.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Jméno")]
        public string FirstName { get; set; }

        [Display(Name = "Příjmení")]
        public string LastName { get; set; }

        [Display(Name = "Celé jméno")]
        public string FullName { get { return FirstName + " " + LastName; } }
    }
}
