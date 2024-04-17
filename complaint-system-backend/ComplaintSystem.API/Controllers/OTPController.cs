using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ComplaintSystem.Application.Features.OTP.Request.Commands;
using ComplaintSystem.Application.Features.OTP.Request.Queries;
using ComplaintSystem.Application.Responses;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ComplaintSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OTPController : ControllerBase
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMediator _mediator;
    
        public OTPController(IHttpContextAccessor httpContextAccessor, IMediator mediator)
        {
            _contextAccessor = httpContextAccessor;
            _mediator = mediator;
        }
        [HttpPost]
        [Route("CreateOTP")]
        public async Task<ActionResult<BaseResponseClass>> CreateOTP(string email )
        {
            var userEmail = _contextAccessor.HttpContext!.User.FindFirstValue("useremail");
            var command = new CreateOtpRequest {  UserEmail = userEmail };
            var resp = await _mediator.Send(command);
            return StatusCode(resp.StatusCode, resp);
        }

        //to verify the otp it belongs to the user and inbound with the expiring time

        [HttpPost]
        [Route("VerifyOTP")]
        public async Task<ActionResult<BaseResponseClass>> VerifyOTP(string email, string OTPCode)
        {
            // use the email to check
            var command = new VerifyOtpRequest { Email = email, OtpCode = OTPCode };
            var resp = await _mediator.Send(command);
            return StatusCode(resp.StatusCode, resp);
        }

    }
}
