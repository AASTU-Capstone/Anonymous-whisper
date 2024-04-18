using FluentValidation;

namespace ComplaintSystem.Application.DTOs.SubordinateDto.Validators
{
    public class CreateSubordinateDtoValidator : AbstractValidator<CreateSubordinateDto>
    {
        public CreateSubordinateDtoValidator()
        {
            // Rule for Name
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required");

            // Rule for MitigatedCount
            RuleFor(u => u.MitigatedCount)
                .NotEmpty().WithMessage("{PropertyName} is required!");

            // Rule for ManagerId
            RuleFor(u => u.ManagerId)
                .NotEmpty().WithMessage("{PropertyName} is required!");

        }
    }
}