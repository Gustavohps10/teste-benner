using System.Collections.Generic;
using System.Threading.Tasks;
using microwave_benner.Domain.Entities;

namespace microwave_benner.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(int id);
        Task<User> GetByUserName(string username);
        Task Insert(User user);
        Task Update(User user);
        Task Delete(int id);
    }
}
