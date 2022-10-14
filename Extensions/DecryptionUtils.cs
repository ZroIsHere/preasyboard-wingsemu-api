// See https://aka.ms/new-console-template for more information

using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using System;
using System.IO;


namespace noswebapp_api.Extensions
{
    public static class DecryptionUtils
    {
        // Draws inspiration from: 
        // https://stackoverflow.com/questions/70997010/manually-decrypt-the-signature-in-a-digital-certificate
        // (See the accepted answer, posted on Feb 5 2022 at 11:04, by Topaco.
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
            var publicKeyReader = ReadPublicKeyFromMemory();
            var pemReader = new PemReader(publicKeyReader).ReadObject();
            return (RsaKeyParameters)pemReader;
        }


        private static TextReader ReadPublicKeyFromFile()
        {
            return File.OpenText("public_key.perm");
        }


        private static TextReader ReadPublicKeyFromMemory()
        {
            return new StringReader(NosWebAppEnvVariables.EncryptionKey);
 
        }
    }
}