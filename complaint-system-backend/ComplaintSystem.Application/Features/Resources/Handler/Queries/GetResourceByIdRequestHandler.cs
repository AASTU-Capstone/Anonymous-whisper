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
public class GetResourceByIdRequestHandler : IRequestHandler<GetResourceByIdRequest, BaseResponseClass>
{
    private readonly IResourceRepository _resourceRepository;
    private readonly IMapper _mapper;
    public GetResourceByIdRequestHandler(IResourceRepository resourceRepository, IMapper mapper)
    {
        _resourceRepository = resourceRepository;
        _mapper = mapper;
    }
    public async Task<BaseResponseClass> Handle(GetResourceByIdRequest request, CancellationToken cancellationToken)
    {
        var resource = await _resourceRepository.GetAsync(request.ResourceId);
        var getResource = _mapper.Map<GetResourceDto>(resource);
        BaseResponseClass response = new BaseResponseClass
        {
            Data = getResource,
            StatusCode = 200,
            Success = true,
            Message = "Resource Fetched Successfully"
        };

        return response;
    }
}
