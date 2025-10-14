using Application.Dtos.User;
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
       Task<string> RegisterAsync(RegisterDto dto);
       Task<string> LoginAsync(LoginDto dto);
    }
}
