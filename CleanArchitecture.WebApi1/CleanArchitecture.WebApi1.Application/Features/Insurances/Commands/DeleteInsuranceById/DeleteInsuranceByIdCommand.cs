using CleanArchitecture.WebApi1.Application.Exceptions;
using CleanArchitecture.WebApi1.Application.Interfaces.Repositories;
using CleanArchitecture.WebApi1.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi1.Application.Features.Insurances.Commands.DeleteInsuranceById
{
    public class DeleteInsuranceByIdCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public class DeleteInsuranceByIdCommandHandler : IRequestHandler<DeleteInsuranceByIdCommand, Result<int>>
        {
            private readonly IInsuranceRepositoryAsync _Repository;
            public DeleteInsuranceByIdCommandHandler(IInsuranceRepositoryAsync Repository)
            {
                _Repository = Repository;
            }
            public async Task<Result<int>> Handle(DeleteInsuranceByIdCommand command, CancellationToken cancellationToken)
            {
                var entity = await _Repository.GetByIdAsync(command.Id);
                if (entity == null) return  await Result<int>.FailAsync($"Insurance Not Found.");              
                await _Repository.DeleteAsync(entity);
                return await Result<int>.SuccessAsync(entity.Id, "Insurance Deleted");
         
            }
        }
    }
}
