using System;
using System.Collections.Generic;
using System.Text;

namespace PreasyBoard.Api.Configuration;

public class PreasyBoardEnvVariables
{
    //This need be hardcoded, env dont support multiline read from .env file
    public static string EncryptionKey = @"
MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA9Pu/hDkkkMfyexhOzCU3
3tnfcDfArJuYvJHsHElHSdxSLIVnYPai/nWtJqKO4NCzh+h4SHTWJcg42YBABg9R
oiP54qkFR9XjAIOfnxg1gdAGSbEZYxAYWnM0NJ7sw2ydDNCZS9flkoXL3LgU3ch0
oNqhWrMnkd0qb8Jz5kCT/vfUSqffMuedcM2XiMBGIPa4t8/ePnnkl6gNNupRYgVL
qoIMrpfCJWQKei8uYLsUZvcngO8WkeB0uM4wEHCjvxW7YI1or61UzDUWLfgwcc6w
LyLGMNSfRJ5MG6P/v/7jccu1SdOQPRFIevXSNwIBJli25LWq7niL74G76oSPDmxb
jQIDAQAB
-----END PUBLIC KEY-----";


    private static string _WebApiUrl = "";
    static string _JwtIssuer = "";
    static string _JwtAudience = "";
    static string _JwtKey = "";
    
    public static string JwtIssuer
    {
        get
        {
            if (string.IsNullOrEmpty(_JwtIssuer))
            {
                _JwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? "https://preasyboard.com/";
            }
            return _JwtIssuer;
        }
    }

    public static string JwtAudience
    {
        get
        {
            if (string.IsNullOrEmpty(_JwtAudience))
            {
                _JwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? "https://preasyboard.com/";
            }
            return _JwtAudience;
        }
    }

    public static string JwtKey
    {
        get
        {
            if (string.IsNullOrEmpty(_JwtKey))
            {
                _JwtKey = Environment.GetEnvironmentVariable("JWT_KEY") ?? "This is a sample secret key - please don't use in production environment.'";
            }

            return _JwtKey;
        }
    }

    public static string WebApiUrl
    {
        get
        {
            if (string.IsNullOrEmpty(_WebApiUrl))
            {
                _WebApiUrl = Environment.GetEnvironmentVariable("WEB_API_URL") ?? "http://0.0.0.0:21487/";
            }

            return _WebApiUrl;
        }
    }
}