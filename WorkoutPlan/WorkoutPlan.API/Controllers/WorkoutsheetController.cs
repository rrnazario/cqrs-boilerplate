using Microsoft.AspNetCore.Mvc;

namespace WorkoutPlan.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkoutsheetController : ControllerBase
    {

        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
