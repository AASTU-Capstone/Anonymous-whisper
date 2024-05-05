using ComplaintSystem.Application.DTOs.ComplaintDto;
using ComplaintSystem.Application.Features.Complaints.Handlers.Queries;
using ComplaintSystem.Application.Features.Complaints.Requests.Commands;
using ComplaintSystem.Application.Features.Complaints.Requests.Queries;
using ComplaintSystem.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ComplaintSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(policy:"User")]
    public class ComplaintController : ControllerBase
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMediator _mediator;
        public ComplaintController(IHttpContextAccessor httpContextAccessor, IMediator mediator)
        {
            _contextAccessor = httpContextAccessor;
            _mediator = mediator;
        }
        [HttpGet]
        [Route("GetAcceptedComplaints")]
        public async Task<ActionResult<BaseResponseClass>> GetAcceptedComplaints()
        {
            var userId = new Guid(_contextAccessor.HttpContext.User.FindFirstValue("userid"));
            var request = new GetUserAcceptedComplaintsRequest { UserId = userId, Status = "Accepted" };
            var response = await _mediator.Send(request);

            return StatusCode(response.StatusCode, response);
        }
        [HttpGet]
        [Route("GetRejectedComplaints")]
        public async Task<ActionResult<BaseResponseClass>> GetResolvedComplaints()
        {
            var userId = new Guid(_contextAccessor.HttpContext.User.FindFirstValue("userid"));
            var request = new GetRejectedComplaintsRequest { UserId = userId, Status = "rejected" };
            var response = await _mediator.Send(request);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Route("CreateComplaint")]
        public async Task<ActionResult<BaseResponseClass>> CreateComplaint([FromForm]CreateComplaintControllerDto createComplaintDto)
        {
            var userId = new Guid(_contextAccessor.HttpContext.User.FindFirstValue("userid"));
            var command = new CreateComplaintCommand { UserId = userId, CreateComplaintDto = createComplaintDto };
            var response = await _mediator.Send(command);

            return StatusCode(response.StatusCode, response);
        }

    }
}
