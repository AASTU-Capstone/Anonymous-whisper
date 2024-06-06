using ComplaintSystem.Application.DTOs.ManagerDto;
using ComplaintSystem.Application.Responses;
using MediatR;

namespace ComplaintSystem.Application.Features.Managers.Requests.Commands
{
    public class UpdateManagerCommand : IRequest<BaseResponseClass>
    {
        public UpdateManagerDto UpdateManagerDto { get; set; }
    }
}