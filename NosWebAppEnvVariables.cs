using System;
using System.Collections.Generic;
using System.Text;

namespace noswebapp_api;

public class NosWebAppEnvVariables
{
    static string _AuthKey = "";
    static string _EncryptionKey = "";
    static string _JwtIssuer = "";
    static string _JwtAudience = "";
    static string _JwtKey = "";
    public static string AuthKey
    {
        get
        {
            if (string.IsNullOrEmpty(_AuthKey))
            {
                _AuthKey = Environment.GetEnvironmentVariable("API_AUTH_KEY") ?? "skSdhgASdS";
            }
            return _AuthKey;
        }
    }
    
    public static string EncryptionKey
    {
        get
        {
            if (string.IsNullOrEmpty(_EncryptionKey))
            {
                _EncryptionKey = Environment.GetEnvironmentVariable("API_ENCRYPTION_KEY") ??
                    @"-----BEGIN PUBLIC KEY-----
MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAp/DpfsTzvSYYEPBnxraG
t8VdQBcF53883wYR3WrT/Jr0vhhgBCFF7DMLOgbjZ0gbH6rCVI02AaPECapvJ7E5
KkgZIrooGEiKf/K0r/rRqmgl9gTN8Nnl5ePRYA/J26SbqrpqgIaUUsnEEtiyu+2G
0lAEuA61h2Q//WcqoVX44rYHCYTK/I4DL+ACNbkgklmX67LYApbGZW6wf4Q9Cq/X
fyusCx0MbZjLepcJAACCOenkXFPu0zOBZ6r/XsNypR18Gg9DhZ/kbqrZWEi9Clyr
GMe/zkXJmCDbcJ+C4rI6Gn7zwOLKSVV51DMgh8QG38WD4ga5XbBdHSg2nSsxnQR+
mwIDAQAB
-----END PUBLIC KEY-----";
            }
            return _EncryptionKey;
        }
    }
    
    public static string JwtIssuer
    {
        get
        {
            if (string.IsNullOrEmpty(_JwtIssuer))
            {
                _JwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? "https://joydipkanjilal.com/";
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
                _JwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? "https://joydipkanjilal.com/";
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
}