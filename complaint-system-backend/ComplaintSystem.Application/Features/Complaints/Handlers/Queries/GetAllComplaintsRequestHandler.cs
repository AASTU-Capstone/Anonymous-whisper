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
public class GetAllComplaintsRequestHandler : IRequestHandler<GetAllComplaintsRequest, BaseResponseClass>
{
    private readonly IComplaintRepository _complaintRepository;
    private readonly IMapper _mapper;
    public GetAllComplaintsRequestHandler(IMapper mapper, IComplaintRepository complaintRepository)
    {
        _complaintRepository = complaintRepository;
        _mapper = mapper;
    }
    public async Task<BaseResponseClass> Handle(GetAllComplaintsRequest request, CancellationToken cancellationToken)
    {
        var complaints = await _complaintRepository.GetAllComplaintsForAdmin(request.PaginationDto);
        var viewComplaints = _mapper.Map<List<ViewComplaintDto>>(complaints);
        BaseResponseClass response = new BaseResponseClass
        {
            Data = viewComplaints,
            StatusCode = 200,
            Success = true,
            Message = "All Complaints Fetched Successfully"
        };

        return response;
    }
}
