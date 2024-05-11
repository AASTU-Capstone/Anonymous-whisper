using AutoMapper;
using ComplaintSystem.Application.Features.Listings.Requests.Commands;
using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.Listings.Handlers.Commands;
public class UpdateListingsRequestHandler : IRequestHandler<UpdateListingsRequest, BaseResponseClass>
{
    private readonly IListingsRepository _listingsRepository;
    private readonly IMapper _mapper;
    public UpdateListingsRequestHandler(IMapper mapper, IListingsRepository listingsRepository)
    {
        _listingsRepository = listingsRepository;
        _mapper = mapper;
    }
    public async Task<BaseResponseClass> Handle(UpdateListingsRequest request, CancellationToken cancellationToken)
    {
        var listing = await _listingsRepository.GetAsync(request.UpdateListings.Id);
        _mapper.Map(request.UpdateListings,listing);

        await _listingsRepository.Update(listing);
        BaseResponseClass response = new BaseResponseClass
        {
            StatusCode = 201,
            Success = true,
            Message = "listing succesfully updated",
        };

        return response;
    }
}
