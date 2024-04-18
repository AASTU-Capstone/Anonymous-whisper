using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Domain.Entities;

namespace ComplaintSystem.Persistence.Repositories
{
    public class SubordinateRepository : GenericRepository<Subordinate>, ISubordinateRepository
    {
        private readonly ComplaintSystemAppDbContext _complaintSystemAppDbContext;
        public SubordinateRepository(ComplaintSystemAppDbContext complaintSystemAppDbContext) : base(complaintSystemAppDbContext)
        {
            _complaintSystemAppDbContext = complaintSystemAppDbContext;
        }
    }
}