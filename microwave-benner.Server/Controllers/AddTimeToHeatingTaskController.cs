using Microsoft.AspNetCore.Mvc;
using microwave_benner.Application.DTOs;
using microwave_benner.Application.UseCases;

namespace microwave_benner.Server.Controllers
{
    [Route("api/heating")]
    [ApiController]
    public class AddTimeToHeatingTaskController : ControllerBase
    {
        private readonly IAddTimeToHeatingTaskUseCase _addTimeToHeatingTaskUseCase;

        public AddTimeToHeatingTaskController(IAddTimeToHeatingTaskUseCase addTimeToHeatingTaskUseCase)
        {
            _addTimeToHeatingTaskUseCase = addTimeToHeatingTaskUseCase;
        }

        [HttpPatch("add-time")]
        public async Task<IActionResult> handle([FromBody] HeatingTaskDTO heatingTaskDTO)
        {
            try
            {
                await _addTimeToHeatingTaskUseCase.Execute(heatingTaskDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
