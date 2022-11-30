<%@ Page Title="Rút tiền app" Language="C#" MasterPageFile="~/App.Master" AutoEventWireup="true" CodeBehind="rut-tien-app.aspx.cs" Inherits="NHST.rut_tien_app" %>

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
                        <h2 class="title_page">RÚT TIỀN</h2>
                        <div class="content_page">
                            <p class="user">
                                <a class="item-user"><i class="fa fa-user"></i></a>
                                <asp:Literal runat="server" ID="ltrUserName"></asp:Literal>
                            </p>
                            <div class="content_create_order">
                                <div class="bottom_order">
                                    <ul>
                                        <li>
                                            <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false"></asp:Label></li>
                                        <li>
                                            <telerik:RadNumericTextBox runat="server" CssClass="input_control" Skin="MetroTouch"
                                                ID="pAmount" NumberFormat-DecimalDigits="0" MinValue="100000"
                                                NumberFormat-GroupSizes="3" Width="100%" placeholder="Số tiền muốn rút" Value="100000">
                                            </telerik:RadNumericTextBox>
                                        </li>
                                    </ul>                                    
                                    <ul>
                                        <li>Số tài khoản: </li>
                                        <li>
                                            <asp:TextBox ID="txtBankNumber" runat="server" CssClass="input_control message"></asp:TextBox>
                                        </li>
                                    </ul>
                                     <ul>
                                        <li>Chủ tài khoản: </li>
                                        <li>
                                            <asp:TextBox ID="txtBeneficiary" runat="server" CssClass="input_control message"></asp:TextBox>
                                        </li>
                                    </ul>
                                    <ul>
                                        <li>Địa chỉ ngân hàng: </li>
                                        <li>
                                            <asp:TextBox ID="txtBankAddress" runat="server" CssClass="input_control message"></asp:TextBox>
                                        </li>
                                    </ul>
                                     <ul>
                                        <li>Nội dung: </li>
                                        <li>
                                            <asp:TextBox ID="txtNote" runat="server" CssClass="input_control message" TextMode="MultiLine"></asp:TextBox>
                                        </li>
                                    </ul>
                                </div>
                                <p class="btn_order">
                                    <a href="javascript:;" onclick="confirmrutien()" class="btn_ordersp">Gửi yêu cầu</a>
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
    <asp:Button ID="btnCreate" runat="server" Text="Tạo lệnh rút tiền" CssClass="btn btn-success btn-block pill-btn primary-btn"
        OnClick="btnCreate_Click" Style="display: none" />

    <script type="text/javascript">
        function confirmrutien() {
            var r = confirm("Bạn muốn tạo lệnh rút tiền?");
            if (r == true) {
                $("#<%=btnCreate.ClientID%>").click();
            }
            else {
            }
        }
    </script>

</asp:Content>
