using System;
using System.Collections.Generic;
using System.Text;

namespace MSLibrary
{
    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string ExpiryInMinutes { get; set; }
    }
}
