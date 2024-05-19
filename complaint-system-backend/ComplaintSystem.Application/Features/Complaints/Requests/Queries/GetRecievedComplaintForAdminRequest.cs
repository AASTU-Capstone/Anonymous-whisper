using ComplaintSystem.Application.DTOs.PaginationDto;
using ComplaintSystem.Application.Responses;
using MediatR;


namespace ComplaintSystem.Application.Features.Complaints.Requests.Queries;

public class GetRecievedComplaintForAdminRequest : IRequest<PaginatedResponseClass>
{
    public PaginationDto PaginationDto { get; set; }
}
