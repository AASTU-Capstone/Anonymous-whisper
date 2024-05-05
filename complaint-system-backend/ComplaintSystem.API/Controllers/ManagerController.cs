using ComplaintSystem.Application.DTOs.ComplaintLogDto;
using ComplaintSystem.Application.DTOs.SubordinateDto;
using ComplaintSystem.Application.Features.ComplaintLogs.Requests.Queries;
using ComplaintSystem.Application.Features.Managers.Requests.Commands;
using ComplaintSystem.Application.Features.Managers.Requests.Queries;
using ComplaintSystem.Application.Features.Subordinates.Requests.Commands;
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
    [Authorize(Policy ="Manager")]
    [ApiController]
    //policy
    public class ManagerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _contextAccessor;
        public ManagerController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor;
            _mediator = mediator;
        }
        [HttpGet]
        [Route("GetSubordinates")]
        public async Task<ActionResult<BaseResponseClass>> GetSubordinates()
        {
            var ManagerId = new Guid(_contextAccessor.HttpContext.User.FindFirstValue("userId"));
            var request = new GetSubordinatesRequest { ManagerId = ManagerId };
            var response = await _mediator.Send(request);

            return StatusCode(response.StatusCode, response);
        }
        [HttpGet]
        [Route("GetComplaintLogForManager")]
        public async Task<ActionResult<BaseResponseClass>> GetComplaintLogForManager()
        {
            var ManagerId = new Guid(_contextAccessor.HttpContext.User.FindFirstValue("userId"));
            var request = new GetComplaintLogRequestForManager { ManagerId = ManagerId };
            var response = await _mediator.Send(request);

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("GetComplaintLogByIdForManager")]
        public async Task<ActionResult<BaseResponseClass>> GetComplaintLogByIdForManager(Guid ComplaintLogId)
        {
            var request = new GetComplaintLogByIdRequest { ComplaintLogId = ComplaintLogId };
            var response = await _mediator.Send(request);

            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        [Route("CreateSubordinate")]
        public async Task<ActionResult<BaseResponseClass>> CreateSubordinates(CreateSubordinateControllerDto createSubordinateControllerDto)
        {
            var ManagerId = new Guid(_contextAccessor.HttpContext.User.FindFirstValue("userId"));
            CreateSubordinateDto createSubordinateDto = new CreateSubordinateDto
            {
                MitigatedCount = 0,
                Name = createSubordinateControllerDto.Name,
                ManagerId = ManagerId,
                Email = createSubordinateControllerDto.Email,

            };

            var command = new CreateSubordinateRequest { CreateSubordinateDto = createSubordinateDto };
            var response = await _mediator.Send(command);

            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        [Route("AssignSubordinate")]
        public async Task<ActionResult<BaseResponseClass>> AssignSubordinate(AssignSubordinateControllerDto assignSubordinateControllerDto)
        {
            var ManagerId = new Guid(_contextAccessor.HttpContext.User.FindFirstValue("userId"));
            AssignSubordinateComplaintLogDto assignSubordinateComplaintLogDto = new AssignSubordinateComplaintLogDto
            {
                SubordinateId = assignSubordinateControllerDto.SubordinateId,
                ComplaintLogId = assignSubordinateControllerDto.ComplaintLogId,
                ManagerId = ManagerId
            };

            var command = new AssignSubordinateCommand { ComplaintLog = assignSubordinateComplaintLogDto };
            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch]
        [Route("UpdateComplaintLogStatus")]
        public async Task<ActionResult<BaseResponseClass>> UpdateComplaintLogStatus(UpdateComplaintLogStatusDto updateComplaintLogStatusDto)
        {
            var ManagerId = new Guid(_contextAccessor.HttpContext.User.FindFirstValue("userId"));
            var command = new UpdateComplaintLogStatusForManagerRequest { ComplaintLogStatus = updateComplaintLogStatusDto, ManagerId = ManagerId };

            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode, response);
        }
    }
}
