using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FullStackChallenge.Data.Repositories.Interfaces;
using FullStackChallenge.Web.API.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FullStackChallenge.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var employees = await _employeeRepository.GetAsync();

            if (employees == null || !employees.Any())
                return NoContent();

            return new JsonResult(employees);
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Feedback
        [HttpPost]
        public void Post([FromBody] UpsertEmployeeDto request)
        {
        }

        // PUT: api/Feedback/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] UpsertEmployeeDto request)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
