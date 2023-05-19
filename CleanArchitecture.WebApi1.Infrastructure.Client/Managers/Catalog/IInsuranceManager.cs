
using CleanArchitecture.WebApi1.Application.Features.Insurances.Commands.CreateInsurance;
using CleanArchitecture.WebApi1.Application.Features.Insurances.Commands.UpdateInsurance;
using CleanArchitecture.WebApi1.Application.Features.Insurances.Queries.GatAllInsurances;
using CleanArchitecture.WebApi1.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi1.Infrastructure.Client.Managers
{

    public interface IInsuranceManager : IManager
    {
        Task<IResult<List<GetAllInsuranceViewModel>>> GetAllAsync();

        Task<IResult<int>> CreateAsync(CreateInsuranceCommand request);

        Task<IResult<int>> UpdateAsync(UpdateInsuranceCommand request);

        Task<IResult<int>> DeleteAsync(int id);
    }
}
