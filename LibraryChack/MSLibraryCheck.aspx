<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MSLibraryCheck.aspx.cs" Inherits="LibraryChack.MSLibraryCheck" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" >
        <div>
            <asp:Label ID="lblDbString" runat="server" Text="Enter your Database Connection String."></asp:Label><br />
            <asp:TextBox ID="txtDbString" runat="server"></asp:TextBox>
            <asp:Button ID="btnDbStringEncryp" runat="server" Text="DB Encrypt" OnClick="btnDbStringEncryp_Click" /><br />
            <asp:TextBox ID="txtDbStringresult" runat="server" TextMode="MultiLine"></asp:TextBox>
            <asp:Button ID="btnDbStringDecryp" runat="server" Text="DB Decrypt" OnClick="btnDbStringDecryp_Click" />
        </div>
        <div>
            <asp:Label ID="lblJwtString" runat="server" Text="Enter your JWT Configuration String."></asp:Label><br />
            <asp:TextBox ID="txtJwtString" runat="server"></asp:TextBox>
            <asp:Button ID="btnJwtStringEncrypt" runat="server" Text="JWT Encrypt" OnClick="btnJwtStringEncrypt_Click" /><br />
            <asp:TextBox ID="txtJwtStringresult" runat="server" TextMode="MultiLine"></asp:TextBox>
            <asp:Button ID="btnJwtStringDecrypt" runat="server" Text="JWT Decrypt" OnClick="btnJwtStringDecrypt_Click" />
        </div>
    </form>
</body>
</html>
