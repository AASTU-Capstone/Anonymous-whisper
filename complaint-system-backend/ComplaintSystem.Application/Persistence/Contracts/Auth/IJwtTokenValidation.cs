using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  ComplaintSystem.Application.Persistence.Contracts.Auth;

public interface IJwtTokenValidation
{
    bool ValidateToken(string token);
    Guid ExtractUserIdFromToken(string token);
}
