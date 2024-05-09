using ComplaintSystem.Application.Persistence.Contracts;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.DTOs.ComplaintLogDto.Validators;
public class CreateComplaintLogDtoValidator : AbstractValidator<CreateComplaintLogDto>
{
    private readonly IComplaintRepository _complaintRepository;
    private readonly IManagerRepository _managerRepository;
    private bool IsPriorityType(string priorityType)
    {
        var priorityTypes = new List<string> { "low", "medium", "high" };
        var match = priorityTypes.Where(type => priorityType.ToLower() == type);
        return match.Any();
    }

    public CreateComplaintLogDtoValidator(IManagerRepository managerRepository, IComplaintRepository complaintRepository)
    {
        _managerRepository = managerRepository;
        _complaintRepository = complaintRepository;
        RuleFor(c => c.Title).NotEmpty().NotNull().WithMessage("{PropertyName} can not be empty").MaximumLength(233).WithMessage("{PropertyName} can not exceed 233 characters");
        RuleFor(c => c.Priority).NotEmpty().NotNull().WithMessage("{PropertyName} can not be empty").Must((log, token) =>
        {
            if (log.Priority == null)
            {
                return false;
            }
            bool isPriority = IsPriorityType(log.Priority);
            return isPriority;
        }).WithMessage("{PropertyName} can only be low, medium or hard");
        RuleFor(c => c.ManagerId).MustAsync(async (id, token) =>
        {
            var manager = await _managerRepository.GetAsync(id);
            return manager != null;
        }).WithMessage("{PropertyName} does not exist");
        RuleFor(c => c.ComplaintId).MustAsync(async (id, token) =>
        {
            var complaint = await _complaintRepository.GetAsync(id);
            return complaint != null;
        }).WithMessage("{PropertyName} does not exist");
    }
}
