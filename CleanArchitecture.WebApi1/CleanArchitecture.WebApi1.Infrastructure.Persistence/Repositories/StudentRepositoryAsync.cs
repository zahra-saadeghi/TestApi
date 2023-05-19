﻿using CleanArchitecture.WebApi1.Application.Interfaces.Repositories;
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
    public class StudentRepositoryAsync : GenericRepositoryAsync<Student>, IStudentRepositoryAsync
    {
        private readonly DbSet<Student> _Student;

        public StudentRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)

        {
            _Student = dbContext.Set<Student>();
        }

     
    }
}
