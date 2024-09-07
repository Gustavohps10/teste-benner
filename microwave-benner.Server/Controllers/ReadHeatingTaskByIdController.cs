using Microsoft.AspNetCore.Mvc;
using microwave_benner.Application.DTOs;
using microwave_benner.Application.UseCases;
using System;
using System.Threading.Tasks;

namespace microwave_benner.Server.Controllers
{
    [Route("api/heatings")]
    [ApiController]
    public class ReadHeatingTaskByIdController : ControllerBase
    {
        private readonly IReadHeatingTaskByIdUseCase _readHeatingTaskByIdService;

        public ReadHeatingTaskByIdController(IReadHeatingTaskByIdUseCase readHeatingTaskByIdService)
        {
            _readHeatingTaskByIdService = readHeatingTaskByIdService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Handle(int id)
        {
            try
            {
                var heatingTaskDTO = await _readHeatingTaskByIdService.Execute(id);
                return Ok(heatingTaskDTO);
            }
            catch (ArgumentException)
            {
                return NotFound("Tarefa de aquecimento não encontrada.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao obter a tarefa de aquecimento");
            }
        }
    }
}
