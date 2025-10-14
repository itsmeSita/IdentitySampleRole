using Domain.Entities.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {

        Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password);
        Task<ApplicationUser?> GetUserByEmailAsync(string email);
    }
}
