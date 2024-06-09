using ComplaintSystem.Application.DTOs.PaginationDto;
using ComplaintSystem.Application.DTOs.ResourceDto;
using ComplaintSystem.Application.Features.Resources.Request.Commands;
using ComplaintSystem.Application.Features.Resources.Request.Queries;
using ComplaintSystem.Application.DTOs.PaginationDto;
using ComplaintSystem.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComplaintSystem.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "Usin")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ResourceController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [Route("GetAllResources")]
        public async Task<ActionResult<PaginatedResponseClass>> GetAllResources([FromQuery] PaginationDto pagination)
        {
            var request = new GetResourcesRequest { PaginationDto = pagination};
            var response = await _mediator.Send(request);

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("GetResourceById")]
        public async Task<ActionResult<BaseResponseClass>> GetResourceById(Guid ResourceId)
        {
            var request = new GetResourceByIdRequest { ResourceId = ResourceId };
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Route("CreateResource")]
        public async Task<ActionResult<BaseResponseClass>> CreateResource([FromBody] CreateResourceDto createResourceDto)
        {
            var command = new CreateResourceCommand { createResourceDto = createResourceDto };
            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode, response);
        }


    }
}
