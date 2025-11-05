using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            
        }
        public async Task<IEnumerable<Product>> GetProductsWithCategoryAsync()
        {
            
           return await _context.Products
                    .Include(p => p.Category)
                    .ToListAsync();

           
        }
    }
}
