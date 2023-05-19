using AutoMapper;
using CleanArchitecture.WebApi1.Application.Interfaces.Repositories;
using CleanArchitecture.WebApi1.Application.Wrappers;
using CleanArchitecture.WebApi1.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using FluentValidation;

namespace CleanArchitecture.WebApi1.Application.Features.Students.Commands.CreateStudent
{
    public class CreateStudentCommandValidator : AbstractValidator<CreateStudentCommand>
    {
        private readonly IStudentRepositoryAsync Repository;

        public CreateStudentCommandValidator(IStudentRepositoryAsync Repository)
        {
            this.Repository = Repository;

            //RuleFor(p => p.Url)
            //    .NotEmpty().WithMessage("{PropertyName} is required.")
            //    .NotNull()
            //    .MaximumLength(150).WithMessage("{PropertyName} must not exceed 50 characters.");

            //RuleFor(p => p.ExpDate)
            //    .LessThan(p=> DateTime.Now).WithMessage("{PropertyName} not Valid");

        }

       
    }
}
