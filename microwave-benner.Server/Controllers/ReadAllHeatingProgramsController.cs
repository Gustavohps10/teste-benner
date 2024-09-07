using Microsoft.AspNetCore.Mvc;
using microwave_benner.Application.UseCases;
using microwave_benner.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace microwave_benner.API.Controllers
{
    [ApiController]
    [Route("api/programs")]
    public class ReadAllHeatingProgramsController : ControllerBase
    {
        private readonly IReadAllHeatingProgramsUseCase _readAllHeatingProgramsService;

        public ReadAllHeatingProgramsController(IReadAllHeatingProgramsUseCase readAllHeatingProgramsService)
        {
            _readAllHeatingProgramsService = readAllHeatingProgramsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HeatingProgram>>> Handle()
        {
            var programs = await _readAllHeatingProgramsService.Execute();
            return Ok(programs);
        }
    }
}
