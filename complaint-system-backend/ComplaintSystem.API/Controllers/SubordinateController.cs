using ComplaintSystem.Application.DTOs.ComplaintLogDto;
using ComplaintSystem.Application.DTOs.PaginationDto;
using ComplaintSystem.Application.Features.Admins.Requests.Queries;
using ComplaintSystem.Application.Features.ComplaintLogs.Requests.Commands;
using ComplaintSystem.Application.Features.ComplaintLogs.Requests.Queries;
using ComplaintSystem.Application.Features.Subordinates.Requests.Queries;
using ComplaintSystem.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ComplaintSystem.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "Subordinate")]
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
        [Route("GetProfile")]
        public async Task<ActionResult<BaseResponseClass>> GetProfile()
        {
            var subordinateId = new Guid(_contextAccessor.HttpContext.User.FindFirstValue("userid"));
            var request = new GetSubordinateProfileRequest { SubordinateId = subordinateId };
            var response = await _mediator.Send(request);

            return StatusCode(response.StatusCode, response);
        }


        [HttpGet]
        [Route("GetComplaintLogsToUpdate")]
        public async Task<ActionResult<BaseResponseClass>> GetComplaintLogsToUpdate([FromQuery] PaginationDto PaginationDto)
        {
            var subordinateId = new Guid(_contextAccessor.HttpContext.User.FindFirstValue("userId"));
            var request = new GetComplaintLogsForSubordinateRequest { UserId = subordinateId, Status = "processing", PaginationDto = PaginationDto };
            var response = await _mediator.Send(request);

            return StatusCode(response.StatusCode, response);
        }


        [HttpPatch]
        [Route("UpdateComplaintLog")]
        public async Task<ActionResult<BaseResponseClass>> UpdateComplaintLog(UpdateComplaintLogDto updateComplaintLogDto)
        {
            var subordinateId = new Guid(_contextAccessor.HttpContext.User.FindFirstValue("userId"));
            var command = new UpdateComplaintLogDtoCommand { UpdateComplaintLogDto = updateComplaintLogDto, UserId = subordinateId };
            var response = await _mediator.Send(command);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch]
        [Route("UpdateComplaintLogStatus")]
        public async Task<ActionResult<BaseResponseClass>> UpdateComplaintLogStatus(UpdateComplaintLogStatusControllerDto updateComplaintLogStatusControllerDto)
        {
            var userId = new Guid(_contextAccessor.HttpContext.User.FindFirstValue("userId"));
            UpdateComplaintLogStatusDto updateComplaintLogStatusDto = new UpdateComplaintLogStatusDto
            {
                ComplaintLogId = updateComplaintLogStatusControllerDto.ComplaintLogId,
                StatusChangerId = userId,
                Status = updateComplaintLogStatusControllerDto.Status,
                Role = "subordinate"
            };
            var command = new UpdateComplaintLogStatusForSubordinateCommand { ComplaintLogStatus = updateComplaintLogStatusDto };
            var response = await _mediator.Send(command);

            return StatusCode(response.StatusCode, response);
        }
    }
}
