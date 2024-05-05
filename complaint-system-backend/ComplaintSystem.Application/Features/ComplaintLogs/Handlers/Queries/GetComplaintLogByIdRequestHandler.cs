using AutoMapper;
using ComplaintSystem.Application.DTOs.ComplaintDto;
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
public class GetComplaintLogByIdRequestHandler : IRequestHandler<GetComplaintLogByIdRequest, BaseResponseClass>
{
    private readonly IComplaintLogRepository _complaintLogRepository;
    private readonly IComplaintRepository _complaintRepository;
    private readonly IMapper _mapper;
    public GetComplaintLogByIdRequestHandler(
        IComplaintRepository complaintRepository,
        IComplaintLogRepository complaintLogRepository,
        IMapper mapper)
    {
        _complaintLogRepository = complaintLogRepository;
        _complaintRepository = complaintRepository;
        _mapper = mapper;
    }
    public async Task<BaseResponseClass> Handle(GetComplaintLogByIdRequest request, CancellationToken cancellationToken)
    {
        var complaintLog = await _complaintLogRepository.GetAsync(request.ComplaintLogId);
        BaseResponseClass response;
        if (complaintLog != null)
        {
            var complaint = await _complaintRepository.GetAsync(complaintLog.ComplaintId);
            var getComplaint = _mapper.Map<GetComplaintsDto>(complaint);

            GetComplaintLogByIdDto getComplaintLogByIdDto = new GetComplaintLogByIdDto
            {
                Status = complaintLog.Status,
                Complaints = getComplaint,
                Priority = complaintLog.Priority,
                Report = complaintLog.Report,
                Title = complaintLog.Title,
            };

            response = new BaseResponseClass
            {
                Data = getComplaintLogByIdDto,
                StatusCode = 200,
                Success = true,
                Message = "Complaint Log By Id Fetched Successfully"
            };
        }
        else
        {
            response = new BaseResponseClass
            {
                Success = false,
                StatusCode = 400,
                Error = ["Complaint Log Does Not exist"],
                Message = "Complaint Log By Id Fetch Failed"
            };
        }

        return response;
    }
}
