using AutoMapper;
using ComplaintSystem.Application.DTOs.ComplaintDto.Validators;
using ComplaintSystem.Application.DTOs.ComplaintLogDto.Validators;
using ComplaintSystem.Application.DTOs.CorruptionTrendDto;
using ComplaintSystem.Application.DTOs.NotificationDto;
using ComplaintSystem.Application.Features.ComplaintLogs.Requests.Commands;
using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Application.Persistence.Contracts.Notification;
using ComplaintSystem.Application.Responses;
using ComplaintSystem.Domain.Entities;
using MediatR;


namespace ComplaintSystem.Application.Features.ComplaintLogs.Handlers.Commands
{
    public class CreateComplaintLogCommandHandler : IRequestHandler<CreateComplaintLogCommand, BaseResponseClass>
    {
        private readonly IComplaintLogRepository _complaintLogRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IComplaintRepository _complaintRepository;
        private readonly IManagerRepository _managerRepository;
        private readonly ICorruptionTrendRepository _corruptionTrendRepository;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        private readonly INotificationRepository _notificationRepository;


        public CreateComplaintLogCommandHandler(
            IComplaintRepository complaintRepository,
            IComplaintLogRepository complaintLogRepository,
            IAdminRepository adminRepository,
            IManagerRepository managerRepository,
            IMapper mapper,
            ICorruptionTrendRepository corruptionTrendRepository,
            INotificationService notificationService,
            INotificationRepository notificationRepository)
        {
            _complaintLogRepository = complaintLogRepository;
            _complaintRepository = complaintRepository;
            _adminRepository = adminRepository;
            _managerRepository = managerRepository;
            _mapper = mapper;
            _corruptionTrendRepository = corruptionTrendRepository;
            _notificationService = notificationService;
            _notificationRepository = notificationRepository;

        }
        public async Task<BaseResponseClass> Handle(CreateComplaintLogCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateComplaintLogDtoValidator(_managerRepository, _complaintRepository);
            var validated = await validator.ValidateAsync(request.ComplaintLogDto, cancellationToken);

            BaseResponseClass response;
            if (validated.IsValid)
            {
                // update the corruption trend by category by 1
                var admin = await _adminRepository.GetAsync(request.AdminId);
                var complaint = await _complaintRepository.GetAsync(request.ComplaintLogDto.ComplaintId);
                var manager = await _managerRepository.GetAsync(request.ComplaintLogDto.ManagerId);
                var corruptionTrend = await _corruptionTrendRepository.GetCorruptionTrendByName(complaint.Category);
                if (corruptionTrend != null)
                {
                    corruptionTrend.TotalCount += 1;
                    await _corruptionTrendRepository.Update(corruptionTrend);
                }
                else
                {
                    CreateCorruptionTrendDto createCorruptionTrendDto = new CreateCorruptionTrendDto
                    {
                        Name = complaint.Category.ToLower(),
                        MitigatedCount = 0,
                        TotalCount = 1
                    };
                    var trend = _mapper.Map<CorruptionTrend>(createCorruptionTrendDto);

                    await _corruptionTrendRepository.Add(trend);
                }



                //create the complaint log with adminid nad status set to pending

                var complaintLog = _mapper.Map<ComplaintLog>(request.ComplaintLogDto);
                complaintLog.Status = "progressing";
                complaintLog.AdminId = request.AdminId;
                await _complaintLogRepository.Add(complaintLog);

                // update the complaint status to pending

                complaint.Status = "pending";
                await _complaintRepository.Update(complaint);

                response = new BaseResponseClass
                {
                    Message = "Manager Asssigned Successfully",
                    Success = true,
                    StatusCode = 201,
                    Id = complaintLog.Id
                };

                // notify
                var notify = new CreateNotificationDto
                {
                    Sender = admin.Name!,
                    Message = $"Assigned you a complaint log '{complaintLog.Title}'.",
                    RecieverId = manager.UserEntityId,
                };

                var Notification = _mapper.Map<NotificationEntity>(notify);
                await _notificationRepository.Add(Notification);
                await _notificationService.SendNotificationAsync(manager.UserEntityId.ToString(), Notification);
            }
            else
            {
                response = new BaseResponseClass
                {
                    StatusCode = 400,
                    Success = false,
                    Error = validated.Errors.Select(e => e.ErrorMessage).ToList(),
                    Message = "Manager Assigning Failed"
                };

            }

            return response;
        }
    }
}
