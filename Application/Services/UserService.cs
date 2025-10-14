using Application.Dtos.User;
using Application.Interfaces;
using Domain.Entities.User;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUnitOfWork _uow;
        public UserService(UserManager<ApplicationUser> userManager , RoleManager<Role> roleManager , SignInManager<ApplicationUser> signInManager, IUnitOfWork uow  )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _uow = uow;

        }
        public async Task<string> RegisterAsync(RegisterDto dto)
        {
            var user = new ApplicationUser
            {
                UserName = dto.UserName,
                Email = dto.Email
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (result.Succeeded)
            {
                return "Registration Successful";
            }
            else
            {
                return "Registration Failed";
            }
        }

        public async Task<string> LoginAsync(LoginDto dto)
        {
           var user = await _userManager.FindByNameAsync(dto.UserName);
            if (user == null)
            {
                return "User not found";
            }
            var result = await _signInManager.PasswordSignInAsync(user, dto.Password, false, false);
            if (result.Succeeded)
            {
                return "Login Successful";
            }
            else
            {
                return "Invalid credentials";
            }
        }
    }
}
