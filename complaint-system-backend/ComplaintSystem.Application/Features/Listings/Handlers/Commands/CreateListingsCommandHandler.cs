using AutoMapper;
using ComplaintSystem.Application.Features.Listings.Requests.Commands;
using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Application.Responses;
using ComplaintSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.Listings.Handlers.Commands;
public class CreateListingsCommandHandler : IRequestHandler<CreateListingsCommand, BaseResponseClass>
{
    private readonly IListingsRepository _listingsRepository;
    private readonly IMapper _mapper;
    public CreateListingsCommandHandler(IListingsRepository listingsRepository, IMapper mapper)
    {
        _listingsRepository = listingsRepository;
        _mapper = mapper;
    }
    public async Task<BaseResponseClass> Handle(CreateListingsCommand request, CancellationToken cancellationToken)
    {
        var listing = _mapper.Map<ListingsEntity>(request.CreateListingsDto);
        await _listingsRepository.Add(listing);
        BaseResponseClass response = new BaseResponseClass
        {
            StatusCode = 201,
            Success = true,
            Message = "listing succesfully added",
        };

        return response;
    }
}
