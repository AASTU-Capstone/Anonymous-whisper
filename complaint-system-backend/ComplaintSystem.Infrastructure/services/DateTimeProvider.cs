using   ComplaintSystem.Application.Persistence.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace   ComplaintSystem.Infrastructure.services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime CreateTime => DateTime.Now;
}
