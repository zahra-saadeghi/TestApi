using CleanArchitecture.WebApi1.Application.Enums;
using CleanArchitecture.WebApi1.Application.Statics;
using CleanArchitecture.WebApi1.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi1.Infrastructure.Identity.Seeds
{
    public static class DefaultBasicUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "basicuser",
                Email = "basicuser@gmail.com",
                FirstName = "John",
                LastName = "Doe",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, StaticValues.Password);
                    await userManager.AddToRoleAsync(defaultUser, Roles.Customer.ToString());
                }

            }

            var defaultUser2 = new ApplicationUser
            {
                UserName = "sadeghi",
                Email = "zahra.saadeghi@outlook.com",
                FirstName = "zahra",
                LastName = "sadeghi",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser2.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser2.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser2, StaticValues.Password);
                    await userManager.AddToRoleAsync(defaultUser2, Roles.Customer.ToString());
                }

            }
        }
    }
}
