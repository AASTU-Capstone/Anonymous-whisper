using ComplaintSystem.Application.DTOs.SubordinateDto;
using ComplaintSystem.Application.Features.Subordinates.Requests.Commands;
using ComplaintSystem.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ComplaintSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _contextAccessor;
        public ManagerController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor;
            _mediator = mediator;
        }
        [HttpPost]
        [Route("CreateSubordinate")]
        public async Task<ActionResult<BaseResponseClass>> CreateSubordinates(CreateSubordinateControllerDto createSubordinateControllerDto)
        {
            var ManagerId = new Guid(_contextAccessor.HttpContext.User.FindFirstValue("userId"));
            CreateSubordinateDto createSubordinateDto = new CreateSubordinateDto
            {
                MitigatedCount = createSubordinateControllerDto.MitigatedCount,
                Name = createSubordinateControllerDto.Name,
                ManagerId = ManagerId,

            };

            var command = new CreateSubordinateRequest { CreateSubordinateDto = createSubordinateDto };
            var response = await _mediator.Send(command);

            return StatusCode(response.StatusCode, response);
        }
    }
}
