using ComplaintSystem.Application.Persistence.Contracts;
using FluentValidation;

namespace ComplaintSystem.Application.DTOs.SubordinateDto.Validators
{
    public class CreateSubordinateDtoValidator : AbstractValidator<CreateSubordinateDto>
    {
        private readonly IUserRepository _userRepository;
        public CreateSubordinateDtoValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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

            RuleFor(u => u.Email).NotEmpty().NotNull().WithMessage("{PropertyName} is required")
                .MustAsync(async (email, token) =>
                {
                    var user = await _userRepository.GetByEmail(email);
                    return user != null && user.User_Type.ToLower() != "subordinate";
                }).WithMessage("{PropertyName} is invalid");

        }
    }
}