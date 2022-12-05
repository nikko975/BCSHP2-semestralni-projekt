using System;
namespace EvidenceHodinWebMVC.Models
{
	public class Prace
	{
		public int Id { get; set; }
		public Contact Uzivatel { get; set; }
		public Zakaznik Zakaznik { get; set; }
		public Projekt Projekt { get; set; }
		public Cinnost Cinnost { get; set; }
		public DateTime datum { get; set; }
		public long time { get; set; }
    }
}

