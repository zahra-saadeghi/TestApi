
using CleanArchitecture.WebApi1.Application.Features.Families.Commands.CreateFamily;
using CleanArchitecture.WebApi1.Application.Features.Families.Commands.UpdateFamily;
using CleanArchitecture.WebApi1.Application.Features.Families.Queries.GatAllFamilies;
using CleanArchitecture.WebApi1.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi1.Infrastructure.Client.Managers
{

    public interface IFamilyManager : IManager
    {
        Task<IResult<List<GetAllFamiliesViewModel>>> GetAllAsync();

        Task<IResult<int>> CreateAsync(CreateFamilyCommand request);

        Task<IResult<int>> UpdateAsync(UpdateFamilyCommand request);

        Task<IResult<int>> DeleteAsync(int id);
    }
}
