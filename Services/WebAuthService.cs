
using PreasyBoard.Api.RequestEntities;
using PreasyBoard.Api.ResponseEntities;
using PreasyBoard.Api.Services.Interfaces;
using PreasyBoard.Api.RequestEntities;
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
    public static Dictionary<int, WebAuthRequest> _challengeAttempts = new();

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
        return null;
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
                string issuer = PreasyBoardEnvVariables.JwtIssuer;
                string audience = PreasyBoardEnvVariables.JwtAudience;
                byte[] encKey = Encoding.ASCII.GetBytes(PreasyBoardEnvVariables.JwtKey);
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

    private string RandomString(int size, bool lowerCase = false)
    {
        var builder = new StringBuilder();
        var random = new Random();
        for (int i = 1; i < size + 1; i++)
        {
            builder.Append(Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65))));
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
        var challengeAttempt = new WebAuthRequest(RandomString(16));
        _challengeAttempts.Add(challengeAttempt.Id, challengeAttempt);
        return challengeAttempt;
    }

    public List<WebAuthRequest> GetChallenges() => _challengeAttempts.Values.ToList();

    public WebAuthRequest GetChallengeById(int id) => _challengeAttempts[id];



}