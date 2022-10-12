using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using ProtoBuf.Serializers;

namespace noswebapp_api.Extensions;

public static class StringExtension
{
    //Is inversed, the key is the public, but i will put private in all because is the real system.
    public static string DecryptWithPrivateKey(this string value)
    {
        using TextReader privateKeyTextReader = new StringReader(File.ReadAllText("public_key.perm"));
        AsymmetricCipherKeyPair readKeyPair = (AsymmetricCipherKeyPair)new PemReader(privateKeyTextReader).ReadObject();

        RSAParameters rsaParams = DotNetUtilities.ToRSAParameters((RsaPrivateCrtKeyParameters)readKeyPair.Private);
        RSACryptoServiceProvider csp = new RSACryptoServiceProvider();
        csp.ImportParameters(rsaParams);
        var bytes = csp.Decrypt(Convert.FromBase64String(value), false);
        return Encoding.UTF8.GetString(bytes, 0, bytes.Length);
    }
}