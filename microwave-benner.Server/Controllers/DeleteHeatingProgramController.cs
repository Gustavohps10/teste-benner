using Microsoft.AspNetCore.Mvc;
using microwave_benner.Application.UseCases;
using System;
using System.Threading.Tasks;

namespace microwave_benner.Server.Controllers
{
    [Route("api/programs")]
    [ApiController]
    public class DeleteHeatingProgramController : ControllerBase
    {
        private readonly IDeleteHeatingProgramUseCase _deleteHeatingProgramService;

        public DeleteHeatingProgramController(IDeleteHeatingProgramUseCase deleteHeatingProgramService)
        {
            _deleteHeatingProgramService = deleteHeatingProgramService;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Handle(int id)
        {
            try
            {
                await _deleteHeatingProgramService.Execute(id);
                return Ok("Programa de aquecimento excluído com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao excluir o programa de aquecimento.");
            }
        }
    }
}
