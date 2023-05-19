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

namespace CleanArchitecture.WebApi1.Application.Features.Insurances.Queries.GetInsuranceById
{
    public class GetInsuranceByIdQuery : IRequest<Response<Insurance>>
    {
        public int Id { get; set; }
        public class GetInsuranceByIdQueryHandler : IRequestHandler<GetInsuranceByIdQuery, Response<Insurance>>
        {
            private readonly IInsuranceRepositoryAsync _Repository;
            public GetInsuranceByIdQueryHandler(IInsuranceRepositoryAsync Repository)
            {
                _Repository = Repository;
            }
            public async Task<Response<Insurance>> Handle(GetInsuranceByIdQuery query, CancellationToken cancellationToken)
            {
                var entity = await _Repository.GetByIdAsync(query.Id);
                if (entity == null) throw new ApiException($"Insurance Not Found.");
                return new Response<Insurance>(entity);
            }
        }
    }
}