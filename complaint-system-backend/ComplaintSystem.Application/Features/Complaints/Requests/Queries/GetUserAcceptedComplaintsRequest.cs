using ComplaintSystem.Application.Responses;
using MediatR;
using ComplaintSystem.Application.DTOs.PaginationDto;


namespace ComplaintSystem.Application.Features.Complaints.Requests.Queries;

public class GetUserAcceptedComplaintsRequest : IRequest<PaginatedResponseClass>
{
    public Guid UserId { get; set; }
    public PaginationDto PaginationDto { get; set; }
}
