using System.Security.Claims;
using ComplaintSystem.Application.DTOs.NotificationDto;
using ComplaintSystem.Application.Features.Notifications.Request.Commands;
using ComplaintSystem.Application.Features.Notifications.Request.Queries;
using ComplaintSystem.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComplaintSystem.API.Controllers
{

    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMediator _mediator;

        public NotificationController(IHttpContextAccessor httpContextAccessor, IMediator mediator)
        {
            _contextAccessor = httpContextAccessor;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("GetUnreadNotifications")]
        public async Task<ActionResult<BaseResponseClass>> GetUnreadNotifications()
        {
            var userId = new Guid(_contextAccessor.HttpContext.User.FindFirstValue("userid"));
            var request = new GetUnreadNotificationsRequest { UserId = userId };
            var response = await _mediator.Send(request);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Route("MarkNotificationsToRead")]
        public async Task<ActionResult<BaseResponseClass>> MarkNotificationsToRead([FromBody] MarkNotificationsToReadCommand command)
        {
            var request = new MarkNotificationsToReadCommand { NotificationIds = command.NotificationIds };
            var response = await _mediator.Send(request);

            return StatusCode(response.StatusCode, response);
        }
    }
}