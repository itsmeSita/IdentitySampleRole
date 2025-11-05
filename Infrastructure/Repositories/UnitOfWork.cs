using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        
        public IUserRepository Users { get; }

        public IGenericRepository<Product> Products { get; }

        public IGenericRepository<Category> Categories { get; }

        public UnitOfWork(ApplicationDbContext context , IUserRepository userRepository , IGenericRepository<Product> productRepository)
        {
            _context = context;
            Users = userRepository;
            Products = productRepository;
            Categories = new GenericRepository<Category>(_context);
        }

        public async  Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
          _context.Dispose();
        }
    }
}
