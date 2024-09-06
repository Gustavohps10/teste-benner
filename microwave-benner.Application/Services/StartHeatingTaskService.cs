using microwave_benner.Application.UseCases;
using microwave_benner.Domain.Entities;
using microwave_benner.Domain.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using microwave_benner.Application.DTOs;

namespace microwave_benner.Application.Services
{
    public class StartHeatingTaskService : IStartHeatingTaskUseCase
    {
        private readonly IHeatingTaskRepository _heatingTaskRepository;
        private readonly IMapper _mapper;

        public StartHeatingTaskService(IHeatingTaskRepository heatingTaskRepository, IMapper mapper)
        {
            _heatingTaskRepository = heatingTaskRepository;
            _mapper = mapper;
        }

        public async Task Execute(HeatingTaskDTO heatingTaskDTO)
        {

            HeatingTask heatingTask = _mapper.Map<HeatingTask>(heatingTaskDTO);
            heatingTask.Start();

            await _heatingTaskRepository.Insert(heatingTask);
        }
    }
}
