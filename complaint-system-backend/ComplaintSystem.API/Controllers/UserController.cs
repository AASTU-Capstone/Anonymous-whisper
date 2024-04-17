using MediatR;
using Microsoft.AspNetCore.Mvc;
using  ComplaintSystem.Application.Responses;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Cors;
using  ComplaintSystem.Application.DTOs.UserDto;
using  ComplaintSystem.Application.Features.User.Request.Commands;
using  ComplaintSystem.Application.Features.User.Request.Queries;


namespace   ComplaintSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController: ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<ActionResult<BaseResponseClass>> CreateUser([FromBody] CreateUserDto createuserdto)
        {
            var command = new CreateUserRequest {User = createuserdto };
            var result = await _mediator.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        //future admin authorization
        [HttpGet]
        [Route("GetUsers")]
        public async Task<ActionResult<BaseResponseClass>> GetAllUsers()
        {
            var query = new GetAllUsersRequest();
            var result = await _mediator.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        [Route("GetUserById")]
        public async Task<ActionResult<BaseResponseClass>> GetUsers(Guid Id)
        {
            var query = new GetUserByIdRequest {Id = Id};
            var result = await _mediator.Send(query);
            return StatusCode(result.StatusCode, result);
        }
    }  
}