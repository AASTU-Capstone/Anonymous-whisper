using ComplaintSystem.Application.DTOs.ComplaintLogDto;
using ComplaintSystem.Application.DTOs.PaginationDto;
using ComplaintSystem.Application.DTOs.SubordinateDto;
using ComplaintSystem.Application.Features.Admins.Requests.Queries;
using ComplaintSystem.Application.Features.ComplaintLogs.Requests.Commands;
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
    [Authorize(Policy = "Manager")]
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
        [Route("GetProfile")]
        public async Task<ActionResult<BaseResponseClass>> GetProfile()
        {
            var mangerId = new Guid(_contextAccessor.HttpContext.User.FindFirstValue("userid"));
            var request = new GetManagerProfileRequest { ManagerId = mangerId };
            var response = await _mediator.Send(request);

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("GetSubordinates")]
        public async Task<ActionResult<BaseResponseClass>> GetSubordinates([FromQuery] PaginationDto PaginationDto)
        {
            var ManagerId = new Guid(_contextAccessor.HttpContext.User.FindFirstValue("userId"));
            var request = new GetSubordinatesRequest { ManagerId = ManagerId, PaginationDto = PaginationDto };
            var response = await _mediator.Send(request);

            return StatusCode(response.StatusCode, response);
        }
        [HttpGet]
        [Route("GetComplaintLogToAssign")]
        public async Task<ActionResult<BaseResponseClass>> GetComplaintLogToAssign([FromQuery] PaginationDto PaginationDto)
        {
            var ManagerId = new Guid(_contextAccessor.HttpContext.User.FindFirstValue("userId"));
            var request = new GetComplaintLogRequestForManager { ManagerId = ManagerId, Status = "progressing", PaginationDto = PaginationDto };
            var response = await _mediator.Send(request);

            return StatusCode(response.StatusCode, response);
        }
        [HttpGet]
        [Route("GetComplaintLogToUpdate")]
        public async Task<ActionResult<BaseResponseClass>> GetComplaintLogToUpdate([FromQuery] PaginationDto PaginationDto)
        {
            var ManagerId = new Guid(_contextAccessor.HttpContext.User.FindFirstValue("userId"));
            var request = new GetComplaintLogRequestForManager { ManagerId = ManagerId, Status = "overviewing", PaginationDto = PaginationDto };
            var response = await _mediator.Send(request);

            return StatusCode(response.StatusCode, response);
        }


        [HttpGet]
        [Route("SearchSubordinates")]
        public async Task<ActionResult<BaseResponseClass>> SearchSubordinates(string Keyword, [FromQuery] PaginationDto PaginationDto)
        {
            var request = new SearchSubordinatesRequest { Keyword = Keyword, PaginationDto = PaginationDto };
            var response = await _mediator.Send(request);

            return StatusCode(response.StatusCode, response);
        }


        [HttpPost]
        [Route("CreateSubordinate")]
        public async Task<ActionResult<BaseResponseClass>> CreateSubordinates(CreateSubordinateControllerDto createSubordinateControllerDto)
        {
            var ManagerId = new Guid(_contextAccessor.HttpContext.User.FindFirstValue("userId"));

            var command = new CreateSubordinateRequest { CreateSubordinateDto = createSubordinateControllerDto, UserId = ManagerId };
            var response = await _mediator.Send(command);

            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        [Route("AssignSubordinate")]
        public async Task<ActionResult<BaseResponseClass>> AssignSubordinate(AssignSubordinateControllerDto assignSubordinateControllerDto)
        {
            var ManagerId = new Guid(_contextAccessor.HttpContext.User.FindFirstValue("userId"));
            var command = new AssignSubordinateCommand { ComplaintLog = assignSubordinateControllerDto, UserId = ManagerId };
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
                Role = "manager"
            };
            var command = new UpdateComplaintLogStatusForManagerCommand { ComplaintLogStatus = updateComplaintLogStatusDto };

            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete]
        [Route("DeleteSubordinate")]
        public async Task<ActionResult<BaseResponseClass>> DeleteSubordinate(DeleteSubordinateDto deleteSubordinateDto)
        {
            var userId = new Guid(_contextAccessor.HttpContext.User.FindFirstValue("userId"));
            var command = new DeleteSubordinateCommand { DeleteSubordinateDto = deleteSubordinateDto, UserId = userId };
            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode, response);
        }
    }
}
