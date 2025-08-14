using System;
using System.Collections.Generic;
using System.Text;

namespace MSLibrary.DBCSConf
{
    public interface IDbcsConfigService
    {
        //It's Use only for Plain text DataBase Connection String Convert into Chiper Text i.e Encrypted. 
        //Input :Server=Your Server Name;Database=DataBase Name;User Id=DataBase User;Password=DataBase Password;Persist Security Info=True;TrustServerCertificate=true;ApplicationIntent=ReadWrite;
        //Output:8H7fF6xk1fNFfTJXtjz8QgJF2CpaoC6iFdL7neKMk6yrqAuoEov6CKNnzrU1m7Yjdfc2By4uycy4Tvb+bKzeJvL4ZnkwhntzEoKpWTkpYmUx/aZHo0NBofjXbYN4yEJsC5TLgBO8DKAaYZz/NdcthuhjwCyog8pVoVwCgpiBduyUrrzv3
        string GetEncryptedConnectionString(string connectionString);
        //It's Use only for Encrypted string into plain text.
        //Input :8H7fF6xk1fNFfTJXtjz8QgJF2CpaoC6iFdL7neKMk6yrqAuoEov6CKNnzrU1m7Yjdfc2By4uycy4Tvb+bKzeJvL4ZnkwhntzEoKpWTkpYmUx/aZHo0NBofjXbYN4yEJsC5TLgBO8DKAaYZz/NdcthuhjwCyog8pVoVwCgpiBduyUrrzv3
        //Output:Server=Your Server Name;Database=DataBase Name;User Id=DataBase User;Password=DataBase Password;Persist Security Info=True;TrustServerCertificate=true;ApplicationIntent=ReadWrite;
        string GetConnectionString(string chiperText);
        //It's Use only for GET DataBase Connection String from System Environment. Below Environment variable.
        //DB_SERVER=Your Server Name.
        //DB_NAME=DataBase Name.
        //DB_USER=DataBase User.
        //DB_PASSWORD = DataBase Password.
        string GetConnectionString();
    }
}
