using microwave_benner.Domain.Entities;
using microwave_benner.Domain.Interfaces;
using microwave_benner.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace microwave_benner.Infra.Data.Repositories
{
    public class HeatingProgramRepository : IHeatingProgramRepository
    {
        private readonly ApplicationDbContext _context;

        public HeatingProgramRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HeatingProgram>> GetAll()
        {
            return await _context.HeatingPrograms.ToListAsync();
        }

        public async Task<HeatingProgram> GetById(int id)
        {
            HeatingProgram? heatingProgram = await _context.HeatingPrograms.FindAsync(id);
            if (heatingProgram == null)
            {
                throw new KeyNotFoundException("Programa de aquecimento não encontrado.");
            }
            return heatingProgram;
        }

        public async Task Insert(HeatingProgram heatingProgram)
        {
            await _context.HeatingPrograms.AddAsync(heatingProgram);
            await _context.SaveChangesAsync();
        }

        public async Task Update(HeatingProgram heatingProgram)
        {
            _context.HeatingPrograms.Update(heatingProgram);
            await _context.SaveChangesAsync();
        }
    }
}