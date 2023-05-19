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

namespace CleanArchitecture.WebApi1.Application.Features.Parents.Queries.GetParentById
{
    public class GetParentByIdQuery : IRequest<Response<Parent>>
    {
        public int Id { get; set; }
        public class GetParentByIdQueryHandler : IRequestHandler<GetParentByIdQuery, Response<Parent>>
        {
            private readonly IParentRepositoryAsync _Repository;
            public GetParentByIdQueryHandler(IParentRepositoryAsync Repository)
            {
                _Repository = Repository;
            }
            public async Task<Response<Parent>> Handle(GetParentByIdQuery query, CancellationToken cancellationToken)
            {
                var entity = await _Repository.GetByIdAsync(query.Id);
                if (entity == null) throw new ApiException($"Parent Not Found.");
                return new Response<Parent>(entity);
            }
        }
    }
}