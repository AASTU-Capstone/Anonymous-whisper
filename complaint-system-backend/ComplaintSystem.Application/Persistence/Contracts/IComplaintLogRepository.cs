using ComplaintSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Persistence.Contracts;
public interface IComplaintLogRepository : IGenericRepository<ComplaintLog>
{

}
