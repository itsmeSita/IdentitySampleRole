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

        //Get all roles
        [HttpGet()]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleService.GetRolesAsync();
            return Ok(roles);
        }
        
        [HttpGet("user/{email}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<string>>> GetUserRoles(string email)
        {
            var userRoles = await _roleService.GetUserRolesAsync(email);
            return Ok(userRoles);
        }
        //Add new roles
        [HttpPost("add")]
       // [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<string>>> AddRoles([FromBody] string[] roles)
        {
            if (roles == null || roles.Length == 0)
            {
                return BadRequest("No roles provided.");
            }
            else
            {
                var result = await _roleService.AddRolesAsync(roles);
                return Ok(result);
            }
        }

        //Assign roles to a user by email
        [HttpPost("assign")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AssignRoles([FromBody] AssignRoleDto assignRole)
        {
            if (ModelState.IsValid)
            {
                // modelstate le user input DTO sanga match garxa ki gardaina bhanera check garxa
                var success = await _roleService.AddUserRolesAsync(assignRole);
                if (success)
                {  // services bata aako response return garne
                    return Ok(success);
                }
                else 
                {      
                    return BadRequest("Failed to assign roles.");
                }
            }
            return BadRequest("Failed to assign roles.");
        }
    }
}
