using GymSystemDAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemDAL.Data.DataSeed
{
    public class IdentityDBContextSeeding
    {
        public static bool SeedData(RoleManager<IdentityRole>roleManager,UserManager<ApplicationUser> userManager)
        {
            try
            {
                var HasUsers = userManager.Users.Any();
                var HasRoles = roleManager.Roles.Any();

                if (HasUsers && HasRoles)
                {
                    return false; // No seeding needed
                }
                if (!HasRoles)
                {

                    var roles = new List<IdentityRole>
                {
                    new IdentityRole { Name = "Super Admin" },
                    new IdentityRole { Name = "Admin"},


                };
                    foreach (var role in roles)
                    {
                        var roleExists = roleManager.RoleExistsAsync(role.Name).Result;
                        if (!roleExists)
                        {
                            roleManager.CreateAsync(role).Wait();

                        }

                    }
                }
                if (!HasUsers)
                {
                    var MainAdmin = new ApplicationUser
                    {
                        FirstName = "Arwa",
                        LastName = "Walaa",
                        UserName = "ArwaWalaa",
                        Email = "arwa.walaa88@gmail.com",
                        PhoneNumber= "01022223333",

                    };
                    userManager.CreateAsync(MainAdmin, "Admin@123").Wait();

                    userManager.AddToRoleAsync(MainAdmin, "Super Admin").Wait();

                    var Admin = new ApplicationUser
                    {
                        FirstName = "Sara",
                        LastName = "Walaa",
                        UserName = "SaraWalaa",
                        Email = "sara.walaa88@gmail.com",
                        PhoneNumber = "01022773333",

                    };
                    userManager.CreateAsync(Admin, "Admin@123").Wait();

                    userManager.AddToRoleAsync(Admin, "Admin").Wait();

                  
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Seeding failed: {ex}");
                return false;


            }
           
        }
    }
}
