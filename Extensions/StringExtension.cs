using PreasyBoard.Api.Managers;

namespace PreasyBoard.Api.Extensions
{
    public static class StringExtension
    {
        public static string DecryptWithPublicKey(this string dataEncryptedBase64) => 
            RSAManager.DecryptWithPublicKey(dataEncryptedBase64);
    }
}