using AutoMapper;
using CleanArchitecture.WebApi1.Application.Interfaces.Repositories;
using CleanArchitecture.WebApi1.Application.Wrappers;
using CleanArchitecture.WebApi1.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CleanArchitecture.WebApi1.Application.Features.Insurances.Commands.CreateInsurance
{
    public partial class CreateInsuranceCommand : IRequest<Response<int>>
    {
        public string ExternalId { get; set; }
        public string CompanyName { get; set; }
        public string PolicyNumber { get; set; }
        public string CompanyPhone { get; set; }
    }
    public class CreateInsuranceCommandHandler : IRequestHandler<CreateInsuranceCommand, Response<int>>
    {
        private readonly IInsuranceRepositoryAsync _Repository;
      private readonly IMapper _mapper;
        public CreateInsuranceCommandHandler(IInsuranceRepositoryAsync Repository, IMapper mapper)
        {
            _Repository = Repository;
         _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateInsuranceCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Insurance>(request);
        
            entity.Created = DateTime.Now;
            await _Repository.AddAsync(entity);
            return new Response<int>(entity.Id);
        }
    }
}
