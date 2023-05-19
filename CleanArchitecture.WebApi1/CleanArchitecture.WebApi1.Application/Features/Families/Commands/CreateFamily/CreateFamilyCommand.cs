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

namespace CleanArchitecture.WebApi1.Application.Features.Families.Commands.CreateFamily
{
    public partial class CreateFamilyCommand : IRequest<Response<int>>
    {
        public int? BusinessId { get; set; }
        public string Email { get; set; }
        public string ExternalId { get; set; }
        public int? InsuranceId { get; set; }
        public int? ContactInfoId { get; set; }
    }
    public class CreateBannerCommandHandler : IRequestHandler<CreateFamilyCommand, Response<int>>
    {
        private readonly IFamilyRepositoryAsync _Repository;
      private readonly IMapper _mapper;
        public CreateBannerCommandHandler(IFamilyRepositoryAsync Repository, IMapper mapper)
        {
            _Repository = Repository;
         _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateFamilyCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Family>(request);
        
            entity.Created = DateTime.Now;
            await _Repository.AddAsync(entity);
            return new Response<int>(entity.Id);
        }
    }
}
