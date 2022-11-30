<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="NHST.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="txtNumIids" runat="server" placeholder="NumIids"></asp:TextBox>
            <asp:TextBox ID="txtIP" runat="server" placeholder="Ip"></asp:TextBox>

              <asp:TextBox ID="txtFID" runat="server" placeholder="Favorite_ID"></asp:TextBox>
            <asp:Button ID="btnCheck" runat="server" OnClick="btnCheck_Click" Text="LogIn" />

            <asp:Button runat="server" ID="btnCoupon" OnClick="btnCoupon_Click" />


            <asp:Button runat="server" ID="btnConvertCoupon" OnClick="btnConvertCoupon_Click" />

             <asp:Button runat="server" Text="GetProduct" ID="btnGetProduct" OnClick="btnGetProduct_Click" />

            <asp:Button runat="server" Text="GetOrder" ID="btnGetOrder" OnClick="btnGetOrder_Click" />

             <asp:Button runat="server" Text="GetFavorite" ID="btnGetFavorite" OnClick="btnGetFavorite_Click" />

                 <asp:Button runat="server" Text="GetMaterial" ID="btnMaterial" OnClick="btnMaterial_Click" />

                <asp:Button runat="server" Text="GetRecommend" ID="btnGetRecommend" OnClick="btnGetRecommend_Click" />

             <asp:Button runat="server" Text="GetCoupon" ID="btnGetCoupon" OnClick="btnGetCoupon_Click" />

            <asp:Literal ID="ltrTB" runat="server"></asp:Literal>


            <br /> <br />


            <asp:Literal runat="server" ID="ltrResult"></asp:Literal>

        </div>
    </form>
</body>
</html>
