using microwave_benner.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microwave_benner.Application.UseCases
{
    public interface IPauseOrCancelHeatingTaskUseCase
    {
        Task Execute(int id);
    }
}
