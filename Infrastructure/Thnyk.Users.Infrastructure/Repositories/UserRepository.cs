using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Thnyk.Core.Domain.Entities;
using Thnyk.Core.Domain.Interfaces;

namespace Thnyk.Users.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UsersDbContext _context;

        public UserRepository(UsersDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitofWork
        {
            get { return _context; }
        }

        public async Task<int> Add(User user)
        {
            user.CreatedDate = DateTime.Now;
            await _context.AddAsync(user);
            return user.Id;
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetAsync(int Id)
        {
            return await _context.Users.FirstOrDefaultAsync(r => r.Id == Id);
        }

        public void  Update(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}
