using EvidenceHodinWebMVC.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

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

                CreateRoles(serviceProvider).Wait();
                SeedAdminAcc(serviceProvider).Wait();




                context.SaveChanges();

                context.Zakaznik.AddRange(
                    new Zakaznik
                    {
                        //ZakaznikId = 0,
                        Aktivita = 100,
                        Nazev = "Město Pardubice",
                        Zkratka = "PCE",
                    },
                    new Zakaznik
                    {
                        //ZakaznikId = 1,
                        Aktivita = 100,
                        Nazev = "magistrát města Brna",
                        Zkratka = "MMB",
                    },
                    new Zakaznik
                    {
                        //ZakaznikId = 2,
                        Aktivita = 100,
                        Nazev = "Ostrava",
                        Zkratka = "ova",
                    }
                );

                context.SaveChanges();



                context.Projekt.AddRange(
                    new Projekt
                    {
                        //ProjektId = 1,
                        Aktivita = 100,
                        Nazev = "Analýza dat",
                        MaxMinut = 600,
                        ZakaznikId = 1
                    },
                    new Projekt
                    {
                        //ProjektId = 2,
                        Aktivita = 100,
                        Nazev = "Servis programů",
                        MaxMinut = 80,
                        ZakaznikId = 1
                    },
                    new Projekt
                    {
                        //ProjektId = 3,
                        Aktivita = 100,
                        Nazev = "hotline",
                        MaxMinut = 1000,
                        ZakaznikId = 2
                    }

                    ); ;
                context.SaveChanges();

                //context.Cinnost.AddRange(
                //    new Cinnost
                //    {
                //        //ProjektId = 1,
                //        Aktivita = 100,
                //        Nazev = "Analýza dat",
                //        MaxMinut = 600,
                //        ZakaznikId = 1
                //    },
                //    new Cinnost
                //    {
                //        //ProjektId = 2,
                //        Aktivita = 100,
                //        Nazev = "Servis programů",
                //        MaxMinut = 80,
                //        ZakaznikId = 1
                //    },
                //    new Cinnost
                //    {
                //        //ProjektId = 3,
                //        Aktivita = 100,
                //        Nazev = "hotline",
                //        MaxMinut = 1000,
                //        ZakaznikId = 2
                //    }

                //    ); ;
                //context.SaveChanges();




            }
        }

        private static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = { "Admin", "Manager", "Member", "Cekatel" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }

        public static async Task SeedAdminAcc(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            //Seed Default User
            var defaultAdmin = new ApplicationUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                FirstName = "Ucet",
                LastName = "Admina",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            if (userManager.Users.All(u => u.Id != defaultAdmin.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultAdmin.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultAdmin, "Heslo1!");
                    await userManager.AddToRoleAsync(defaultAdmin, "Admin");
                    await userManager.AddToRoleAsync(defaultAdmin, "Manager");
                    await userManager.AddToRoleAsync(defaultAdmin, "Member");
                }

            }
        }
    }
}
