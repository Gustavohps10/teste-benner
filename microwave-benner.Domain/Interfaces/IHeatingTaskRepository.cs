using microwave_benner.Domain.Entities;

namespace microwave_benner.Domain.Interfaces
{
    public interface IHeatingTaskRepository
    {
        Task<IEnumerable<HeatingTask>> GetAll();
        Task<HeatingTask> GetById(int id);
        Task Insert(HeatingTask heatingTask);
        Task Update(HeatingTask heatingTask);
    }
}
