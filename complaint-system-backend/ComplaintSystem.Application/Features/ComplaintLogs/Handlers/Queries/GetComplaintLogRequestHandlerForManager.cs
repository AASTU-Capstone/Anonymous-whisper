using AutoMapper;
using ComplaintSystem.Application.DTOs.ComplaintLogDto;
using ComplaintSystem.Application.Features.ComplaintLogs.Requests.Queries;
using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.ComplaintLogs.Handlers.Queries;

public class GetComplaintLogRequestHandlerForManager : IRequestHandler<GetComplaintLogRequestForManager, BaseResponseClass>
{
    private readonly IManagerRepository _managerRepository;
    private readonly IComplaintLogRepository _complaintLogRepository;
    private readonly IMapper _mapper;
    public GetComplaintLogRequestHandlerForManager(
        IMapper mapper, 
        IComplaintLogRepository complaintLogRepository,
        IManagerRepository managerRepository)
    {
        _complaintLogRepository = complaintLogRepository;
        _mapper = mapper;
        _managerRepository = managerRepository;
    }
    public async Task<BaseResponseClass> Handle(GetComplaintLogRequestForManager request, CancellationToken cancellationToken)
    {
        var manager = await _managerRepository.GetManagerByUserId(request.ManagerId);
        BaseResponseClass response;
        if (manager != null)
        {
            var complaints = await _complaintLogRepository.GetForManager(manager.Id, request.Status);
            var complaintsLog = _mapper.Map<List<GetComplaintLogsDto>>(complaints);
            response = new BaseResponseClass
            {
                StatusCode = 200,
                Success = true,
                Data = complaintsLog,
                Message = "Complaint Logs Fetched Successfully"
            };
        }
        else
        {
            response = new BaseResponseClass
            {
                StatusCode = 404,
                Success = false,
                Error = ["Manager Not Found"],
                Message = "Get Complaint Log Failed"
            };
        }

        return response;
    }
}
