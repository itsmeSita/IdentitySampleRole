using Application.Dtos.User;
using Application.Interfaces;
using Application.Response;
using Application.Response.Role;
using Domain.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{

    public class RoleService : IRoleService
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public RoleService(RoleManager<Role> roleManager, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;

        }
        // Get all roles
        public async Task<List<Role>> GetRolesAsync()
        {
            return await _roleManager.Roles.ToListAsync();
        }
        // Get roles of a user by email
        public async Task<List<string>> GetUserRolesAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new Exception("User not found.");

            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }

        // Add new roles
        public async Task<ServiceResponse<AddRoleResponse>> AddRolesAsync(string[] roles)
        {
            var createdRoles = new List<string>();
            var roleExisting = new List<string>();
            foreach (var roleName in roles)
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    var role = new Role { Name = roleName };
                    var result = await _roleManager.CreateAsync(role);

                    if (result.Succeeded)
                    {
                        createdRoles.Add(roleName);
                    }
                    else
                    {
                        // unsuccessful vako case ni return huna paryo 
                       roleExisting.Add(roleName);
                    }
                }
                else 
                {
                    // role exist vako case ma k garne vanera garauna paryo
                    return new ServiceResponse<AddRoleResponse>
                    {
                        Success = false,
                        Message = $"Failed to create role: {roleName}",
                        Data = new AddRoleResponse
                                { Roles = roleExisting }
                    };
                }
            }

            return new ServiceResponse<AddRoleResponse>
            {
                Data = new AddRoleResponse { Roles = createdRoles },
                Message = "Roles processed successfully.",
                Success = true
            };
        }

        // Assign roles to a user by email
        public async Task<bool> AddUserRolesAsync(AssignRoleDto assignRoleDto)
        {
            var user = await _userManager.FindByEmailAsync(assignRoleDto.Email);
            if (user == null)
                throw new Exception("User not found.");

            // Ensure roles exist before assigning
            foreach (var roleName in assignRoleDto.Roles)
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                    await _roleManager.CreateAsync(new Role { Name = roleName });
                // role exist gardena vane k Garne handle garna paryo 
            }

            var result = await _userManager.AddToRolesAsync(user, assignRoleDto.Roles);
            // yeha ni check garna paryo succeed vaxa ki nai 
            //ani appropriate response pathauna paryo
            return result.Succeeded;
        }
    }
}
