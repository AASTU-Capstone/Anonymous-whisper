using ComplaintSystem.Application.DTOs.PaginationDto;
using ComplaintSystem.Application.Responses;
using MediatR;


namespace ComplaintSystem.Application.Features.ComplaintLogs.Requests.Queries;
public class GetComplaintLogsForSubordinateRequest : IRequest<PaginatedResponseClass>
{
    public Guid UserId { get; set; }
    public string Status { get; set; }
    public PaginationDto PaginationDto { get; set; }
}
