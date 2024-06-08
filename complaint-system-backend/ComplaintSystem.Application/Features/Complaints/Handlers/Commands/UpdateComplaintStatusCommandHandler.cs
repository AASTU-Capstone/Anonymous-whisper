using AutoMapper;
using ComplaintSystem.Application.DTOs.ComplaintDto.Validators;
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

    public UpdateComplaintStatusCommandHandler(
        IComplaintRepository complaintRepository, 
        IMapper mapper,
        INotificationService notificationService)
    {
        _complaintRepository = complaintRepository;
        _mapper = mapper;
        _notificationService = notificationService;
    }
    public async Task<BaseResponseClass> Handle(UpdateComplaintStatusCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateComplaintDtoValidator(_complaintRepository);
        var validated = await validator.ValidateAsync(request.UpdateComplainDto, cancellationToken);
        BaseResponseClass response;
        if(validated.IsValid)
        {
            var complaint = await _complaintRepository.GetAsync(request.UpdateComplainDto.ComplaintId);
            _mapper.Map(request.UpdateComplainDto,complaint);
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
                var notify = new NotificationEntity
                {
                    Sender = "System",
                    Message = $"Accepted your complaint '{complaint.Title}'.",
                    ReceiverId = complaint.UserEntityId,
                    Date = DateTime.Now,
                };

                await _notificationService.SendNotificationAsync((complaint.UserEntityId).ToString(), notify);
                // send notification to the admin
            }
            else if (complaint.Status.ToLower() == "rejected")
            {
                // send notification to the user
                var notify = new NotificationEntity
                {
                    Sender = "System",
                    Message = $"Rejected your complaint '{complaint.Title}'.",
                    ReceiverId = complaint.UserEntityId,
                    Date = DateTime.Now,
                };

                await _notificationService.SendNotificationAsync((complaint.UserEntityId).ToString(), notify);
                
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
