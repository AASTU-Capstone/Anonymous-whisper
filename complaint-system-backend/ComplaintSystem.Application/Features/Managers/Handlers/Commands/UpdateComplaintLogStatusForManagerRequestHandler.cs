using ComplaintSystem.Application.DTOs.ComplaintLogDto.Validators;
using ComplaintSystem.Application.Features.Managers.Requests.Commands;
using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.Managers.Handlers.Commands;

public class UpdateComplaintLogStatusForManagerRequestHandler : IRequestHandler<UpdateComplaintLogStatusForManagerRequest, BaseResponseClass>
{
    private readonly IComplaintLogRepository _complaintLogRepository;
    private readonly IManagerRepository _managerRepository;
    public UpdateComplaintLogStatusForManagerRequestHandler(IComplaintLogRepository complaintLogRepository, IManagerRepository managerRepository)
    {
        _complaintLogRepository = complaintLogRepository;
        _managerRepository = managerRepository;
    }
    public async Task<BaseResponseClass> Handle(UpdateComplaintLogStatusForManagerRequest request, CancellationToken cancellationToken)
    {
        var validator = new UpdateComplaintLogStatusDtoValidator(_complaintLogRepository);
        var validated = await validator.ValidateAsync(request.ComplaintLogStatus);
        BaseResponseClass response;
        if (validated.IsValid)
        {
            var complaint = await _complaintLogRepository.GetAsync(request.ComplaintLogStatus.ComplainLogId);
            var manager = await _managerRepository.GetAsync(request.ManagerId);
            if (manager != null && manager.Id == complaint.ManagerId)
            {
                response = new BaseResponseClass
                {
                    StatusCode = 204,
                    Success = true,
                    Id = complaint.Id,
                    Message = "Complaint Log Updated Successfully"
                };
            }
            else
            {
                response = new BaseResponseClass
                {
                    Success = false,
                    StatusCode = 400,
                    Error = ["Ownership Mismatch"],
                    Message = "Update Complaint Log status Failed"
                };
            }
        }
        else
        {
            response = new BaseResponseClass
            {
                StatusCode = 400,
                Success = false,
                Error = validated.Errors.Select(err => err.ErrorMessage).ToList(),
                Message = "Update Complaint Log status Failed"
            };
        }

        return response;
    }
}
