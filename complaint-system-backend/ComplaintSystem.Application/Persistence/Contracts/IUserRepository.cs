using  ComplaintSystem.Domain.Entities;

namespace  ComplaintSystem.Application.Persistence.Contracts
{
    public interface IUserRepository : IGenericRepository<UserEntity>
    {
        public Task<UserEntity> GetByEmail(string email);
    }
}