using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace noswebapp_api.Helpers;

public class JwtHelper
{
    public static bool DecryptAndValidateJwe(string token, SecurityKey signingKey, SecurityKey encryptionKey)
    {
        var handler = new JsonWebTokenHandler();

        TokenValidationResult result = handler.ValidateToken(
            token,
            new TokenValidationParameters
            {
                ValidAudience = "api1",
                ValidIssuer = "https://0.0.0.0:21487",

                // public key for signing
                IssuerSigningKey = signingKey,

                // private key for encryption
                TokenDecryptionKey = encryptionKey
            });

        return result.IsValid;
    }
}