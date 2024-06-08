using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.DTOs.ResourceDto.validators
{
    public class CreateResourceValidator : AbstractValidator<CreateResourceDto>
    {
        public CreateResourceValidator()
        {
            RuleFor(resource=>resource.Title).NotEmpty()
                .WithMessage("{PropertyName} is required");

            RuleFor(resource=>resource.Description).NotEmpty()
                .WithMessage("{PropertyName} is required");
        }
    }
}
