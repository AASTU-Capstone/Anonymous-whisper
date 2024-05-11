using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace  ComplaintSystem.Application.DTOs.UserDto
{
    public class GetUserDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
    }
}