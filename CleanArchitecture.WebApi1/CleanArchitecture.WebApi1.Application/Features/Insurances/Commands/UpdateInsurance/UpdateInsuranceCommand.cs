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

namespace CleanArchitecture.WebApi1.Application.Features.Insurances.Commands.UpdateInsurance
{
    public class UpdateInsuranceCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public string ExternalId { get; set; }
        public string CompanyName { get; set; }
        public string PolicyNumber { get; set; }
        public string CompanyPhone { get; set; }

        public class UpdateInsuranceCommandHandler : IRequestHandler<UpdateInsuranceCommand, Result<int>>
        {
            private readonly IInsuranceRepositoryAsync _Repository;
            public UpdateInsuranceCommandHandler(IInsuranceRepositoryAsync Repository)
            {
                _Repository = Repository;
            }
            public async Task<Result<int>> Handle(UpdateInsuranceCommand command, CancellationToken cancellationToken)
            {
                var entity = await _Repository.GetByIdAsync(command.Id);

                if (entity == null)
                {
                  return  await Result<int>.FailAsync($"Insurance Not Found.");
                   
                }
                else
                {
                    entity.ExternalId = command.ExternalId;
                    entity.CompanyName = command.CompanyName;
                    entity.PolicyNumber = command.PolicyNumber;
                    entity.CompanyPhone = command.CompanyPhone;

                    await _Repository.UpdateAsync(entity);
                    return await Result<int>.SuccessAsync(entity.Id, "Insurance Updated");
              
                }
              
            }
        }
    }
}
