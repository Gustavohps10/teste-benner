using microwave_benner.Application.DTOs;
using microwave_benner.Domain.Entities;
using microwave_benner.Domain.Interfaces;
using System;
using System.Threading.Tasks;
using AutoMapper;

namespace microwave_benner.Application.UseCases
{
    public class ReadHeatingTaskByIdService : IReadHeatingTaskByIdUseCase
    {
        private readonly IHeatingTaskRepository _heatingTaskRepository;
        private readonly IMapper _mapper;

        public ReadHeatingTaskByIdService(IHeatingTaskRepository heatingTaskRepository, IMapper mapper)
        {
            _heatingTaskRepository = heatingTaskRepository;
            _mapper = mapper;
        }

        public async Task<HeatingTaskDTO> Execute(int id)
        {
            try
            {
                HeatingTask heatingTask = await _heatingTaskRepository.GetById(id);
                return _mapper.Map<HeatingTaskDTO>(heatingTask);
            }
            catch (KeyNotFoundException)
            {
                throw new ArgumentException("Tarefa de aquecimento não encontrada.");
            }
        }
    }
}