using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Application.Persistence.Contracts.APIs;
using FluentValidation;

namespace ComplaintSystem.Application.DTOs.ComplaintDto.Validators
{
    public class CreateComplaintDtoValidator : AbstractValidator<CreateComplaintControllerDto>
    {
        private readonly IImaggaService _maggaService;
        public CreateComplaintDtoValidator(IImaggaService imaggaService)
        {
            _maggaService = imaggaService;
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title is required");
            RuleFor(x => x.Content)
                .NotEmpty()
                .WithMessage("Content is required");
            RuleFor(x => x.Category)
                .NotEmpty()
                .WithMessage("Category is required");

            RuleFor(x => x.ImageEvidence).MustAsync( async (images, token) =>
            {
                // handle the api to check if its a valid image
                foreach(var image in images)
                {
                    var isAiGenereated = await _maggaService.AIGenerated(image);
                    if (!isAiGenereated)
                    {
                        return isAiGenereated;
                    }
                }
                return true;
            }).WithMessage("{PropertyName} is not valid");
        }

    }
}