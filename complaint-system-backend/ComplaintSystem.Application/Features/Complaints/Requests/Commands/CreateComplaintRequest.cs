using ComplaintSystem.Application.DTOs.ComplaintDto;
using ComplaintSystem.Application.Responses;
using MediatR;

namespace ComplaintSystem.Application.Features.Complaints.Requests.Commands
{
    public class CreateComplaintRequest : IRequest<BaseResponseClass>
    {
        public CreateComplaintDto CreateComplaintDto { get; set; }

    }
}