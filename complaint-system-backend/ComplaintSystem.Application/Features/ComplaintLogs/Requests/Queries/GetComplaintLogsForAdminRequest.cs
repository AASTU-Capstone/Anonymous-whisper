using ComplaintSystem.Application.DTOs.PaginationDto;
using ComplaintSystem.Application.Responses;
using MediatR;


namespace ComplaintSystem.Application.Features.ComplaintLogs.Requests.Queries;
public class GetComplaintLogsForAdminRequest : IRequest<PaginatedResponseClass>
{
    public Guid AdminId { get; set; }
    public string Status { get; set; }
    public PaginationDto PaginationDto { get; set; }
}
