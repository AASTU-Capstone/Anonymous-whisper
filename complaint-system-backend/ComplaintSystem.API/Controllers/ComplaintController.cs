using ComplaintSystem.Application.DTOs.ComplaintDto;
using ComplaintSystem.Application.Features.Complaints.Requests.Commands;
using ComplaintSystem.Application.Features.Complaints.Requests.Queries;
using ComplaintSystem.Application.Responses;
using ComplaintSystem.Application.DTOs.PaginationDto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ComplaintSystem.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "user")]
    [ApiController]
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
        [Route("GetAllComplaintsForUser")]
        public async Task<ActionResult<BaseResponseClass>> GetAllComplaintsForUser([FromQuery] PaginationDto pagination)
        {
            var userId = new Guid(_contextAccessor.HttpContext.User.FindFirstValue("userid"));
            var request = new GetAllComplaintsForUserRequest { UserId = userId, PaginationDto = pagination };
            var response = await _mediator.Send(request);

            return StatusCode(response.StatusCode, response);
        }

        //[HttpGet]
        //[Route("GetAcceptedComplaints")]
        //public async Task<ActionResult<BaseResponseClass>> GetAcceptedComplaints([FromQuery] PaginationDto PaginationDto)
        //{
        //    var userId = new Guid(_contextAccessor.HttpContext.User.FindFirstValue("userid"));
        //    var request = new GetUserAcceptedComplaintsRequest { UserId = userId, PaginationDto = PaginationDto };
        //    var response = await _mediator.Send(request);

        //    return StatusCode(response.StatusCode, response);
        //}
        //[HttpGet]
        //[Route("GetRejectedComplaints")]
        //public async Task<ActionResult<BaseResponseClass>> GetResolvedComplaints([FromQuery] PaginationDto PaginationDto)
        //{
        //    var userId = new Guid(_contextAccessor.HttpContext.User.FindFirstValue("userid"));
        //    var request = new GetRejectedComplaintsRequest { UserId = userId, Status = "rejected", PaginationDto = PaginationDto };
        //    var response = await _mediator.Send(request);

        //    return StatusCode(response.StatusCode, response);
        //}

        [HttpGet]
        [Route("GetComplaintByID")]
        public async Task<ActionResult<BaseResponseClass>> GetComplaintById(Guid ComplaintId)
        {
            var request = new GetComplaintByIdRequest { ComplaintId = ComplaintId };
            var response = await _mediator.Send(request);

            return StatusCode(response.StatusCode, response);
        }
        [HttpGet]
        [Route("SearchComplaints")]
        public async Task<ActionResult<BaseResponseClass>> SearchComplaints(string keyword, string? category, string dateOrder, [FromQuery] PaginationDto PaginationDto)
        {
            var request = new SearchComplaintRequest { Keyword = keyword , Category = category, DateOrder = dateOrder, PaginationDto = PaginationDto};
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Route("CreateComplaint")]
        public async Task<ActionResult<BaseResponseClass>> CreateComplaint([FromForm] CreateComplaintControllerDto createComplaintDto)
        {
            var userId = new Guid(_contextAccessor.HttpContext.User.FindFirstValue("userid"));
            var command = new CreateComplaintCommand { UserId = userId, CreateComplaintDto = createComplaintDto };
            var response = await _mediator.Send(command);

            return StatusCode(response.StatusCode, response);
        }

    }
}
