<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PUSHDEMO.aspx.cs" Inherits="NHST.PUSHDEMO" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <%-- <input name="txturl" type="text" id="txturl" class="form-control" placeholder="Device token" />--%>

            <asp:TextBox runat="server" ID="txturl" CssClass="form-control" placeholder="Device token"></asp:TextBox>

            <asp:DropDownList CssClass="form-control" Style="width: 500px;" ID="ddlType" runat="server">
                <asp:ListItem Value="android" Text="Android"></asp:ListItem>
                <asp:ListItem Value="ios" Text="iOS"></asp:ListItem>
            </asp:DropDownList>

            <asp:Button ID="btnpush" runat="server" Text="PUSH" OnClick="btnpush_Click1" />

        </div>
    </form>
</body>
</html>
