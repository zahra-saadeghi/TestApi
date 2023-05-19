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
    public class InsuranceRepositoryAsync : GenericRepositoryAsync<Insurance>, IInsuranceRepositoryAsync
    {
        private readonly DbSet<Insurance> _Insurance;

        public InsuranceRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)

        {
            _Insurance = dbContext.Set<Insurance>();
        }

     
    }
}
