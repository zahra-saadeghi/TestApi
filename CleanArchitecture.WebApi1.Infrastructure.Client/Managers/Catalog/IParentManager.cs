using CleanArchitecture.WebApi1.Application.Features.Parents.Commands.CreateParent;
using CleanArchitecture.WebApi1.Application.Features.Parents.Commands.UpdateParent;
using CleanArchitecture.WebApi1.Application.Features.Parents.Queries.GatAllParent;
using CleanArchitecture.WebApi1.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi1.Infrastructure.Client.Managers
{

    public interface IParentManager : IManager
    {
        Task<IResult<List<GetAllParentViewModel>>> GetAllAsync();

        Task<IResult<int>> CreateAsync(CreateParentCommand request);

        Task<IResult<int>> UpdateAsync(UpdateParentCommand request);

        Task<IResult<int>> DeleteAsync(int id);
    }
}
