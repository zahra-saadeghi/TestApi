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

namespace CleanArchitecture.WebApi1.Application.Features.Parents.Queries.GatAllParent
{
    public class GatAllParentQuery : IRequest<Response<IEnumerable<GetAllParentViewModel>>>
    {
      
    }
    public class GatAllParentQueryHandler : IRequestHandler<GatAllParentQuery, Response<IEnumerable<GetAllParentViewModel>>>
    {
        private readonly IParentRepositoryAsync _Repository;
        private readonly IMapper _mapper;
        public GatAllParentQueryHandler(IParentRepositoryAsync Repository, IMapper mapper)
        {
            _Repository = Repository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<GetAllParentViewModel>>> Handle(GatAllParentQuery request, CancellationToken cancellationToken)
        {

            var Entities = await _Repository.GetAllAsync();            
            var ViewModel = _mapper.Map<IEnumerable<GetAllParentViewModel>>(Entities.ToList());            
            return new Response<IEnumerable<GetAllParentViewModel>>(ViewModel);
        }
    }
}
