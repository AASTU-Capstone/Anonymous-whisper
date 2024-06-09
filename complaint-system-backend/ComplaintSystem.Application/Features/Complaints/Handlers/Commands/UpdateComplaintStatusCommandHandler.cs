using AutoMapper;
using ComplaintSystem.Application.DTOs.ComplaintDto.Validators;
using ComplaintSystem.Application.DTOs.NotificationDto;
using ComplaintSystem.Application.Features.Complaints.Requests.Commands;
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

namespace ComplaintSystem.Application.Features.Complaints.Handlers.Commands;
public class UpdateComplaintStatusCommandHandler : IRequestHandler<UpdateComplaintStatusCommand, BaseResponseClass>
{
    private readonly IComplaintRepository _complaintRepository;
    private readonly IMapper _mapper;
    private readonly INotificationService _notificationService;
    private readonly INotificationRepository _notificationRepository;

    public UpdateComplaintStatusCommandHandler(
        IComplaintRepository complaintRepository,
        IMapper mapper,
        INotificationService notificationService,
        INotificationRepository notificationRepository)
    {
        _complaintRepository = complaintRepository;
        _mapper = mapper;
        _notificationService = notificationService;
        _notificationRepository = notificationRepository;
    }
    public async Task<BaseResponseClass> Handle(UpdateComplaintStatusCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateComplaintDtoValidator(_complaintRepository);
        var validated = await validator.ValidateAsync(request.UpdateComplainDto, cancellationToken);
        BaseResponseClass response;
        if (validated.IsValid)
        {
            var complaint = await _complaintRepository.GetAsync(request.UpdateComplainDto.ComplaintId);
            _mapper.Map(request.UpdateComplainDto, complaint);
            await _complaintRepository.Update(complaint);

            response = new BaseResponseClass
            {
                StatusCode = 204,
                Success = true,
                Id = complaint.Id,
                Message = "Complaint Updated Successfully"
            };

            if (complaint.Status.ToLower() == "pending")
            {
                // send notification to the user
                var notify = new CreateNotificationDto
                {
                    sender = "System",
                    message = $"Accepted your complaint '{complaint.Title}'.",
                    recieverId = complaint.UserEntityId,
                };

                var Notification = _mapper.Map<NotificationEntity>(notify);
                await _notificationRepository.Add(Notification);

                await _notificationService.SendNotificationAsync(complaint.UserEntityId.ToString(), Notification);
                // send notification to the admin
            }
            else if (complaint.Status.ToLower() == "rejected")
            {
                // send notification to the user
                var notify = new CreateNotificationDto
                {
                    sender = "System",
                    message = $"Rejected your complaint '{complaint.Title}'.",
                    recieverId = complaint.UserEntityId,
                };

                var Notification = _mapper.Map<NotificationEntity>(notify);
                await _notificationRepository.Add(Notification);
                await _notificationService.SendNotificationAsync(complaint.UserEntityId.ToString(), Notification);

            }
        }
        else
        {
            response = new BaseResponseClass
            {
                Success = false,
                StatusCode = 400,
                Error = validated.Errors.Select(err => err.ErrorMessage).ToList(),
                Message = "Complaint Update Failed"
            };
        }

        return response;
    }
}
