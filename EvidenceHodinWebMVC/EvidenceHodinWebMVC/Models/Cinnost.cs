using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvidenceHodinWebMVC.Models
{
	public class Cinnost : BaseViewModel
    {
        [Key]
        public int CinnostId { get; set; }

        [Required]
        [Display(Name = "Název činnosti")]
        public string Nazev { get; set; }

        [Required]
        [Range(100, 900)]
        [DefaultValue(100)]
        public int Aktivita { get; set; } = 100;

        public ICollection<ProjektCinnostVazba>? CinnostVazba { get; set; }


    }
}

