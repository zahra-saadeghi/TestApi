using AutoMapper;

using CleanArchitecture.WebApi1.Application.Interfaces.Repositories;
using CleanArchitecture.WebApi1.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace CleanArchitecture.WebApi1.Application.Features.Insurances.Queries.GatAllInsurances
{
    public class GatAllInsuranceQuery : IRequest<Response<IEnumerable<GetAllInsuranceViewModel>>>
    {
      
    }
    public class GatAllInsuranceQueryHandler : IRequestHandler<GatAllInsuranceQuery, Response<IEnumerable<GetAllInsuranceViewModel>>>
    {
        private readonly IInsuranceRepositoryAsync _Repository;
        private readonly IMapper _mapper;
        public GatAllInsuranceQueryHandler(IInsuranceRepositoryAsync Repository, IMapper mapper)
        {
            _Repository = Repository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<GetAllInsuranceViewModel>>> Handle(GatAllInsuranceQuery request, CancellationToken cancellationToken)
        {

            var Entities = await _Repository.GetAllAsync();            
            var ViewModel = _mapper.Map<IEnumerable<GetAllInsuranceViewModel>>(Entities.ToList());            
            return new Response<IEnumerable<GetAllInsuranceViewModel>>(ViewModel);
        }
    }
}
