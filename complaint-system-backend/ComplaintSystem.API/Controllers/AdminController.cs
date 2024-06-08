using ComplaintSystem.Application.DTOs.ComplaintDto;
using ComplaintSystem.Application.DTOs.ComplaintLogDto;
using ComplaintSystem.Application.DTOs.ManagerDto;
using ComplaintSystem.Application.DTOs.PaginationDto;
using ComplaintSystem.Application.Features.Admins.Requests.Queries;
using ComplaintSystem.Application.Features.ComplaintLogs.Requests.Commands;
using ComplaintSystem.Application.Features.ComplaintLogs.Requests.Queries;
using ComplaintSystem.Application.Features.Complaints.Requests.Commands;
using ComplaintSystem.Application.Features.Complaints.Requests.Queries;
using ComplaintSystem.Application.Features.Managers.Requests.Commands;
using ComplaintSystem.Application.Features.Managers.Requests.Queries;
using ComplaintSystem.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ComplaintSystem.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "Admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _contextAccessor;
        public AdminController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _contextAccessor = httpContextAccessor;
        }
        [HttpGet]
        [Route("GetProfile")]
        public async Task<ActionResult<BaseResponseClass>> GetProfile()
        {
            var adminId = new Guid(_contextAccessor.HttpContext.User.FindFirstValue("userid"));
            var request = new GetAdminProfileRequest { AdminId = adminId };
            var response = await _mediator.Send(request);

            return StatusCode(response.StatusCode, response);
        }



        [HttpGet]
        [Route("GetManagers")]
        public async Task<ActionResult<BaseResponseClass>> GetManagers()
        {
            var adminId = new Guid(_contextAccessor.HttpContext.User.FindFirstValue("userid"));
            var request = new GetManagersRequest { AdminId = adminId };
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("GetAllComplaints")]
        public async Task<ActionResult<BaseResponseClass>> GetAllComplaints([FromQuery] PaginationDto paginationDto)
        {
            var request = new GetAllComplaintsRequest { PaginationDto = paginationDto };
            var response = await _mediator.Send(request);

            return StatusCode(response.StatusCode, response);
        }
        [HttpGet]
        [Route("GetRecievedComplaints")]
        public async Task<ActionResult<BaseResponseClass>> GetRecievedComplaints([FromQuery] PaginationDto PaginationDto)
        {
            var request = new GetRecievedComplaintForAdminRequest { Status = "recieved", PaginationDto = PaginationDto };
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("GetAcceptedComplaints")]
        public async Task<ActionResult<BaseResponseClass>> GetAcceptedComplaints([FromQuery] PaginationDto PaginationDto)
        {
            var request = new GetRecievedComplaintForAdminRequest { Status = "accepted", PaginationDto = PaginationDto };
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("GetPendingComplaintsLogs")]
        public async Task<ActionResult<BaseResponseClass>> GetComplaintsToAssign([FromQuery] PaginationDto PaginationDto)
        {
            var adminId = new Guid(_contextAccessor.HttpContext.User!.FindFirstValue("userid"));
            var request = new GetComplaintLogsForAdminRequest { AdminId = adminId, Status = "pending", PaginationDto = PaginationDto };
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);

        }
        [HttpGet]
        [Route("GetComplaintById")]
        public async Task<ActionResult<BaseResponseClass>> GetComplaintById(Guid ComplaintID)
        {
            var request = new GetComplaintByIdRequest { ComplaintId = ComplaintID };
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("GetComplaintLogsToUpdate")]
        public async Task<ActionResult<BaseResponseClass>> GetComplaintLogsToUpdate([FromQuery] PaginationDto PaginationDto)
        {
            var adminId = new Guid(_contextAccessor.HttpContext.User!.FindFirstValue("userid"));
            var request = new GetComplaintLogsForAdminRequest { AdminId = adminId, Status = "submitted", PaginationDto = PaginationDto };
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);

        }

        [HttpPost]
        [Route("CreateManagers")]
        public async Task<ActionResult<BaseResponseClass>> CreateManager(CreateManagerDto createManagerDto)
        {
            var adminId = new Guid(_contextAccessor.HttpContext.User!.FindFirstValue("userid"));
            var command = new CreateManagerRequest { CreateManagerDto = createManagerDto, AdminId = adminId };
            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch]
        [Route("UpdateManager")]
        public async Task<ActionResult<BaseResponseClass>> UpdateManager(UpdateManagerDto updateManagerDto)
        {
            var adminId = new Guid(_contextAccessor.HttpContext.User!.FindFirstValue("userid"));
            var command = new UpdateManagerCommand { UpdateManagerDto = updateManagerDto, AdminId = adminId };
            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Route("AssignManagers")]
        public async Task<ActionResult<BaseResponseClass>> AssignManager(CreateComplaintLogDto createComplaintLogDto)
        {
            var adminId = new Guid(_contextAccessor.HttpContext.User!.FindFirstValue("userid"));
            var command = new CreateComplaintLogCommand { ComplaintLogDto = createComplaintLogDto, AdminId = adminId };
            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch]
        [Route("UpdateComplaintStatus")]
        public async Task<ActionResult<BaseResponseClass>> UpdateComplaintStatus(UpdateComplaintDto updateComplainDto)
        {
            var command = new UpdateComplaintStatusCommand { UpdateComplainDto = updateComplainDto };
            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch]
        [Route("UpdateReportStatus")]
        public async Task<ActionResult<BaseResponseClass>> UpdateReportStatus(UpdateComplaintLogStatusControllerDto updateComplaintLogStatusControllerDto)
        {
            var userId = new Guid(_contextAccessor.HttpContext.User.FindFirstValue("userId"));
            UpdateComplaintLogStatusDto updateComplaintLogStatusDto = new UpdateComplaintLogStatusDto
            {
                ComplaintLogId = updateComplaintLogStatusControllerDto.ComplaintLogId,
                StatusChangerId = userId,
                Status = updateComplaintLogStatusControllerDto.Status,
                Role = "admin"
            };
            var command = new UpdateComplaintLogStatusForAdminCommand { ComplaintLogStatus = updateComplaintLogStatusDto };
            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode, response);
        }
    }
}
