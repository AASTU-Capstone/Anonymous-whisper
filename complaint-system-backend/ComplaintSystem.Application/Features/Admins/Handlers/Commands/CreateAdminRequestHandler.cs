using AutoMapper;
using ComplaintSystem.Application.DTOs.AdminDto.Validators;
using ComplaintSystem.Application.Features.Admins.Requests.Commands;
using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Application.Responses;
using ComplaintSystem.Domain.Entities;
using MediatR;

namespace ComplaintSystem.Application.Features.Admins.Handlers.Commands
{
    public class CreateAdminRequestHandler : IRequestHandler<CreateAdminRequest, BaseResponseClass>
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;

        public CreateAdminRequestHandler(
            IAdminRepository adminRepository,
            IMapper mapper)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponseClass> Handle(CreateAdminRequest request, CancellationToken cancellationToken)
        {
            var Validator = new CreateAdminDtoValidator();
            var validationResult = await Validator.ValidateAsync(request.CreateAdminDto, cancellationToken);
            var response = new BaseResponseClass();

            if (!validationResult.IsValid)
            {
                response.StatusCode = 400;
                response.Success = false;
                response.Error = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            else
            {

                var admin = _mapper.Map<Admin>(request.CreateAdminDto);
                await _adminRepository.Add(admin);

                response.StatusCode = 201;
                response.Success = true;
                response.Message = "Admin created successfully";

            }

            return response;
        }
    }
}