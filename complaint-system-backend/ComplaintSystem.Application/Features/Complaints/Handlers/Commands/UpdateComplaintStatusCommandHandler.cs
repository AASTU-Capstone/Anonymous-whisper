using AutoMapper;
using ComplaintSystem.Application.DTOs.ComplaintDto.Validators;
using ComplaintSystem.Application.Features.Complaints.Requests.Commands;
using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.Complaints.Handlers.Commands;
public class UpdateComplaintStatusCommandHandler : IRequestHandler<UpdateComplaintStatusCommand, BaseResponseClass>
{
    private readonly IComplaintRepository _complaintRepository;
    private readonly IMapper _mapper;
    public UpdateComplaintStatusCommandHandler(IComplaintRepository complaintRepository, IMapper mapper)
    {
        _complaintRepository = complaintRepository;
        _mapper = mapper;
    }
    public async Task<BaseResponseClass> Handle(UpdateComplaintStatusCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateComplaintDtoValidator(_complaintRepository);
        var validated = await validator.ValidateAsync(request.UpdateComplainDto, cancellationToken);
        BaseResponseClass response;
        if(validated.IsValid)
        {
            var complaint = await _complaintRepository.GetAsync(request.UpdateComplainDto.ComplaintId);
            _mapper.Map(request.UpdateComplainDto,complaint);
            await _complaintRepository.Update(complaint);

            response = new BaseResponseClass
            {
                StatusCode = 204,
                Success = true,
                Id = complaint.Id,
                Message = "Complaint Updated Successfully"
            };
        }
        else
        {
            response = new BaseResponseClass
            {
                Success = false,
                StatusCode = 400,
                Error = validated.Errors.Select(err => err.ErrorMessage).ToList(),
                Message = "Complaint Update Failed"
            };
        }

        return response;
    }
}
