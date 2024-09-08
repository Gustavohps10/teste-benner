using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using microwave_benner.Application.DTOs;
using System.Threading.Tasks;

public interface IUpdateHeatingProgramUseCase
{
    Task Execute(HeatingProgramDTO heatingProgramDTO);
}