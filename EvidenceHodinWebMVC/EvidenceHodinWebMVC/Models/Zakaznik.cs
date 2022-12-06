using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvidenceHodinWebMVC.Models
{
	public class Zakaznik : BaseViewModel
    {
        [Key]
        public int ZakaznikId { get; set; }

        [Required]
        [Display(Name = "Zkratka")]
        public string Zkratka { get; set; }

        [Required]
        [Display(Name = "Zákazník")]
        public string Nazev { get; set; }

        [Display(Name = "Projekty")]
        public ICollection<Projekt>? Projekty { get; set; }

        [Required]
        [Range(100, 900)]
        [DefaultValue(100)]
        public int Aktivita { get; set; } = 100;

    }
}

