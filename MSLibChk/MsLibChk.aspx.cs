
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
            //string dbstring = "Server=aal-esql01;Database=ERP_APP;User Id=NPrNwUs@Ag;Password=NPa2sLs@Ag;Persist Security Info=True;TrustServerCertificate=true;ApplicationIntent=ReadWrite;"; //txtDbString.Text;
            //txtDbStringresult.Text = DBCSConf.GetEncryptedConnectionString(dbstring);
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
            _jwtsObj.SecretKey = "EHhPKO52f374clOqtBtuEg==";
            _jwtsObj.Secret = "4e095b0f54086cff60f4ff8bc51a7a13f631701697f7469e4144a02a3bd55cc9b42a725c7cbe12877116d07ac27e5793";
            _jwtsObj.Issuer = "AkijGroup_Issuer";
            _jwtsObj.Audience = "AkijGroup_audience";
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