using ComplaintSystem.Application.Features.Subordinates.Requests.Queries;
using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.Subordinates.Handlers.Queries;
public class GetSubordinateProfileRequestHandler : IRequestHandler<GetSubordinateProfileRequest, BaseResponseClass>
{
    private readonly ISubordinateRepository _subordinateRepository;
    public GetSubordinateProfileRequestHandler(ISubordinateRepository subordinateRepository)
    {
        _subordinateRepository = subordinateRepository;
    }
    public async Task<BaseResponseClass> Handle(GetSubordinateProfileRequest request, CancellationToken cancellationToken)
    {
        var subordinate = await _subordinateRepository.GetSubordinateByUserId(request.SubordinateId);
        BaseResponseClass response;
        if (subordinate != null)
        {
            response = new BaseResponseClass
            {
                Data = subordinate,
                StatusCode = 200,
                Success = true,
                Message = "Subordinate Fetched Successfully"
            };
        }
        else
        {
            response = new BaseResponseClass
            {
                Success = false,
                StatusCode = 404,
                Error = ["subordinate does not exist"],
                Message = "Subordinate Fetch Failed"
            };
        }

        return response;
    }
}
