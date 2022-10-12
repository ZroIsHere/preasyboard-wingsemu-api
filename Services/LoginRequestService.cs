namespace noswebapp_api.Services;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Nest;
using noswebapp_api.Entities;
using noswebapp_api.Helpers;
using noswebapp_api.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AuthenticateRequest = Models.AuthenticateRequest;
using AuthenticateResponse = Models.AuthenticateResponse;

public interface ILoginRequestService
{
    List<LoginRequest> GetChallenges();
    LoginRequest GetChallengeById(int id);

    LoginRequest AddChallenge();

    AuthenticateResponse Authenticate(AuthenticateRequest model);
    IEnumerable<LoginRequest> GetAll();

    LoginRequest GetById(int id);

    String RandomString(int size, bool lowerCase);
}

public class LoginRequestService : ILoginRequestService
// TODO: Remove all the logging.
{
  


    private readonly AppSettings _appSettings;
    private readonly Random _random;
    public IDictionary<int, LoginRequest> _challengeAttempts = new Dictionary<int, LoginRequest>();
    //FRO ZRO : I CREATED A DIC TO SAVE ALL THE REQUESTS, TODO: DELETE ALL REQUEST THAT HAVE A TIMESTAMP DELTA BIGGER THAN SOME SECONDS, BUT I WILL IMPLEMENT LATER
    public LoginRequestService(IOptions<AppSettings> appSettings)
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

        return (IEnumerable<LoginRequest>)_challengeAttempts;
    }



    public LoginRequest GetById(int id)
    {
        return _challengeAttempts[0];
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
        if (lowerCase)
            return builder.ToString().ToLower();
        else
            return builder.ToString();
    }

   
    public List<LoginRequest> GetChallenges()
    {
        Console.WriteLine("UserService::GetChallenges " + _challengeAttempts.Count);
        return _challengeAttempts.Values.ToList();
    }

    public LoginRequest GetChallengeById(int id)
    {
        Console.WriteLine("AAA");
        Console.WriteLine(id);
        Console.WriteLine("BBB");

        if (_challengeAttempts.ContainsKey(id))
        {
            return _challengeAttempts[id];
        }
        else return null;
    }

    public LoginRequest AddChallenge()
    {
        // Remember to add a timestamp to the challenges and to make
        // them expire after a timeout.
        // You could filter out expired challenges every time Add/GetChallenge 
        // are invoked.

        var challengeAttempt = new LoginRequest { Id = _random.Next(1, 255), Challenge = RandomString(2048, false), TimeStamp = DateTime.Now.ToFileTime() };
        _challengeAttempts.Add(challengeAttempt.Id, challengeAttempt);
        return challengeAttempt;
    }

}