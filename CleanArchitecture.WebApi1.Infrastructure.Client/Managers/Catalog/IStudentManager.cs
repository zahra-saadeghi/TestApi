using CleanArchitecture.WebApi1.Application.Features.Students.Commands.CreateStudent;
using CleanArchitecture.WebApi1.Application.Features.Students.Commands.UpdateStudent;
using CleanArchitecture.WebApi1.Application.Features.Students.Queries.GatAllStudent;
using CleanArchitecture.WebApi1.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi1.Infrastructure.Client.Managers
{

    public interface IStudentManager : IManager
    {
        Task<IResult<List<GetAllStudentViewModel>>> GetAllAsync();

        Task<IResult<int>> CreateAsync(CreateStudentCommand request);

        Task<IResult<int>> UpdateAsync(UpdateStudentCommand request);

        Task<IResult<int>> DeleteAsync(int id);
    }
}
