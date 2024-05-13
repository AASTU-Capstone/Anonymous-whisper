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
public class SearchComplaintRequestHandler : IRequestHandler<SearchComplaintRequest, BaseResponseClass>
{
    private readonly IComplaintRepository _complaintRepository;
    private readonly IMapper _mapper;
    public SearchComplaintRequestHandler(IMapper mapper, IComplaintRepository complaintRepository)
    {
        _mapper = mapper;
        _complaintRepository = complaintRepository;
    }
    public async Task<BaseResponseClass> Handle(SearchComplaintRequest request, CancellationToken cancellationToken)
    {
        var complaints = await _complaintRepository.GetMatchingComplaints(request.Keyword);
        var getComplaints = _mapper.Map<List<GetComplaintsDto>>(complaints);
        BaseResponseClass response = new BaseResponseClass
        {
            Data = getComplaints,
            StatusCode = 200,
            Success = true,
            Message = "Search Results Fetched Successfully"
        };

        return response;
    }
}
