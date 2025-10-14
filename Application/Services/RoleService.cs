using Application.Interfaces;
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
        public async Task<List<string>> AddRolesAsync(string[] roles)
        {
            var createdRoles = new List<string>();

            foreach (var roleName in roles)
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    var role = new Role { Name = roleName };
                    var result = await _roleManager.CreateAsync(role);

                    if (result.Succeeded)
                        createdRoles.Add(roleName);
                }
            }

            return createdRoles;
        }

        // Assign roles to a user by email
        public async Task<bool> AddUserRolesAsync(string userEmail, string[] roles)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
                throw new Exception("User not found.");

            // Ensure roles exist before assigning
            foreach (var roleName in roles)
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                    await _roleManager.CreateAsync(new Role { Name = roleName });
            }

            var result = await _userManager.AddToRolesAsync(user, roles);
            return result.Succeeded;
        }
    }
}
