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
    private readonly IAdminRepository _adminRepository;
    private readonly IManagerRepository _managerRepository;
    private readonly ISubordinateRepository _subordinateRepository;
    /*
     complaint: recieved, accepted, rejected

     complaintLog: resolved Admin Processing   manager pending,Started subordinate progressing
     */
    private bool IsStatusType(string statusType)
    {
        var statusTypes = new List<string> { "resolved", "submitted", "progressing", "overviewing", "processing" };
        var match = statusTypes.Where(type => statusType.ToLower() == type);
        return match.Any();
    }
    public UpdateComplaintLogStatusDtoValidator(
        IComplaintLogRepository complaintLogRepository, 
        ISubordinateRepository subordinateRepository, 
        IManagerRepository managerRepository, 
        IAdminRepository adminRepository)
    {
        _complaintLogRepository = complaintLogRepository;
        _subordinateRepository = subordinateRepository;
        _managerRepository = managerRepository;
        _adminRepository = adminRepository;

        RuleFor(c => c.ComplaintLogId).NotEmpty().NotNull().WithMessage("{PropertyName} can not be empty").MustAsync(async (id, token) =>
        {
            var complaintLog = await _complaintLogRepository.GetAsync(id);
            return complaintLog != null && complaintLog.Report != null;
        }).WithMessage("{PropertyName} does not exist or is empty");

        RuleFor(c => c.Status).NotNull().NotEmpty().WithMessage("{PropertyName} can not be empty").Must((log, token) =>
        {
            var isStatus = IsStatusType(log.Status);
            return isStatus;
        }).WithMessage("invalid {PropertyName} used");

        //set rule to check for the changer entity by role and id
        RuleFor(c => new { c.Role, c.StatusChangerId }).NotEmpty().NotNull().WithMessage("{PropertyName} can not be empty").MustAsync(async (log, token) =>
        {
            if (log.Role.ToLower() == "manager")
            {
                var manager = await _managerRepository.GetManagerByUserId(log.StatusChangerId);
                return manager != null;
            }
            else if (log.Role.ToLower() == "admin")
            {
                var admin = await _adminRepository.GetAsync(log.StatusChangerId);
                return admin != null;
            }
            else if (log.Role.ToLower() == "subordinate")
            {
                var subordiante = await _subordinateRepository.GetSubordinateByUserId(log.StatusChangerId);
                return subordiante != null;
            }
            else
            {
                return false;
            }
        }).WithMessage("{PropertyName} is not valid");
        
    }
}
