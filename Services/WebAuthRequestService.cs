using noswebapp_api.InternalEntities;
using noswebapp_api.RequestEntities;
using noswebapp_api.ResponseEntities;
using noswebapp_api.Services.Interfaces;
using noswebapp.RequestEntities;

namespace noswebapp_api.Services;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using noswebapp_api.Helpers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

public class WebAuthRequestService : IWebAuthRequestService
// TODO: Remove all the logging.
{
    private readonly AppSettings _appSettings;
    private readonly Random _random;
    public WebAuthRequestService(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
        _random = new Random();
    }

    public AuthenticateResponse Authenticate(AuthenticateRequest loginReq)
    {

        WebAuthRequest currentWebAuthRequest = GetChallengeById(loginReq.Id);
        
        // return null if user not found
        if (currentWebAuthRequest == null) return null;

        //FOR ZRO : HERE, SHOULD BE THE LOGIC FOR CHECK ENCRYPT DECRYPT,
        //BEFORE GENERATING TOKEN
        
        // authentication successful so generate jwt token
        var token = GenerateJwtToken(currentWebAuthRequest);
        
        return new AuthenticateResponse(currentWebAuthRequest, token);
    }

    public IEnumerable<WebAuthRequest> GetAll()
    {

        return (IEnumerable<WebAuthRequest>)StaticDataManagement.ChallengeAttempts;
    }



    public WebAuthRequest GetById(int id)
    {
        return StaticDataManagement.ChallengeAttempts[0];
    }

    // helper methods

    private string GenerateJwtToken(WebAuthRequest user)
    {
        // generate token that is valid for 7 days
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(NosWebAppEnvVariables.JwtKey);
        DateTime ExpireTime = DateTime.UtcNow.AddDays(7);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
            Expires = ExpireTime,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        string token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        StaticDataManagement.ValidatedTokens.Add(token, ExpireTime);
        return token;
    }

    public string RandomString(int size, bool lowerCase)
    {
        var builder = new StringBuilder();
        var random = new Random();
        char ch;
        for (int i = 1; i < size + 1; i++)
        {
            ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
            builder.Append(ch);
        }
        string retval = builder.ToString();
        if (lowerCase)
        {
            return retval.ToLower();
        }
        return retval;
    }

   
    public List<WebAuthRequest> GetChallenges()
    {
        Console.WriteLine("UserService::GetChallenges " + StaticDataManagement.ChallengeAttempts.Count);
        return StaticDataManagement.ChallengeAttempts.Values.ToList();
    }

    public WebAuthRequest GetChallengeById(int id)
    {
        Console.WriteLine("AAA");
        Console.WriteLine(id);
        Console.WriteLine("BBB");

        if (StaticDataManagement.ChallengeAttempts.ContainsKey(id))
        {
            return StaticDataManagement.ChallengeAttempts[id];
        }
        return null;
    }

    public WebAuthRequest AddChallenge()
    {
        var challengeAttempt = new WebAuthRequest() { Id = _random.Next(1, 255), Challenge = RandomString(2048, false), TimeStamp = DateTime.Now.ToFileTime() };
        StaticDataManagement.ChallengeAttempts.Add(challengeAttempt.Id, challengeAttempt);
        return challengeAttempt;
    }

}