using System;
using System.Collections.Generic;

namespace noswebapp_api;

public class NosWebAppEnvVariables
{
    static string _AuthKey = "";
    static string _EncryptionKey = "";
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
                _EncryptionKey = Environment.GetEnvironmentVariable("API_ENCRYPTION_KEY") ?? "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAp/DpfsTzvSYYEPBnxraGt8VdQBcF53883wYR3WrT/Jr0vhhgBCFF7DMLOgbjZ0gbH6rCVI02AaPECapvJ7E5KkgZIrooGEiKf/K0r/rRqmgl9gTN8Nnl5ePRYA/J26SbqrpqgIaUUsnEEtiyu+2G0lAEuA61h2Q//WcqoVX44rYHCYTK/I4DL+ACNbkgklmX67LYApbGZW6wf4Q9Cq/XfyusCx0MbZjLepcJAACCOenkXFPu0zOBZ6r/XsNypR18Gg9DhZ/kbqrZWEi9ClyrGMe/zkXJmCDbcJ+C4rI6Gn7zwOLKSVV51DMgh8QG38WD4ga5XbBdHSg2nSsxnQR+mwIDAQAB";
            }
            return _EncryptionKey;
        }
    }

    public static string AesKey = "Hp.X2Mf1^.gv&k;WFO]I}o~Q^hc=7lG~";
    public static List<byte> ArrayReplaceFromIndex = new(){ 26, 2, 3, 9, 8, 15, 24, 13, 21, 18, 29, 20, 14, 11, 25, 23, 5, 22, 16, 27, 7, 6, 30, 19, 12, 28, 10, 17, 4, 1, 0 };
}