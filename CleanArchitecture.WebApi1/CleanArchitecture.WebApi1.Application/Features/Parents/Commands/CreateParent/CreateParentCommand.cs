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

namespace CleanArchitecture.WebApi1.Application.Features.Parents.Commands.CreateParent
{
    public partial class CreateParentCommand : IRequest<Response<int>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }

        public int FamilyId { get; set; }

        public int? ContactInfoId { get; set; }
    }
    public class CreateParentCommandHandler : IRequestHandler<CreateParentCommand, Response<int>>
    {
        private readonly IParentRepositoryAsync _Repository;
      private readonly IMapper _mapper;
        public CreateParentCommandHandler(IParentRepositoryAsync Repository, IMapper mapper)
        {
            _Repository = Repository;
         _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateParentCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Parent>(request);
        
            entity.Created = DateTime.Now;
            await _Repository.AddAsync(entity);
            return new Response<int>(entity.Id);
        }
    }
}
