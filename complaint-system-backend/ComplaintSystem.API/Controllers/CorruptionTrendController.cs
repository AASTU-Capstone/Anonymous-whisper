using ComplaintSystem.Application.Features.CorruptionTrends.Requests.Queries;
using ComplaintSystem.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComplaintSystem.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "Worker")]
    [ApiController]
    public class CorruptionTrendController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _contextAccessor;
        public CorruptionTrendController(IMediator mediator, IHttpContextAccessor contextAccessor)
        {

            _contextAccessor = contextAccessor;
            _mediator = mediator;

        }
        [HttpGet]
        [Route("GetCorruptionTrends")]
        public async Task<ActionResult<BaseResponseClass>> GetCorruptionTrends()
        {
            var request = new GetCorruptionTrendsRequest { };
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }



    }
}
