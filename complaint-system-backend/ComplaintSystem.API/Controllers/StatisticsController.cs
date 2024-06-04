using ComplaintSystem.Application.Features.Complaints.Requests.Queries;
using ComplaintSystem.Application.Features.CorruptionTrends.Requests.Queries;
using ComplaintSystem.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComplaintSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _contextAccessor;
        public StatisticsController(IMediator mediator, IHttpContextAccessor contextAccessor)
        {

            _contextAccessor = contextAccessor;
            _mediator = mediator;

        }
        [HttpGet]
        [Authorize(Policy = "Worker")]
        [Route("GetCorruptionTrends")]
        public async Task<ActionResult<BaseResponseClass>> GetCorruptionTrends()
        {
            var request = new GetCorruptionTrendsRequest { };
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }
        // for user and admin
        [HttpGet]
        [Authorize(Policy = "Usin")]
        [Route("GetComplaintStatistics")]
        public async Task<ActionResult<BaseResponseClass>> GetComplaintStatistics(Guid? UserId)
        {
            var request = new GetComplaintStatisticsRequest { UserId = UserId };
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        //for managerr and subordinate
        [HttpGet]
        [Authorize(Policy = "Worker")]
        [Route("GetComplaintLogStatistics")]
        public async Task<ActionResult<BaseResponseClass>> GetComplaintLogStatistics(Guid? ManagerId, Guid? SubordinateId)
        {
            var request = new GetComplaintLogsStatisticsRequest { ManagerId = ManagerId, SubordinateId = SubordinateId };
            var response = await _mediator.Send(request);

            return StatusCode(response.StatusCode, response);
        }
    }
}
