using ComplaintSystem.Application.Responses;
using MediatR;
using ComplaintSystem.Application.DTOs.PaginationDto;

namespace ComplaintSystem.Application.Features.Complaints.Requests.Queries;

public class GetRejectedComplaintsRequest : IRequest<PaginatedResponseClass>
{
    public Guid UserId { get; set; }
    public string Status {  get; set; }
    public PaginationDto PaginationDto { get; set; }
}
