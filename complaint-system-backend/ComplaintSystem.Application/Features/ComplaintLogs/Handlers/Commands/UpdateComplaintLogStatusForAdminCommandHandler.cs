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

public class UpdateComplaintLogStatusForAdminCommandHandler : IRequestHandler<UpdateComplaintLogStatusForAdminCommand, BaseResponseClass>
{
    private readonly IComplaintLogRepository _complaintLogRepository;
    private readonly IAdminRepository _adminRepository;
    private readonly IManagerRepository _managerRepository;
    private readonly ISubordinateRepository _subordinateRepository;
    private readonly IComplaintRepository _complaintRepository;
    private readonly ICorruptionTrendRepository _corruptionTrendRepository;

    public UpdateComplaintLogStatusForAdminCommandHandler(
        IComplaintLogRepository complaintLogRepository,
        ISubordinateRepository subordinateRepository,
        IManagerRepository managerRepository,
        IAdminRepository adminRepository,
        IComplaintRepository complaintRepository,
        ICorruptionTrendRepository corruptionTrendRepository)
    {
        _complaintLogRepository = complaintLogRepository;
        _subordinateRepository = subordinateRepository;
        _managerRepository = managerRepository;
        _adminRepository = adminRepository;
        _complaintRepository = complaintRepository;
        _corruptionTrendRepository = corruptionTrendRepository;

    }
    public async Task<BaseResponseClass> Handle(UpdateComplaintLogStatusForAdminCommand request, CancellationToken cancellationToken)
    {
        
        var validator = new UpdateComplaintLogStatusDtoValidator(_complaintLogRepository,_subordinateRepository,_managerRepository,_adminRepository);
        var validated = await validator.ValidateAsync(request.ComplaintLogStatus, cancellationToken);
        BaseResponseClass response;
        if (validated.IsValid)
        {
            var complaintlog = await _complaintLogRepository.GetAsync(request.ComplaintLogStatus.ComplainLogId);
            var complaint = await _complaintRepository.GetAsync(complaintlog.ComplaintId);
            if (complaintlog.AdminId == request.ComplaintLogStatus.StatusChangerId)
            {
                complaintlog.Status = request.ComplaintLogStatus.Status;
                await _complaintLogRepository.Update(complaintlog);

                //update the mititaged count for the subordinate
                var subordinate = await _subordinateRepository.GetAsync(complaintlog.SubordinateId);
                if (subordinate != null && request.ComplaintLogStatus.Status.ToLower() == "resolved")
                {
                    //get corruption trend and update the mitigated count
                    var corruptionTrend = await _corruptionTrendRepository.GetCorruptionTrendByName(complaint.Category);
                    corruptionTrend.MitigatedCount += 1;
                    await _corruptionTrendRepository.Update(corruptionTrend);


                    //set complaint status to resolved
                    complaint.Status = "resolved";
                    await _complaintRepository.Update(complaint);

                    //set subordinate mitigated count to + 1
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
            }
            else
            {
                response = new BaseResponseClass
                {
                    StatusCode = 400,
                    Success = false,
                    Error = ["Ownership does not exist"],
                    Message = "Complaint Log Status Failed"
                };
            }

            

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
