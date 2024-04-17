using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using  ComplaintSystem.Application.Persistence.Contracts;
using  ComplaintSystem.Domain.Entities;

namespace  ComplaintSystem.Persistence.Repositories
{
    public class UserRepository : GenericRepository<UserEntity>, IUserRepository
    {
        private readonly  ComplaintSystemAppDbContext _sparkTankAppDbContext;
        
        public UserRepository( ComplaintSystemAppDbContext sparkTankAppDbContext) : base(sparkTankAppDbContext)
        {
            _sparkTankAppDbContext = sparkTankAppDbContext;
        }

        public async Task<UserEntity> GetByEmail(string email)
        {
            var user = await _sparkTankAppDbContext.Users.SingleOrDefaultAsync(x => x.Email == email);
            if (user == null)
            {
                return null;
            }
            return user;
        }

    }
}