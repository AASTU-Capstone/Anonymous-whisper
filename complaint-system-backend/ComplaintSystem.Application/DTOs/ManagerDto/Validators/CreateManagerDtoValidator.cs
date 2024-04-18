using FluentValidation;

namespace ComplaintSystem.Application.DTOs.ManagerDto.Validators
{
    public class CreateManagerDtoValidator : AbstractValidator<CreateManagerDto>
    {
        public CreateManagerDtoValidator()
        {
            // Rule for Name
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required");

            // Rule for Email
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("{PropertyName} is required!")
                .EmailAddress().WithMessage("{PropertyName} is not a valid email address!");

            // Rule for Password
            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("{PropertyName} is required!")
                .MinimumLength(8).WithMessage("{PropertyName} must be atleast 8 characters. ")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one number.")
                .Matches("[!@#$%^&*]").WithMessage("Password must contain at least one special character.");

            // Rule for Role
            RuleFor(x => x.Role)
                .NotEmpty()
                .WithMessage("Role is required");
        }
    }
}