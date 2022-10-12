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
using noswebapp.Tmp;
using noswebapp_api;
using noswebapp_api.Helpers;
using WingsAPI.Communication;

namespace noswebapp.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : Controller
{
    private readonly IServiceProvider _container;
    
    private readonly ILogger<TestController> _logger;

    public TestController(ILogger<TestController> logger, IServiceProvider container)
    {
        _logger = logger;
        _container = container;
    }

    //[HttpGet("Test")]
    //public BasicRpcResponse Test([FromHeader] string AuthKey, [FromBody] string value)
    //{
    //    if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
    //    {
    //        return null;
    //    }
        
    //    var encryptionKey = RSA.Create(3072); // public key for encryption, private key for decryption
    //    var signingKey = ECDsa.Create(ECCurve.NamedCurves.nistP256); // private key for signing, public key for validation

    //    var encryptionKid = Guid.NewGuid().ToString("N");
    //    var signingKid = Guid.NewGuid().ToString("N");

    //    var privateEncryptionKey = new RsaSecurityKey(encryptionKey) {KeyId = encryptionKid};
    //    var publicEncryptionKey = new RsaSecurityKey(encryptionKey.ExportParameters(false)) {KeyId = encryptionKid};
    //    var privateSigningKey = new ECDsaSecurityKey(signingKey) {KeyId = signingKid};
    //    var publicSigningKey = new ECDsaSecurityKey(ECDsa.Create(signingKey.ExportParameters(false))) {KeyId = signingKid};

    //    if (JwtHelper.DecryptAndValidateJwe(value, publicSigningKey, publicEncryptionKey))
    //    {
    //        return new BasicRpcResponse()
    //        {
    //            ResponseType = RpcResponseType.SUCCESS
    //        };
    //    }

    //    return null;
    //}
    
    [HttpGet("TestAes")]
    public BasicRpcResponse TestAes([FromBody] string value)
    {
        string decrypted = Encryption.Decrypt(value);
        char[] valuechar = value.ToCharArray();
       // Encryption.UpdateKey(new[] { valuechar[0], value[14] });

        return JsonSerializer.Deserialize<BasicRpcResponse>(decrypted);
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
    public ActionResult CreateToken([FromBody] Request req)
    {
        var incoming_req = new Request(req);

        if (req.id == Convert.ToInt32(XMLHandler.xmlReadId()) && req.challenge == XMLHandler.xmlReadChallenge())
        {
            string issuer = NosWebAppEnvVariables.JwtIssuer;
            string audience = NosWebAppEnvVariables.JwtAudience;
            byte[] key = Encoding.ASCII.GetBytes(NosWebAppEnvVariables.JwtKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, XMLHandler.xmlReadId()),
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
            global::Microsoft.IdentityModel.Tokens.SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            var stringToken = tokenHandler.WriteToken(token);

            return Ok(stringToken);


        }
        String message = "401 NOT AUTHORIZED";
        return Unauthorized(message);
    }

    


}