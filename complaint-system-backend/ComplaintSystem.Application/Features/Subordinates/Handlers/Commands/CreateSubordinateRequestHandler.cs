using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ComplaintSystem.Application.DTOs.SubordinateDto;
using ComplaintSystem.Application.DTOs.SubordinateDto.Validators;
using ComplaintSystem.Application.Features.Subordinates.Requests.Commands;
using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Application.Persistence.Contracts.Notification;
using ComplaintSystem.Application.Responses;
using ComplaintSystem.Domain.Entities;
using MediatR;

namespace ComplaintSystem.Application.Features.Subordinates.Handlers.Commands
{
    public class CreateSubordinateRequestHandler : IRequestHandler<CreateSubordinateRequest, BaseResponseClass>
    {
        private readonly ISubordinateRepository _subordinateRepository;
        private readonly IUserRepository _userRepository;
        private readonly IManagerRepository _managerRepository;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public CreateSubordinateRequestHandler(
            ISubordinateRepository subordinateRepository, 
            IMapper mapper, 
            IUserRepository userRepository, 
            IManagerRepository managerRepository,
            INotificationService notificationService)
        {
            _subordinateRepository = subordinateRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _managerRepository = managerRepository;
            _notificationService = notificationService;
        }

        public async Task<BaseResponseClass> Handle(CreateSubordinateRequest request, CancellationToken cancellationToken)
        {
            var Validator = new CreateSubordinateDtoValidator(_userRepository);
            var validationResult = await Validator.ValidateAsync(request.CreateSubordinateDto, cancellationToken);
            var manager = await _managerRepository.GetManagerByUserId(request.UserId);
            var response = new BaseResponseClass();

            if (!validationResult.IsValid)
            {
                response.StatusCode = 400;
                response.Success = false;
                response.Error = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }
            else if(manager == null)
            {
                response.Error = ["manager does not exist"];
                response.Success = false ;
                response.StatusCode = 400;
                response.Message = "Create Subordinate Failed";
            }
            else
            {
                CreateSubordinateDto createSubordinateDto = new CreateSubordinateDto
                {
                    MitigatedCount = 0,
                    Name = request.CreateSubordinateDto.Name,
                    ManagerId = manager.Id,
                    Email = request.CreateSubordinateDto.Email,

                };

                var user = await _userRepository.GetByEmail(request.CreateSubordinateDto.Email);
                var subordinate = _mapper.Map<Subordinate>(createSubordinateDto);
                subordinate.UserEntityId = user.Id;
                await _subordinateRepository.Add(subordinate);

                user.User_Type = "subordinate";
                await _userRepository.Update(user);

                response.StatusCode = 201;
                response.Success = true;
                response.Message = "Subordinate created successfully";


                // notification
                var notify = new NotificationEntity
                {
                    Sender = manager.Name!,
                    Message = $"Promoted you to Subordinate.",
                    Date = DateTime.Now
                };

                await _notificationService.SendNotificationAsync(user.Id.ToString(), notify);

            }

            return response;
        }

    }
}
