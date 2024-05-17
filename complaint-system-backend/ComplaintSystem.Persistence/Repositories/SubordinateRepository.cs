using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComplaintSystem.Persistence.Repositories
{
    public class SubordinateRepository : GenericRepository<Subordinate>, ISubordinateRepository
    {
        private readonly ComplaintSystemAppDbContext _complaintSystemAppDbContext;
        public SubordinateRepository(ComplaintSystemAppDbContext complaintSystemAppDbContext) : base(complaintSystemAppDbContext)
        {
            _complaintSystemAppDbContext = complaintSystemAppDbContext;
        }

        public async Task<List<Subordinate>> GetSubordinatesForManager(Guid ManagerId)
        {
            var subordinates = await _complaintSystemAppDbContext.Subordinates.Where(sub => sub.ManagerId == ManagerId).ToListAsync();
            return subordinates;
        }

        public async Task<List<Subordinate>> SearchSubordinates(string Keyword)
        {
            var subordinates = await _complaintSystemAppDbContext.Subordinates.Where(subordinate=>EF.Functions.ILike(subordinate.Name, "%"+Keyword+"%")).ToListAsync();
            return subordinates;
        }
    }
}