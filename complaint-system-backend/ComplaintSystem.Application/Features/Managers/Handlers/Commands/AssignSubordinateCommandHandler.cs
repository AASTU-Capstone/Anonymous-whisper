using AutoMapper;
using ComplaintSystem.Application.DTOs.ComplaintLogDto;
using ComplaintSystem.Application.DTOs.ComplaintLogDto.Validators;
using ComplaintSystem.Application.DTOs.NotificationDto;
using ComplaintSystem.Application.Features.Managers.Requests.Commands;
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

namespace ComplaintSystem.Application.Features.Managers.Handlers.Commands;
public class AssignSubordinateCommandHandler : IRequestHandler<AssignSubordinateCommand, BaseResponseClass>
{
    private readonly IManagerRepository _managerRepository;
    private readonly ISubordinateRepository _subordinateRepository;
    private readonly IComplaintLogRepository _complaintLogRepository;
    private readonly INotificationService _notificationService;
    private readonly IMapper _mapper;
    private readonly INotificationRepository _notificationRepository;
    public AssignSubordinateCommandHandler(
        IComplaintLogRepository complaintLogRepository,
        ISubordinateRepository subordinateRepository,
        IManagerRepository managerRepository,
        INotificationService notificationService,
        IMapper mapper,
        INotificationRepository notificationRepository)
    {
        _complaintLogRepository = complaintLogRepository;
        _subordinateRepository = subordinateRepository;
        _managerRepository = managerRepository;
        _notificationService = notificationService;
        _mapper = mapper;
        _notificationRepository = notificationRepository;
    }
    public async Task<BaseResponseClass> Handle(AssignSubordinateCommand request, CancellationToken cancellationToken)
    {
        var validator = new AssignSubordinateComplaintLogDtoValidator(_complaintLogRepository, _subordinateRepository, _managerRepository);
        var validated = await validator.ValidateAsync(request.ComplaintLog, cancellationToken);
        var manager = await _managerRepository.GetManagerByUserId(request.UserId);

        BaseResponseClass response;
        if (validated.IsValid)
        {
            var complaintLog = await _complaintLogRepository.GetAsync(request.ComplaintLog.ComplaintLogId);
            if (manager != null && complaintLog.ManagerId == manager.Id)
            {

                complaintLog.SubordinateId = request.ComplaintLog.SubordinateId;
                complaintLog.Status = "processing";
                await _complaintLogRepository.Update(complaintLog);
                response = new BaseResponseClass
                {
                    StatusCode = 204,
                    Success = true,
                    Message = "Subordinate Assigned Successfully",
                    Id = complaintLog.Id
                };

                var subordinate = await _subordinateRepository.GetAsync(complaintLog.SubordinateId);

                // notification

                var notify = new CreateNotificationDto
                {
                    Sender = manager.Name!,
                    Message = $"Assigned you a complaint log '{complaintLog.Title}'.",
                    RecieverId = subordinate.UserEntityId
                };

                var Notification = _mapper.Map<NotificationEntity>(notify);
                await _notificationRepository.Add(Notification);
                await _notificationService.SendNotificationAsync(subordinate.UserEntityId.ToString(), Notification);
            }
            else
            {
                response = new BaseResponseClass
                {
                    Success = false,
                    StatusCode = 400,
                    Error = ["Ownership mismatch"],
                    Message = "Assigning subbordinate Failed"
                };
            }
        }
        else
        {
            response = new BaseResponseClass
            {
                StatusCode = 400,
                Success = false,
                Error = validated.Errors.Select(err => err.ErrorMessage).ToList()
            };
        }

        return response;
    }
}
