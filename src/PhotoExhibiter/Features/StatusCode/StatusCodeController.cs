using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Diagnostics;

namespace PhotoExhibiter.Features.StatusCode
{
    public class StatusCodeController : Controller
    {
        private readonly ILogger<StatusCodeController> _logger;

        public StatusCodeController(ILogger<StatusCodeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("/StatusCode/{statusCode}")]
        public IActionResult Index(int statusCode)
        {
            var reExecute = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            return View(statusCode);
        }
    }
}
