using ComplaintSystem.Application.DTOs.ComplaintLogDto;
using ComplaintSystem.Application.DTOs.ManagerDto;
using ComplaintSystem.Application.Features.ComplaintLogs.Requests.Commands;
using ComplaintSystem.Application.Features.ComplaintLogs.Requests.Queries;
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
        [Route("GetManagers")]
        public async Task<ActionResult<BaseResponseClass>> GetManagers()
        {
            var adminId = new Guid(_contextAccessor.HttpContext.User.FindFirstValue("userid"));
            var request = new GetManagersRequest { AdminId = adminId };
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("GetRecievedComplaints")]
        public async Task<ActionResult<BaseResponseClass>> GetRecievedComplaints()
        {
            var request = new GetRecievedComplaintForAdminRequest { };
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet]
        [Route("GetComplaintLogs")]
        public async Task<ActionResult<BaseResponseClass>> GetComplaintLogs()
        {
            var adminId = new Guid(_contextAccessor.HttpContext.User!.FindFirstValue("userid"));
            var request = new GetComplaintLogsForAdminRequest { AdminId = adminId };
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);

        }

        [HttpPost]
        [Route("CreateManagers")]
        public async Task<ActionResult<BaseResponseClass>> CreateManager(CreateManagerDto createManagerDto)
        {
            var adminId = new Guid( _contextAccessor.HttpContext.User!.FindFirstValue("userid"));
            var command = new CreateManagerRequest { CreateManagerDto = createManagerDto, AdminId = adminId };
            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Route("AssignManagers")]
        public async Task<ActionResult<BaseResponseClass>> AssignManager(CreateComplaintLogDto createComplaintLogDto)
        {
            var command = new CreateComplaintLogCommand { ComplaintLogDto = createComplaintLogDto };
            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPatch]
        [Route("UpdateReportStatus")]
        public async Task<ActionResult<BaseResponseClass>> UpdateReportStatus(UpdateComplaintLogStatusDto updateComplaintLogStatusDto)
        {
            var command = new UpdateComplaintLogStatusCommand { ComplaintLogStatus = updateComplaintLogStatusDto };
            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode, response);
        }
    }
}
