using AutoMapper;
using CleanArchitecture.WebApi1.Application.Features.Families.Commands.CreateFamily;
using CleanArchitecture.WebApi1.Application.Features.Families.Queries.GatAllFamilies;
using CleanArchitecture.WebApi1.Application.Features.Insurances.Commands.CreateInsurance;
using CleanArchitecture.WebApi1.Application.Features.Insurances.Queries.GatAllInsurances;
using CleanArchitecture.WebApi1.Application.Features.Parents.Commands.CreateParent;
using CleanArchitecture.WebApi1.Application.Features.Parents.Queries.GatAllParent;
using CleanArchitecture.WebApi1.Application.Features.Students.Commands.CreateStudent;
using CleanArchitecture.WebApi1.Application.Features.Students.Queries.GatAllStudent;
using CleanArchitecture.WebApi1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.WebApi1.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
  

            //CreateMap<Banner, GetAllBannersViewModel>().ForMember(dest => dest.IsActiveStr, opt => opt.MapFrom(src => src.IsActive ? "Enable" : "Disable"))
            //    .ForMember(dest => dest.ExpDateStr, opt => opt.MapFrom(src => src.ExpDate.ToShortDateString())).ReverseMap();          
            //CreateMap<CreateBannerCommand, Banner>();
            //CreateMap<Banner, GetAllBannersForAppViewModel>().ForMember(dest => dest.IsActiveStr, opt => opt.MapFrom(src => src.IsActive ? "Enable" : "Disable"))
            //    .ForMember(dest => dest.ExpDateStr, opt => opt.MapFrom(src => src.ExpDate.ToShortDateString())).ReverseMap();


            CreateMap<Family, GetAllFamiliesViewModel>().ReverseMap();
            CreateMap<CreateFamilyCommand, Family>();

            CreateMap<Insurance, GetAllInsuranceViewModel>().ReverseMap();
            CreateMap<CreateInsuranceCommand, Insurance>();

            CreateMap<Parent, GetAllParentViewModel>().ReverseMap();
            CreateMap<CreateParentCommand, Parent>();

            CreateMap<Student, GetAllStudentViewModel>().ReverseMap();
            CreateMap<CreateStudentCommand, Student>();
        }
    }
}
