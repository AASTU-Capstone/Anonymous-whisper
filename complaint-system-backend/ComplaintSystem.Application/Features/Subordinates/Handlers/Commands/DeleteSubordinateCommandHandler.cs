using AutoMapper;
using ComplaintSystem.Application.DTOs.NotificationDto;
using ComplaintSystem.Application.Features.Subordinates.Requests.Commands;
using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Application.Persistence.Contracts.Notification;
using ComplaintSystem.Application.Responses;
using ComplaintSystem.Domain.Entities;
using MediatR;

namespace ComplaintSystem.Application.Features.Subordinates.Handlers.Commands
{
    public class DeleteSubordinateCommandHandler : IRequestHandler<DeleteSubordinateCommand, BaseResponseClass>
    {
        private readonly ISubordinateRepository _subordinateRepository;
        private readonly IManagerRepository _managerRepository;
        private readonly IComplaintLogRepository _complaintLogRepository;
        private readonly IUserRepository _userRepository;
        private readonly INotificationService _notificationService;
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;


        public DeleteSubordinateCommandHandler(
            ISubordinateRepository subordinateRepository,
            IManagerRepository managerRepository,
            IComplaintLogRepository complaintLogRepository,
            IUserRepository userRepository,
            INotificationService notificationService,
            INotificationRepository notificationRepository,
            IMapper mapper)
        {
            _subordinateRepository = subordinateRepository;
            _managerRepository = managerRepository;
            _complaintLogRepository = complaintLogRepository;
            _userRepository = userRepository;
            _notificationService = notificationService;
            _notificationRepository = notificationRepository;
            _mapper = mapper;
        }


        public async Task<BaseResponseClass> Handle(DeleteSubordinateCommand request, CancellationToken cancellationToken)
        {
            var subordinate = await _subordinateRepository.GetAsync(request.DeleteSubordinateDto.Id);
            var manager = await _managerRepository.GetManagerByUserId(request.UserId);
            var response = new BaseResponseClass();

            if (subordinate == null)
            {
                response.Success = false;
                response.StatusCode = 404;
                response.Message = "Subordinate not found";
            }

            else
            {
                var complaintLogs = await _complaintLogRepository.GetComplaintLogsBySubordinateId(request.DeleteSubordinateDto.Id);
                var user = await _userRepository.GetAsync(subordinate.UserEntityId);
                for (int i = 0; i < complaintLogs.Count; i++)
                {
                    complaintLogs[i].SubordinateId = new Guid("00000000-0000-0000-0000-000000000000");
                    complaintLogs[i].Status = "progressing";
                    complaintLogs[i].Report = null;
                    await _complaintLogRepository.Update(complaintLogs[i]);
                }

                user.User_Type = "user";
                await _userRepository.Update(user);
                await _subordinateRepository.Delete(subordinate);

                response.Success = true;
                response.StatusCode = 204;
                response.Message = "Subordinate deleted successfully";

                // notify
                var notify = new CreateNotificationDto
                {
                    sender = manager.Name!,
                    message = $"Demoted you to User.",
                    recieverId = user.Id
                };

                var Notification = _mapper.Map<NotificationEntity>(notify);
                await _notificationRepository.Add(Notification);
                await _notificationService.SendNotificationAsync(user.Id.ToString(), Notification);
            }

            return response;
        }
    }
}