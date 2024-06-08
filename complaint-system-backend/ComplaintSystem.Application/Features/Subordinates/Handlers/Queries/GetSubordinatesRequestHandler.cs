using AutoMapper;
using ComplaintSystem.Application.DTOs.SubordinateDto;
using ComplaintSystem.Application.Features.Subordinates.Requests.Queries;
using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Application.Persistence.Contracts.Notification;
using ComplaintSystem.Application.Responses;
using ComplaintSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.Subordinates.Handlers.Queries;
public class GetSubordinatesRequestHandler : IRequestHandler<GetSubordinatesRequest, PaginatedResponseClass>
{
    private readonly ISubordinateRepository _subordinateRepository;
    private readonly IManagerRepository _managerRepository;
    private readonly INotificationService _notificationService;
    private readonly IMapper _mapper;
    public GetSubordinatesRequestHandler(
        IManagerRepository managerRepository,
        IMapper mapper,
        ISubordinateRepository subordinateRepository,
        INotificationService notificationService)
    {
        _managerRepository = managerRepository;
        _mapper = mapper;
        _subordinateRepository = subordinateRepository;
        _notificationService = notificationService;
    }
    public async Task<PaginatedResponseClass> Handle(GetSubordinatesRequest request, CancellationToken cancellationToken)
    {
        var manager = await _managerRepository.GetManagerByUserId(request.ManagerId);
        PaginatedResponseClass response;
        if (manager != null)
        {
            var subordinates = await _subordinateRepository.GetSubordinatesForManager(manager.Id, request.PaginationDto);
            var getSubordinates = _mapper.Map<List<GetSubordinateDto>>(subordinates);

            response = new PaginatedResponseClass
            {
                StatusCode = 200,
                Success = true,
                Data = getSubordinates,
                Message = "Subordinates Fetched Successfully",
                TotalCount = await _subordinateRepository.GetSubordinatesForManagerCount(manager.Id),
                PageNumber = request.PaginationDto.PageNumber,
                PageSize = request.PaginationDto.PageSize
            };
        }
        else
        {
            response = new PaginatedResponseClass
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
