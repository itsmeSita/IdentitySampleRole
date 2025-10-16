using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.User
{
    public class RegisterDto
    {
        [Required]
        public string UserName { get; set; } = null!;

        [Required, EmailAddress]
        public required string Email { get; set; }

        [Required, Phone]
        public required string PhoneNumber { get; set; } 

        [Required , MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public required string Password { get; set; } 

        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public required string ConfirmPassword { get; set; } 
    }
}
