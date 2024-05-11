using AutoMapper;
using ComplaintSystem.Application.DTOs.ListingsDto;
using ComplaintSystem.Application.Features.Listings.Requests.Queries;
using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.Listings.Handlers.Queries;
public class GetListingsRequestHandler : IRequestHandler<GetListingsRequest, BaseResponseClass>
{
    private readonly IListingsRepository _listingsRepository;
    private readonly IMapper _mapper;
    public GetListingsRequestHandler(IMapper mapper, IListingsRepository listingsRepository)
    {
        _listingsRepository = listingsRepository;
        _mapper = mapper;
    }
    public async Task<BaseResponseClass> Handle(GetListingsRequest request, CancellationToken cancellationToken)
    {
        var listings = await _listingsRepository.GetAllAsync();
        var getlistings = _mapper.Map<List<GetListingsDto>>(listings);

        BaseResponseClass response = new BaseResponseClass
        {
            StatusCode = 200,
            Success = true,
            Message = "listing succesfully added",
            Data = getlistings
        };

        return response;
    }
}
