using Microsoft.AspNetCore.Mvc;
using microwave_benner.Application.DTOs;
using microwave_benner.Application.UseCases;

namespace microwave_benner.Server.Controllers
{
    [Route("api/heating")]
    [ApiController]
    public class PauseOrCancelHeatingTaskController : ControllerBase
    {
        private readonly IPauseOrCancelHeatingTaskUseCase _pauseOrCancelHeatingTaskUseCase;

        public PauseOrCancelHeatingTaskController(IPauseOrCancelHeatingTaskUseCase pauseOrCancelHeatingTaskUseCase)
        {
            _pauseOrCancelHeatingTaskUseCase = pauseOrCancelHeatingTaskUseCase;
        }

        [HttpPost("pause-or-cancel")]
        public async Task<IActionResult> Handle([FromBody] HeatingTaskDTO heatingTaskDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _pauseOrCancelHeatingTaskUseCase.Execute(heatingTaskDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
