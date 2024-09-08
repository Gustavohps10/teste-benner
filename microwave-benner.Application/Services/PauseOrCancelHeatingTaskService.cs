using microwave_benner.Application.DTOs;
using microwave_benner.Application.UseCases;
using microwave_benner.Domain.Entities;
using microwave_benner.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microwave_benner.Application.Services
{
    public class PauseOrCancelHeatingTaskService : IPauseOrCancelHeatingTaskUseCase
    {
        private readonly IHeatingTaskRepository _heatingTaskRepository;

        public PauseOrCancelHeatingTaskService(IHeatingTaskRepository heatingTaskRepository)
        {
            _heatingTaskRepository = heatingTaskRepository;
        }

        public async Task Execute(int id)
        {
            HeatingTask heatingTask = await _heatingTaskRepository.GetById(id);
            if (heatingTask == null)
            {
                throw new InvalidOperationException("Tarefa de aquecimento não encontrada.");
            }

            if (heatingTask.IsFinished())
            {
                throw new InvalidOperationException("A tarefa de aquecimento já foi concluída e não pode ser pausada ou cancelada.");
            }

            if (heatingTask.IsRunning())
            {
                heatingTask.Pause();
            }
            else
            {
                heatingTask.Interrupt();
            }

            await _heatingTaskRepository.Update(heatingTask);
        }
    }
}
