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
using noswebapp_api.Helpers;
using noswebapp_api.InternalEntities;
using noswebapp_api.Services.Interfaces;
using noswebapp.RequestEntities;
using WingsAPI.Communication;

namespace noswebapp_api.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginRequestController : Controller
{
    private readonly IServiceProvider _container;
    
    private readonly ILogger<LoginRequestController> _logger;

    public LoginRequestController(ILogger<LoginRequestController> logger, IServiceProvider container)
    {
        _logger = logger;
        _container = container;
    }
    
    [HttpGet("LoginRequest")]
    public WebAuthRequest LoginRequest()
    {
        return _container.GetService<IWebAuthRequestService>().AddChallenge();
    }

    [Authorize]
    [HttpGet("GetMessage")]
    public ActionResult GetMessage()
    {
        return Ok("Hello World");
    }

    [HttpPost("CreateToken")]
    public ActionResult CreateToken([FromBody] WebAuthRequest req)
    {
        if (StaticDataManagement.ChallengeAttempts.ContainsKey(req.Id))
        {
            WebAuthRequest oldreq = null;
            StaticDataManagement.ChallengeAttempts.TryGetValue(req.Id, out oldreq);
            if (oldreq != null)
            {
                if (req.Id.Equals(oldreq.Id) && req.Challenge.DecryptWithPrivateKey().Equals(oldreq.Challenge) &&  oldreq.TimeStamp < DateTime.UtcNow.ToFileTime())
                {
                    StaticDataManagement.ChallengeAttempts = new();
                    string issuer = NosWebAppEnvVariables.JwtIssuer;
                    string audience = NosWebAppEnvVariables.JwtAudience;
                    byte[] key = Encoding.ASCII.GetBytes(NosWebAppEnvVariables.JwtKey);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[]
                        {
                            new Claim("Id", Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Sub, oldreq.Id.ToString()),
                            new Claim(JwtRegisteredClaimNames.Jti,
                                Guid.NewGuid().ToString())
                        }),
                        Expires = DateTime.UtcNow.AddMinutes(5),
                        Issuer = issuer,
                        Audience = audience,
                        SigningCredentials = new SigningCredentials
                        (new SymmetricSecurityKey(key),
                            SecurityAlgorithms.HmacSha512Signature)
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
                    var stringToken = tokenHandler.WriteToken(token);

                    return Ok(stringToken);
                }
            }
        }
        
        String message = "401 NOT AUTHORIZED";
        return Unauthorized(message);
    }
}