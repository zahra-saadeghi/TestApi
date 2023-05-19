
using CleanArchitecture.WebApi1.Application.Features.Parents.Commands.CreateParent;
using CleanArchitecture.WebApi1.Application.Features.Parents.Commands.DeleteParentById;
using CleanArchitecture.WebApi1.Application.Features.Parents.Commands.UpdateParent;
using CleanArchitecture.WebApi1.Application.Features.Parents.Queries.GatAllParent;
using CleanArchitecture.WebApi1.Application.Features.Parents.Queries.GetParentById;
using CleanArchitecture.WebApi1.WebApi.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi1.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ParentController : BaseApiController
    {


        // GET: api/<controller>
        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {

            return Ok(await Mediator.Send(new GatAllParentQuery()));
        }

      
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetParentByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HttpPost]
       // [Authorize]
        public async Task<IActionResult> Post(CreateParentCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
      //  [Authorize]
        public async Task<IActionResult> Put(int id, UpdateParentCommand command)
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
            return Ok(await Mediator.Send(new DeleteParentByIdCommand { Id = id }));
        }
    }
}
