using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ComplaintSystem.Application.DTOs.SubordinateDto.Validators;
using ComplaintSystem.Application.Features.Subordinates.Requests.Commands;
using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Application.Responses;
using ComplaintSystem.Domain.Entities;
using MediatR;

namespace ComplaintSystem.Application.Features.Subordinates.Handlers.Commands
{
    public class CreateSubordinateRequestHandler : IRequestHandler<CreateSubordinateRequest, BaseResponseClass>
    {
        private readonly ISubordinateRepository _subordinateRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CreateSubordinateRequestHandler(ISubordinateRepository subordinateRepository, IMapper mapper, IUserRepository userRepository)
        {
            _subordinateRepository = subordinateRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<BaseResponseClass> Handle(CreateSubordinateRequest request, CancellationToken cancellationToken)
        {
            var Validator = new CreateSubordinateDtoValidator(_userRepository);
            var validationResult = await Validator.ValidateAsync(request.CreateSubordinateDto, cancellationToken);
            var response = new BaseResponseClass();

            if (!validationResult.IsValid)
            {
                response.StatusCode = 400;
                response.Success = false;
                response.Error = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            else
            {
                var user = await _userRepository.GetByEmail(request.CreateSubordinateDto.Email);
                var subordinate = _mapper.Map<Subordinate>(request.CreateSubordinateDto);
                await _subordinateRepository.Add(subordinate);

                user.User_Type = "subordinate";
                await _userRepository.Update(user);

                response.StatusCode = 201;
                response.Success = true;
                response.Message = "Subordinate created successfully";

            }

            return response;
        }

    }
}