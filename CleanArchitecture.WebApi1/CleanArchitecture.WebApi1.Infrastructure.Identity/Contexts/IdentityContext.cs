﻿using CleanArchitecture.WebApi1.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.WebApi1.Infrastructure.Identity.Contexts
{
    public class IdentityContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("Identity");
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "User");
                entity.Property(e => e.FirstName).HasMaxLength(250);
                entity.Property(e => e.LastName).HasMaxLength(250);
                entity.Property(e => e.NickName).HasMaxLength(250);

                entity.Property(e => e.DeviceModel).HasMaxLength(70);
                entity.Property(e => e.DeviceManufacturer).HasMaxLength(70);
                entity.Property(e => e.DeviceOSVersion).HasMaxLength(70);
                entity.Property(e => e.DeviceIdiom).HasMaxLength(70);
                entity.Property(e => e.DevicePlatform).HasMaxLength(70);
                entity.Property(e => e.DeviceName).HasMaxLength(70);
            });

            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });

            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");

            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });
        }
    }
}
