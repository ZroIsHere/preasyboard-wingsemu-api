using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
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

    [HttpGet("Test")]
    public BasicRpcResponse Test([FromHeader] string AuthKey, [FromBody] string value)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
        
        var encryptionKey = RSA.Create(3072); // public key for encryption, private key for decryption
        var signingKey = ECDsa.Create(ECCurve.NamedCurves.nistP256); // private key for signing, public key for validation

        var encryptionKid = Guid.NewGuid().ToString("N");
        var signingKid = Guid.NewGuid().ToString("N");

        var privateEncryptionKey = new RsaSecurityKey(encryptionKey) {KeyId = encryptionKid};
        var publicEncryptionKey = new RsaSecurityKey(encryptionKey.ExportParameters(false)) {KeyId = encryptionKid};
        var privateSigningKey = new ECDsaSecurityKey(signingKey) {KeyId = signingKid};
        var publicSigningKey = new ECDsaSecurityKey(ECDsa.Create(signingKey.ExportParameters(false))) {KeyId = signingKid};

        if (JwtHelper.DecryptAndValidateJwe(value, publicSigningKey, publicEncryptionKey))
        {
            return new BasicRpcResponse()
            {
                ResponseType = RpcResponseType.SUCCESS
            };
        }

        return null;
    }
    
    [HttpGet("TestAes")]
    public BasicRpcResponse TestAes([FromBody] string value)
    {
        string decrypted = Encryption.Decrypt(value);
        char[] valuechar = value.ToCharArray();
        Encryption.UpdateKey(new[] { valuechar[0], value[14] });

        return JsonSerializer.Deserialize<BasicRpcResponse>(decrypted);
    }
}