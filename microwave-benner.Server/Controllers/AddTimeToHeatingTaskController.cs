using Microsoft.AspNetCore.Mvc;
using microwave_benner.Application.DTOs;
using microwave_benner.Application.UseCases;

namespace microwave_benner.Server.Controllers
{
    [Route("api/heating")]
    [ApiController]
    public class AddTimeToHeatingTaskController : ControllerBase
    {
        private readonly IAddTimeToHeatingTaskUseCase _addTimeToHeatingTaskService;

        public AddTimeToHeatingTaskController(IAddTimeToHeatingTaskUseCase addTimeToHeatingTaskService)
        {
            _addTimeToHeatingTaskService = addTimeToHeatingTaskService;
        }

        [HttpPatch("add-time")]
        public async Task<IActionResult> handle([FromBody] HeatingTaskDTO heatingTaskDTO)
        {
            try
            {
                await _addTimeToHeatingTaskService.Execute(heatingTaskDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
