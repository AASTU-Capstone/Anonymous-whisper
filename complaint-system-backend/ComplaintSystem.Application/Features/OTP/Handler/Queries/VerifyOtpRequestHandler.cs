using MediatR;
using OtpNet;
using  ComplaintSystem.Application.Features.OTP.Request.Queries;
using  ComplaintSystem.Application.Persistence.Contracts;
using  ComplaintSystem.Application.Persistence.Contracts.Auth;
using  ComplaintSystem.Application.Responses;
using  ComplaintSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  ComplaintSystem.Application.Persistence.Contracts.Common;

namespace  ComplaintSystem.Application.Features.OTP.Handler.Queries;
public class VerifyOtpRequestHandler : IRequestHandler<VerifyOtpRequest, BaseResponseClass>
{
    private readonly IOtpRepository _otpRepository;
    private readonly IUserRepository _userRepository;
    private readonly IEmailSender _emailSender;
    public VerifyOtpRequestHandler(IOtpRepository otpRepository, IUserRepository userRepository, IEmailSender emailSender)
    {
        _otpRepository = otpRepository;
        _userRepository = userRepository;
        _emailSender = emailSender;
    }
    public async Task<BaseResponseClass> Handle(VerifyOtpRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmail(request.Email);
        var response = new BaseResponseClass();
        
        if (user == null)
        {
            response = new BaseResponseClass
            {
                Message = "User not found",
                Success = false,
                StatusCode = 400,
                Error = new List<string> { "User not found" }
            };
        }

        else
        {
            var otp = await _otpRepository.VerifyOtpCode(request.OtpCode, user.Id);
            if (otp == null)
            {
                response = new BaseResponseClass
                {
                    Message = "Invalid OTP",
                    Success = false,
                    StatusCode = 400,
                    Error = new List<string> { "Invalid OTP" }
                };
            }

            else if ((DateTime.Now - otp.CreatedAt).TotalSeconds > 300)
            {
                var bytes = Base32Encoding.ToBytes("qwertyuioplkjhgfdsazxcvbnmoiuytre");
                var totp = new Totp(bytes, mode: OtpHashMode.Sha512, step: 1, totpSize: 6);
                var totpCode = totp.ComputeTotp();

                otp.Otp = totpCode;
                otp.CreatedAt = DateTime.Now;
                await _otpRepository.Update(otp);
                await _emailSender.SendEmail(new Email()
                {
                    To = user.Email,
                    Subject = "OTP for Account Verification",
                    Body = "Your OTP is " + totpCode + ". Please use this to verify your account."
                });

                response = new BaseResponseClass
                {
                    Message = "OTP expired. New OTP sent to your email",
                    Success = false,
                    StatusCode = 202,
                    Error = new List<string> { "OTP expired. New OTP sent to your email" }
                };
            }

            else
            {
                await _userRepository.Update(user);
                
                response = new BaseResponseClass
                {
                    Id = user.Id,
                    Message = "OTP verified successfully",
                    Success = true,
                    StatusCode = 200
                };
                
                await _otpRepository.Delete(otp);
            }
        }

        return response;
    }
}
