using FluentValidation;
using  ComplaintSystem.Application.Persistence.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  ComplaintSystem.Application.DTOs.Authentication.Validator;
public class ResetPasswordValidator : AbstractValidator<ResetPasswordDto>
{
    private readonly IOtpRepository _otpRepository;
    private readonly IUserRepository _userRepository;
    public ResetPasswordValidator(IUserRepository userRepository, IOtpRepository otpRepository)
    {
        _otpRepository = otpRepository;
        _userRepository = userRepository;
        RuleFor(u => u.NewPassword)
            .NotEmpty().WithMessage("{PropertyName} is required!")
            .MinimumLength(8).WithMessage("{PropertyName} must be atleast 8 characters. ")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain at least one number.")
            .Matches("[!@#$%^&*]").WithMessage("Password must contain at least one special character.");
        
        RuleFor(u => u.Email)
            .MustAsync(async (id, token) =>
            {
                var EmailExists = await _userRepository.GetByEmail(id);
                return EmailExists != null;
            }).WithMessage("{PropertyName} does not exist");
    }
}
