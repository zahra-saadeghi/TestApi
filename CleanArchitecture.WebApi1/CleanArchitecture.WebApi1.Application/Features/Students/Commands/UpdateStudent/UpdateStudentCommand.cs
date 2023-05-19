using CleanArchitecture.WebApi1.Application.Exceptions;

using CleanArchitecture.WebApi1.Application.Interfaces.Repositories;
using CleanArchitecture.WebApi1.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using CleanArchitecture.WebApi1.Domain.Entities;
using CleanArchitecture.WebApi1.Application.Features.Parents.Commands.UpdateParent;

namespace CleanArchitecture.WebApi1.Application.Features.Students.Commands.UpdateStudent
{
    public class UpdateStudentCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }
        public string GradeLevel { get; set; }
        public int? ContactInfoId { get; set; }

        public int FamilyId { get; set; }

        public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, Result<int>>
        {
            private readonly IStudentRepositoryAsync _Repository;
            public UpdateStudentCommandHandler(IStudentRepositoryAsync Repository)
            {
                _Repository = Repository;
            }
            public async Task<Result<int>> Handle(UpdateStudentCommand command, CancellationToken cancellationToken)
            {
                var entity = await _Repository.GetByIdAsync(command.Id);

                if (entity == null)
                {
                  return  await Result<int>.FailAsync($"Student Not Found.");
                   
                }
                else
                {
                    entity.FirstName = command.FirstName;
                    entity.LastName = command.LastName;
                    entity.Gender = command.Gender;
                    entity.DateOfBirth = command.DateOfBirth;
                    entity.FamilyId = command.FamilyId;
                    entity.GradeLevel = command.GradeLevel;
                    entity.ContactInfoId = command.ContactInfoId;

                    await _Repository.UpdateAsync(entity);
                    return await Result<int>.SuccessAsync(entity.Id, "Student Updated");
              
                }
              
            }
        }
    }
}
