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

        public async Task<List<Complaint>> GetComplaintsForAdminByStatus(string status)
        {
            var complaints = await _complaintSystemAppDbContext.Complaints.Where(c => c.Status.ToLower() == status.ToLower()).ToListAsync();
            return complaints;
        }

        public async Task<List<Complaint>> GetMatchingComplaints(string Keyword, string category, string dateOrder)
        {
            var query =  _complaintSystemAppDbContext.Complaints.Where(complaint => EF.Functions.ILike(complaint.Title, "%" + Keyword + "%") || complaint.Tag.Contains(Keyword.ToLower()) ||
            EF.Functions.ILike(complaint.Content, "%" + Keyword + "%") && EF.Functions.ILike(complaint.Category, "%" + category + "%"));
            List<Complaint> complaints;
            if(dateOrder.ToLower() == "asc")
            {
                complaints = await query.OrderBy(comp=>comp.CreatedAt).ToListAsync();
            }
            else
            {
                complaints = await query.OrderByDescending(comp=> comp.CreatedAt).ToListAsync();    
            }

            return complaints;
        }

        public async Task<List<Complaint>> GetUserComplaints(Guid UserId)
        {
            var complaints = await _complaintSystemAppDbContext.Complaints.Where(c => c.UserEntityId == UserId).ToListAsync();
            return complaints;
        }

        public async Task<List<Complaint>> GetUserComplaintsByStatus(Guid UserId, string status)
        {
            var complaints = await _complaintSystemAppDbContext.Complaints.Where(c=>c.UserEntityId == UserId && c.Status.ToLower() == status.ToLower() ).ToListAsync();
            return complaints;
        }
    }
}