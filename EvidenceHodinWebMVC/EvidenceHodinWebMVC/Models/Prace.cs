using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvidenceHodinWebMVC.Models
{
	public class Prace
	{
        [Key]
        public int Id { get; set; }
		public Contact Uzivatel { get; set; }
		public Zakaznik Zakaznik { get; set; }
		public Projekt Projekt { get; set; }
		public Cinnost Cinnost { get; set; }
		public DateTime datum { get; set; }
		public long time { get; set; }
    }
}

