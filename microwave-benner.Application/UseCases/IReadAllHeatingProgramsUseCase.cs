using microwave_benner.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace microwave_benner.Application.UseCases
{
    public interface IReadAllHeatingProgramsUseCase
    {
        Task<IEnumerable<HeatingProgram>> Execute();
    }
}