using Microsoft.EntityFrameworkCore;
using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Domain.Entities;

namespace ComplaintSystem.Persistence.Repositories
{
    public class UserRepository : GenericRepository<UserEntity>, IUserRepository
    {
        private readonly ComplaintSystemAppDbContext _complaintSystemAppDbContext;

        public UserRepository(ComplaintSystemAppDbContext complaintSystemAppDbContext) : base(complaintSystemAppDbContext)
        {
            _complaintSystemAppDbContext = complaintSystemAppDbContext;
        }

        public async Task<UserEntity> GetByEmail(string email)
        {
            var user = await _complaintSystemAppDbContext.Users.SingleOrDefaultAsync(x => x.Email == email);
            if (user == null)
            {
                return null;
            }
            return user;
        }

    }
}