using System;
using System.ComponentModel.DataAnnotations;

namespace EvidenceHodinWebMVC.Models
{
	public class Projekt
	{
		public int Id { get; set; }
        [Required]
        public string Nazev { get; set; }
        public long MaxMinutCelkem { get; set; }

    }
}

