using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ComplaintSystem.Application.DTOs.PaginationDto;

namespace ComplaintSystem.Persistence.Repositories
{
    public class ComplaintRepository : GenericRepository<Complaint>, IComplaintRepository
    {
        private readonly ComplaintSystemAppDbContext _complaintSystemAppDbContext;

        public ComplaintRepository(ComplaintSystemAppDbContext complaintSystemAppDbContext) : base(complaintSystemAppDbContext)
        {
            _complaintSystemAppDbContext = complaintSystemAppDbContext;
        }

        #region Entity Specific Methods Implementation


        // Get all complaints with status recieved
        public async Task<List<Complaint>> GetAcceptedComplaints(PaginationDto paginationDto)
        {
            var complaints = await _complaintSystemAppDbContext.Complaints
                .Where(c => c.Status.ToLower() == "recieved")
                .OrderByDescending(c => c.CreatedAt)
                .Skip((paginationDto.PageNumber - 1) * paginationDto.PageSize)
                .Take(paginationDto.PageSize)
                .ToListAsync();
            return complaints;
        }


        // Get all complaints of a user with status not rejected
        public async Task<List<Complaint>> GetUserComplaints(Guid UserId, PaginationDto criteria)
        {
            var complaints = await _complaintSystemAppDbContext.Complaints
                .Where(c => c.UserEntityId == UserId)
                .Where(c => c.Status.ToLower() != "rejected")
                .Skip((criteria.PageNumber - 1) * criteria.PageSize)
                .Take(criteria.PageSize)
                .ToListAsync();

            return complaints;
        }

        // Get all complaints of a user that are rejected
        public async Task<List<Complaint>> GetUserComplaintsByStatus(Guid UserId, string status, PaginationDto criteria)
        {
            var complaints = await _complaintSystemAppDbContext.Complaints
                .Where(c => c.UserEntityId == UserId && c.Status.ToLower() == status.ToLower())
                .Skip((criteria.PageNumber - 1) * criteria.PageSize)
                .Take(criteria.PageSize)
                .ToListAsync();

            return complaints;
        }

        // Get all complaints that match the keyword
        public async Task<List<Complaint>> GetMatchingComplaints(string Keyword, PaginationDto paginationDto)
        {
            var complaints = await _complaintSystemAppDbContext.Complaints
                .Where(complaint => EF.Functions.ILike(complaint.Title, "%" + Keyword + "%") || complaint.Tag.Contains(Keyword.ToLower()) ||
            EF.Functions.ILike(complaint.Content, "%" + Keyword + "%"))
                .Skip((paginationDto.PageNumber - 1) * paginationDto.PageSize)
                .Take(paginationDto.PageSize)
                .ToListAsync();

            return complaints;
        }

        #endregion



        #region Count Methods Implementation


        // Get the count of complaints with status recieved
        public Task<int> GetRecievedComplaintsCount()
        {
            return _complaintSystemAppDbContext.Complaints.CountAsync(c => c.Status.ToLower() == "recieved");
        }

        // Get the count of complaints of a user that are not rejected
        public async Task<int> GetUserAcceptedComplaintsCount(Guid UserId)
        {
            return await _complaintSystemAppDbContext.Complaints.CountAsync(c => c.UserEntityId == UserId && c.Status.ToLower() != "rejected");
        }


        // Get the count of complaints of a user that are rejected
        public async Task<int> GetUserRejectedComplaintsCount(Guid UserId)
        {
            return await _complaintSystemAppDbContext.Complaints.CountAsync(c => c.UserEntityId == UserId && c.Status.ToLower() == "rejected");
        }

        // Get the count of complaints that match the keyword
        public async Task<int> GetMatchingComplaintsCount(string Keyword)
        {
            return await _complaintSystemAppDbContext.Complaints
                .CountAsync(complaint => EF.Functions.ILike(complaint.Title, "%" + Keyword + "%") || complaint.Tag.Contains(Keyword.ToLower()) ||
            EF.Functions.ILike(complaint.Content, "%" + Keyword + "%"));
        }

        #endregion
    }
}