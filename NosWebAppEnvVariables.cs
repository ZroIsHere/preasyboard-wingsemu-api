using System;

namespace noswebapp_api;

public class NosWebAppEnvVariables
{
    static string _AuthKey = "";
    static string _EncryptionKey = -1;
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
            if (_EncryptionKey == -1)
            {
                _EncryptionKey = Environment.GetEnvironmentVariable("API_ENCRYPTION_KEY") ?? "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAp/DpfsTzvSYYEPBnxraGt8VdQBcF53883wYR3WrT/Jr0vhhgBCFF7DMLOgbjZ0gbH6rCVI02AaPECapvJ7E5KkgZIrooGEiKf/K0r/rRqmgl9gTN8Nnl5ePRYA/J26SbqrpqgIaUUsnEEtiyu+2G0lAEuA61h2Q//WcqoVX44rYHCYTK/I4DL+ACNbkgklmX67LYApbGZW6wf4Q9Cq/XfyusCx0MbZjLepcJAACCOenkXFPu0zOBZ6r/XsNypR18Gg9DhZ/kbqrZWEi9ClyrGMe/zkXJmCDbcJ+C4rI6Gn7zwOLKSVV51DMgh8QG38WD4ga5XbBdHSg2nSsxnQR+mwIDAQAB";
            }
            return _EncryptionKey;
        }
    }
}