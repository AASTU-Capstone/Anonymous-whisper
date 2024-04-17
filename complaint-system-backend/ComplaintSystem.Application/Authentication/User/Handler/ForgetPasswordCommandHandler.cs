using  ComplaintSystem.Application.Authentication.Request;
using  ComplaintSystem.Application.Persistence.Contracts;
using  ComplaintSystem.Application.Persistence.Contracts.Auth;
using  ComplaintSystem.Application.Responses;
using  ComplaintSystem.Domain.Entities;
using MediatR;
using  ComplaintSystem.Application.Features.OTP.Request.Commands;


namespace  ComplaintSystem.Application.Authentication.User.Handler;

public class ForgetPasswordCommandHandler : IRequestHandler<ForgetPasswordCommand, BaseResponseClass>
{
    private readonly IUserRepository _userRepository;
    private readonly IOtpRepository _otpRepository;
    private readonly IMediator _mediator;
    public ForgetPasswordCommandHandler(
        IUserRepository userRepository,
        IMediator mediator,
        IOtpRepository otpRepository)
    {
        _userRepository = userRepository;
        _mediator = mediator;
        _otpRepository = otpRepository;
    }
    public async Task<BaseResponseClass> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmail(request.forgetPasswordDto.Email);
        var response = new BaseResponseClass();
        if (user == null)
        {
            response.Success = false;
            response.Message = "forget password failed";
            response.Error = new List<string> { "user not found" };
            response.StatusCode = 400;
        }
        else if((await _otpRepository.FindUser(user.Id)) != null)
        {
            response.Success = false;
            response.Message = "forget password failed";
            response.Error = new List<string> { "user not verified" };
            response.StatusCode = 400;
        }
        else
        {
            //otp generation and send email
            var command = new CreateOtpRequest {UserEmail = request.forgetPasswordDto.Email };
            await _mediator.Send(command);

            response.Id = user.Id;
            response.Success = true;
            response.Message = "check ur email for otp verification";
            response.StatusCode = 201;
        }

        return response;
    }
}
