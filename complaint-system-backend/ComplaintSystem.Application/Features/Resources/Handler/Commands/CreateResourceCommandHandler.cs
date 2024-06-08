using AutoMapper;
using ComplaintSystem.Application.DTOs.ResourceDto.validators;
using ComplaintSystem.Application.Features.Resources.Request.Commands;
using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Application.Responses;
using ComplaintSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.Resources.Handler.Commands;
public class CreateResourceCommandHandler : IRequestHandler<CreateResourceCommand, BaseResponseClass>
{
    private readonly IResourceRepository _resourceRepository;
    private readonly IMapper _mapper;
    public CreateResourceCommandHandler(IResourceRepository resourceRepository, IMapper mapper)
    {
        _resourceRepository = resourceRepository;
        _mapper = mapper;
    }
    public async Task<BaseResponseClass> Handle(CreateResourceCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateResourceValidator();
        var validated = await validator.ValidateAsync(request.createResourceDto, cancellationToken);
        BaseResponseClass response;
        if(validated.IsValid)
        {
            var resource = _mapper.Map<Resource>(request.createResourceDto);
            await _resourceRepository.Add(resource);
            response = new BaseResponseClass
            {
                StatusCode = 201,
                Success = true,
                Message = "Resource Created Successfully",
                Id = resource.Id
            };
        }
        else
        {
            response = new BaseResponseClass
            {
                Success = false,
                StatusCode = 400,
                Error = validated.Errors.Select(x => x.ErrorMessage).ToList(),
                Message = "Resource Creation Failed"
            };
        }

        return response;
    }
}
