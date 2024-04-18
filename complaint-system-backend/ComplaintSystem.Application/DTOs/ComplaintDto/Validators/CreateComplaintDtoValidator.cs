using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace ComplaintSystem.Application.DTOs.ComplaintDto.Validators
{
    public class CreateComplaintDtoValidator : AbstractValidator<CreateComplaintDto>
    {
        public CreateComplaintDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title is required");
            RuleFor(x => x.Content)
                .NotEmpty()
                .WithMessage("Content is required");
            RuleFor(x => x.Category)
                .NotEmpty()
                .WithMessage("Category is required");
            RuleFor(x => x.Tag)
                .NotEmpty()
                .WithMessage("Tag is required");
        }

    }
}