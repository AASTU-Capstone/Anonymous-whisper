using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Persistence.Repositories
{
    public class ComplaintLogRepository : GenericRepository<ComplaintLog>, IComplaintLogRepository
    {
        public readonly ComplaintSystemAppDbContext _complaintSystemAppDbContext;
        public ComplaintLogRepository(ComplaintSystemAppDbContext complaintSystemAppDbContext): base(complaintSystemAppDbContext)
        {
            _complaintSystemAppDbContext = complaintSystemAppDbContext;
        }

        public async Task<List<ComplaintLog>> GetForAdmin(Guid AdminId)
        {
            var complaints = await _complaintSystemAppDbContext.ComplaintLogs.Where(log => log.AdminId == AdminId).ToListAsync();
            return complaints;
        }

        public async Task<List<ComplaintLog>> GetForManager(Guid ManagerId)
        {
            var complaints = await _complaintSystemAppDbContext.ComplaintLogs.Where(log=> log.ManagerId == ManagerId).ToListAsync();
            return complaints;
        }

        public async Task<List<ComplaintLog>> GetForSubordinate(Guid SubordinateId)
        {
            var complaints = await _complaintSystemAppDbContext.ComplaintLogs.Where(log => log.SubordinateId == SubordinateId).ToListAsync();
            return complaints;
        }
    }
}
