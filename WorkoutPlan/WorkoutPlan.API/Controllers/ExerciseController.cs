using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkoutPlan.Application.Commands.Exercises;
using WorkoutPlan.Application.Queries.Exercises;

namespace WorkoutPlan.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExerciseController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ExerciseController> _logger;

        public ExerciseController(IMediator mediator, ILogger<ExerciseController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromBody] CreateExerciseCommand exercise)
        {
            try
            {
                var exerciseId = await _mediator.Send(exercise);

                return Created(nameof(Add), new { Id = exerciseId });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                return BadRequest();
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(Guid Id)
        {
            try
            {
                var exercise = await _mediator.Send(new GetExerciseByIdQuery(Id));

                return exercise == default ? NotFound() : Ok(exercise);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                return BadRequest();
            }
        }
    }
}
