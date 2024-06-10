using ComplaintSystem.Application.DTOs.PaginationDto;
using ComplaintSystem.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.ComplaintLogs.Requests.Queries;
public class SearchComplaintLogRequest : IRequest<PaginatedResponseClass>
{
    public string Keyword { get; set; }
    public string Status { get; set; }
    public PaginationDto Pagination { get; set; }
}
