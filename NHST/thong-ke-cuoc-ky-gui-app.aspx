<%@ Page Language="C#" MasterPageFile="~/App.Master" AutoEventWireup="true" CodeBehind="thong-ke-cuoc-ky-gui-app.aspx.cs" Inherits="NHST.thong_ke_cuoc_ky_gui_app" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <style>
        .ip-with-sufix .fcontrol {
            background-color: #fff;
        }

        .thanhtoanho-list {
            margin-bottom: 15px;
        }

        table.tb-wlb {
            margin-bottom: 5px;
        }

        .page-title {
            text-align: center;
            padding: 10px 20px;
            font-size: 20px;
        }
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <main id="main-wrap">
        <div class="page-wrap">
            <asp:Panel ID="pnMobile" runat="server" Visible="false">
                <div class="page-body">
                    <div class="heading-search">
                        <div class="all">
                            <h1 class="page-title">Thống kê cước VC</h1>
                        </div>
                    </div>

                    <asp:Literal runat="server" ID="ltrtth"></asp:Literal>

                    <div class="tbl-footer clear">
                        <div class="subtotal fr">
                            <asp:Literal ID="ltrTotal" runat="server"></asp:Literal>
                        </div>
                        <div class="all">
                            <div class="pagenavi fl">
                                <%this.DisplayHtmlStringPaging1();%>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="page-bottom-toolbar">
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
    <asp:HiddenField runat="server" ID="hdfID" />
    <asp:Button runat="server" ID="btnPay" Style="display: none" OnClick="btnPay_Click" UseSubmitBehavior="false" />
    <script type="text/javascript">
        function Pay(obj, ID) {
            var c = confirm('Bạn muốn thanh toán?');
            if (c) {
                $("#<%=hdfID.ClientID%>").val(ID);
                $("#<%=btnPay.ClientID%>").click();
            }
        }
    </script>
</asp:Content>
