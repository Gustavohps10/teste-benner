using Microsoft.AspNetCore.Mvc;
using microwave_benner.Application.DTOs;
using microwave_benner.Application.UseCases;
using System.Threading.Tasks;

namespace microwave_benner.Server.Controllers
{
    [Route("api/heatings")]
    [ApiController]
    public class StartHeatingTaskController : ControllerBase
    {
        private readonly IStartHeatingTaskUseCase _startHeatingTaskService;

        public StartHeatingTaskController(IStartHeatingTaskUseCase startHeatingTaskService)
        {
            _startHeatingTaskService = startHeatingTaskService;
        }

        [HttpPost]
        public async Task<IActionResult> Handle([FromBody] HeatingTaskDTO heatingTaskDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                HeatingTaskDTO responseDTO = await _startHeatingTaskService.Execute(heatingTaskDTO);
                return Ok(responseDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao processar sua solicitação.");
            }
        }
    }
}
