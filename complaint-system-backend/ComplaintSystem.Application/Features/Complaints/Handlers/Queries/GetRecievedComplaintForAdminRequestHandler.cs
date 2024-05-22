using AutoMapper;
using ComplaintSystem.Application.DTOs.ComplaintDto;
using ComplaintSystem.Application.Features.Complaints.Requests.Queries;
using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.Complaints.Handlers.Queries;

public class GetRecievedComplaintForAdminRequestHandler : IRequestHandler<GetRecievedComplaintForAdminRequest, BaseResponseClass>
{
    private readonly IComplaintRepository _complaintRepository;
    private readonly IMapper _mapper;
    public GetRecievedComplaintForAdminRequestHandler(IComplaintRepository complaintRepository, IMapper mapper)
    {
        _complaintRepository = complaintRepository;
        _mapper = mapper;
    }
    public async Task<BaseResponseClass> Handle(GetRecievedComplaintForAdminRequest request, CancellationToken cancellationToken)
    {
        var acceptedComplaints = await _complaintRepository.GetComplaintsForAdminByStatus(request.Status);
        var getAcceptedComplaints = _mapper.Map<List<GetComplaintsDto>>(acceptedComplaints);

        BaseResponseClass response = new BaseResponseClass
        {
            StatusCode = 200,
            Success = true,
            Data = getAcceptedComplaints,
            Message = "Complaints Fetched Successfully"
        };

        return response;
    }
}
