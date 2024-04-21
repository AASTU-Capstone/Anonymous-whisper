using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Domain.Entities;

namespace ComplaintSystem.Persistence.Repositories
{
    public class AdminRepository : GenericRepository<Admin>, IAdminRepository
    {
        private readonly ComplaintSystemAppDbContext _complaintSystemAppDbContext;
        public AdminRepository(ComplaintSystemAppDbContext complaintSystemAppDbContext) : base(complaintSystemAppDbContext)
        {
            _complaintSystemAppDbContext = complaintSystemAppDbContext;
        }
    }
}