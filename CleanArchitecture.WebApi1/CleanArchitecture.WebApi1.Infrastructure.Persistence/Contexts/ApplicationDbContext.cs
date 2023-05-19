using CleanArchitecture.WebApi1.Application.Interfaces;
using CleanArchitecture.WebApi1.Domain.Common;
using CleanArchitecture.WebApi1.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi1.Infrastructure.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IDateTimeService _dateTime;
        private readonly IAuthenticatedUserService _authenticatedUser;        

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTimeService dateTime, IAuthenticatedUserService authenticatedUser) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dateTime = dateTime;
            _authenticatedUser = authenticatedUser;
        }
      

       
        public DbSet<Address> Addresses { get; set; }

        public DbSet<ContactInfo> ContactInfos { get; set; }

        public DbSet<Family> Families { get; set; }

        public DbSet<Insurance> Insurances { get; set; }

        public DbSet<Parent> Parents { get; set; }

        public DbSet<Student> Students { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = _dateTime.NowUtc;
                        entry.Entity.CreatedBy = _authenticatedUser.UserId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = _dateTime.NowUtc;
                        entry.Entity.LastModifiedBy = _authenticatedUser.UserId;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //All Decimals will have 18,6 Range
            foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,6)");
            }
            base.OnModelCreating(builder);

            

            builder.Entity<Address>(entity =>
            {
                entity.Property(e => e.City).HasMaxLength(50);
                entity.Property(e => e.Country).HasMaxLength(50);
                entity.Property(e => e.County).HasMaxLength(50);
                entity.Property(e => e.State).HasMaxLength(50);
                entity.Property(e => e.Zip).HasMaxLength(50);
                
            });

            builder.Entity<ContactInfo>(entity =>
            {
                entity.Property(e => e.PrimaryPhone).HasMaxLength(50);
                entity.Property(e => e.AlternatePhone).HasMaxLength(50);
                entity.HasOne(d => d.Address)
                   .WithMany(p => p.ContactInfos)
                   .HasForeignKey(d => d.AddressId )
                   .HasConstraintName("FK_ContactInfos_Address");
            });

            builder.Entity<Family>(entity =>
            {
                entity.Property(e => e.Email).HasMaxLength(250);
                entity.Property(e => e.ExternalId).HasMaxLength(50);
                entity.Property(e => e.InsuranceId).HasMaxLength(50);

            });
            builder.Entity<Insurance>(entity =>
            {
               
                entity.Property(e => e.ExternalId).HasMaxLength(50);
                entity.Property(e => e.CompanyPhone).HasMaxLength(20);
                entity.Property(e => e.CompanyName).HasMaxLength(150);
                entity.Property(e => e.PolicyNumber).HasMaxLength(50);
            });
            builder.Entity<Parent>(entity =>
            {
                entity.Property(e => e.FirstName).HasMaxLength(150);
                entity.Property(e => e.LastName).HasMaxLength(150);
                entity.Property(e => e.Gender).HasMaxLength(10);
                entity.Property(e => e.DateOfBirth).HasMaxLength(20);

                entity.HasOne(d => d.Family)
                    .WithMany(p => p.Parents)
                    .HasForeignKey(d => d.FamilyId)
                    .HasConstraintName("FK_Parents_Family");
            });
            builder.Entity<Student>(entity =>
            {
                entity.Property(e => e.FirstName).HasMaxLength(150);
                entity.Property(e => e.LastName).HasMaxLength(150);
                entity.Property(e => e.Gender).HasMaxLength(10);
                entity.Property(e => e.DateOfBirth).HasMaxLength(20);
                entity.Property(e => e.GradeLevel).HasMaxLength(150);

                entity.HasOne(d => d.Family)
                  .WithMany(p => p.Students)
                  .HasForeignKey(d => d.FamilyId)
                  .HasConstraintName("FK_Students_Family");
            });
        }
    }
}
