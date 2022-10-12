namespace noswebapp_api.Helpers;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using noswebapp_api.Services;
using noswebapp_api.Models;
using noswebapp_api.Helpers;


using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using noswebapp.Tmp;
using System.Linq;
using System;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly NosWebAppEnvVariables _appSettings;

    public JwtMiddleware(RequestDelegate next, IOptions<NosWebAppEnvVariables> appSettings)
    {
        _next = next;
        _appSettings = appSettings.Value;
    }

    public async Task Invoke(HttpContext context, ILoginRequestService userService)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null)
            attachUserToContext(context, userService, token);

        await _next(context);
    }

    private void attachUserToContext(HttpContext context, ILoginRequestService userService, string token)
    {
        try
        {
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
            context.Items["User"] = userService.GetById(userId);
        }
        catch
        {
            // do nothing if jwt validation fails
            // user is not attached to context so request won't have access to secure routes
        }
    }
}