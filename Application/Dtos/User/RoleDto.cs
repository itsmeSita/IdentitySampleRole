using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.User
{
    public class RoleDto
    {
        [Required]
        public required string RoleName { get; set; } 
    }
}
