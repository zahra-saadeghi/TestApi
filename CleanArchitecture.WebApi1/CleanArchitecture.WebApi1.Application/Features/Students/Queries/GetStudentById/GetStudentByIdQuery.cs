using CleanArchitecture.WebApi1.Application.Exceptions;

using CleanArchitecture.WebApi1.Application.Interfaces.Repositories;
using CleanArchitecture.WebApi1.Application.Wrappers;
using CleanArchitecture.WebApi1.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CleanArchitecture.WebApi1.Application.Features.Students.Queries.GetStudentById
{
    public class GetStudentByIdQuery : IRequest<Response<Student>>
    {
        public int Id { get; set; }
        public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, Response<Student>>
        {
            private readonly IStudentRepositoryAsync _Repository;
            public GetStudentByIdQueryHandler(IStudentRepositoryAsync Repository)
            {
                _Repository = Repository;
            }
            public async Task<Response<Student>> Handle(GetStudentByIdQuery query, CancellationToken cancellationToken)
            {
                var entity = await _Repository.GetByIdAsync(query.Id);
                if (entity == null) throw new ApiException($"Student Not Found.");
                return new Response<Student>(entity);
            }
        }
    }
}