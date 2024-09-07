using microwave_benner.Application.UseCases;
using microwave_benner.Domain.Entities;
using microwave_benner.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace microwave_benner.Application.Services
{
    public class ReadAllHeatingProgramsService : IReadAllHeatingProgramsUseCase
    {
        private readonly IHeatingProgramRepository _heatingProgramRepository;

        public ReadAllHeatingProgramsService(IHeatingProgramRepository heatingProgramRepository)
        {
            _heatingProgramRepository = heatingProgramRepository;
        }

        public async Task<IEnumerable<HeatingProgram>> Execute()
        {
            return await _heatingProgramRepository.GetAll();
        }
    }
}
