using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Persistence.Contracts.Auth;

public interface IPasswordService
{
    bool VerifyPassword(string PlainPassword, string HashedPassword);
    string HashPassword(string password);
}
