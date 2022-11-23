using PreasyBoard.Api.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Linq;
using System;
using PreasyBoard.Api.Configuration;

namespace PreasyBoard.Api.Managers;

public class JwtManager
{
    private readonly RequestDelegate _next;

    public JwtManager(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IWebAuthService authRequestService)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null)
        {
            AttachUserToContext(context, authRequestService, token);
        }

        await _next(context);
    }

    private void AttachUserToContext(HttpContext context, IWebAuthService authRequestService, string token)
    {
        try
        {
            string issuer = NosWebAppEnvVariables.JwtIssuer;
            string audience = NosWebAppEnvVariables.JwtAudience;          
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(NosWebAppEnvVariables.JwtKey);

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

            // attach user to context on successful jwt validation
       
            context.Items["WebAuthRequest"] = authRequestService;

        }
        catch
        {
            // do nothing if jwt validation fails
            // user is not attached to context so request won't have access to secure routes
        }
    }
}