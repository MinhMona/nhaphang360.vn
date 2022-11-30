<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="demon.aspx.cs" Inherits="NHST.demon" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="txtkey" runat="server" placeholder="key"></asp:TextBox>
            <asp:Button ID="btngetaccount" runat="server" OnClick="btngetaccount_Click" Text="Get"/>
            <asp:TextBox ID="txtID" runat="server" placeholder="ID"></asp:TextBox>
            <asp:Button ID="btnlogin" runat="server" OnClick="btnlogin_Click" Text="LogIn"/>
            <table>
                <asp:Literal ID="ltrTB" runat="server"></asp:Literal>
            </table>
        </div>
    </form>
</body>
</html>
