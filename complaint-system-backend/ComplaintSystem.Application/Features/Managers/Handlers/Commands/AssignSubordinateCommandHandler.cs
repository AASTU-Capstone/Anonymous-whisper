using ComplaintSystem.Application.DTOs.ComplaintLogDto.Validators;
using ComplaintSystem.Application.Features.Managers.Requests.Commands;
using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.Managers.Handlers.Commands;
public class AssignSubordinateCommandHandler : IRequestHandler<AssignSubordinateCommand, BaseResponseClass>
{
    private readonly IManagerRepository _managerRepository;
    private readonly ISubordinateRepository _subordinateRepository;
    private readonly IComplaintLogRepository _complaintLogRepository;
    public AssignSubordinateCommandHandler(
        IComplaintLogRepository complaintLogRepository, 
        ISubordinateRepository subordinateRepository, 
        IManagerRepository managerRepository)
    {
        _complaintLogRepository = complaintLogRepository;
        _subordinateRepository = subordinateRepository;
        _managerRepository = managerRepository;
    }
    public async Task<BaseResponseClass> Handle(AssignSubordinateCommand request, CancellationToken cancellationToken)
    {
        var validator = new AssignSubordinateComplaintLogDtoValidator(_complaintLogRepository, _subordinateRepository, _managerRepository);
        var validated = await validator.ValidateAsync(request.ComplaintLog);
        BaseResponseClass response;
        if(validated.IsValid)
        {
            var complaintLog = await _complaintLogRepository.GetAsync(request.ComplaintLog.ComplaintLogId);
            if(complaintLog.ManagerId == request.ComplaintLog.ManagerId)
            {
                complaintLog.SubordinateId = request.ComplaintLog.SubordinateId;
                await _complaintLogRepository.Update(complaintLog);
                response = new BaseResponseClass
                {
                    StatusCode = 204,
                    Success = true,
                    Message = "Subordinate Assigned Successfully",
                    Id = complaintLog.Id
                };
            }
            else
            {
                response = new BaseResponseClass
                {
                    Success = false,
                    StatusCode = 400,
                    Error = ["Ownership mismatch"],
                    Message = "Assigning subbordinate Failed"
                };
            }
        }
        else
        {
            response = new BaseResponseClass
            {
                StatusCode = 400,
                Success = false,
                Error = validated.Errors.Select(err => err.ErrorMessage).ToList()
            };
        }

        return response;
    }
}
