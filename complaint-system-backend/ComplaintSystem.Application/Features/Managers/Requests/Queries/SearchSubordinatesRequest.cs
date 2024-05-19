using ComplaintSystem.Application.DTOs.PaginationDto;
using ComplaintSystem.Application.Responses;
using MediatR;


namespace ComplaintSystem.Application.Features.Managers.Requests.Queries;
public class SearchSubordinatesRequest : IRequest<PaginatedResponseClass>
{
    public string Keyword { get; set; }
    public PaginationDto PaginationDto { get; set; }
}
