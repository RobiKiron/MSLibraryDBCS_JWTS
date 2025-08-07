using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace MSLibrary
{
    [Obfuscation(Exclude = false)]
    internal class JwtConfigServiceImpl : IJwtConfigService
    {
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("EHhPKO52f374clOqtBtuEg==");
        public string EncryptJwtSettings(JwtSettings settings)
        {
            var wrapper = new JwtConfigWrapper { Jwt = settings };
            string json = JsonConvert.SerializeObject(wrapper);
            return EncryptJson(json);
        }

        public JwtSettings GetJwtSettings()
        {
            return new JwtSettings
            {
                SecretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY"),
                Secret = Environment.GetEnvironmentVariable("JWT_SECRET"),
                Issuer = Environment.GetEnvironmentVariable("JWT_ISSUER"),
                Audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
                ExpiryInMinutes = Environment.GetEnvironmentVariable("JWT_EXPIRYINMINUTES")
            };
        }

        public JwtSettings GetJwtSettings(IConfiguration _configuration)
        {
            return new JwtSettings
            {
                SecretKey = _configuration["Jwt:SecretKey"],
                Secret = _configuration["Jwt:Secret"],
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                ExpiryInMinutes = _configuration["Jwt:ExpiryInMinutes"]
            };
        }

        public JwtSettings GetJwtSettingsFromEncryptedJson(string encryptedJson)
        {
            try
            {
                string decryptedJson = DecryptJson(encryptedJson);
                var config = JsonConvert.DeserializeObject<JwtConfigWrapper>(decryptedJson);
                return config.Jwt;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to process JWT settings: {ex.Message}");
            }
        }



        private class JwtConfigWrapper
        {
            public JwtSettings Jwt { get; set; }
        }

        private static string EncryptJson(string jsonString)
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
                        swEncrypt.Write(jsonString);
                    }

                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        private static string DecryptJson(string encryptedJson)
        {
            try
            {
                byte[] buffer = Convert.FromBase64String(encryptedJson);

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
    }
}
