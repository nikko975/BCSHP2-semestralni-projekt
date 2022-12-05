using System;
using System.ComponentModel.DataAnnotations;

namespace EvidenceHodinWebMVC.Models
{
	public class Zakaznik
	{
        
        public int Id { get; set; }

        [Required]
        public string Nazev { get; set; }
        public List<Projekt> Projekty { get; set; }

    }
}

