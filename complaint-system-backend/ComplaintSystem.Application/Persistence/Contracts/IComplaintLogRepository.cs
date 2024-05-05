using ComplaintSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Persistence.Contracts;
public interface IComplaintLogRepository : IGenericRepository<ComplaintLog>
{
    public Task<List<ComplaintLog>> GetForManager(Guid ManagerId);
    public Task<List<ComplaintLog>> GetForAdmin(Guid AdminId);
    public Task<List<ComplaintLog>> GetForSubordinate(Guid SubordinateId);
}
