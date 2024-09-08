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

        [HttpPost("{id}/pause")]
        public async Task<IActionResult> Handle(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _pauseOrCancelHeatingTaskService.Execute(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
