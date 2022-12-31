using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvidenceHodinWebMVC.Models
{
    [NotMapped]
    public class UserRolesViewModel
    {
        public string UserId { get; set; }
        [Display(Name = "Jméno")]
        public string FirstName { get; set; }
        [Display(Name = "příjmení")]
        public string LastName { get; set; }
        [Display(Name = "Uživatelské jméno")]
        public string UserName { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Role")]
        public IEnumerable<string> Roles { get; set; }
    }
}
