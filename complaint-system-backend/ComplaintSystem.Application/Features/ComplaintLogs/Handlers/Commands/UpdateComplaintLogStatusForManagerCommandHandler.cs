using ComplaintSystem.Application.DTOs.ComplaintLogDto.Validators;
using ComplaintSystem.Application.Features.ComplaintLogs.Requests.Commands;
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

namespace ComplaintSystem.Application.Features.ComplaintLogs.Handlers.Commands;
public class UpdateComplaintLogStatusForManagerCommandHandler : IRequestHandler<UpdateComplaintLogStatusForManagerCommand, BaseResponseClass>
{
    private readonly IComplaintLogRepository _complaintLogRepository;
    private readonly IAdminRepository _adminRepository;
    private readonly IManagerRepository _managerRepository;
    private readonly ISubordinateRepository _subordinateRepository;
    private readonly INotificationService _notificationService;

    public UpdateComplaintLogStatusForManagerCommandHandler(
        IComplaintLogRepository complaintLogRepository,
        ISubordinateRepository subordinateRepository,
        IManagerRepository managerRepository,
        IAdminRepository adminRepository,
        INotificationService notificationService)
    {
        _complaintLogRepository = complaintLogRepository;
        _subordinateRepository = subordinateRepository;
        _managerRepository = managerRepository;
        _adminRepository = adminRepository; 
        _notificationService = notificationService;
    }
    public async Task<BaseResponseClass> Handle(UpdateComplaintLogStatusForManagerCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateComplaintLogStatusDtoValidator(_complaintLogRepository,_subordinateRepository,_managerRepository ,_adminRepository);
        var validated = await validator.ValidateAsync(request.ComplaintLogStatus, cancellationToken);
        BaseResponseClass response;

        if (validated.IsValid)
        {
            var manager = await _managerRepository.GetManagerByUserId(request.ComplaintLogStatus.StatusChangerId);
            var complaintLog = await _complaintLogRepository.GetAsync(request.ComplaintLogStatus.ComplainLogId);
            var admin = await _adminRepository.GetAsync(complaintLog.AdminId);
            if(manager.Id == complaintLog.ManagerId)
            {
                complaintLog.Status = request.ComplaintLogStatus.Status;
                await _complaintLogRepository.Update(complaintLog);

                response = new BaseResponseClass
                {
                    StatusCode = 204,
                    Success = true,
                    Message = "Status Updated Successfully",
                    Id = complaintLog.Id,
                };

                //send notification to the admin if the status is submitted
                if (request.ComplaintLogStatus.Status.ToLower() == "submitted")
                {
                    var notify = new NotificationEntity
                    {
                        Sender = admin.Name!,
                        Message = $"Submitted a complaint log '{complaintLog.Title}'.",
                        ReceiverId = complaintLog.AdminId,
                        Date = DateTime.Now,
                    };

                    await _notificationService.SendNotificationAsync((complaintLog.AdminId).ToString(), notify);
                }

                //send notification to the subordinate if the status is processing
                else if (request.ComplaintLogStatus.Status.ToLower() == "processing")
                {
                    var notify = new NotificationEntity
                    {
                        Sender = manager.Name!,
                        Message = $"Rejected your report for the complaint log '{complaintLog.Title}'. Please review and resubmit.",
                        ReceiverId = complaintLog.SubordinateId,
                        Date = DateTime.Now,
                    };

                    await _notificationService.SendNotificationAsync((complaintLog.SubordinateId).ToString(), notify);
                }
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
        }
        else
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
