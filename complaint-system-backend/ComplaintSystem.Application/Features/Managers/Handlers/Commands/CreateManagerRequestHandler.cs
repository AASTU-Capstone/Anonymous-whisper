using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ComplaintSystem.Application.DTOs.ManagerDto.Validators;
using ComplaintSystem.Application.Features.Managers.Requests.Commands;
using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Application.Persistence.Contracts.Notification;
using ComplaintSystem.Application.Responses;
using ComplaintSystem.Domain.Entities;
using MediatR;

namespace ComplaintSystem.Application.Features.Managers.Handlers.Commands
{
    public class CreateManagerRequestHandler : IRequestHandler<CreateManagerRequest, BaseResponseClass>
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IUserRepository _userRepository;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public CreateManagerRequestHandler(
            IManagerRepository managerRepository,
            IAdminRepository adminRepository,
            IMapper mapper, 
            IUserRepository userRepository,
            INotificationService notificationService)
        {
            _managerRepository = managerRepository;
            _adminRepository = adminRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _notificationService = notificationService;
        }

        public async Task<BaseResponseClass> Handle(CreateManagerRequest request, CancellationToken cancellationToken)
        {
            var Validator = new CreateManagerDtoValidator(_userRepository);
            var validationResult = await Validator.ValidateAsync(request.CreateManagerDto, cancellationToken);
            var admin = await _adminRepository.GetAsync(request.AdminId);
            var preManager = await _managerRepository.GetMananger(request.AdminId,"premitigation");
            var postManager = await _managerRepository.GetMananger(request.AdminId, "postmitigation");
            var response = new BaseResponseClass();

            if (!validationResult.IsValid)
            {
                response.StatusCode = 400;
                response.Success = false;
                response.Error = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }
            else if(postManager != null && preManager != null)
            {
                response.Error = ["Manager limit excedeed"];
                response.Success = false;
                response.Message = "Manager Create Failed";
                response.StatusCode = 400;
            }
            else if((postManager != null && postManager.Role.ToLower() == request.CreateManagerDto.Role!.ToLower()) || (preManager != null && preManager.Role.ToLower() == request.CreateManagerDto.Role!.ToLower() ))
            {
                response.Error = ["Manger with the role exists"];
                response.Success = false;
                response.Message = "Manager Create Failed";
                response.StatusCode = 400;
            }
            else
            {
                var user = await _userRepository.GetByEmail(request.CreateManagerDto.Email!);
                user.User_Type = "manager";
                await _userRepository.Update(user);

                var manager = _mapper.Map<Manager>(request.CreateManagerDto);
                manager.AdminId = request.AdminId;
                manager.UserEntityId = user.Id;
                await _managerRepository.Add(manager);

                response.StatusCode = 201;
                response.Success = true;
                response.Message = "Manager created successfully";


                // notification
                var notify = new NotificationEntity
                {
                    Sender = admin.Name!,
                    Message = $"Promoted you to {manager.Role} Manager.",
                    Date = DateTime.Now,
                };
                await _notificationService.SendNotificationAsync(user.Id.ToString(), notify);

            }

            return response;
        }

    }
}