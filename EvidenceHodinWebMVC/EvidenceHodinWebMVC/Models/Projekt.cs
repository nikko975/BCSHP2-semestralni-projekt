using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvidenceHodinWebMVC.Models
{
	public class Projekt : BaseViewModel
    {
        [Key]
        public int ProjektId { get; set; }

        [Required]
        [Display(Name = "Projekt")]
        public string Nazev { get; set; }

        [Display(Name = "Časový limit h/rok")]
        public long? MaxMinut { get; set; }

        [Display(Name = "Zákazník")]
        public Zakaznik? Zakaznik { get; set; }

        [Display(Name = "Identifikátor zákazníka")]
        public int ZakaznikId { get; set; }

        public ICollection<ProjektCinnostVazba>? CinnostVazba { get; set; }

        [Required]
        [Range(100, 900)]
        [DefaultValue(100)]
        public int Aktivita { get; set; } = 100;

    }
}

