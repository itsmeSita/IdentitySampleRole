using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.User
{
    public class AssignRoleDto
    {
        public string Email { get; set; }
        public List<string> Roles { get; set; }
    }
}
