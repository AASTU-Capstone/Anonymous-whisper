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
public class DeleteListingCommandHandler : IRequestHandler<DeleteListingCommand, BaseResponseClass>
{
    private readonly IListingsRepository _listingsRepository;

    public DeleteListingCommandHandler(IListingsRepository listingsRepository)
    {
        _listingsRepository = listingsRepository;
    }
    public async Task<BaseResponseClass> Handle(DeleteListingCommand request, CancellationToken cancellationToken)
    {
        var listing = await _listingsRepository.GetAsync(request.Id);
        await _listingsRepository.Delete(listing);

        BaseResponseClass response = new BaseResponseClass
        {
            StatusCode = 201,
            Success = true,
            Message = "listing succesfully deleted",
        };
        return response;
    }
}
