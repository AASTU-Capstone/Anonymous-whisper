using ComplaintSystem.Application.Features.ChatBots.Requests.Queries;
using ComplaintSystem.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComplaintSystem.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "User")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ChatController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Route("MessageChat")]
        public async Task<ActionResult<BaseResponseClass>> MessageChat(string message)
        {
            var request = new GetChatMessageRequest { Message = message };
            var response = await _mediator.Send(request);

            return StatusCode(response.StatusCode, response);
        }
    }
}
