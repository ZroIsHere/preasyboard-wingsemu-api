//using System;
//using Org.BouncyCastle.Crypto;
//using Org.BouncyCastle.Crypto.Parameters;
//using Org.BouncyCastle.OpenSsl;
//using Org.BouncyCastle.Security;
//using System.IO;
//using System.Security.Cryptography;
//using System.Text;

//namespace noswebapp_api.Helper
//{
//    public class RsaHelper
//    {
//        private readonly RSACryptoServiceProvider _privateKey;
//        //private readonly void _publicKey;

//        public RsaHelper()
//        {
//            string public_pem = @"C:\OpenSSL\bin\posvendor.pub.pem";
//            string private_pem = @"C:\OpenSSL\bin\posvendor.key.pem";

//            _privateKey = GetPrivateKeyFromPemFile(private_pem);

//            var provider = new RSACryptoServiceProvider();
//            //var publicKey = PublicKey.GetPublicKey();
//            //_publicKey = provider.FromXmlString(
//            //    "<RSAKeyValue>" +
//            //        "<Modulus>CmZ5HcaYgWjeerd0Gbt/sMABxicQJwB1FClC4ZqNjFHQU7PjeCod5dxa9OvplGgXARSh3+Z83Jqa9V1lViC7qw==</Modulus>" +
//            //        "<Exponent>AQAB</Exponent>" +
//            //    "</RSAKeyValue>");

//        }


//        public string Encrypt(string text)
//        {
//            var encryptedBytes =_publicKey.Encrypt(Encoding.UTF8.GetBytes(text), false);
//            return Convert.ToBase64String(encryptedBytes);
//        }

//        public string Decrypt(string encrypted)
//        {
//            var decryptedBytes = _privateKey.Decrypt(Convert.FromBase64String(encrypted), false);
//            return Encoding.UTF8.GetString(decryptedBytes, 0, decryptedBytes.Length);
//        }

//        private RSACryptoServiceProvider GetPrivateKeyFromPemFile(string filePath)
//        {
//            using (TextReader privateKeyTextReader = new StringReader(File.ReadAllText(filePath)))
//            {
//                var readKeyPair = (AsymmetricCipherKeyPair)new PemReader(privateKeyTextReader).ReadObject();

//                var rsaParams = DotNetUtilities.ToRSAParameters((RsaPrivateCrtKeyParameters)readKeyPair.Private);
//                var csp = new RSACryptoServiceProvider();
//                csp.ImportParameters(rsaParams);
//                return csp;
//            }
//        }

//        private RSACryptoServiceProvider GetPublicKeyFromPemFile(String filePath)
//        {
//            using (TextReader publicKeyTextReader = new StringReader(File.ReadAllText(filePath)))
//            {
//                var publicKeyParam = (RsaKeyParameters)new PemReader(publicKeyTextReader).ReadObject();

//                var rsaParams = DotNetUtilities.ToRSAParameters((RsaKeyParameters)publicKeyParam);

//                var csp = new RSACryptoServiceProvider();
//                csp.ImportParameters(rsaParams);
//                return csp;
//            }
//        }
//    }
//}
