using microwave_benner.Application.UseCases;
using microwave_benner.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace microwave_benner.Application.Services
{
    public class DeleteHeatingProgramService : IDeleteHeatingProgramUseCase
    {
        private readonly IHeatingProgramRepository _heatingProgramRepository;

        public DeleteHeatingProgramService(IHeatingProgramRepository heatingProgramRepository)
        {
            _heatingProgramRepository = heatingProgramRepository;
        }

        public async Task Execute(int id)
        {
            var heatingProgram = await _heatingProgramRepository.GetById(id);
            if (heatingProgram == null)
            {
                throw new ArgumentException("Programa de aquecimento não encontrado.");
            }

            await _heatingProgramRepository.Delete(id);
        }
    }
}