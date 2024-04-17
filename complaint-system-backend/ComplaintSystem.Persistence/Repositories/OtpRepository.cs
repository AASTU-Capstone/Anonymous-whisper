using Microsoft.EntityFrameworkCore;
using  ComplaintSystem.Application.Persistence.Contracts;
using  ComplaintSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  ComplaintSystem.Persistence.Repositories
{
    public class OtpRepository : GenericRepository<OTPEntity>, IOtpRepository
    {
        private readonly  ComplaintSystemAppDbContext _context;
        public OtpRepository( ComplaintSystemAppDbContext sparkTankAppDbContext):base(sparkTankAppDbContext)
        {
            _context = sparkTankAppDbContext;
        }

        public async Task<OTPEntity> FindUser(Guid userId)
        {
            var otp = await _context.OTPs.Where(o => o.EntityId == userId).FirstOrDefaultAsync();
            return otp;
        }

        public async Task<OTPEntity> VerifyOtpCode(string otpCode, Guid userId)
        {
            var otp = await _context.OTPs.Where(o=>o.Otp == otpCode).FirstOrDefaultAsync();
            
            return otp;

        }

    }
}
