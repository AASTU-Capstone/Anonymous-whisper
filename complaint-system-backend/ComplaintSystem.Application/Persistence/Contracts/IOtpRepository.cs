using  ComplaintSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  ComplaintSystem.Application.Persistence.Contracts;
public interface IOtpRepository : IGenericRepository<OTPEntity>
{
    Task<OTPEntity> VerifyOtpCode(string otpCode, Guid userId);
    Task<OTPEntity> FindUser(Guid userId);
}
