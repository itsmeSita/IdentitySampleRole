using Application.Dtos.User;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentitySampleRole.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("register/{role}")]
        public async Task<IActionResult> Register(RegisterDto registerDto, string role)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterAsync(registerDto, role);

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
    }
}
