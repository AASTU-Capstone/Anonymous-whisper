using ComplaintSystem.Domain.Entities;

namespace ComplaintSystem.Application.Persistence.Contracts
{
    public interface IManagerRepository : IGenericRepository<Manager>
    {
        public Task<Manager> GetMananger(Guid AdminId, string Role);
    }
}