
using PreasyBoard.Api.RequestEntities;
using PreasyBoard.Api.ResponseEntities;
using PreasyBoard.Api.Services.Interfaces;
using noswebapp.RequestEntities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PreasyBoard.Api.Managers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using PreasyBoard.Api.Configuration;
using PreasyBoard.Api.Extensions;

namespace PreasyBoard.Api.Services;

public class WebAuthService : IWebAuthService
// TODO: Remove all the logging.
{
    private readonly Random _random;
    public static Dictionary<int, WebAuthRequest> _challengeAttempts = new();

    public WebAuthService()
    {
        _random = new Random();
    }

    public WebAuthResponse Authenticate(AuthenticateRequest authReq)
    {

        WebAuthRequest currentWebAuthRequest = GetChallengeById(authReq.Id);
        if (_challengeAttempts.ContainsKey(authReq.Id))
        {
            WebAuthRequest oldreq = null;
            _challengeAttempts.TryGetValue(authReq.Id, out oldreq);
            

            if (oldreq != null)
            {
                if (authReq.Id.Equals(oldreq.Id) && authReq.Challenge.DecryptWithPublicKey().Equals(oldreq.Challenge))
                {
                    var token = GenerateJwtToken(currentWebAuthRequest);
                    return new WebAuthResponse(currentWebAuthRequest, token);
                }
            }
        }
        if (currentWebAuthRequest == null) return null;


        return null;
    }

    public IEnumerable<WebAuthRequest> GetAll()
    {

        return (IEnumerable<WebAuthRequest>)_challengeAttempts;
    }



    public WebAuthRequest GetById(int id)
    {
        return _challengeAttempts[id];
    }

    private string GenerateJwtToken(WebAuthRequest req)
    {
        if (_challengeAttempts.ContainsKey(req.Id))
        {
            WebAuthRequest oldreq = null;

            _challengeAttempts.TryGetValue(req.Id, out oldreq);
            if (oldreq != null)
            {

                _challengeAttempts = new();
                string issuer = NosWebAppEnvVariables.JwtIssuer;
                string audience = NosWebAppEnvVariables.JwtAudience;
                byte[] encKey = Encoding.ASCII.GetBytes(NosWebAppEnvVariables.JwtKey);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] {new Claim("id", oldreq.Id.ToString())}),
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    SigningCredentials = new SigningCredentials
                    (new SymmetricSecurityKey(encKey),
                        SecurityAlgorithms.HmacSha512Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
                var stringToken = tokenHandler.WriteToken(token);

                return stringToken;
                
            }
        }

        return null;

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

    public WebAuthRequest AddChallenge()
    {
        var challengeAttempt = new WebAuthRequest() { Id = _random.Next(1, 255), Challenge = RandomString(16, false), TimeStamp = DateTime.UtcNow.ToFileTime() };
        _challengeAttempts.Add(challengeAttempt.Id, challengeAttempt);
        return challengeAttempt;
    }

    public List<WebAuthRequest> GetChallenges()
    {
        return _challengeAttempts.Values.ToList();
    }

    public  WebAuthRequest GetChallengeById(int id)
    {
         
        return _challengeAttempts[id];
    }

    

}