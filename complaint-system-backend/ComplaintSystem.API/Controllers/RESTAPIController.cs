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
            var result = await _imaggaService.Tagger("https://res.cloudinary.com/dwmujpiu9/image/upload/v1715959505/giv6tgyb5bzn6wmtlsx4.png");
            return Ok( result);
        }
    }
}
