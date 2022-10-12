namespace noswebapp_api;

using Microsoft.AspNetCore.Mvc;
using Nest;
using noswebapp_api.Helpers;
using noswebapp_api.Services;
using noswebapp_api.Models;

using System.Web.Mvc;
using AuthorizeAttribute = Helpers.AuthorizeAttribute;
using ControllerBase = Microsoft.AspNetCore.Mvc.ControllerBase;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using AuthenticateRequest = Models.AuthenticateRequest;
using System.Collections.Generic;
using noswebapp_api.Entities;
using System;
using Microsoft.AspNetCore.Http;

[ApiController]
[Route("[controller]")]
public class LoginRequestsController : ControllerBase
{
    private ILoginRequestService _loginRequestService;

    public LoginRequestsController(ILoginRequestService loginRequestService)
    {
        Console.WriteLine("Started");
        _loginRequestService = loginRequestService;
    }

    [Produces("application/json")]
    [HttpGet("login")]
    public LoginRequest Login()
    {
        return _loginRequestService.AddChallenge();
    }

    [HttpGet("users")]
    public List<LoginRequest> Users()
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