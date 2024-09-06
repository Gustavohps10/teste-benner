using microwave_benner.Application.DTOs;
using System.Threading.Tasks;

namespace microwave_benner.Application.UseCases
{
    public interface IAddTimeToHeatingTaskUseCase
    {
        Task Execute(HeatingTaskDTO heatingTaskDTO);
    }
}
