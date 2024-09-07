using microwave_benner.Application.DTOs;
using microwave_benner.Domain.Entities;
using microwave_benner.Domain.Interfaces;
using AutoMapper;
using System;
using System.Threading.Tasks;
using microwave_benner.Application.UseCases;

namespace microwave_benner.Application.Services
{
    public class StartHeatingTaskService : IStartHeatingTaskUseCase
    {
        private readonly IHeatingTaskRepository _heatingTaskRepository;
        private readonly IHeatingProgramRepository _heatingProgramRepository;
        private readonly IMapper _mapper;

        public StartHeatingTaskService(
            IHeatingTaskRepository heatingTaskRepository,
            IHeatingProgramRepository heatingProgramRepository,
            IMapper mapper)
        {
            _heatingTaskRepository = heatingTaskRepository;
            _heatingProgramRepository = heatingProgramRepository;
            _mapper = mapper;
        }

        public async Task<HeatingTaskDTO> Execute(HeatingTaskDTO heatingTaskDTO)
        {
            if (heatingTaskDTO.heatingProgramId.HasValue)
            {
                HeatingProgram? heatingProgram = await _heatingProgramRepository.GetById(heatingTaskDTO.heatingProgramId.Value);

                if (heatingProgram == null)
                {
                    throw new ArgumentException("Programa de aquecimento não encontrado.");
                }

                heatingTaskDTO.time = heatingTaskDTO.time ?? heatingProgram.time;
                heatingTaskDTO.power = heatingTaskDTO.power ?? heatingProgram.power;
            }

            HeatingTask heatingTask = _mapper.Map<HeatingTask>(heatingTaskDTO);
            heatingTask.Start();
            await _heatingTaskRepository.Insert(heatingTask);

            // Mapear HeatingTask para HeatingTaskDTO
            HeatingTaskDTO responseDTO = _mapper.Map<HeatingTaskDTO>(heatingTask);
            return responseDTO;
        }
    }
}
