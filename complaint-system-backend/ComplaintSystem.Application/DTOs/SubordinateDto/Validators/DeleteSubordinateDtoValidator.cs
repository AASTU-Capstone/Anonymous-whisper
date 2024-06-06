using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComplaintSystem.Application.Persistence.Contracts;
using FluentValidation;

namespace ComplaintSystem.Application.DTOs.SubordinateDto.Validators
{
    public class DeleteSubordinateDtoValidator : AbstractValidator<DeleteSubordinateDto>
    {
        private readonly ISubordinateRepository _subordinateRepository;

        public DeleteSubordinateDtoValidator(ISubordinateRepository subordinateRepository)
        {
            _subordinateRepository = subordinateRepository;

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required")
                .MustAsync(async (Id, token) =>
                {
                    var subordinate = await _subordinateRepository.GetAsync(Id);
                    return subordinate != null;
                })
                .WithMessage("Subordinate does not exist");
        }
    }
}