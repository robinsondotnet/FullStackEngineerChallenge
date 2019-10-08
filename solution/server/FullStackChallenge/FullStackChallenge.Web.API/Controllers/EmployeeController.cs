using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using FullStackChallenge.Data.Commands;
using FullStackChallenge.Data.Models;
using FullStackChallenge.Data.Dto.Employee;
using FullStackChallenge.Data.Repositories;
using FullStackChallenge.Infra;
using Microsoft.AspNetCore.Mvc;

namespace FullStackChallenge.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        private readonly ICommandHandler<UpdateEmployeeAndReviewCommand> _commandHandler;

        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepository employeeRepository, ICommandHandler<UpdateEmployeeAndReviewCommand> commandHandler)
        {
            _employeeRepository = employeeRepository;
            _commandHandler = commandHandler;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var employees = await _employeeRepository.GetAsync();

            if (employees == null || !employees.Any())
                return NoContent();

            return new JsonResult(employees);
        }

        [HttpPost]
        public Task<ActionResult> Post([FromBody] UpsertEmployeeDto request)
        {
            throw new NotImplementedException();

        }

        [HttpPut]
        public async Task<ActionResult>  Put([FromBody] UpsertEmployeeDto request)
        {
            var employee = new Employee
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Age = request.Age
            };
            
            var command = new UpdateEmployeeAndReviewCommand{ Employee = employee, PerformanceReviewValue = request.PerformanceReviewValue};

            await _commandHandler.HandleAsync(command);

            return Ok();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
