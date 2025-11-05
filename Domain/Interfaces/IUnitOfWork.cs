using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; } 
        IGenericRepository<Product> Products { get; }
        IGenericRepository<Category> Categories { get; }
        Task<int> SaveAsync();
    }
}
