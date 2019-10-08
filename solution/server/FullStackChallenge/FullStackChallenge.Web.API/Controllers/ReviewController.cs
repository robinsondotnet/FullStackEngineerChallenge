using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace FullStackChallenge.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}
