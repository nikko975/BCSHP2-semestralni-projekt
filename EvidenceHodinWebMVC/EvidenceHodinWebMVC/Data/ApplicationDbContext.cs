﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EvidenceHodinWebMVC.Models;

namespace EvidenceHodinWebMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<EvidenceHodinWebMVC.Models.Zakaznik> Zakaznik { get; set; } = default!;
        public DbSet<EvidenceHodinWebMVC.Models.Contact> Contact { get; set; } = default!;
        public DbSet<EvidenceHodinWebMVC.Models.Projekt> Projekt { get; set; } = default!;
        public DbSet<EvidenceHodinWebMVC.Models.Cinnost> Cinnost { get; set; } = default!;
    }
}