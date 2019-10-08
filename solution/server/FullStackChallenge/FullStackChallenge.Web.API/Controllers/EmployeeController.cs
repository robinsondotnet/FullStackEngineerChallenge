using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FullStackChallenge.Data.Commands;
using FullStackChallenge.Data.Models;
using FullStackChallenge.Data.Dto.Employee;
using FullStackChallenge.Data.Queries;
using FullStackChallenge.Infra;
using Microsoft.AspNetCore.Mvc;

namespace FullStackChallenge.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IQueryHandler<GetEmployeesWithReviewAndAssigneeQuery, List<EmployeeDto>> _queryHandler;

        private readonly ICommandHandler<UpdateEmployeeReviewAndAssigneeCommand> _commandHandler;

        private readonly IMapper _mapper;

        public EmployeeController(IQueryHandler<GetEmployeesWithReviewAndAssigneeQuery, List<EmployeeDto>> queryHandler, ICommandHandler<UpdateEmployeeReviewAndAssigneeCommand> commandHandler)
        {
            _queryHandler = queryHandler;
            _commandHandler = commandHandler;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var query = new GetEmployeesWithReviewAndAssigneeQuery();
            
            var employees = await _queryHandler.RetrieveAsync(query);

            if (employees == null || !employees.Any())
                return NoContent();

            return new JsonResult(employees);
        }
        
        [HttpPut]
        public async Task<ActionResult>  Put([FromBody] UpdateEmployeeDto request)
        {
            // TODO: Use mapper of translator (Validation would be good too)
            var employee = new Employee
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Age = request.Age
            };
            
            var command = new UpdateEmployeeReviewAndAssigneeCommand
            {
                Employee = employee,
                PerformanceReviewValue = request.PerformanceReviewValue,
                ReviewFeedbackAssigneeIds = request.ReviewFeedbackAssigneeIds
            };

            await _commandHandler.HandleAsync(command);

            return Ok();
        }

        [HttpPost]
        public Task<ActionResult> Post([FromBody] UpdateEmployeeDto request)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
