using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Thnyk.Core.Domain.Entities;

namespace Thnyk.Core.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<int> Add(User user);

        void Update(User user);

        void Delete(User user);

        Task<User> GetAsync(int Id);

        Task<List<User>> GetAllAsync();
    }
}
