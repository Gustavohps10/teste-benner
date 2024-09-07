using microwave_benner.Application.DTOs;
using System.Threading.Tasks;

namespace microwave_benner.Application.UseCases
{
    public interface IStartHeatingTaskUseCase
    {
        Task<HeatingTaskDTO> Execute(HeatingTaskDTO heatingTaskDTO);
    }
}