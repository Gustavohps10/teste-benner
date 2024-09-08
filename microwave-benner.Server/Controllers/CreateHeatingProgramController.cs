using Microsoft.AspNetCore.Mvc;
using microwave_benner.Application.DTOs;
using microwave_benner.Application.UseCases;
using System;
using System.Threading.Tasks;

namespace microwave_benner.Server.Controllers
{
    [Route("api/programs")]
    [ApiController]
    public class CreatingHeatingProgramController : ControllerBase
    {
        private readonly ICreateHeatingProgramUseCase _createHeatingProgramService;

        public CreatingHeatingProgramController(ICreateHeatingProgramUseCase createHeatingProgramService)
        {
            _createHeatingProgramService = createHeatingProgramService;
        }

        [HttpPost]
        public async Task<IActionResult> Handle([FromBody] HeatingProgramDTO heatingProgramDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _createHeatingProgramService.Execute(heatingProgramDTO);
                return Ok("Programa de aquecimento criado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao criar o programa de aquecimento.");
            }
        }
    }
}