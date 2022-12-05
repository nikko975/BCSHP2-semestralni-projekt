using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvidenceHodinWebMVC.Models
{
	public class Zakaznik
	{
        [Key]
        public int Id { get; set; }

        [Required]
        public string Zkratka { get; set; }
        [Required]
        public string Nazev { get; set; }
        public List<Projekt>? Projekty { get; set; }

    }
}

