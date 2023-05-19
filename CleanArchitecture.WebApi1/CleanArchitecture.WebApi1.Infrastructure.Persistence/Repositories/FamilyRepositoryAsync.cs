using CleanArchitecture.WebApi1.Application.Interfaces.Repositories;
using CleanArchitecture.WebApi1.Domain.Entities;
using CleanArchitecture.WebApi1.Infrastructure.Persistence.Contexts;
using CleanArchitecture.WebApi1.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi1.Infrastructure.Persistence.Repositories
{
    public class FamilyRepositoryAsync : GenericRepositoryAsync<Family>, IFamilyRepositoryAsync
    {
        private readonly DbSet<Family> _Family;

        public FamilyRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)

        {
            _Family = dbContext.Set<Family>();
        }

       
    }
}
