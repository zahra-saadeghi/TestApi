using CleanArchitecture.WebApi1.Application.Exceptions;
using CleanArchitecture.WebApi1.Application.Interfaces.Repositories;
using CleanArchitecture.WebApi1.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi1.Application.Features.Students.Commands.DeleteStudentById
{
    public class DeleteStudentByIdCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public class DeleteStudentByIdCommandHandler : IRequestHandler<DeleteStudentByIdCommand, Result<int>>
        {
            private readonly IInsuranceRepositoryAsync _Repository;
            public DeleteStudentByIdCommandHandler(IInsuranceRepositoryAsync Repository)
            {
                _Repository = Repository;
            }
            public async Task<Result<int>> Handle(DeleteStudentByIdCommand command, CancellationToken cancellationToken)
            {
                var entity = await _Repository.GetByIdAsync(command.Id);
                if (entity == null) return  await Result<int>.FailAsync($"Student Not Found.");              
                await _Repository.DeleteAsync(entity);
                return await Result<int>.SuccessAsync(entity.Id, "Student Deleted");
         
            }
        }
    }
}
