using ComplaintSystem.Domain.Entities;

namespace ComplaintSystem.Application.Persistence.Contracts;
public interface IOtpRepository : IGenericRepository<OTPEntity>
{
    Task<OTPEntity> VerifyOtpCode(string otpCode, Guid userId);
    Task<OTPEntity> FindUser(Guid userId);
}
