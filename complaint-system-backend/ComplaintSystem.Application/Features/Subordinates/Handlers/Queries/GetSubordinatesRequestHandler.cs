using AutoMapper;
using ComplaintSystem.Application.DTOs.SubordinateDto;
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
public class GetSubordinatesRequestHandler : IRequestHandler<GetSubordinatesRequest, BaseResponseClass>
{
    private readonly ISubordinateRepository _subordinateRepository;
    private readonly IManagerRepository _managerRepository;
    private readonly IMapper _mapper;
    public GetSubordinatesRequestHandler(IManagerRepository managerRepository, IMapper mapper, ISubordinateRepository subordinateRepository)
    {
        _managerRepository = managerRepository;
        _mapper = mapper;
        _subordinateRepository = subordinateRepository;
    }
    public async Task<BaseResponseClass> Handle(GetSubordinatesRequest request, CancellationToken cancellationToken)
    {
        var manager = await _managerRepository.GetAsync(request.ManagerId);
        BaseResponseClass response;
        if (manager != null)
        {
            var subordinates = await _subordinateRepository.GetSubordinatesForManager(request.ManagerId);
            var getSubordinates = _mapper.Map<GetSubordinateDto>(subordinates);
            response = new BaseResponseClass
            {
                StatusCode = 200,
                Success = true,
                Data = getSubordinates,
                Message = "Subordinates Fetched Successfully"

            };

        }
        else
        {
            response = new BaseResponseClass
            {
                Success = false,
                StatusCode = 400,
                Error = ["Manager Does Not Exist"],
                Message = "Subordinates Fetch Failed"
            };
        }

        return response;
    }
}
