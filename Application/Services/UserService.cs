using Application.Dtos.User;
using Application.Interfaces;
using Application.Response;
using Application.Response.User;
using AutoMapper;
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
        private readonly IMapper _mapper;
        public UserService(UserManager<ApplicationUser> userManager , RoleManager<Role> roleManager , SignInManager<ApplicationUser> signInManager, IUnitOfWork uow ,IMapper mapper )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _uow = uow;
            _mapper = mapper;

        }

        // Register a new user with a specific role

        public async Task<ServiceResponse<RegisterResponse>> RegisterAsync(RegisterDto registerDto, string role)
        {
            var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
            if (existingUser != null && existingUser.UserName == registerDto.UserName)
            {
                return new ServiceResponse<RegisterResponse>
                {
                    Success = false,
                    Message = "User with this email already exists.",
                };
            }
            else
            {
                var roleExistsCheck = await _roleManager.RoleExistsAsync(role);
                if (!roleExistsCheck)
                {
                    return new ServiceResponse<RegisterResponse>
                    {
                        Success = false,
                        Message = "Specified role does not exist.",
                    };
                }
            }

            var registerUser = _mapper.Map<ApplicationUser>(registerDto);
            var roleExists = await _roleManager.FindByNameAsync(role);
            if (roleExists != null)
            {
                registerUser.Id = Guid.NewGuid().ToString();
                var addedUserRole = await _userManager.CreateAsync(registerUser, registerDto.Password);
                if (addedUserRole != null)
                {
                    if (!addedUserRole.Succeeded)
                    {
                        return new ServiceResponse<RegisterResponse>
                        {
                            Success = false,
                            Message = "User creation failed! Please check user details and try again.",
                        };
                    }
                }
            }

            var userName = registerUser.UserName ?? string.Empty;
            return new ServiceResponse<RegisterResponse>
            {
                Success = true,
                Message = "User registered successfully.",
                Data = new RegisterResponse
                {
                    Register = new List<string> { userName },
                    Roles = new List<string> { role }
                }
            };
        }
    }
    

       
            
        
    
}
