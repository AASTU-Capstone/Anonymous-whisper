using ComplaintSystem.Application.DTOs.ComplaintDto;
using ComplaintSystem.Application.Responses;
using MediatR;

namespace ComplaintSystem.Application.Features.Complaints.Requests.Commands
{
    public class CreateComplaintCommand : IRequest<BaseResponseClass>
    {
        public CreateComplaintControllerDto CreateComplaintDto { get; set; }
        public Guid UserId { get; set; }
    }
}