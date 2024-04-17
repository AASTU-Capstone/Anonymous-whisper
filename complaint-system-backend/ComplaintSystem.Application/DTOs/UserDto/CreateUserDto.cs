using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  ComplaintSystem.Application.DTOs.UserDto
{
    public class CreateUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string User_Type { get; set; }
    }
}
