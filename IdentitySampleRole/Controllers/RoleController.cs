using Application.Dtos.User;
using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace IdentitySampleRole.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost]
        [Route("create-role")]
        public async Task<IActionResult> CreateRole([FromBody] RoleDto role)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleService.CreateRoleAsync(role.RoleName);
                if (result.Success)
                {
                    return Ok(result.Data);
                }
                else
                {
                    return BadRequest(result.Message);
                }
            }

            return BadRequest("Invalid model state.");
        }

        [HttpPost]
        [Route("add-roles")]
        public async Task<IActionResult> AddRoles([FromBody] string[] roles)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleService.AddRolesAsync(roles);
                if (result.Success)
                {
                    return Ok(result.Data);
                }
                else
                {
                    return BadRequest(result.Message);
                }
            }
            return BadRequest("Invalid model state.");
        }

        [HttpPost]
        [Route("assign-roles")]
        public async Task<IActionResult> AssignRoles([FromBody] AssignRoleDto assignRoleDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleService.AddUserRolesAsync(assignRoleDto);
                if (result.Success) 
                {
                    return Ok("Roles assigned successfully.");
                }
                else
                {
                    return BadRequest(result.Message); 
                }
            }
            return BadRequest("Invalid model state.");
        }

        [HttpGet]
        [Route("get-roles")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleService.GetRolesAsync();
            return Ok(roles);
        }

    }
}
