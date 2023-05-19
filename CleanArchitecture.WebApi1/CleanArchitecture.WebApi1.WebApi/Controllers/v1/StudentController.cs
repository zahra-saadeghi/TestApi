
using CleanArchitecture.WebApi1.Application.Features.Families.Commands.CreateFamily;
using CleanArchitecture.WebApi1.Application.Features.Families.Commands.DeleteFamilyById;
using CleanArchitecture.WebApi1.Application.Features.Families.Commands.UpdateFamily;
using CleanArchitecture.WebApi1.Application.Features.Families.Queries.GatAllFamilies;
using CleanArchitecture.WebApi1.Application.Features.Families.Queries.GetFamilyById;
using CleanArchitecture.WebApi1.Application.Features.Students.Commands.CreateStudent;
using CleanArchitecture.WebApi1.Application.Features.Students.Commands.DeleteStudentById;
using CleanArchitecture.WebApi1.Application.Features.Students.Commands.UpdateStudent;
using CleanArchitecture.WebApi1.Application.Features.Students.Queries.GatAllStudent;
using CleanArchitecture.WebApi1.Application.Features.Students.Queries.GetStudentById;
using CleanArchitecture.WebApi1.WebApi.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi1.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class StudentController : BaseApiController
    {


        // GET: api/<controller>
        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {

            return Ok(await Mediator.Send(new GatAllStudentQuery()));
        }

      
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetStudentByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HttpPost]
       // [Authorize]
        public async Task<IActionResult> Post(CreateStudentCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
      //  [Authorize]
        public async Task<IActionResult> Put(int id, UpdateStudentCommand command)
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
            return Ok(await Mediator.Send(new DeleteStudentByIdCommand { Id = id }));
        }
    }
}
