using Application.Dtos.User;
using Application.Interfaces;
using Application.Response;
using Application.Response.Role;
using Domain.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.SqlTypes;
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

        // create role if not exist

        public async Task<ServiceResponse<CreateRoleResponse>> CreateRoleAsync(string roleName)
        {
            bool roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (roleExists)
            {
                return new ServiceResponse<CreateRoleResponse>
                {
                    Success = false,
                    Message = $"{roleName} already exists.",
                };
            }
            var role = new Role { Name = roleName };
            var createdRole = await _roleManager.CreateAsync(role);
            if (createdRole.Succeeded)
            {
                return new ServiceResponse<CreateRoleResponse>
                {
                    Success = true,
                    Message = $"{roleName} created successfully.",
                    Data = new CreateRoleResponse { RoleName = roleName }
                };
            }
            else
            {
                return new ServiceResponse<CreateRoleResponse>
                {
                    Success = false,
                    Message = $"Failed to create role: {roleName}.",
                };
            }
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

        // Assign roles to user

        public async Task<ServiceResponse<AddRoleResponse>> AddUserRolesAsync(AssignRoleDto assignRoleDto)
        {
            // Find the user by email
            var user = await _userManager.FindByEmailAsync(assignRoleDto.Email);
            if (user == null)
            {
                return new ServiceResponse<AddRoleResponse>
                {
                    Success = false,
                    Message = "User not found.",
                    Data = null
                };
            }

            var rolesCreated = new List<string>();
            var rolesExisting = new List<string>();
            var rolesFailed = new List<string>();

            // Ensure all roles exist
            foreach (var roleName in assignRoleDto.Roles)
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    var createResult = await _roleManager.CreateAsync(new Role { Name = roleName });
                    if (createResult.Succeeded)
                        rolesCreated.Add(roleName);
                    else
                        rolesFailed.Add(roleName);
                }
                else
                {
                    rolesExisting.Add(roleName);
                }
            }

           
            if (rolesFailed.Count > 0)
            {
                return new ServiceResponse<AddRoleResponse>
                {
                    Success = false,
                    Message = $"Failed to create roles: {rolesFailed}",
                    Data = new AddRoleResponse { Roles = rolesFailed }
                };
            }

           
            var assignResult = await _userManager.AddToRolesAsync(user, assignRoleDto.Roles);

            if (!assignResult.Succeeded)
            {
                return new ServiceResponse<AddRoleResponse>
                {
                    Success = false,
                    Message = $"Failed to assign roles: {assignResult}",
                    Data = new AddRoleResponse { Roles = assignRoleDto.Roles.ToList() }
                };
            }

            
            return new ServiceResponse<AddRoleResponse>
            {
                Success = true,
                Message = $"Roles assigned successfully to {assignRoleDto.Email}.",
                Data = new AddRoleResponse { Roles = assignRoleDto.Roles.ToList() }
            };
        }

        public async Task<List<Role>> GetRolesAsync()
        {
            return await _roleManager.Roles.ToListAsync();
        }
    }
}

      

      


