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
        private readonly IReadAllHeatingProgramsUseCase _readAllHeatingProgramsUseCase;

        public ReadAllHeatingProgramsController(IReadAllHeatingProgramsUseCase readAllHeatingProgramsUseCase)
        {
            _readAllHeatingProgramsUseCase = readAllHeatingProgramsUseCase;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HeatingProgram>>> Handle()
        {
            var programs = await _readAllHeatingProgramsUseCase.Execute();
            return Ok(programs);
        }
    }
}
