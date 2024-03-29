using System.Text;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using System;
using System.IO;
using PreasyBoard.Api.Configuration;

namespace PreasyBoard.Api.Managers;

public class RSAManager
{
    public static string DecryptWithPublicKey(string dataEncryptedBase64)
    {
        var rsaKeyParameters = GetRsaKeyParams();

        var rsa = new Pkcs1Encoding(new RsaEngine());
        rsa.Init(false, rsaKeyParameters);

        var encryptedBytes = Convert.FromBase64String(dataEncryptedBase64);
        var decryptedBytes = rsa.ProcessBlock(encryptedBytes, 0, encryptedBytes.Length);

        return Encoding.UTF8.GetString(decryptedBytes);
    }
    private static RsaKeyParameters GetRsaKeyParams()
    {
        var publicKeyReader = ReadPublicKeyFromFile();
        var pemReader = new PemReader(publicKeyReader).ReadObject();
        return (RsaKeyParameters)pemReader;
    }

    private static TextReader ReadPublicKeyFromFile()
    {
        return File.OpenText("public_key.perm");
    }

    private static TextReader ReadPublicKeyFromMemory() =>
        new StringReader(PreasyBoardEnvVariables.EncryptionKey);
}