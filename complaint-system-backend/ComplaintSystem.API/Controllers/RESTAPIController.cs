using ComplaintSystem.Application.Persistence.Contracts.APIs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComplaintSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RESTAPIController : ControllerBase
    {
        private readonly IImaggaService _imaggaService;
        public RESTAPIController(IImaggaService imaggaService)
        {
            _imaggaService = imaggaService;
        }

        [HttpPost]
        [Route("Categorize")]
        public async Task<IActionResult> Categorize()
        {
            var result = await _imaggaService.Tagger("");
            return Ok( result);
        }
    }
}
