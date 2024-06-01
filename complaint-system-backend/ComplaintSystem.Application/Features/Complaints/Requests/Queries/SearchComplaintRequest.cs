using ComplaintSystem.Application.DTOs.PaginationDto;
using ComplaintSystem.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.Complaints.Requests.Queries;
public class SearchComplaintRequest : IRequest<PaginatedResponseClass>
{
    public string Keyword { get; set; }
    public PaginationDto PaginationDto { get; set; }
    public string? Category { get; set; }
    public string DateOrder { get; set; }
}
