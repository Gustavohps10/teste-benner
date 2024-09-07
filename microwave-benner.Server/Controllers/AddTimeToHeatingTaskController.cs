using Microsoft.AspNetCore.Mvc;
using microwave_benner.Application.DTOs;
using microwave_benner.Application.UseCases;
using System;
using System.Threading.Tasks;

namespace microwave_benner.Server.Controllers
{
    [Route("api/heatings")]
    [ApiController]
    public class AddTimeToHeatingTaskController : ControllerBase
    {
        private readonly IAddTimeToHeatingTaskUseCase _addTimeToHeatingTaskService;

        public AddTimeToHeatingTaskController(IAddTimeToHeatingTaskUseCase addTimeToHeatingTaskService)
        {
            _addTimeToHeatingTaskService = addTimeToHeatingTaskService;
        }

        [HttpPost("{id}/add-time")]
        public async Task<IActionResult> AddTimeToHeatingTask(int id)
        {
            try
            {
                HeatingTaskDTO heatingTaskDTO = await _addTimeToHeatingTaskService.Execute(id);
                return Ok(heatingTaskDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
