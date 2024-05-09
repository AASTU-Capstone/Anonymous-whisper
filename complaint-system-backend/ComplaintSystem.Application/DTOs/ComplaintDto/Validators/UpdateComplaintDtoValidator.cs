using ComplaintSystem.Application.Persistence.Contracts;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.DTOs.ComplaintDto.Validators;
public class UpdateComplaintDtoValidator : AbstractValidator<UpdateComplaintDto>
{
    private bool IsStatusType(string statusType)
    {
        var statusTypes = new List<string> { "accepted", "rejected" };
        var isMatch = statusTypes.Contains(statusType.ToLower());
        return isMatch;
    }
    private readonly IComplaintRepository _complaintRepository;
    public UpdateComplaintDtoValidator(IComplaintRepository complaintRepository)
    {
        _complaintRepository = complaintRepository;

        RuleFor(complaint => complaint.ComplaintId).NotEmpty().NotNull().WithMessage("{PropertyName} is required")
            .MustAsync(async (id, token) =>
            {
                var complaint = await _complaintRepository.GetAsync(id);
                return complaint != null;
            }).WithMessage("{PropertyName} must be valid");

        RuleFor(complaint => complaint.Status).NotEmpty().NotNull().WithMessage("{PropertyName} is required")
            .Must((complaint, token) =>
            {
                bool valid = IsStatusType(complaint.Status);
                return valid;
            }).WithMessage("{PropertyName} must be valid");
    }
}
