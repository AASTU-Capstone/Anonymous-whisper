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

public class GetAcceptedComplaintForAdminRequestHandler : IRequestHandler<GetAcceptedComplaintForAdminRequest, BaseResponseClass>
{
    private readonly IComplaintRepository _complaintRepository;
    public GetAcceptedComplaintForAdminRequestHandler(IComplaintRepository complaintRepository)
    {
        _complaintRepository = complaintRepository;
    }
    public async Task<BaseResponseClass> Handle(GetAcceptedComplaintForAdminRequest request, CancellationToken cancellationToken)
    {
        var acceptedComplaints = await _complaintRepository.GetAcceptedComplaints();
        BaseResponseClass response = new BaseResponseClass
        {
            StatusCode = 200,
            Success = true,
            Data = acceptedComplaints,
            Message = "Complaints Fetched Successfully"
        };

        return response;
    }
}
