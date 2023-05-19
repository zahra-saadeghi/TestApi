using CleanArchitecture.WebApi1.Application.Exceptions;
using CleanArchitecture.WebApi1.Application.Interfaces.Repositories;
using CleanArchitecture.WebApi1.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi1.Application.Features.Families.Commands.DeleteFamilyById
{
    public class DeleteFamilyByIdCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public class DeleteFamilyByIdCommandHandler : IRequestHandler<DeleteFamilyByIdCommand, Result<int>>
        {
            private readonly IFamilyRepositoryAsync _Repository;
            public DeleteFamilyByIdCommandHandler(IFamilyRepositoryAsync Repository)
            {
                _Repository = Repository;
            }
            public async Task<Result<int>> Handle(DeleteFamilyByIdCommand command, CancellationToken cancellationToken)
            {
                var entity = await _Repository.GetByIdAsync(command.Id);
                if (entity == null) return  await Result<int>.FailAsync($"Family Not Found.");              
                await _Repository.DeleteAsync(entity);
                return await Result<int>.SuccessAsync(entity.Id, "Family Deleted");
         
            }
        }
    }
}
