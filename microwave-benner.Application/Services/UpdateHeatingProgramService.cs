using microwave_benner.Application.DTOs;
using microwave_benner.Application.UseCases;
using microwave_benner.Domain.Entities;
using microwave_benner.Domain.Interfaces;
using AutoMapper;
using System;
using System.Threading.Tasks;

public class UpdateHeatingProgramService : IUpdateHeatingProgramUseCase
{
    private readonly IHeatingProgramRepository _heatingProgramRepository;
    private readonly IMapper _mapper;

    public UpdateHeatingProgramService(IHeatingProgramRepository heatingProgramRepository, IMapper mapper)
    {
        _heatingProgramRepository = heatingProgramRepository;
        _mapper = mapper;
    }

    public async Task Execute(HeatingProgramDTO heatingProgramDTO)
    {
        if (heatingProgramDTO.heatingChar.HasValue && heatingProgramDTO.heatingChar.Value == '.')
        {
            throw new ArgumentException("A string de aquecimento não pode conter o caractere '.'");
        }

        if (heatingProgramDTO.heatingChar.HasValue &&
            await _heatingProgramRepository.ExistsHeatingChar(heatingProgramDTO.heatingChar.Value))
        {
            throw new ArgumentException("A string de aquecimento deve ser única.");
        }

        if (!heatingProgramDTO.id.HasValue)
        {
            throw new ArgumentException("ID do programa de aquecimento não pode ser nulo.");
        }

        var heatingProgram = await _heatingProgramRepository.GetById(heatingProgramDTO.id.Value);

        if (heatingProgram == null)
        {
            throw new ArgumentException("Programa de aquecimento não encontrado.");
        }

        _mapper.Map(heatingProgramDTO, heatingProgram);

        await _heatingProgramRepository.Update(heatingProgram);
    }

}
