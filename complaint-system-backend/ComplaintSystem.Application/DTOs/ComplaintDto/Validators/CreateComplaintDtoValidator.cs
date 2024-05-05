using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComplaintSystem.Application.Persistence.Contracts;
using FluentValidation;

namespace ComplaintSystem.Application.DTOs.ComplaintDto.Validators
{
    public class CreateComplaintDtoValidator : AbstractValidator<CreateComplaintControllerDto>
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
            /*RuleFor(x => x.ImageEvidence).MustAsync( (images, token) =>
            {
                // handle the api to check if its a valid image
                foreach(var image in images)
                {
                    return true;
                }
                return true;
            }).WithMessage("{PropertyName} is not valid");*/
        }

    }
}