
using System.Security.Claims;
using  ComplaintSystem.Application.Authentication.Request;
using  ComplaintSystem.Application.DTOs.Authentication;
using  ComplaintSystem.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using  ComplaintSystem.Application.Authentication.User.Request;

namespace  ComplaintSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController: ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _contextAccessor;
        
        public AuthenticationController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _contextAccessor = httpContextAccessor;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var query = new LoginCommandRequest(request.Email, request.Password);
            var authResult = await _mediator.Send(query);
            var response = new AuthenticationResponse();
            response.Success = authResult.Success;
            response.Message = authResult.Message;
            response.IsVerified = authResult.Verified;

            if (authResult.Success)
            {
                response.Id = authResult.User.Id;
                response.Email = authResult.User.Email;
                response.Token = authResult.Token;
            }
            
            return StatusCode(authResult.StatusCode, response);
        }
        [HttpPost]
        [Route("FirebaseLogin")]
        public async Task<IActionResult> FirebaseLogin(string fbToken, string userType)
        {
            var command = new FirebaseLoginCommand { fbToken = fbToken, userType = userType };
            var authResponse = await _mediator.Send(command);
            var response = new AuthenticationResponse();
            response.Success = authResponse.Success;
            response.Message = authResponse.Message;
            response.IsVerified = authResponse.Verified;

            if (authResponse.Success)
            {
                response.Id = authResponse.User.Id;
                response.Email = authResponse.User.Email;
                response.Token = authResponse.Token;
            }

            return StatusCode(authResponse.StatusCode, response);
        }

        [HttpPost("forgotPassword")]
        public async Task<ActionResult<BaseResponseClass>> ForgetPassword([FromBody] ForgetPasswordDto forgetPasswordDto)
        {
            var command = new ForgetPasswordCommand {forgetPasswordDto = forgetPasswordDto };
            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost("resetPassword")]
        public async Task<ActionResult<BaseResponseClass>> ResetPassword(ResetPasswordDto passwordDto)
        {
            var command = new ResetPasswordCommand { ResetPassword = passwordDto};
            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode, response);
        }
        [Authorize]
        [HttpPost("changePassword")]
        public async Task<ActionResult<BaseResponseClass>> ChangePassword(ChangePasswordDto passwordDto)
        {
            var userId = new Guid(_contextAccessor.HttpContext!.User.FindFirstValue("userid"));
            var request = new ChangePasswordRequest {ChangePassword = passwordDto, UserId = userId};
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }
    }
}