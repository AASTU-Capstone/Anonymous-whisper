using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Domain.Entities;
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
    }
}
