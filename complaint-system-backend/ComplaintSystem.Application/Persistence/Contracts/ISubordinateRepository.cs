using ComplaintSystem.Domain.Entities;

namespace ComplaintSystem.Application.Persistence.Contracts
{
    public interface ISubordinateRepository : IGenericRepository<Subordinate>
    {
        public Task<List<Subordinate>> GetSubordinatesForManager(Guid ManagerId);
        public Task<List<Subordinate>> SearchSubordinates(string Keyword);
        public Task<Subordinate> GetSubordinateByUserId(Guid UserId);
    }
}