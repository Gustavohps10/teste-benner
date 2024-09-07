using microwave_benner.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace microwave_benner.Domain.Interfaces
{
    public interface IHeatingProgramRepository
    {
        Task<IEnumerable<HeatingProgram>> GetAll();
        Task<HeatingProgram> GetById(int id);
        Task Insert(HeatingProgram heatingProgram);
        Task Update(HeatingProgram heatingProgram);
    }
}