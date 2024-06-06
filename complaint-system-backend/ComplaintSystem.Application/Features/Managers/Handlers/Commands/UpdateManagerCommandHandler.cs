using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ComplaintSystem.Application.DTOs.ManagerDto.Validators;
using ComplaintSystem.Application.Features.Managers.Requests.Commands;
using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Application.Responses;
using MediatR;

namespace ComplaintSystem.Application.Features.Managers.Handlers.Commands
{
    public class UpdateManagerCommandHandler : IRequestHandler<UpdateManagerCommand, BaseResponseClass>
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UpdateManagerCommandHandler(IManagerRepository managerRepository, IMapper mapper, IUserRepository userRepository)
        {
            _managerRepository = managerRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<BaseResponseClass> Handle(UpdateManagerCommand request, CancellationToken cancellationToken)
        {
            var Validator = new UpdateManagerDtoValidator(_userRepository);
            var validationResult = await Validator.ValidateAsync(request.UpdateManagerDto, cancellationToken);
            var response = new BaseResponseClass();

            if (!validationResult.IsValid)
            {
                response.StatusCode = 400;
                response.Success = false;
                response.Error = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                var user = await _userRepository.GetByEmail(request.UpdateManagerDto.Email);
                var manager = await _managerRepository.GetAsync(request.UpdateManagerDto.Id);
                user.User_Type = "manager";
                await _userRepository.Update(user);

                _mapper.Map(request.UpdateManagerDto, manager);
                manager.UserEntityId = user.Id;
                await _managerRepository.Update(manager);

                response.StatusCode = 204;
                response.Success = true;
                response.Message = "Manager updated successfully";
            }

            return response;
        }
    }
}