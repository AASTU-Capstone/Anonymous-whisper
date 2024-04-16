using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Persistence.Contracts.Common;

public interface IOpenAiServices
{
    public Task<string> ExtractPdfToJson(string texts);
    public Task<string> MakeitProffessional(string text);
}
