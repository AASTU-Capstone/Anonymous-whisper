using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  ComplaintSystem.Application.DTOs.Authentication.Validator
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordValidator() {
            RuleFor(u => u.NewPassword)
            .NotEmpty().WithMessage("{PropertyName} is required!")
            .MinimumLength(8).WithMessage("{PropertyName} must be atleast 8 characters. ")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain at least one number.")
            .Matches("[!@#$%^&*]").WithMessage("Password must contain at least one special character.");
        }
    }
}
