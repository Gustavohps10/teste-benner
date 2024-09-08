using Microsoft.AspNetCore.Mvc;
using microwave_benner.Application.DTOs;
using microwave_benner.Application.UseCases;
using System;
using System.Threading.Tasks;

namespace microwave_benner.Server.Controllers
{
    [Route("api/heatings")]
    [ApiController]
    public class UpdateHeatingProgramController : ControllerBase
    {
        private readonly IUpdateHeatingProgramUseCase _updateHeatingProgramService;

        public UpdateHeatingProgramController(IUpdateHeatingProgramUseCase updateHeatingProgramService)
        {
            _updateHeatingProgramService = updateHeatingProgramService;
        }

        [HttpPut]
        public async Task<IActionResult> Handle([FromBody] HeatingProgramDTO heatingProgramDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _updateHeatingProgramService.Execute(heatingProgramDTO);
                return Ok("Programa de aquecimento atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao atualizar o programa de aquecimento.");
            }
        }
    }
}
