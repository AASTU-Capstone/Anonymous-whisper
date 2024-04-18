using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ComplaintSystem.Application.DTOs.ManagerDto.Validators;
using ComplaintSystem.Application.Features.Managers.Requests.Commands;
using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Application.Responses;
using ComplaintSystem.Domain.Entities;
using MediatR;

namespace ComplaintSystem.Application.Features.Managers.Handlers.Commands
{
    public class CreateManagerRequestHandler : IRequestHandler<CreateManagerRequest, BaseResponseClass>
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IMapper _mapper;

        public CreateManagerRequestHandler(IManagerRepository managerRepository, IMapper mapper)
        {
            _managerRepository = managerRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponseClass> Handle(CreateManagerRequest request, CancellationToken cancellationToken)
        {
            var Validator = new CreateManagerDtoValidator();
            var validationResult = await Validator.ValidateAsync(request.CreateManagerDto, cancellationToken);
            var response = new BaseResponseClass();

            if (!validationResult.IsValid)
            {
                response.StatusCode = 400;
                response.Success = false;
                response.Error = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            else
            {

                var manager = _mapper.Map<Manager>(request.CreateManagerDto);
                await _managerRepository.Add(manager);

                response.StatusCode = 201;
                response.Success = true;
                response.Message = "Manager created successfully";

            }

            return response;
        }

    }
}