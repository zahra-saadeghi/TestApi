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

namespace CleanArchitecture.WebApi1.Application.Features.Students.Queries.GatAllStudent
{
    public class GatAllStudentQuery : IRequest<Response<IEnumerable<GetAllStudentViewModel>>>
    {
      
    }
    public class GatAllStudentQueryHandler : IRequestHandler<GatAllStudentQuery, Response<IEnumerable<GetAllStudentViewModel>>>
    {
        private readonly IStudentRepositoryAsync _Repository;
        private readonly IMapper _mapper;
        public GatAllStudentQueryHandler(IStudentRepositoryAsync Repository, IMapper mapper)
        {
            _Repository = Repository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<GetAllStudentViewModel>>> Handle(GatAllStudentQuery request, CancellationToken cancellationToken)
        {

            var Entities = await _Repository.GetAllAsync();            
            var ViewModel = _mapper.Map<IEnumerable<GetAllStudentViewModel>>(Entities.ToList());            
            return new Response<IEnumerable<GetAllStudentViewModel>>(ViewModel);
        }
    }
}
