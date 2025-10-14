using Domain.Interfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UnitOfWork :IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IUserRepository Users { get; }

        public UnitOfWork(ApplicationDbContext context, IUserRepository userRepository)
        {
            _context = context;
            Users = userRepository;
        }
        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();

    }
}
