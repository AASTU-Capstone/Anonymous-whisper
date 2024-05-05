using ComplaintSystem.Application.Persistence.Contracts;
using FluentValidation;

namespace ComplaintSystem.Application.DTOs.ManagerDto.Validators
{
    public class CreateManagerDtoValidator : AbstractValidator<CreateManagerDto>
    {
        private bool IsManageeRole(string roleType)
        {
            var managerRoles = new List<string> { "preprotective", "corrective"};
            var match = managerRoles.Where(type => roleType.ToLower() == type);
            return match.Any();
        }

        private readonly IUserRepository _userRepository;
        public CreateManagerDtoValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            // Rule for Name
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required");

            // Rule for Email
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("{PropertyName} is required!")
                .EmailAddress().WithMessage("{PropertyName} is not a valid email address!");

            // Rule for Role
            RuleFor(x => x.Role)
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .Must((curr, token) =>
                {
                    bool isMatch = IsManageeRole(curr.Role);
                    return isMatch;
                }).WithMessage("{PropertyName} must be valid");

            RuleFor(x => x.Email).MustAsync(async (email, token) =>
            {
                if (email == null)
                {
                    return false;
                }
                var user = await _userRepository.GetByEmail(email);
                return user != null  && user.User_Type != "manager";
            }).WithMessage("{PropertyName} must be valid");
        }
    }
}