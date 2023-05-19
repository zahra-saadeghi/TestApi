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

namespace CleanArchitecture.WebApi1.Application.Features.Parents.Commands.UpdateParent
{
    public class UpdateParentCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }

        public int FamilyId { get; set; }

        public int? ContactInfoId { get; set; }

        public class UpdateParentCommandHandler : IRequestHandler<UpdateParentCommand, Result<int>>
        {
            private readonly IParentRepositoryAsync _Repository;
            public UpdateParentCommandHandler(IParentRepositoryAsync Repository)
            {
                _Repository = Repository;
            }
            public async Task<Result<int>> Handle(UpdateParentCommand command, CancellationToken cancellationToken)
            {
                var entity = await _Repository.GetByIdAsync(command.Id);

                if (entity == null)
                {
                  return  await Result<int>.FailAsync($"Parent Not Found.");
                   
                }
                else
                {
                    entity.FirstName = command.FirstName;
                    entity.LastName = command.LastName;
                    entity.Gender = command.Gender;
                    entity.DateOfBirth = command.DateOfBirth;
                    entity.FamilyId = command.FamilyId;

                    await _Repository.UpdateAsync(entity);
                    return await Result<int>.SuccessAsync(entity.Id, "Parent Updated");
              
                }
              
            }
        }
    }
}
