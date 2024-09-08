using System.Threading.Tasks;

namespace microwave_benner.Application.UseCases
{
    public interface IDeleteHeatingProgramUseCase
    {
        Task Execute(int id);
    }
}
