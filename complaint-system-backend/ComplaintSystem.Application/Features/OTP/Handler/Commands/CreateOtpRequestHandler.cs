using MediatR;
using OtpNet;
using  ComplaintSystem.Application.Features.OTP.Request.Commands;
using  ComplaintSystem.Application.Persistence.Contracts;
using  ComplaintSystem.Application.Persistence.Contracts.Auth;
using  ComplaintSystem.Application.Responses;
using  ComplaintSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using  ComplaintSystem.Application.Persistence.Contracts.Common;

namespace  ComplaintSystem.Application.Features.OTP.Handler.Commands;

public class CreateOtpRequestHandler : IRequestHandler<CreateOtpRequest, BaseResponseClass>
{
    private readonly IOtpRepository _otprepository;
    private readonly IUserRepository _userRepository;
    private readonly IEmailSender _emailSender;
    public CreateOtpRequestHandler(IOtpRepository otpRepository, IUserRepository userRepository, IEmailSender emailSender)
    {
        _otprepository = otpRepository;
        _userRepository = userRepository;
        _emailSender = emailSender;
    }
    public async Task<BaseResponseClass> Handle(CreateOtpRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmail(request.UserEmail);
        var response = new BaseResponseClass();
        if (user != null)
        {
            var otpEntity = await _otprepository.FindUser(user.Id);
            var bytes = Base32Encoding.ToBytes("qwertyuioplkjhgfdsazxcvbnmoiuytre");
            var totp = new Totp(bytes, mode: OtpHashMode.Sha512, step: 1, totpSize: 6);
            var totpCode = totp.ComputeTotp();

            //if entity does not exist create new one and insert into the table
            if (otpEntity == null)
            {
                otpEntity = new OTPEntity()
                {
                    EntityId = user.Id,
                    Otp = totpCode,
                };
                otpEntity = await _otprepository.Add(otpEntity);
            }
            // else update the otp and insert it into the table
            else { 
                otpEntity.Otp = totpCode;
                otpEntity.CreatedAt = DateTime.Now;
                await _otprepository.Update(otpEntity);

            }

            var resp = await _emailSender.SendEmail(new Email()

            {
                To = request.UserEmail,
                Subject = "OTP for Account Verification",
                Body = "Your OTP is " + totpCode + ". Please use this to verify your account."
            });

            response = new BaseResponseClass
            {
                Message = "OTP created successfully",
                Success = true,
                StatusCode = 200
            };
        }
        else
        {
            response = new BaseResponseClass
            {
                Message = "User not found",
                Success = false,
                StatusCode = 400,
                Error = new List<string> { "User not found" }
            };
        }

        return response;
    }
}
