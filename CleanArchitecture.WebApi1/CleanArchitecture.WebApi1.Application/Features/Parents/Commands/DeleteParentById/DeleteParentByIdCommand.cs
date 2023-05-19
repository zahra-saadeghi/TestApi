using CleanArchitecture.WebApi1.Application.Exceptions;
using CleanArchitecture.WebApi1.Application.Interfaces.Repositories;
using CleanArchitecture.WebApi1.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi1.Application.Features.Parents.Commands.DeleteParentById
{
    public class DeleteParentByIdCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public class DeleteParentByIdCommandHandler : IRequestHandler<DeleteParentByIdCommand, Result<int>>
        {
            private readonly IParentRepositoryAsync _Repository;
            public DeleteParentByIdCommandHandler(IParentRepositoryAsync Repository)
            {
                _Repository = Repository;
            }
            public async Task<Result<int>> Handle(DeleteParentByIdCommand command, CancellationToken cancellationToken)
            {
                var entity = await _Repository.GetByIdAsync(command.Id);
                if (entity == null) return  await Result<int>.FailAsync($"Parent Not Found.");              
                await _Repository.DeleteAsync(entity);
                return await Result<int>.SuccessAsync(entity.Id, "Parent Deleted");
         
            }
        }
    }
}
