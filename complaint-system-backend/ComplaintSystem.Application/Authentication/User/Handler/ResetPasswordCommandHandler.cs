using MediatR;
using  ComplaintSystem.Application.Authentication.User.Request;
using  ComplaintSystem.Application.DTOs.Authentication.Validator;
using  ComplaintSystem.Application.Persistence.Contracts;
using  ComplaintSystem.Application.Persistence.Contracts.Auth;
using  ComplaintSystem.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  ComplaintSystem.Application.Authentication.User.Handler
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, BaseResponseClass>
    {
        private readonly IUserRepository _userRepository;
        private readonly IOtpRepository _otpRepository;
        private readonly IPasswordService _passwordService;
        public ResetPasswordCommandHandler(IUserRepository userRepository, IPasswordService passwordService, IOtpRepository otpRepository)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
            _otpRepository = otpRepository;
        }
        public async Task<BaseResponseClass> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var validator = new ResetPasswordValidator(_userRepository,_otpRepository);
            var validated = await validator.ValidateAsync(request.ResetPassword, cancellationToken);
            var response = new BaseResponseClass();
            if (!validated.IsValid)
            {
                response.Success = false;
                response.Message = "change password failed";
                response.Error = validated.Errors.Select(x => x.ErrorMessage).ToList();
                response.StatusCode = 400;
            }
            else
            {
                var user = await _userRepository.GetByEmail(request.ResetPassword.Email);
                var otp = await _otpRepository.FindUser(user.Id);

                if (otp != null)
                {
                    response.Success = false;
                    response.Message = "change password failed";
                    response.StatusCode = 400;
                    response.Error = new List<string> { "User is not verified" };
                }

                else
                {
                    user.Password = _passwordService.HashPassword(request.ResetPassword.NewPassword);
                    await _userRepository.Update(user);
                    response.Success = true;
                    response.Message = "Password changed successfully";
                    response.StatusCode = 201;
                    response.Id = user.Id;
                }
            }

            return response;
        }
    }
}
