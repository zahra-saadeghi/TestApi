using CleanArchitecture.WebApi1.Application.Enums;
using CleanArchitecture.WebApi1.Application.Statics;
using CleanArchitecture.WebApi1.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Crypto.Prng.Drbg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi1.Infrastructure.Identity.Seeds
{
    public static class DefaultSuperAdmin
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            //var defaultUser = new ApplicationUser
            //{
            //    UserName = "fallah",
            //    Email = "amir.fallah@hotmail.com",
            //    FirstName = "amir",
            //    LastName = "fallah",
            //    EmailConfirmed = true,
            //    PhoneNumberConfirmed = true
            //};
            //if (userManager.Users.All(u => u.Id != defaultUser.Id))
            //{
            //    var user = await userManager.FindByEmailAsync(defaultUser.Email);
            //    if (user == null)
            //    {
            //        await userManager.CreateAsync(defaultUser, StaticValues.Password);
            //        //await userManager.AddToRoleAsync(defaultUser, Roles.Customer.ToString());
            //        //await userManager.AddToRoleAsync(defaultUser, Roles.Moderator.ToString());
            //        await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
            //      //  await userManager.AddToRoleAsync(defaultUser, Roles.SuperAdmin.ToString());
            //    }

            //}
        }
    }
}
