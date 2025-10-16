﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.User
{
    public class AssignRoleDto
    {
        public required string Email { get; set; } 
        public required List<string> Roles { get; set; } 
    }
}
