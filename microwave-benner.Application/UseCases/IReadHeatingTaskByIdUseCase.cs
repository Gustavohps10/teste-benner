using System.Threading.Tasks;
using microwave_benner.Application.DTOs;

namespace microwave_benner.Application.UseCases
{
    public interface IReadHeatingTaskByIdUseCase
    {
        Task<HeatingTaskDTO> Execute(int id);
    }
}
