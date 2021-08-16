using System;
using System.Collections.Generic;
using System.Text;

namespace Thnyk.Core.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IUnitOfWork UnitofWork { get; }
    }
}
