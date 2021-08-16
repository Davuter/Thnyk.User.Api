using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Thnyk.Core.Domain.Entities;
using Thnyk.Core.Domain.Interfaces;

namespace Thnyk.Users.Infrastructure
{
    public class UsersDbContext : DbContext, IUnitOfWork
    {
        public UsersDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
            System.Diagnostics.Debug.WriteLine("UsersDbContext::ctor ->" + this.GetHashCode());
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await base.SaveChangesAsync(cancellationToken);

            return true;
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UsersDbContext).Assembly);
        }
    }
}
