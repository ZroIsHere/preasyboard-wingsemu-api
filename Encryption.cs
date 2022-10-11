using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace noswebapp_api;

public class Encryption
{
    public static string Encrypt(string text)
    {

        byte[] src = Encoding.UTF8.GetBytes(text);
        byte[] key = Encoding.ASCII.GetBytes(NosWebAppEnvVariables.AesKey);
        var aes = new AesManaged();
        aes.Mode = CipherMode.ECB;
        aes.Padding = PaddingMode.PKCS7;
        aes.KeySize = 256;

        using (ICryptoTransform encrypt = aes.CreateEncryptor(key, null))
        {
            byte[] dest = encrypt.TransformFinalBlock(src, 0, src.Length);
            encrypt.Dispose();
            return Convert.ToBase64String(dest);
        }
    }

    public static string Decrypt(string text)
    {

        byte[] src = Convert.FromBase64String(text);
        var aes = new AesManaged();
        byte[] key = Encoding.ASCII.GetBytes(NosWebAppEnvVariables.AesKey);
        aes.KeySize = 256;
        aes.Padding = PaddingMode.PKCS7;
        aes.Mode = CipherMode.ECB;
        using (ICryptoTransform decrypt = aes.CreateDecryptor(key, null))
        {
            byte[] dest = decrypt.TransformFinalBlock(src, 0, src.Length);
            decrypt.Dispose();
            return Encoding.UTF8.GetString(dest);
        }
    }

    public static void UpdateKey(char[] AddValues)
    {
        char[] keyarray = NosWebAppEnvVariables.AesKey.ToCharArray();
        for (int i = 0; i < 2; i++)
        {
            byte index = NosWebAppEnvVariables.ArrayRemoveFromIndex.FirstOrDefault();
            keyarray[index] = AddValues[i];
            NosWebAppEnvVariables.ArrayRemoveFromIndex.Remove(index);
            NosWebAppEnvVariables.ArrayRemoveFromIndex.Add(index);
        }
        NosWebAppEnvVariables.AesKey = string.Join("", keyarray);
        Console.WriteLine("New key: " + NosWebAppEnvVariables.AesKey);
    }
}