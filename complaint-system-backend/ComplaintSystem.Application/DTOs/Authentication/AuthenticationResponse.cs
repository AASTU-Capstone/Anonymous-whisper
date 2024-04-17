using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace  ComplaintSystem.Application.DTOs.Authentication
{
    public class AuthenticationResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public bool IsVerified { get; set; }
    }
}