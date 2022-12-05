using System;
using System.ComponentModel.DataAnnotations;

namespace EvidenceHodinWebMVC.Models
{
	public class Cinnost
	{
        public int Id { get; set; }
        [Required]
        public string Nazev { get; set; }
        public long MaxMinut { get; set; }
    }
}

