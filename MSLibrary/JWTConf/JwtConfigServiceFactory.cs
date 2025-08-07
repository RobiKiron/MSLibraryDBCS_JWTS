using System;
using System.Collections.Generic;
using System.Text;

namespace MSLibrary
{
    public static class JwtConfigServiceFactory
    {
        public static IJwtConfigService Create()
        {
            return new JwtConfigServiceImpl();
        }
    }
}
