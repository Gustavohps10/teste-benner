using Microsoft.AspNetCore.Mvc;
using microwave_benner.Application.DTOs;
using microwave_benner.Application.UseCases;

namespace microwave_benner.Server.Controllers
{
    [Route("api/heating")]
    [ApiController]
    public class StartHeatingTaskController : ControllerBase
    {
        private readonly IStartHeatingTaskUseCase _startHeatingTaskService;

        public StartHeatingTaskController(IStartHeatingTaskUseCase startHeatingTaskService)
        {
            _startHeatingTaskService = startHeatingTaskService;
        }

        [HttpPost]
        public async Task<IActionResult> handle([FromBody] HeatingTaskDTO heatingTaskDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _startHeatingTaskService.Execute(heatingTaskDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao iniciar o aquecimento.");
            }
        }
    }
}
