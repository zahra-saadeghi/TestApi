﻿using CleanArchitecture.WebApi1.Application.Interfaces;
using CleanArchitecture.WebApi1.Application.Interfaces.Repositories;
using CleanArchitecture.WebApi1.Infrastructure.Persistence.Contexts;
using CleanArchitecture.WebApi1.Infrastructure.Persistence.Repositories;
using CleanArchitecture.WebApi1.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.WebApi1.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(
                   configuration.GetConnectionString("DefaultConnection"),
                   b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }
            #region Repositories
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));           
          

            services.AddTransient<IFamilyRepositoryAsync, FamilyRepositoryAsync>(); 

            services.AddTransient<IInsuranceRepositoryAsync, InsuranceRepositoryAsync>();

            services.AddTransient<IParentRepositoryAsync, ParentRepositoryAsync>();

            services.AddTransient<IStudentRepositoryAsync, StudentRepositoryAsync>();

            #endregion
        }
    }
}
