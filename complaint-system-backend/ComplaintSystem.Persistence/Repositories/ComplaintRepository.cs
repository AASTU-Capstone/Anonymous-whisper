using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComplaintSystem.Persistence.Repositories
{
    public class ComplaintRepository : GenericRepository<Complaint>, IComplaintRepository
    {
        private readonly ComplaintSystemAppDbContext _complaintSystemAppDbContext;

        public ComplaintRepository(ComplaintSystemAppDbContext complaintSystemAppDbContext) : base(complaintSystemAppDbContext)
        {
            _complaintSystemAppDbContext = complaintSystemAppDbContext;
        }

        public async Task<List<Complaint>> GetAcceptedComplaints()
        {
            var complaints = await _complaintSystemAppDbContext.Complaints.Where(c => c.Status.ToLower() == "recieved").ToListAsync();
            return complaints;
        }

        public async Task<List<Complaint>> GetUserComplaints(Guid UserId, string status)
        {
            var complaints = await _complaintSystemAppDbContext.Complaints.Where(c=>c.UserEntityId == UserId && c.Status.ToLower() == status.ToLower() ).ToListAsync();
            return complaints;
        }
    }
}