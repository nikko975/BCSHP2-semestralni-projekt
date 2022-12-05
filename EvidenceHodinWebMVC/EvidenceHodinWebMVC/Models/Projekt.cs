using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvidenceHodinWebMVC.Models
{
	public class Projekt
	{
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nazev { get; set; }
        public long MaxMinutCelkem { get; set; }
        public Zakaznik Zakaznik { get; set; }

    }
}

