using microwave_benner.Domain.Entities;
using microwave_benner.Domain.Interfaces;
using microwave_benner.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace microwave_benner.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetById(int id)
        {
            User? user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado.");
            }
            return user;
        }

        public async Task<User> GetByUserName(string userName)
        {
            User? user = await _context.Users
                .FirstOrDefaultAsync(u => u.username == userName);
            if (user == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado.");
            }
            return user;
        }

        public async Task Insert(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task Update(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            User? user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado.");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
