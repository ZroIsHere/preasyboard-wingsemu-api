using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PreasyBoard.Api.Attributes;
using System;

namespace PreasyBoard.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : Controller
{
    private readonly IServiceProvider _container;

    private readonly ILogger<TestController> _logger;

    public TestController(ILogger<TestController> logger, IServiceProvider container)
    {
        _logger = logger;
        _container = container;
    }

    [Authorize]
    [HttpGet("TestApi")]
    public ActionResult TestApi() => Ok("Working");
}
