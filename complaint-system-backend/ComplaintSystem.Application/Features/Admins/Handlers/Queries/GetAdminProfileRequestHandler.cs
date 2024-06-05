using ComplaintSystem.Application.Features.Admins.Requests.Queries;
using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.Admins.Handlers.Queries;
public class GetAdminProfileRequestHandler : IRequestHandler<GetAdminProfileRequest, BaseResponseClass>
{
    private readonly IAdminRepository _adminRepository;
    public GetAdminProfileRequestHandler(IAdminRepository adminRepository)
    {
        _adminRepository = adminRepository;
    }
    public async Task<BaseResponseClass> Handle(GetAdminProfileRequest request, CancellationToken cancellationToken)
    {
        var admin = await _adminRepository.GetAsync(request.AdminId);
        BaseResponseClass response;
        if (admin != null)
        {
            response = new BaseResponseClass
            {
                Data = admin,
                StatusCode = 200,
                Success = true,
                Message = "Admin Fetched Successfully"
            };
        }
        else
        {
            response = new BaseResponseClass
            {
                Success = false,
                StatusCode = 404,
                Error = ["admin does not exist"],
                Message = "Admin Fetch Failed"
            };
        }

        return response;
    }
}
