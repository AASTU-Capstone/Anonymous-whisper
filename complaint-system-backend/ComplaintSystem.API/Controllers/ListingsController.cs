using ComplaintSystem.Application.DTOs.ListingsDto;
using ComplaintSystem.Application.Features.Listings.Requests.Commands;
using ComplaintSystem.Application.Features.Listings.Requests.Queries;
using ComplaintSystem.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComplaintSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ListingsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ListingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("CreateListing")]
        public async Task<ActionResult<BaseResponseClass>> CreateListing(CreateListingsDto createListingDto)
        {
            var command = new CreateListingsCommand { CreateListingsDto = createListingDto };
            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch]
        [Route("UpdateListing")]

        public async Task<ActionResult<BaseResponseClass>> UpdateListing(UpdateListingsDto updateListingsDto)
        {
            var command = new UpdateListingsRequest { UpdateListings = updateListingsDto };
            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete]
        [Route("DeleteListing")]
        public async Task<ActionResult<BaseResponseClass>> DeleteListing(Guid Id)
        {
            var command = new DeleteListingCommand { Id = Id };
            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("GetListings")]
        public async Task<ActionResult<BaseResponseClass>> GetListings()
        {
            var request = new GetListingsRequest { };
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }
    }
}
