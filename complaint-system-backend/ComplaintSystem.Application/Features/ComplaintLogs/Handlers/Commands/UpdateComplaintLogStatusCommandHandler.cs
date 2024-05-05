using ComplaintSystem.Application.DTOs.ComplaintLogDto.Validators;
using ComplaintSystem.Application.Features.ComplaintLogs.Requests.Commands;
using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.ComplaintLogs.Handlers.Commands;

public class UpdateComplaintLogStatusCommandHandler : IRequestHandler<UpdateComplaintLogStatusCommand, BaseResponseClass>
{
    private readonly IComplaintLogRepository _complaintLogRepository;
    private readonly ISubordinateRepository _subordinateRepository;
    public UpdateComplaintLogStatusCommandHandler(IComplaintLogRepository complaintLogRepository, ISubordinateRepository subordinateRepository)
    {
        _complaintLogRepository = complaintLogRepository;
        _subordinateRepository = subordinateRepository;
        
    }
    public async Task<BaseResponseClass> Handle(UpdateComplaintLogStatusCommand request, CancellationToken cancellationToken)
    {
        
        var validator = new UpdateComplaintLogStatusDtoValidator(_complaintLogRepository);
        var validated = await validator.ValidateAsync(request.ComplaintLogStatus, cancellationToken);
        BaseResponseClass response;
        if (validated.IsValid)
        {
            var complaintlog = await _complaintLogRepository.GetAsync(request.ComplaintLogStatus.ComplainLogId);
            complaintlog.Status = request.ComplaintLogStatus.Status;
            await _complaintLogRepository.Update(complaintlog);

            //update the mititaged count for the subordinate
            var subordinate = await _subordinateRepository.GetAsync(complaintlog.SubordinateId);
            if(subordinate != null)
            {
                subordinate.MitigatedCount += 1;
                await _subordinateRepository.Update(subordinate);
            }

            response = new BaseResponseClass
            {
                StatusCode = 204,
                Success = true,
                Message = "Status Updated Successfully",
                Id = complaintlog.Id,
            };

        }else
        {
            response = new BaseResponseClass
            {
                Success = false,
                StatusCode = 400,
                Message = "Status Update Failed",
                Error = validated.Errors.Select(e => e.ErrorMessage).ToList()
            };
        }
        return response;
        
    }
}
