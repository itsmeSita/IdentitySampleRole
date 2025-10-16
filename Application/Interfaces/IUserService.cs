using Application.Dtos.User;
using Application.Response;
using Application.Response.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
       Task<ServiceResponse<RegisterResponse>> RegisterAsync(RegisterDto registerDto , string role);

    }
}
