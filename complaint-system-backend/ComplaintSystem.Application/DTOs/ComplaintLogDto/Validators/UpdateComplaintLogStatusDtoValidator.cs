using ComplaintSystem.Application.Persistence.Contracts;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.DTOs.ComplaintLogDto.Validators;
public class UpdateComplaintLogStatusDtoValidator:AbstractValidator<UpdateComplaintLogStatusDto>
{
    private readonly IComplaintLogRepository _complaintLogRepository;
    private bool IsStatusType(string statusType)
    {
        var statusTypes = new List<string> { "accepted", "resolved" };
        var match = statusTypes.Where(type => statusType.ToLower() == type);
        return match.Any();
    }
    public UpdateComplaintLogStatusDtoValidator(IComplaintLogRepository complaintLogRepository)
    {
        _complaintLogRepository = complaintLogRepository;
        RuleFor(c => c.ComplainLogId).NotEmpty().NotNull().WithMessage("{PropertyName} can not be empty").MustAsync(async (id, token) =>
        {
            var complaintLog = await _complaintLogRepository.GetAsync(id);
            return complaintLog != null;
        }).WithMessage("{PropertyName} does not exist");

        RuleFor(c => c.Status).NotNull().NotEmpty().WithMessage("{PropertyName} can not be empty").Must((log, token) =>
        {
            var isStatus = IsStatusType(log.Status);
            return isStatus;
        }).WithMessage("invalid {PropertyName} used");
        
    }
}
