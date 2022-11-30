<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Website-under-contructing.aspx.cs" Inherits="NHST.Website_under_contructing" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="txtTracking" runat="server"></asp:TextBox>
            <asp:Button ID="btnTrack" runat="server" Text="Lấy thông tin" OnClick="btnTrack_Click" />
        </div>
    </form>
</body>
</html>
