using ComplaintSystem.Application.DTOs.PaginationDto;
using ComplaintSystem.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.Complaints.Requests.Queries;
public class GetAllComplaintsRequest : IRequest<BaseResponseClass>
{
    public PaginationDto PaginationDto { get; set; }
}
