using FullStackChallenge.Data.Dto.Feedback;
using Microsoft.AspNetCore.Mvc;

namespace FullStackChallenge.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        [HttpPost]
        public void Post([FromBody] CreateFeedbackDto request)
        {
        }
    }
}
