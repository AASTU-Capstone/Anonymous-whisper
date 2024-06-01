using ComplaintSystem.Application.DTOs.PaginationDto;
using ComplaintSystem.Application.Features.ComplaintLogs.Requests.Queries;
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
    public class ComplaintLogController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ComplaintLogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("GetResolvedComplaintLogs")]
        public async Task<ActionResult<BaseResponseClass>> GetResolvedComplaintLogs([FromQuery] PaginationDto PaginationDto)
        {
            var request = new GetResolvedComplaintLogsRequest { Status = "resolved", PaginationDto = PaginationDto };
            var response = await _mediator.Send(request);

            return StatusCode(response.StatusCode, response);
        }
        [HttpGet]
        [Route("GetComplaintLogById")]
        public async Task<ActionResult<BaseResponseClass>> GetComplaintLogById(Guid ComplaintLogId)
        {
            var request = new GetComplaintLogByIdRequest { ComplaintLogId = ComplaintLogId };
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }
    }
}
