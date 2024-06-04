using ComplaintSystem.Application.DTOs.PaginationDto;
using ComplaintSystem.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.Complaints.Requests.Queries;
public class GetAllComplaintsForUserRequest : IRequest<BaseResponseClass>
{
    public Guid UserId { get; set; }
    public PaginationDto PaginationDto { get; set; }
}
