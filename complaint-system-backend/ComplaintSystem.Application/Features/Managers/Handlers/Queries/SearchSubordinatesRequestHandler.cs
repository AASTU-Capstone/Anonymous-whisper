using AutoMapper;
using ComplaintSystem.Application.DTOs.SubordinateDto;
using ComplaintSystem.Application.Features.Managers.Requests.Queries;
using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.Managers.Handlers.Queries;
public class SearchSubordinatesRequestHandler : IRequestHandler<SearchSubordinatesRequest, BaseResponseClass>
{
    private readonly ISubordinateRepository _subordinateRepository;
    private readonly IMapper _mapper;
    public SearchSubordinatesRequestHandler(ISubordinateRepository subordinateRepository, IMapper mapper)
    {
        _mapper = mapper;
        _subordinateRepository = subordinateRepository;
    }
    public async Task<BaseResponseClass> Handle(SearchSubordinatesRequest request, CancellationToken cancellationToken)
    {
        var subordiantes = await _subordinateRepository.SearchSubordinates(request.Keyword);
        var getSubordinates = _mapper.Map<List<GetSubordinateDto>>(subordiantes);
        BaseResponseClass response = new BaseResponseClass
        {
            StatusCode = 200,
            Success = true,
            Data = getSubordinates,
            Message = "Subordinate Search Results Fetched Successfully"
        };

        return response;
    }
}
