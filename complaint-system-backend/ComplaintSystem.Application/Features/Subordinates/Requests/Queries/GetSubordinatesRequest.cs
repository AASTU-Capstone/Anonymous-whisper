using ComplaintSystem.Application.DTOs.PaginationDto;
using ComplaintSystem.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.Subordinates.Requests.Queries;

public class GetSubordinatesRequest : IRequest<PaginatedResponseClass>
{
    public Guid ManagerId { get; set; }
    public PaginationDto PaginationDto { get; set; }
}
