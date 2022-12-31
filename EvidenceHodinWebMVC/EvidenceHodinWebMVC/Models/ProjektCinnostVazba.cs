using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EvidenceHodinWebMVC.Models
{
    public class ProjektCinnostVazba
    {
        [Key]
        public int Id { get; set; }
        public int ProjektId { get; set; }
        public int CinnostId { get; set; }
        [Display(Name = "Projekt")]
        public Projekt Projekt { get; set; }
        [Display(Name = "Činnost")]
        public Cinnost Cinnost { get; set; }
    }
}
