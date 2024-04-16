using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Persistence.Contracts.Common;

public interface IStringValidator
{
    public string TrimFromStart(string value, string Identifier);
    public string TrimFromEnd(string value, string Identifier);
}
