using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PreasyBoard.Api;
using PreasyBoard.Api.Attributes;
using PreasyBoard.Api.Extensions;
using PreasyBoard.Api.Managers;
using PreasyBoard.Api.Services.Interfaces;
using PreasyBoard.Api.RequestEntities;
using WingsAPI.Communication;
using PreasyBoard.Api.RequestEntities;
using System.Collections.Generic;

namespace PreasyBoard.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WebAuthController : Controller
{
    private readonly IServiceProvider _container;
    private IWebAuthService _webAuthRequestService;
    private readonly ILogger<WebAuthController> _logger;

    public WebAuthController(ILogger<WebAuthController> logger, IServiceProvider container, IWebAuthService webAuthRequestService)
    {
        _logger = logger;
        _container = container;
        _webAuthRequestService = webAuthRequestService;

    }
    
    [HttpGet("LoginRequest")]
    public WebAuthRequest LoginRequest() => _webAuthRequestService.AddChallenge();

    [HttpGet("AllReqs")]
    public List<WebAuthRequest> AllReqs() => _webAuthRequestService.GetChallenges();

    [HttpGet("Byid")]
    public WebAuthRequest AllReqs([FromQuery] int id) => _webAuthRequestService.GetChallengeById(id);

    [Authorize]
    [HttpGet("GetMessage")]
    public JsonResult GetMessage() => new JsonResult(new { message = "Authorized and ready to communicate" }) { StatusCode = StatusCodes.Status200OK };

    [HttpPost("Auth")]
    public IActionResult Authenticate(AuthenticateRequest loginReq)
    {
        var response = _webAuthRequestService.Authenticate(loginReq);

        if (response == null)
            return BadRequest(new { message = "Challenge not valid" });
        return Ok(response);
    }
}