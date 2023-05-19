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

namespace CleanArchitecture.WebApi1.Application.Features.Families.Commands.UpdateFamily
{
    public class UpdateFamilyCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public int? BusinessId { get; set; }
        public string Email { get; set; }
        public string ExternalId { get; set; }
        public int? InsuranceId { get; set; }
        public int? ContactInfoId { get; set; }

        public class UpdateFamilyCommandHandler : IRequestHandler<UpdateFamilyCommand, Result<int>>
        {
            private readonly IFamilyRepositoryAsync _Repository;
            public UpdateFamilyCommandHandler(IFamilyRepositoryAsync Repository)
            {
                _Repository = Repository;
            }
            public async Task<Result<int>> Handle(UpdateFamilyCommand command, CancellationToken cancellationToken)
            {
                var entity = await _Repository.GetByIdAsync(command.Id);

                if (entity == null)
                {
                  return  await Result<int>.FailAsync($"Family Not Found.");
                   
                }
                else
                {
                    entity.BusinessId = command.BusinessId;
                    entity.Email = command.Email;
                    entity.ExternalId = command.ExternalId;
                    entity.InsuranceId = command.InsuranceId;
                    entity.ContactInfoId = command.ContactInfoId;

                    await _Repository.UpdateAsync(entity);
                    return await Result<int>.SuccessAsync(entity.Id, "Family Updated");
              
                }
              
            }
        }
    }
}
