using AutoMapper;
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
public class GetComplaintByIdRequestHandler : IRequestHandler<GetComplaintByIdRequest, BaseResponseClass>
{
    private readonly IComplaintRepository _complaintRepository;
    private readonly IMapper _mapper;
    public GetComplaintByIdRequestHandler(IMapper mapper, IComplaintRepository complaintRepository)
    {
        _complaintRepository = complaintRepository;
        _mapper = mapper;
    }
    public async Task<BaseResponseClass> Handle(GetComplaintByIdRequest request, CancellationToken cancellationToken)
    {
        var complaint = await _complaintRepository.GetAsync(request.ComplaintId);
        BaseResponseClass response;
        if (complaint != null)
        {
            response = new BaseResponseClass
            {
                Data = complaint,
                StatusCode = 200,
                Success = true,
                Message = "Fe"
            };
        }
        else
        {
            response = new BaseResponseClass
            {
                Success = false,
                StatusCode = 404,
                Error = ["Complaint Does Not Exist"],
                Message = "Complaint Fetch Failed"
            };
        }
        return response;
    }
}
