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
		public Contact Uzivatel { get; set; }
		public Zakaznik Zakaznik { get; set; }
		public Projekt Projekt { get; set; }
		public Cinnost Cinnost { get; set; }
		public DateTime datum { get; set; }
		public long time { get; set; }

        [Required]
        [Range(100, 900)]
        [DefaultValue(100)]
        public int Aktivita { get; set; } = 100;
    }
}

