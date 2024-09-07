using Microsoft.AspNetCore.Mvc;
using microwave_benner.Application.DTOs;
using microwave_benner.Application.UseCases;

namespace microwave_benner.Server.Controllers
{
    [Route("api/heatings")]
    [ApiController]
    public class PauseOrCancelHeatingTaskController : ControllerBase
    {
        private readonly IPauseOrCancelHeatingTaskUseCase _pauseOrCancelHeatingTaskService;

        public PauseOrCancelHeatingTaskController(IPauseOrCancelHeatingTaskUseCase pauseOrCancelHeatingTaskService)
        {
            _pauseOrCancelHeatingTaskService = pauseOrCancelHeatingTaskService;
        }

        [HttpPost("pause")]
        public async Task<IActionResult> Handle([FromBody] HeatingTaskDTO heatingTaskDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _pauseOrCancelHeatingTaskService.Execute(heatingTaskDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
