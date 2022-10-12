using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using noswebapp_api;
using noswebapp_api.Attributes;
using noswebapp_api.Helpers;
using noswebapp.Helpers;
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
    
    [HttpPost("LoginRequest")]
    public HttpResponseMessage LoginRequest([FromBody] string value)
    {

        return JsonSerializer.Deserialize<HttpResponseMessage>(value);
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
        var incoming_req = new WebAuthRequest(req);

        if (req.id == Convert.ToInt32(XMLHelper.XmlReadId()) && req.challenge == XMLHelper.XmlReadChallenge())
        {
            string issuer = NosWebAppEnvVariables.JwtIssuer;
            string audience = NosWebAppEnvVariables.JwtAudience;
            byte[] key = Encoding.ASCII.GetBytes(NosWebAppEnvVariables.JwtKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, XMLHelper.XmlReadId()),
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
            var jwtToken = tokenHandler.WriteToken(token);
            var stringToken = tokenHandler.WriteToken(token);

            return Ok(stringToken);


        }
        String message = "401 NOT AUTHORIZED";
        return Unauthorized(message);
    }
}