using microwave_benner.Domain.Entities;
using microwave_benner.Domain.Interfaces;
using microwave_benner.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace microwave_benner.Infra.Data.Repositories
{
    public class HeatingTaskRepository : IHeatingTaskRepository
    {
        private readonly ApplicationDbContext _context;

        public HeatingTaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<HeatingTask> GetById(int id)
        {
            HeatingTask? heatingTask = await _context.HeatingTasks.FindAsync(id);
            if (heatingTask == null)
            {
                throw new KeyNotFoundException("Tarefa de aquecimento não encontrada.");
            }
            return heatingTask;
        }

        public async Task<IEnumerable<HeatingTask>> GetAll()
        {
            return await _context.HeatingTasks.ToListAsync();
        }

        public async Task Insert(HeatingTask heatingTask)
        {
            await _context.HeatingTasks.AddAsync(heatingTask);
            await _context.SaveChangesAsync();
        }

        public async Task Update(HeatingTask heatingTask)
        {
            _context.HeatingTasks.Update(heatingTask);
            await _context.SaveChangesAsync();
        }
    }
}
