using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Domain.Entities;

namespace ComplaintSystem.Persistence.Repositories
{
    public class ManagerRepository : GenericRepository<Manager>, IManagerRepository
    {
        private readonly ComplaintSystemAppDbContext _complaintSystemAppDbContext;
        public ManagerRepository(ComplaintSystemAppDbContext complaintSystemAppDbContext) : base(complaintSystemAppDbContext)
        {
            _complaintSystemAppDbContext = complaintSystemAppDbContext;
        }
    }
}