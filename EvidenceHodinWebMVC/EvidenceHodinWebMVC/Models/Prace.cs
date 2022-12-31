using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvidenceHodinWebMVC.Models
{
	public class Prace : BaseViewModel
    {
        [Key]
        public int PraceId { get; set; }

        [Display(Name = "Uživatel")]
        public ApplicationUser? Uzivatel { get; set; }

        [Display(Name = "Zákazník")]
        public Zakaznik? Zakaznik { get; set; }
        public int ZakaznikId { get; set; }

        [Display(Name = "Projekt")]
        public Projekt? Projekt { get; set; }
        public int ProjektId { get; set; }

        [Display(Name = "Činnost")]
        public Cinnost? Cinnost { get; set; }
        public int CinnostId { get; set; }

        [Display(Name = "Datum")]
        public DateTime datum { get; set; }

        [Display(Name = "Čas (min)")]
        public long time { get; set; }

        [Required]
        [Range(100, 900)]
        [DefaultValue(100)]
        public int Aktivita { get; set; } = 100;
    }
}

