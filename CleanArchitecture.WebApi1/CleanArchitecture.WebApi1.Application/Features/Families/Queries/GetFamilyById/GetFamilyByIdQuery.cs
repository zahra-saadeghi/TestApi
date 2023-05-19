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

namespace CleanArchitecture.WebApi1.Application.Features.Families.Queries.GetFamilyById
{
    public class GetFamilyByIdQuery : IRequest<Response<Family>>
    {
        public int Id { get; set; }
        public class GetFamilyByIdQueryHandler : IRequestHandler<GetFamilyByIdQuery, Response<Family>>
        {
            private readonly IFamilyRepositoryAsync _Repository;
            public GetFamilyByIdQueryHandler(IFamilyRepositoryAsync Repository)
            {
                _Repository = Repository;
            }
            public async Task<Response<Family>> Handle(GetFamilyByIdQuery query, CancellationToken cancellationToken)
            {
                var entity = await _Repository.GetByIdAsync(query.Id);
                if (entity == null) throw new ApiException($"Family Not Found.");
                return new Response<Family>(entity);
            }
        }
    }
}