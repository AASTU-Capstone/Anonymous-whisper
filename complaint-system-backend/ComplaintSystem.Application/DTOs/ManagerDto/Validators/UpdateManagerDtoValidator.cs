using ComplaintSystem.Application.Persistence.Contracts;
using FluentValidation;

namespace ComplaintSystem.Application.DTOs.ManagerDto.Validators
{
    public class UpdateManagerDtoValidator : AbstractValidator<UpdateManagerDto>
    {
        private readonly IUserRepository _userRepository;

        public UpdateManagerDtoValidator(IUserRepository userRepository)
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

            RuleFor(x => x.Email).MustAsync(async (email, token) =>
            {
                // if (email == null)
                // {
                //     return false;
                // }
                var user = await _userRepository.GetByEmail(email);
                return user != null && user.User_Type == "user";
            }).WithMessage("{PropertyName} must be valid");

        }

    }
}