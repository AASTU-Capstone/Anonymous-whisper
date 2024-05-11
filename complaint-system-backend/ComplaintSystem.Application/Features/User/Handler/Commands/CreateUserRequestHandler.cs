using MediatR;
using AutoMapper;
using   ComplaintSystem.Application.Features.User.Request.Commands;
using   ComplaintSystem.Application.Responses;
using   ComplaintSystem.Domain.Entities;
using   ComplaintSystem.Application.DTOs.UserDto.Validators;
using   ComplaintSystem.Application.Persistence.Contracts;
using   ComplaintSystem.Application.Persistence.Contracts.Auth;
using   ComplaintSystem.Application.Features.OTP.Request.Commands;


namespace   ComplaintSystem.Application.Features.User.Handler.Commands
{
    public class CreateUserRequestHandler : IRequestHandler<CreateUserRequest, BaseResponseClass>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IUserRepository _UserRepository;
        private readonly IPasswordService _passwordService;
        // private readonly IEmailSender _emailSender;
        public CreateUserRequestHandler(
            IMapper mapper, 
            IUserRepository UserRepository, 
            // IEmailSender emailSender, 
            IMediator mediator,
            IPasswordService passwordService)
        {
            _mapper = mapper;
            _UserRepository = UserRepository;
            // _emailSender = emailSender;
            _mediator = mediator;
            _passwordService = passwordService;
        }

        public async Task<BaseResponseClass> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateUserValidator(_UserRepository);
            var validationResult = await validator.ValidateAsync(request.User, cancellationToken);
            var response = new BaseResponseClass();

            if (!validationResult.IsValid)
            {
                response = new BaseResponseClass
                {
                    Message = "User Creation Failed",
                    Success = false,
                    StatusCode = 400,
                    Error = validationResult.Errors.Select(x => x.ErrorMessage).ToList()
                };
            }

            else
            {
                // hash password
                var hashedPassword = _passwordService.HashPassword(request.User.Password);
                request.User.Password = hashedPassword;
                

                // add user to database
                var User = _mapper.Map<UserEntity>(request.User);
                User.Name = "user";
                await _UserRepository.Add(User);

                // create otp and send email to user with otp
                //var command = new CreateOtpRequest { UserEmail = User.Email };
                //var result = await _mediator.Send(command);
       
                response = new BaseResponseClass
                {
                    Id = User.Id,
                    Message = "User Created Successfully",
                    Success = true,
                    StatusCode = 201,
                };
            }

            return response;
        }
    }
}