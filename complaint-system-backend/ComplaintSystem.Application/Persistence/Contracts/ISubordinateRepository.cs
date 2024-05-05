using ComplaintSystem.Domain.Entities;

namespace ComplaintSystem.Application.Persistence.Contracts
{
    public interface ISubordinateRepository : IGenericRepository<Subordinate>
    {
        public Task<List<Subordinate>> GetSubordinatesForManager(Guid ManagerId);
    }
}