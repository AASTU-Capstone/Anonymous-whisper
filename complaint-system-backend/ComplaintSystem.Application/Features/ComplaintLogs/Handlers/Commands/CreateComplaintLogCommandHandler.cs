using AutoMapper;
using ComplaintSystem.Application.DTOs.ComplaintDto.Validators;
using ComplaintSystem.Application.DTOs.ComplaintLogDto.Validators;
using ComplaintSystem.Application.Features.ComplaintLogs.Requests.Commands;
using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Application.Responses;
using ComplaintSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.ComplaintLogs.Handlers.Commands
{
    public class CreateComplaintLogCommandHandler : IRequestHandler<CreateComplaintLogCommand, BaseResponseClass>
    {
        private readonly IComplaintLogRepository _complaintLogRepository;
        private readonly IComplaintRepository _complaintRepository;
        private readonly IManagerRepository _managerRepository;
        private readonly IMapper _mapper;
        public CreateComplaintLogCommandHandler(
            IComplaintRepository complaintRepository, 
            IComplaintLogRepository complaintLogRepository,
            IManagerRepository managerRepository,
            IMapper mapper)
        {
            _complaintLogRepository = complaintLogRepository;
            _complaintRepository = complaintRepository;
            _managerRepository = managerRepository;
            _mapper = mapper;

        }
        public async Task<BaseResponseClass> Handle(CreateComplaintLogCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateComplaintLogDtoValidator(_managerRepository, _complaintRepository);
            var validated = await validator.ValidateAsync(request.ComplaintLogDto, cancellationToken);
            BaseResponseClass response;
            if (validated.IsValid)
            {
                var complaintLog = _mapper.Map<ComplaintLog>(request.ComplaintLogDto);
                complaintLog.Status = "pending";
                await _complaintLogRepository.Add(complaintLog);
                response = new BaseResponseClass
                {
                    Message = "Manager Asssigned Successfully",
                    Success = true,
                    StatusCode = 201,
                    Id = complaintLog.Id
                };
            }
            else
            {
                response = new BaseResponseClass
                {
                    StatusCode = 400,
                    Success = false,
                    Error = validated.Errors.Select(e => e.ErrorMessage).ToList(),
                    Message = "Manager Assigning Failed"
                };

            }

            return response;
        }
    }
}
