using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvidenceHodinWebMVC.Models
{
	public class Contact
	{
        [Key]
        public int Id { get; set; }

        // user ID from AspNetUser table.
        public string? OwnerID { get; set; }

        public string? Name { get; set; }
        public string? Surname { get; set; }

        public ContactStatus Status { get; set; }
    }

    public enum ContactStatus
    {
        Inactive,
        Active,
        Retired
    }
}

