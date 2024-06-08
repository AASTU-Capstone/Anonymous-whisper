using AutoMapper;
using ComplaintSystem.Application.DTOs.ResourceDto;
using ComplaintSystem.Application.Features.Resources.Request.Queries;
using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.Resources.Handler.Queries;

public class GetResourcesRequestHandler : IRequestHandler<GetResourcesRequest, PaginatedResponseClass>
{
    private readonly IResourceRepository _resourceRepository;
    private readonly IMapper _mapper;
    public GetResourcesRequestHandler(IResourceRepository resourceRepository, IMapper mapper)
    {
        _mapper = mapper;
        _resourceRepository = resourceRepository;
    }
    public async Task<PaginatedResponseClass> Handle(GetResourcesRequest request, CancellationToken cancellationToken)
    {
        var resources = await _resourceRepository.GetAllAsync();
        var getResources = _mapper.Map<List<GetResourcesDto>>(resources);
        PaginatedResponseClass response = new PaginatedResponseClass
        {
            Data = getResources,
            StatusCode = 200,
            Success = true,
            TotalCount = resources.Count(),
        };

        return response;
    }
}
