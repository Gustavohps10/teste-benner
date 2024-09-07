using Microsoft.AspNetCore.Mvc;
using microwave_benner.Application.UseCases;

namespace microwave_benner.Server.Controllers
{
    [Route("api/heatings")]
    [ApiController]
    public class ResumeHeatingTaskController : ControllerBase
    {
        private readonly IResumeHeatingTaskUseCase _resumeHeatingTaskService;

        public ResumeHeatingTaskController(IResumeHeatingTaskUseCase resumeHeatingTaskService)
        {
            _resumeHeatingTaskService = resumeHeatingTaskService;
        }

        [HttpPost("resume/{id}")]
        public async Task<IActionResult> ResumeHeatingTask(int id)
        {
            try
            {
                await _resumeHeatingTaskService.Execute(id);
                return Ok("Tarefa de aquecimento retomada com sucesso.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao retomar a tarefa.");
            }
        }
    }
}
