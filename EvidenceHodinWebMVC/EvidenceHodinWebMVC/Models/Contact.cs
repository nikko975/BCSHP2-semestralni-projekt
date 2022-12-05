﻿using System;
using System.ComponentModel.DataAnnotations;

namespace EvidenceHodinWebMVC.Models
{
	public class Contact
	{
        public int ContactId { get; set; }

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
