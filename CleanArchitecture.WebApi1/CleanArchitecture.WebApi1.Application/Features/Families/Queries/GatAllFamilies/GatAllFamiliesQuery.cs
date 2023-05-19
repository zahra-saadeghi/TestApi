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
using CleanArchitecture.WebApi1.Application.Features.Families.Queries.GatAllFamilies;

namespace CleanArchitecture.WebApi1.Application.Features.Families.Queries.GatAllFamilies
{
    public class GatAllFamiliesQuery : IRequest<Response<IEnumerable<GetAllFamiliesViewModel>>>
    {
      
    }
    public class GatAllFamiliesQueryHandler : IRequestHandler<GatAllFamiliesQuery, Response<IEnumerable<GetAllFamiliesViewModel>>>
    {
        private readonly IFamilyRepositoryAsync _Repository;
        private readonly IMapper _mapper;
        public GatAllFamiliesQueryHandler(IFamilyRepositoryAsync Repository, IMapper mapper)
        {
            _Repository = Repository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<GetAllFamiliesViewModel>>> Handle(GatAllFamiliesQuery request, CancellationToken cancellationToken)
        {

            var Entities = await _Repository.GetAllAsync();            
            var ViewModel = _mapper.Map<IEnumerable<GetAllFamiliesViewModel>>(Entities.ToList());            
            return new Response<IEnumerable<GetAllFamiliesViewModel>>(ViewModel);
        }
    }
}
