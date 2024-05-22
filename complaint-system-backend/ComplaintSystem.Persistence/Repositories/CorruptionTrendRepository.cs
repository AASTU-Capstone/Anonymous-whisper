using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Persistence.Repositories;

public class CorruptionTrendRepository : GenericRepository<CorruptionTrend>, ICorruptionTrendRepository
{
    private readonly ComplaintSystemAppDbContext _appDbContext;
    public CorruptionTrendRepository(ComplaintSystemAppDbContext complaintSystemAppDbContext) : base(complaintSystemAppDbContext)
    {
        _appDbContext = complaintSystemAppDbContext;
    }

    public async Task<CorruptionTrend> GetCorruptionTrendByName(string name)
    {
        var corruptionTrend  = await _appDbContext.CorruptionTrends.FirstOrDefaultAsync(trend=>trend.Name == name.ToLower());
        return corruptionTrend;
    }
}
