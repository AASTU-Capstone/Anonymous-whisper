using  ComplaintSystem.Application.Authentication.common;
using  ComplaintSystem.Application.Authentication.Request;
using  ComplaintSystem.Application.Persistence.Contracts;
using  ComplaintSystem.Application.Persistence.Contracts.Auth;
using  ComplaintSystem.Domain.Entities;
using  ComplaintSystem.Application.Responses;
using MediatR;

namespace  ComplaintSystem.Application.Authentication.User.Handler
{
    public sealed class LoginCommandRequestHandler : IRequestHandler<LoginCommandRequest, AuthenticationResult>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IPasswordService _passwordService;
        private readonly IUserRepository _userRepository;
        private readonly IOtpRepository _otpRepository;
        
        public LoginCommandRequestHandler(IJwtTokenGenerator jwtTokenGenerator, IPasswordService passwordService, IUserRepository userRepository, IOtpRepository otpRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
            _passwordService = passwordService;
            _otpRepository = otpRepository;
        }

        public async Task<AuthenticationResult> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseResponseClass();
            var user = await _userRepository.GetByEmail(request.Email);
            var token = "";
            bool flag = false;
            
            if (user == null){
                response.Success = false;
                response.Message = "User email or password is incorrect";
                response.StatusCode = 400;
            }
            

            else if (!_passwordService.VerifyPassword(request.Password, user.Password))
            {
                response.Success = false;
                response.Message = "User email or password is incorrect";
                response.StatusCode = 400;
            }
            else if ((await _otpRepository.FindUser(user.Id)) != null)
            {
                response.Success = true;
                response.Message = "user not verified";
                response.StatusCode = 400;
            }
            else
            {
                response.Success = true;
                response.Message = "User logged in successfully";
                response.StatusCode = 200;
                token = _jwtTokenGenerator.GenerateToken(user, false);
                flag = true;
            }
   
            return new AuthenticationResult(user, token, response.Success, response.Message,flag, response.StatusCode);
        }
    }
}
