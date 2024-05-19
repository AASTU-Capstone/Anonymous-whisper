using ComplaintSystem.Application.Persistence.Contracts;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.DTOs.ComplaintLogDto.Validators;

public class AssignSubordinateComplaintLogDtoValidator : AbstractValidator<AssignSubordinateControllerDto>
{
    private readonly IManagerRepository _managerRepository;
    private readonly ISubordinateRepository _subordinateRepository;
    private readonly IComplaintLogRepository _complaintLogRepository;
    public AssignSubordinateComplaintLogDtoValidator(
        IComplaintLogRepository complaintLogRepository,
        ISubordinateRepository subordinateRepository,
        IManagerRepository managerRepository)
    {
        _complaintLogRepository = complaintLogRepository;
        _subordinateRepository = subordinateRepository;
        _managerRepository = managerRepository;


        RuleFor(log => log.SubordinateId).NotEmpty().NotNull().WithMessage("{PropertyName} can not be empty").
            MustAsync(async (id, token) =>
        {
            var subordinate = await _subordinateRepository.GetAsync(id);
            return subordinate != null;
        }).WithMessage("{PropertyName} does not exist");

        RuleFor(log => log.ComplaintLogId).NotEmpty().NotNull().WithMessage("{PropertyName} can not be empty")
            .MustAsync(async (id, token) =>
        {
            var complaintLog = await _complaintLogRepository.GetAsync(id);
            return complaintLog != null;
        }).WithMessage("{PropertyName} does not exist");
    }
}
