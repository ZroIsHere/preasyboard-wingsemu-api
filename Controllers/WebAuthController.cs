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
using noswebapp_api;
using noswebapp_api.Attributes;
using noswebapp_api.Extensions;
using noswebapp_api.Managers;
using noswebapp_api.Services.Interfaces;
using noswebapp.RequestEntities;
using WingsAPI.Communication;
using noswebapp_api.RequestEntities;
using System.Collections.Generic;

namespace noswebapp_api.Controllers;

[ApiController]
[Route("[controller]")]
public class WebAuthController : ControllerBase
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
    public WebAuthRequest LoginRequest()
    {
        return _webAuthRequestService.AddChallenge();
    }

    [HttpGet("AllReqs")]
    public List<WebAuthRequest> AllReqs()
    {

        return _webAuthRequestService.GetChallenges();
    }

    [HttpGet("Byid")]
    public WebAuthRequest AllReqs([FromQuery] int id)
    {

        return _webAuthRequestService.GetChallengeById(id);
    }
    
    [Authorize]
    [HttpGet("GetMessage")]
    public JsonResult GetMessage()
    {
        return  new JsonResult(new { message = "Authorized and ready to communicate" }) { StatusCode = StatusCodes.Status200OK }; ;
    }

    [HttpPost("Auth")]
    public IActionResult Authenticate(AuthenticateRequest loginReq)
    {
        var response = _webAuthRequestService.Authenticate(loginReq);

        if (response == null)
            return BadRequest(new { message = "Challenge not valid" });
        return Ok(response);
    }
}