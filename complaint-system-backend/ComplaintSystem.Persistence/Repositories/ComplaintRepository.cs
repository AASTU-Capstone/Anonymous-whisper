using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Domain.Entities;

namespace ComplaintSystem.Persistence.Repositories
{
    public class ComplaintRepository : GenericRepository<Complaint>, IComplaintRepository
    {
        private readonly ComplaintSystemAppDbContext _complaintSystemAppDbContext;

        public ComplaintRepository(ComplaintSystemAppDbContext complaintSystemAppDbContext) : base(complaintSystemAppDbContext)
        {
            _complaintSystemAppDbContext = complaintSystemAppDbContext;
        }
    }
}