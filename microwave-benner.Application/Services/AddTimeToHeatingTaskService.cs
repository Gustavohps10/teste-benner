using AutoMapper;
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
    public class AddTimeToHeatingTaskService : IAddTimeToHeatingTaskUseCase
    {
        private readonly IHeatingTaskRepository _heatingTaskRepository;
        private readonly IMapper _mapper;

        public AddTimeToHeatingTaskService(IHeatingTaskRepository heatingTaskRepository, IMapper mapper)
        {
            _heatingTaskRepository = heatingTaskRepository;
            _mapper = mapper;
        }

        public async Task Execute(HeatingTaskDTO heatingTaskDTO)
        {
            if (!heatingTaskDTO.id.HasValue)
            {
                throw new ArgumentException("ID é necessário para adicionar tempo.");
            }

            HeatingTask heatingTask = await _heatingTaskRepository.GetById(heatingTaskDTO.id.Value);
            if (heatingTask == null)
            {
                throw new InvalidOperationException("Tarefa de aquecimento não encontrada.");
            }

            heatingTask.AddTime();
            await _heatingTaskRepository.Update(heatingTask);
        }
    }
}
