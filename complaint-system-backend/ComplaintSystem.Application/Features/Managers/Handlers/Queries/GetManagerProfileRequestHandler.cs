using ComplaintSystem.Application.Features.Managers.Requests.Queries;
using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.Managers.Handlers.Queries;
public class GetManagerProfileRequestHandler : IRequestHandler<GetManagerProfileRequest, BaseResponseClass>
{
    private readonly IManagerRepository _managerRepository;
    public GetManagerProfileRequestHandler(IManagerRepository managerRepository)
    {
        _managerRepository = managerRepository;
    }
    public async Task<BaseResponseClass> Handle(GetManagerProfileRequest request, CancellationToken cancellationToken)
    {
        var manager = await _managerRepository.GetManagerByUserId(request.ManagerId);
        BaseResponseClass response;
        if (manager != null)
        {
            response = new BaseResponseClass
            {
                Data = manager,
                StatusCode = 200,
                Success = true,
                Message = "manager Fetched Successfully"
            };
        }
        else
        {
            response = new BaseResponseClass
            {
                Success = false,
                StatusCode = 404,
                Error = ["manager does not exist"],
                Message = "manager Fetch Failed"
            };
        }

        return response;
    }
}
