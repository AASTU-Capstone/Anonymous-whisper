using ComplaintSystem.Application.Persistence.Contracts;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.DTOs.ComplaintLogDto.Validators;
public class UpdateComplaintLogDtoValidator : AbstractValidator<UpdateComplaintLogDto>
{
    private readonly IComplaintLogRepository _complaintLogRepository;
    public UpdateComplaintLogDtoValidator(IComplaintLogRepository complaintLogRepository)
    {
        _complaintLogRepository = complaintLogRepository;

        RuleFor(log => log.Id).NotEmpty().NotNull().WithMessage("{PropertyName} is required")
            .MustAsync(async (id, token) =>
            {
                var complaintLog = await _complaintLogRepository.GetAsync(id);
                return complaintLog != null;
            }).WithMessage("{PropertyName} must be valid");

        RuleFor(log => log.Report).NotEmpty().NotNull().WithMessage("{PropertyName} is required");
    }
}
