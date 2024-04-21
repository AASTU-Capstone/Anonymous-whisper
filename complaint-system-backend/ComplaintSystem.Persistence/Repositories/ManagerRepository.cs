using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComplaintSystem.Persistence.Repositories
{
    public class ManagerRepository : GenericRepository<Manager>, IManagerRepository
    {
        private readonly ComplaintSystemAppDbContext _complaintSystemAppDbContext;
        public ManagerRepository(ComplaintSystemAppDbContext complaintSystemAppDbContext) : base(complaintSystemAppDbContext)
        {
            _complaintSystemAppDbContext = complaintSystemAppDbContext;
        }

        public async Task<Manager> GetMananger(Guid AdminId, string Role)
        {
            var manager = await _complaintSystemAppDbContext.Managers.FirstOrDefaultAsync(m=>m.AdminId ==  AdminId && m.Role == Role);
            return manager;
        }
    }
}