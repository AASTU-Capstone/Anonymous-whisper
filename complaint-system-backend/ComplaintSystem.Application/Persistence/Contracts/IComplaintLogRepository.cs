using ComplaintSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Persistence.Contracts;
public interface IComplaintLogRepository : IGenericRepository<ComplaintLog>
{
    public Task<List<ComplaintLog>> GetForManager(Guid ManagerId, string Status);
    public Task<List<ComplaintLog>> GetForAdmin(Guid AdminId, string Status);
    public Task<List<ComplaintLog>> GetForSubordinate(Guid SubordinateId, string Status);
    public Task<List<ComplaintLog>> GetByStatus(string Status);
}
