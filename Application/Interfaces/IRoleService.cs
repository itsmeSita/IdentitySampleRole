using Application.Dtos.User;
using Application.Response;
using Application.Response.Role;
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
        Task<ServiceResponse<CreateRoleResponse>> CreateRoleAsync(string roleName);

        // Service response ma aafu chai chaiyeko response pathaune Response folder ma banayera
        Task<ServiceResponse<AddRoleResponse>> AddRolesAsync(string[] roles);
        Task <ServiceResponse<AddRoleResponse>> AddUserRolesAsync(AssignRoleDto assignRoleDto);
        Task<List<Role>> GetRolesAsync();
    }
}
