using AutoMapper;
using ComplaintSystem.Application.DTOs.ComplaintLogDto.Validators;
using ComplaintSystem.Application.DTOs.NotificationDto;
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
using static Google.Apis.Requests.BatchRequest;

namespace ComplaintSystem.Application.Features.ComplaintLogs.Handlers.Commands;
public class UpdateComplaintLogStatusForSubordinateCommandHandler : IRequestHandler<UpdateComplaintLogStatusForSubordinateCommand, BaseResponseClass>
{
    private readonly IComplaintLogRepository _complaintLogRepository;
    private readonly IAdminRepository _adminRepository;
    private readonly IManagerRepository _managerRepository;
    private readonly ISubordinateRepository _subordinateRepository;
    private readonly INotificationService _notificationService;
    private readonly INotificationRepository _notificationRepository;
    private readonly IMapper _mapper;

    public UpdateComplaintLogStatusForSubordinateCommandHandler(
        IComplaintLogRepository complaintLogRepository,
        ISubordinateRepository subordinateRepository,
        IManagerRepository managerRepository,
        IAdminRepository adminRepository,
        INotificationService notificationService,
        INotificationRepository notificationRepository,
        IMapper mapper)
    {
        _complaintLogRepository = complaintLogRepository;
        _subordinateRepository = subordinateRepository;
        _managerRepository = managerRepository;
        _adminRepository = adminRepository;
        _notificationService = notificationService;
        _notificationRepository = notificationRepository;
        _mapper = mapper;
    }
    public async Task<BaseResponseClass> Handle(UpdateComplaintLogStatusForSubordinateCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateComplaintLogStatusDtoValidator(_complaintLogRepository, _subordinateRepository, _managerRepository, _adminRepository);
        var validated = await validator.ValidateAsync(request.ComplaintLogStatus, cancellationToken);
        BaseResponseClass response;
        if (validated.IsValid)
        {
            var subordinate = await _subordinateRepository.GetSubordinateByUserId(request.ComplaintLogStatus.StatusChangerId);
            var complaintLog = await _complaintLogRepository.GetAsync(request.ComplaintLogStatus.ComplaintLogId);
            var manager = await _managerRepository.GetAsync(complaintLog.ManagerId);

            if (subordinate.Id == complaintLog.SubordinateId)
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

                // send notification to the 
                var notify = new CreateNotificationDto
                {
                    createdAt = DateTime.Now, 
                    sender = subordinate.Name!,
                    message = $"Submited a report for a complaint log '{complaintLog.Title}'.",
                    recieverId = manager.UserEntityId,
                };

                var Notification = _mapper.Map<NotificationEntity>(notify);
                await _notificationRepository.Add(Notification);

                await _notificationService.SendNotificationAsync(manager.Id.ToString(), Notification);

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
