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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<string>>> AddRoles([FromBody] string[] roles)
        {
            var created = await _roleService.AddRolesAsync(roles);
            return Ok(created);
        }

        //Assign roles to a user by email
        [HttpPost("assign")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AssignRoles([FromQuery] string email, [FromBody] string[] roles)
        {
            var success = await _roleService.AddUserRolesAsync(email, roles);
            if (success)
                return Ok("Roles assigned successfully.");

            return BadRequest("Failed to assign roles.");
        }
    }
}
