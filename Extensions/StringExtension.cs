using noswebapp_api.Managers;

namespace noswebapp_api.Extensions
{
    public static class StringExtension
    {
        public static string DecryptWithPublicKey(this string dataEncryptedBase64)
        {
            return RSAManager.DecryptWithPublicKey(dataEncryptedBase64);
        }
    }
}