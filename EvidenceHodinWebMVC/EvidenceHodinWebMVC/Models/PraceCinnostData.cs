using System.ComponentModel.DataAnnotations;

namespace EvidenceHodinWebMVC.Models
{
    public class PraceCinnostData
    {
        public int CinnostId { get; set; }
        [Display(Name = "Název")]
        public string Nazev { get; set; }
        public bool Assigned { get; set; }

    }
}
