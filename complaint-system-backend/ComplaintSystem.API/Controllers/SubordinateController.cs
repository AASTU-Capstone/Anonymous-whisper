using ComplaintSystem.Application.DTOs.ComplaintLogDto;
using ComplaintSystem.Application.Features.ComplaintLogs.Requests.Commands;
using ComplaintSystem.Application.Features.ComplaintLogs.Requests.Queries;
using ComplaintSystem.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ComplaintSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubordinateController : ControllerBase
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMediator _mediator;
        public SubordinateController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor;
            _mediator = mediator;
        }
        [HttpGet]
        [Route("GetComplaintLogs")]
        public async Task<ActionResult<BaseResponseClass>> GetComplaintLogs()
        {
            var subordinateId = new Guid(_contextAccessor.HttpContext.User.FindFirstValue("user_id"));
            var request = new GetComplaintLogsForSubordinateRequest { SubordinateId = subordinateId };
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

        [HttpPatch]
        [Route("UpdateComplaintLog")]
        public async Task<ActionResult<BaseResponseClass>> UpdateComplaintLog(UpdateComplaintLogDto updateComplaintLogDto)
        {
            var subordinateId = new Guid(_contextAccessor.HttpContext.User.FindFirstValue("user_id"));
            var command = new UpdateComplaintLogDtoCommand { UpdateComplaintLogDto = updateComplaintLogDto, SubordinateId = subordinateId };
            var response = await _mediator.Send(command);

            return StatusCode(response.StatusCode, response);
        }
    }
}
