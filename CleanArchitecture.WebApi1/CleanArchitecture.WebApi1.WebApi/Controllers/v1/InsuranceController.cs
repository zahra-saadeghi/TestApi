using CleanArchitecture.WebApi1.Application.Features.Insurances.Commands.CreateInsurance;
using CleanArchitecture.WebApi1.Application.Features.Insurances.Commands.DeleteInsuranceById;
using CleanArchitecture.WebApi1.Application.Features.Insurances.Commands.UpdateInsurance;
using CleanArchitecture.WebApi1.Application.Features.Insurances.Queries.GatAllInsurances;
using CleanArchitecture.WebApi1.Application.Features.Insurances.Queries.GetInsuranceById;
using CleanArchitecture.WebApi1.WebApi.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi1.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class InsuranceController : BaseApiController
    {


        // GET: api/<controller>
        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {

            return Ok(await Mediator.Send(new GatAllInsuranceQuery()));
        }

      
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetInsuranceByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HttpPost]
       // [Authorize]
        public async Task<IActionResult> Post(CreateInsuranceCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
      //  [Authorize]
        public async Task<IActionResult> Put(int id, UpdateInsuranceCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
       // [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteInsuranceByIdCommand { Id = id }));
        }
    }
}
