using microwave_benner.Application.DTOs;
using microwave_benner.Application.UseCases;
using microwave_benner.Domain.Entities;
using microwave_benner.Domain.Interfaces;
using AutoMapper;
using System.Threading.Tasks;

namespace microwave_benner.Application.Services
{
    public class CreateHeatingProgramService : ICreateHeatingProgramUseCase
    {
        private readonly IHeatingProgramRepository _heatingProgramRepository;
        private readonly IMapper _mapper;

        public CreateHeatingProgramService(IHeatingProgramRepository heatingProgramRepository, IMapper mapper)
        {
            _heatingProgramRepository = heatingProgramRepository;
            _mapper = mapper;
        }

        public async Task Execute(HeatingProgramDTO heatingProgramDTO)
        {
            HeatingProgram heatingProgram = _mapper.Map<HeatingProgram>(heatingProgramDTO);

            await _heatingProgramRepository.Insert(heatingProgram);
        }
    }
}
