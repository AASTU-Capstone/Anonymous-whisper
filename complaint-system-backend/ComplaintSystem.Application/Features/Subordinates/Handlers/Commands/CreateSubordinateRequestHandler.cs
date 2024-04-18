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
        private readonly IMapper _mapper;

        public CreateSubordinateRequestHandler(ISubordinateRepository subordinateRepository, IMapper mapper)
        {
            _subordinateRepository = subordinateRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponseClass> Handle(CreateSubordinateRequest request, CancellationToken cancellationToken)
        {
            var Validator = new CreateSubordinateDtoValidator();
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

                var subordinate = _mapper.Map<Subordinate>(request.CreateSubordinateDto);
                await _subordinateRepository.Add(subordinate);

                response.StatusCode = 201;
                response.Success = true;
                response.Message = "Subordinate created successfully";

            }

            return response;
        }

    }
}