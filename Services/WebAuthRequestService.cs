using noswebapp_api.InternalEntities;
using noswebapp_api.RequestEntities;
using noswebapp_api.ResponseEntities;
using noswebapp_api.Services.Interfaces;

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

        LoginRequest currentLoginRequest = GetChallengeById(loginReq.Id);
        
        // return null if user not found
        if (currentLoginRequest == null) return null;

        //FOR ZRO : HERE, SHOULD BE THE LOGIC FOR CHECK ENCRYPT DECRYPT,
        //BEFORE GENERATING TOKEN
        
        // authentication successful so generate jwt token
        var token = generateJwtToken(currentLoginRequest);
        
        return new AuthenticateResponse(currentLoginRequest, token);
    }

    public IEnumerable<LoginRequest> GetAll()
    {

        return (IEnumerable<LoginRequest>)StaticData.ChallengeAttempts;
    }



    public LoginRequest GetById(int id)
    {
        return StaticData.ChallengeAttempts[0];
    }

    // helper methods

    private string generateJwtToken(LoginRequest user)
    {
        // generate token that is valid for 7 days
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(NosWebAppEnvVariables.JwtKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
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

   
    public List<LoginRequest> GetChallenges()
    {
        Console.WriteLine("UserService::GetChallenges " + StaticData.ChallengeAttempts.Count);
        return StaticData.ChallengeAttempts.Values.ToList();
    }

    public LoginRequest GetChallengeById(int id)
    {
        Console.WriteLine("AAA");
        Console.WriteLine(id);
        Console.WriteLine("BBB");

        if (StaticData.ChallengeAttempts.ContainsKey(id))
        {
            return StaticData.ChallengeAttempts[id];
        }
        return null;
    }

    public LoginRequest AddChallenge()
    {
        var challengeAttempt = new LoginRequest { Id = _random.Next(1, 255), Challenge = RandomString(2048, false), TimeStamp = DateTime.Now.ToFileTime() };
        StaticData.ChallengeAttempts.Add(challengeAttempt.Id, challengeAttempt);
        return challengeAttempt;
    }

}