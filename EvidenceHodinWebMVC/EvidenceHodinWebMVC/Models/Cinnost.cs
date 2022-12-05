using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvidenceHodinWebMVC.Models
{
	public class Cinnost
	{
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nazev { get; set; }
        public long MaxMinut { get; set; }
        public Projekt Projekt { get; set; }
    }
}

