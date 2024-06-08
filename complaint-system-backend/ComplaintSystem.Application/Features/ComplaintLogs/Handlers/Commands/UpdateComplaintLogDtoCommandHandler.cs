using AutoMapper;
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
public class UpdateComplaintLogDtoCommandHandler : IRequestHandler<UpdateComplaintLogDtoCommand, BaseResponseClass>
{
    private readonly IComplaintLogRepository _complaintLogRepository;
    private readonly ISubordinateRepository _subordinateRepository;
    private readonly IMapper _mapper;
    private readonly INotificationService _notificationService;

    public UpdateComplaintLogDtoCommandHandler(
        IMapper mapper, 
        IComplaintLogRepository complaintLogRepository, 
        ISubordinateRepository subordinateRepository,
        INotificationService notificationService)
    {
        _complaintLogRepository = complaintLogRepository;
        _subordinateRepository = subordinateRepository;
        _mapper = mapper;
        _notificationService = notificationService;
    }
    public async Task<BaseResponseClass> Handle(UpdateComplaintLogDtoCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateComplaintLogDtoValidator(_complaintLogRepository);
        var validated = await validator.ValidateAsync(request.UpdateComplaintLogDto, cancellationToken);
        BaseResponseClass response;
        if(validated.IsValid)
        {
            var complaintLog = await _complaintLogRepository.GetAsync(request.UpdateComplaintLogDto.Id);
            var subordinate = await _subordinateRepository.GetSubordinateByUserId(request.UserId);
            if(subordinate != null && complaintLog.SubordinateId  == subordinate.Id)
            {
                _mapper.Map(request.UpdateComplaintLogDto, complaintLog);
                await _complaintLogRepository.Update(complaintLog);

                response = new BaseResponseClass
                {
                    StatusCode = 204,
                    Success = true,
                    Message = "Complaint Log Updated Successfully",
                    Id = request.UpdateComplaintLogDto.Id,
                };

                // notify
                var notify = new NotificationEntity
                {
                    Sender = subordinate.Name!,
                    Message = $"Submitted a report for complaint log '{complaintLog.Title}'.",
                    ReceiverId = complaintLog.ManagerId
                    Date = DateTime.Now,
                };
                await _notificationService.SendNotificationAsync((complaintLog.ManagerId).ToString(), notify);
            }
            else
            {
                response = new BaseResponseClass
                {
                    StatusCode = 400,
                    Success = false,
                    Error = ["Invalid complaint log ownership"],
                    Message = "Complaint Log Update Failed"
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
                Message = "Complaint Log Update Failed"
            };
        }

        return response;
    }
}
