using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace MSLibrary.DBCSConf
{
    [Obfuscation(Exclude = false)]
    internal class DbcsConfigServiceImpl : IDbcsConfigService
    {
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("EHhPKO52f374clOqtBtuEg=="); // 16/24/32 bytes
        public string GetConnectionString(string chiperText)
        {
            return DecryptString(chiperText);
        }

        public string GetConnectionString()
        {
            var server = Environment.GetEnvironmentVariable("DB_SERVER");
            var database = Environment.GetEnvironmentVariable("DB_NAME");
            var user = Environment.GetEnvironmentVariable("DB_USER");
            var password = Environment.GetEnvironmentVariable("DB_PASSWORD");

            return $"Server={server};Database={database};User Id={user};Password={password};";
        }

        public string GetEncryptedConnectionString(string connectionString)
        {
            return EncryptString(connectionString);
        }

        private static string DecryptString(string cipherText)
        {
            try
            {
                byte[] buffer = Convert.FromBase64String(cipherText);

                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = Key;

                    byte[] iv = new byte[16];
                    Array.Copy(buffer, 0, iv, 0, iv.Length);
                    aesAlg.IV = iv;

                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    using (MemoryStream msDecrypt = new MemoryStream(buffer, iv.Length, buffer.Length - iv.Length))
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
            catch
            {
                throw new Exception("Decryption failed. Invalid key or corrupted data.");
            }
        }
        private static string EncryptString(string plainText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.GenerateIV();

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);

                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }

                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }
    }
}
