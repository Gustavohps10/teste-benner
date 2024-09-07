using microwave_benner.Application.DTOs;
using microwave_benner.Application.UseCases;
using microwave_benner.Domain.Interfaces;
using AutoMapper;
using System;
using System.Threading.Tasks;

namespace microwave_benner.Application.Services
{
    public class ResumeHeatingTaskService : IResumeHeatingTaskUseCase
    {
        private readonly IHeatingTaskRepository _heatingTaskRepository;
        private readonly IMapper _mapper;

        public ResumeHeatingTaskService(
            IHeatingTaskRepository heatingTaskRepository,
            IMapper mapper)
        {
            _heatingTaskRepository = heatingTaskRepository;
            _mapper = mapper;
        }

        public async Task<HeatingTaskDTO> Execute(int id)
        {
            var heatingTask = await _heatingTaskRepository.GetById(id);
            if (heatingTask == null)
            {
                throw new InvalidOperationException("Tarefa de aquecimento não encontrada.");
            }

            if (heatingTask.IsFinished())
            {
                throw new InvalidOperationException("A tarefa já foi concluída e não pode ser retomada.");
            }

            if (!heatingTask.IsRunning())
            {
                heatingTask.Resume();
                await _heatingTaskRepository.Update(heatingTask);
            }
            else
            {
                throw new InvalidOperationException("A tarefa já está em execução.");
            }

            return _mapper.Map<HeatingTaskDTO>(heatingTask);
        }
    }
}
