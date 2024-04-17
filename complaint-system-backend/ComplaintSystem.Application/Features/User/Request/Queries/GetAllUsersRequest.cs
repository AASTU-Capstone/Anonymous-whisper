using MediatR;
using   ComplaintSystem.Application.DTOs.UserDto;
using   ComplaintSystem.Application.Responses;

namespace   ComplaintSystem.Application.Features.User.Request.Queries
{
    public class GetAllUsersRequest : IRequest<BaseResponseClass>
    {
    }
}
