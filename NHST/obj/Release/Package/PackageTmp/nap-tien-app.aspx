<%@ Page Title="" Language="C#" MasterPageFile="~/App.Master" AutoEventWireup="true" CodeBehind="nap-tien-app.aspx.cs" Inherits="NHST.nap_tien_app" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <link rel="stylesheet" href="/App_Themes/App/css/style-NA.css" media="all">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <main id="main-wrap">

        <div class="page-wrap">
            <asp:Panel ID="pnMobile" runat="server" Visible="false">
                <div class="page-body">
                    <div class="all donhang">
                        <h2 class="title_page">NẠP TIỀN</h2>
                        <div class="content_page">
                            <p class="title_onpage">NẠP TIỀN</p>
                            <p class="user">
                                <a class="item-user"><i class="fa fa-user"></i></a>
                                <asp:Literal ID="ltrIfn" runat="server"></asp:Literal>
                            </p>
                            <div class="content_create_order">
                                <div class="bottom_order">
                                    <ul>
                                        <li>Ngân hàng: </li>
                                        <li>
                                            <asp:DropDownList runat="server" ID="ddlBank" DataTextField="BankInfo" DataValueField="ID" CssClass="input_control"></asp:DropDownList>
                                        </li>
                                    </ul>
                                    <ul>
                                        <li>Số tiền:</li>
                                        <li>
                                            <telerik:RadNumericTextBox runat="server" CssClass="input_control" Skin="MetroTouch"
                                                ID="pAmount" NumberFormat-DecimalDigits="0" Value="0"
                                                NumberFormat-GroupSizes="3" Width="100%">
                                            </telerik:RadNumericTextBox>
                                        </li>
                                    </ul>
                                    <ul>
                                        <li>Nội dung: </li>
                                        <li>
                                            <asp:TextBox ID="txtNote" runat="server" CssClass="input_control message" TextMode="MultiLine"></asp:TextBox>
                                        </li>
                                        <li>
                                            <p>- Nội dung chuyển khoản theo cú pháp:</p>

                                            <p style="color: red; padding-left: 10px">+ NAP tentaikhoan sodienthoai</p>
                                        </li>
                                    </ul>
                                    <ul>
                                        <li>Thông tin tài khoản: </li>
                                        <asp:Literal runat="server" ID="ltrBank"></asp:Literal>
                                    </ul>

                                </div>

                                <p class="btn_order">
                                    <a href="javascript:;" onclick="AddWallet($(this))" class="btn_ordersp">GỬI YÊU CẦU</a>
                                    <asp:Button ID="btnSend" runat="server" Style="display: none" Text="GỬI YÊU CẦU" CssClass="btn_ordersp" OnClick="btnSend_Click" />
                                </p>
                            </div>
                        </div>
                    </div>
                    <div class="page-bottom-toolbar">
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnShowNoti" runat="server" Visible="false">
                <div class="page-body">
                    <div class="heading-search">
                        <h4 class="page-title">Bạn vui lòng đăng xuất và đăng nhập lại!</h4>
                    </div>
                </div>
            </asp:Panel>
        </div>

    </main>
    <script>
        function AddWallet(obj) {
            var r = confirm("Xác nhận nạp tiền!");
            if (r == true) {
                obj.removeAttr("onclick");
                $("#<%=btnSend.ClientID%>").click();
            }
        }
    </script>
</asp:Content>
