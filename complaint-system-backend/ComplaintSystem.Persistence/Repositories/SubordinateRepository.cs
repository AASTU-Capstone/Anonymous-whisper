using ComplaintSystem.Application.DTOs.PaginationDto;
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


        #region Entity Specific Methods Implementation

        // Get Subordinates for a Manager
        public async Task<List<Subordinate>> GetSubordinatesForManager(Guid ManagerId, PaginationDto paginationDto)
        {
            var subordinates = await _complaintSystemAppDbContext.Subordinates.Where(sub => sub.ManagerId == ManagerId)
                .Skip((paginationDto.PageNumber - 1) * paginationDto.PageSize)
                .Take(paginationDto.PageSize)
                .ToListAsync();

            return subordinates;
        }

        // Search Subordinates
        public async Task<List<Subordinate>> SearchSubordinates(string Keyword, PaginationDto paginationDto)
        {
            var subordinates = await _complaintSystemAppDbContext.Subordinates.Where(subordinate => EF.Functions.ILike(subordinate.Name, "%" + Keyword + "%"))
                .Skip((paginationDto.PageNumber - 1) * paginationDto.PageSize)
                .Take(paginationDto.PageSize)
                .ToListAsync();

            return subordinates;
        }


        // Get Subordinate by User Id
        public async Task<Subordinate> GetSubordinateByUserId(Guid UserId)
        {
            var subordinate = await _complaintSystemAppDbContext.Subordinates.FirstOrDefaultAsync(sub => sub.UserEntityId == UserId);
            return subordinate;
        }

        #endregion



        #region Count Methods Implementation

        // Get Subordinates for Manager Count
        public async Task<int> GetSubordinatesForManagerCount(Guid ManagerId)
        {
            var count = await _complaintSystemAppDbContext.Subordinates.Where(sub => sub.ManagerId == ManagerId).CountAsync();
            return count;
        }

        // Search Subordinates Count
        public async Task<int> SearchSubordinatesCount(string Keyword)
        {
            var count = await _complaintSystemAppDbContext.Subordinates.Where(subordinate => EF.Functions.ILike(subordinate.Name, "%" + Keyword + "%")).CountAsync();
            return count;
        }

        #endregion
    }
}