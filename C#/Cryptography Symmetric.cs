using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Common
{
    public static class Cryptography
    {
        static readonly RijndaelManaged rijndael;

        static Cryptography()
        {
            rijndael = GetRijndaelManaged(new byte[] { 23, 2, 76, 234, 34, 76, 134, 76, 248, 4, 7, 56 });
        }

        public static string Encrypt(string plainText) =>
            Convert.ToBase64String(Encrypt(rijndael, plainText));

        public static string Decrypt(string plainText) =>
            Decrypt(rijndael, Convert.FromBase64String(plainText));

        public static string Encrypt(string plainText, string salt) =>
            Convert.ToBase64String(Encrypt(GetRijndaelManaged(salt), plainText));

        public static string Decrypt(string plainText, string salt) =>
            Decrypt(GetRijndaelManaged(salt), Convert.FromBase64String(plainText));

        public static byte[] Encrypt(SymmetricAlgorithm aesAlg, string plainText)
        {
            using (ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV))
            using (var msEncrypt = new MemoryStream())
            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            {
                using (var swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(plainText);
                }
                return msEncrypt.ToArray();
            }
        }

        public static string Decrypt(SymmetricAlgorithm aesAlg, byte[] cipherText)
        {
            using (ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
            using (var msDecrypt = new MemoryStream(cipherText))
            using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
            using (var srDecrypt = new StreamReader(csDecrypt))
            {
                return srDecrypt.ReadToEnd();
            }
        }

        public static RijndaelManaged GetRijndaelManaged(string salt) =>
            GetRijndaelManaged(Encoding.UTF8.GetBytes(salt));

        public static RijndaelManaged GetRijndaelManaged(byte[] saltBytes)
        {
            var key = new Rfc2898DeriveBytes("1cftyguhoijpokfe", saltBytes);

            var rijndaelManaged = new RijndaelManaged();
            rijndaelManaged.Key = key.GetBytes(rijndaelManaged.KeySize / 8);
            rijndaelManaged.IV = key.GetBytes(rijndaelManaged.BlockSize / 8);

            return rijndaelManaged;
        }
    }
}
