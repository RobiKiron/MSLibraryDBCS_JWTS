
using MSLibrary;
using MSLibrary.DBCSConf;
using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MSLibChk
{
    public partial class MsLibChk : System.Web.UI.Page
    {
        private static readonly IJwtConfigService _jwtConfigService = JwtConfigServiceFactory.Create();
        private static readonly IDbcsConfigService _dbcsConfigService = DbcsConfigServiceeFactory.Create();
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnDbStringEncryp_Click(object sender, EventArgs e)
        {
            string dbstring = txtDbString.Text;
            txtDbStringresult.Text = _dbcsConfigService.GetEncryptedConnectionString(dbstring);
        }

        protected void btnDbStringDecryp_Click(object sender, EventArgs e)
        {
            string dbstring = txtDbString.Text;
            txtDbStringresult.Text = _dbcsConfigService.GetConnectionString(dbstring);
        }

        protected void btnJwtStringEncrypt_Click(object sender, EventArgs e)
        {
            var _jwtsObj = new JwtSettings();
            _jwtsObj.SecretKey = "your secret key";
            _jwtsObj.Secret = "your secret";
            _jwtsObj.Issuer = "your issuer";
            _jwtsObj.Audience = "your audience";
            _jwtsObj.ExpiryInMinutes = "30";
            var jwtstring = _jwtConfigService.EncryptJwtSettings(_jwtsObj);
            txtJwtStringresult.Text = jwtstring;
        }

        protected void btnJwtStringDecrypt_Click(object sender, EventArgs e)
        {
            string jwtsValue = txtJwtString.Text;
            var _jwtsObj = _jwtConfigService.GetJwtSettingsFromEncryptedJson(jwtsValue);
            txtJwtStringresult.Text = _jwtsObj.ToString();
        }
    }
}