using EvidenceHodinWebMVC.Data;
using Microsoft.EntityFrameworkCore;

namespace EvidenceHodinWebMVC.Models
{
    public static class SeedData
    {

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                if (context == null || context.Zakaznik == null)
                {
                    throw new ArgumentNullException("Null ApplicationDbContext");
                }

                // Look for any movies.
                if (context.Zakaznik.Any())
                {
                    return;   // DB has been seeded
                }

                context.Zakaznik.AddRange(
                    new Zakaznik
                    {
                        ZakaznikId = 1,
                        Aktivita = 100,
                        Nazev = "Město Pardubice",
                        Zkratka = "PCE",
                    },
                    new Zakaznik
                    {
                        ZakaznikId = 2,
                        Aktivita = 100,
                        Nazev = "magistrát města Brna",
                        Zkratka = "MMB",
                    },
                    new Zakaznik
                    {
                        ZakaznikId = 3,
                        Aktivita = 100,
                        Nazev = "Ostrava",
                        Zkratka = "ova",
                    }
                );

                context.SaveChanges();



                context.Projekt.AddRange(
                    new Projekt
                    {
                        ProjektId = 1,
                        Aktivita = 100,
                        Nazev = "Analýza dat",
                        MaxMinut = 600,
                        ZakaznikId = 1                        
                    },
                    new Projekt
                    {
                        ProjektId = 2,
                        Aktivita = 100,
                        Nazev = "Servis programů",
                        MaxMinut = 80,
                        ZakaznikId = 1
                    },
                    new Projekt
                    {
                        ProjektId = 3,
                        Aktivita = 100,
                        Nazev = "hotline",
                        MaxMinut = 1000,
                        ZakaznikId = 2
                    }

                    ); ;



                context.SaveChanges();







            }
        }



    }
}
