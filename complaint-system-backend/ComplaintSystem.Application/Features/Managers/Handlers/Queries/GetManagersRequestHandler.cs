using AutoMapper;
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

public class GetManagersRequestHandler : IRequestHandler<GetManagersRequest, BaseResponseClass>
{
    private readonly IManagerRepository _managerRepository;
    private readonly IMapper _mapper;
    public GetManagersRequestHandler(IManagerRepository managerRepository, IMapper mapper)
    {
        _managerRepository = managerRepository;
        _mapper = mapper;
    }

    public async Task<BaseResponseClass> Handle(GetManagersRequest request, CancellationToken cancellationToken)
    {
        var type1Managers = await _managerRepository.GetMananger(request.AdminId, "premitigation");
        var type2Managers = await _managerRepository.GetMananger(request.AdminId, "postmitigation");
        BaseResponseClass response = new BaseResponseClass
        {
            Data = (type1Managers, type2Managers),
            StatusCode = 200,
            Success = true,
            Message = "Managers Fetched Successfully"
        };

        return response;
    }
}
