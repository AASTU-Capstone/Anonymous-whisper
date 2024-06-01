using AutoMapper;
using ComplaintSystem.Application.DTOs.CorruptionTrendDto;
using ComplaintSystem.Application.Features.CorruptionTrends.Requests.Queries;
using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.CorruptionTrends.Handlers.Queries;
public class GetCorruptionTrendsRequestHandler : IRequestHandler<GetCorruptionTrendsRequest, BaseResponseClass>
{
    private readonly ICorruptionTrendRepository _corruptionTrendRepository;
    private readonly IMapper _mapper;
    public GetCorruptionTrendsRequestHandler(ICorruptionTrendRepository corruptionTrendRepository, IMapper mapper)
    {
        _corruptionTrendRepository = corruptionTrendRepository;
        _mapper = mapper;
    }
    public async Task<BaseResponseClass> Handle(GetCorruptionTrendsRequest request, CancellationToken cancellationToken)
    {
        var corruptionTrends = await _corruptionTrendRepository.GetAllAsync();
        var getCorruptionTrends = _mapper.Map<List<GetCorruptionTrendDto>>(corruptionTrends);
        BaseResponseClass response = new BaseResponseClass
        {
            Data = getCorruptionTrends,
            StatusCode = 200,
            Success = true,
            Message = "Corruption Trends Fetched successfully"

        };

        return response;
    }
}
