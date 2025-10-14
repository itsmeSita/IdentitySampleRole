using Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRoleService
    {
        Task<List<Role>> GetRolesAsync();
        Task<List<string>> GetUserRolesAsync(string email);
        Task<List<string>> AddRolesAsync(string[] roles);
        Task<bool> AddUserRolesAsync(string userEmail, string[] roles);
    }
}
