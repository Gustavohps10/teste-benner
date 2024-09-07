using AutoMapper;
using microwave_benner.Application.DTOs;
using microwave_benner.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microwave_benner.Application.Mappings
{
    public class DomainToDTOMappingProfile: Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<HeatingTask, HeatingTaskDTO>().ReverseMap();
            CreateMap<HeatingProgramDTO, HeatingProgram>()
                .ConstructUsing(src => new HeatingProgram(
                    src.name!,
                    src.food!,
                    src.time ?? 0,
                    src.power ?? 0,
                    src.heatingChar![0],
                    src.instructions
                ));

            CreateMap<HeatingProgram, HeatingProgramDTO>();
        }
    }
}
