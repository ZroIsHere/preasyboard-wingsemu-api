using noswebapp_api.RequestEntities;
using noswebapp_api.Services.Interfaces;
using noswebapp.RequestEntities;

namespace noswebapp_api;

using Microsoft.AspNetCore.Mvc;
using noswebapp_api.Managers;
using noswebapp_api.Services;

using System.Web.Mvc;
using ControllerBase = Microsoft.AspNetCore.Mvc.ControllerBase;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Http;

[ApiController]
[Route("[controller]")]
public class LoginRequestsController : ControllerBase
{
    private IWebAuthService _loginRequestService;

    public LoginRequestsController(IWebAuthService loginRequestService)
    {
        _loginRequestService = loginRequestService;
    }

    [Produces("application/json")]
    [HttpGet("login")]
    public WebAuthRequest Login()
    {
        return _loginRequestService.AddChallenge();
    }

    [HttpGet("users")]
    public List<WebAuthRequest> Users()
    {
        return _loginRequestService.GetChallenges();
    }

    [HttpPost("authenticate")]
    public IActionResult Authenticate(AuthenticateRequest loginReq)
    {
        var response = _loginRequestService.Authenticate(loginReq);

        if (response == null)
            return BadRequest(new { message = "Username or password is incorrect" });

        return Ok(response);
    }

    [Authorize]
    [HttpGet]
    public IActionResult GetAll()
    {
        var users = _loginRequestService.GetAll();
        return Ok(users);
    }

    [Authorize]
    [HttpGet("message")]
    public IActionResult Msg()
    {
        return Ok("hello worls");
    }
}