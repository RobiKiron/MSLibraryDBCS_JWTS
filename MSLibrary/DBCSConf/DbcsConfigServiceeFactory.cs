using System;
using System.Collections.Generic;
using System.Text;

namespace MSLibrary.DBCSConf
{
    public static class DbcsConfigServiceeFactory
    {
        public static IDbcsConfigService Create()
        {
            return new DbcsConfigServiceImpl();
        }
    }
}
