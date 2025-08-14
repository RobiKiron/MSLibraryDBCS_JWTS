using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSLibrary
{
    public interface IJwtConfigService
    {
        //This get a object and provide a encrypted JWT string. As well as below formate. 
        //Input object:
        //SecretKey = "PKO52f374clOqtBt", 
        //Secret = "4e095b0f54086cff60f4ff8bc51a7a13f631701697f7469e", 
        //Issuer = "your Issuer", 
        //Audience = "your_audience", 
        //ExpiryInMinutes = expiry minutes like: "30"
        //Output = DhBOFq647u78rk+OmjFFeud2TTOhNMYco5GL2BCDam6hLP63aX3vD4EsO6Rndh9tLCErzploM+8Hn/T/0BZInTlpoqKkYraevxGdp3B8IPKELvZ/KIhBhWULnD6wswiqc/7n0/hd/kLj1
        //
        string EncryptJwtSettings(JwtSettings settings);
        //This get a encrypted json string  and provide a object. As well as below formate.
        //Input = DhBOFq647u78rk+OmjFFeud2TTOhNMYco5GL2BCDam6hLP63aX3vD4EsO6Rndh9tLCErzploM+8Hn/T/0BZInTlpoqKkYraevxGdp3B8IPKELvZ/KIhBhWULnD6wswiqc/7n0/hd/kLj1
        //Output object:
        //SecretKey = "PKO52f374clOqtBt", 
        //Secret = "4e095b0f54086cff60f4ff8bc51a7a13f631701697f7469e", 
        //Issuer = "your Issuer", 
        //Audience = "your_audience", 
        //ExpiryInMinutes = expiry minutes like: "30"
        //
        JwtSettings GetJwtSettingsFromEncryptedJson(string encryptedJson);
        //It's Use only for GET JWT String from System Environment. Below Environment variable.
        //JWT_SECRET_KEY = "PKO52f374clOqtBt", 
        //JWT_SECRET = "4e095b0f54086cff60f4ff8bc51a7a13f631701697f7469e", 
        //JWT_ISSUER = "your Issuer",
        //JWT_AUDIENCE = "your_audience", 
        //JWT_EXPIRYINMINUTES = expiry minutes like: "30"
        //
        JwtSettings GetJwtSettings();
        //It's Use only for get IConfiguration i.e appsettings.json file. Add below json string
        //"Jwt": {
        //           "SecretKey":"PKO52f374clOqtBt", 
        //           "Secret" : "4e095b0f54086cff60f4ff8bc51a7a13f631701697f7469e", 
        //           "Issuer":  "your Issuer",
        //           "Audience" : "your_audience", 
        //           "ExpiryInMinutes" :expiry minutes like: "30"
        //         }
        //
        JwtSettings GetJwtSettings(IConfiguration configuration);
    }
}
