<%@ Page Title="" Language="C#" MasterPageFile="~/App.Master" AutoEventWireup="true" CodeBehind="danh-sach-don-hang-khac-app.aspx.cs" Inherits="NHST.danh_sach_don_hang_khac_app" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <link rel="stylesheet" href="/App_Themes/App/css/style-NA.css" media="all">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <main id="main-wrap">
        <div class="page-wrap">
               <asp:Panel ID="pnMobile" runat="server" Visible="false">
            <div class="page-body">
                <div class="all donhang donhang_list">
                    <h2 class="title_page">ĐƠN HÀNG TMĐT</h2>
                    <div class="sort_list">
                        <ul class="group_search">
                            <li>
                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="input_control">
                                    <asp:ListItem Value="-1" Text="Tất cả"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="Chưa đặt cọc"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Hủy đơn hàng"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Đã đặt cọc"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="Đã về kho TQ"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="Đã về kho TQ"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="Đã về kho VN"></asp:ListItem>
                                    <asp:ListItem Value="9" Text="Khách đã thanh toán"></asp:ListItem>
                                    <asp:ListItem Value="10" Text="Đã hoàn thành"></asp:ListItem>
                                </asp:DropDownList>
                            </li>
                            <li>
                                <%-- <input type="submit" name="" class="input_control btn_submit" value="Tìm kiếm"></li>--%>
                                <asp:Button ID="btnSear" runat="server" CssClass="input_control btn_submit" OnClick="btnSear_Click" Text="Tìm kiếm" />
                            </li>
                        </ul>
                    </div>

                    <asp:Literal runat="server" ID="ltrOrder"></asp:Literal>

                    <div class="tbl-footer clear">
                        <div class="subtotal fr">
                            <asp:Literal ID="ltrTotal" runat="server"></asp:Literal>
                        </div>
                        <div class="pagenavi fl">
                            <%this.DisplayHtmlStringPaging1();%>
                        </div>
                    </div>

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
    <asp:HiddenField ID="hdfOrderID" runat="server" />
    <asp:Button ID="btnDeposit" runat="server" UseSubmitBehavior="false" OnClick="btnDeposit_Click" Style="display: none" />
    <script type="text/javascript">
        function depositOrder(orderID) {
            var c = confirm('Bạn muốn đặt cọc đơn: ' + orderID);
            if (c == true) {
                $("#<%=hdfOrderID.ClientID%>").val(orderID);
                $("#<%=btnDeposit.ClientID%>").click();
            }
        }
    </script>
    <style>
        .pane-primary .heading {
            background-color: #366136 !important;
        }
    </style>

</asp:Content>
