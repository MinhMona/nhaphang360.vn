<%@ Page Title="Nạp tiền tệ app" Language="C#" MasterPageFile="~/App.Master" AutoEventWireup="true" CodeBehind="rut-tien-te-app.aspx.cs" Inherits="NHST.rut_tien_te_app" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="/App_Themes/App/css/style-NA.css" media="all">
    <style>
        .page-nap-tien {
            float: left;
            width: 100%;
        }

            .page-nap-tien tr {
                float: left;
                width: 100%;
                margin-bottom: 10px;
            }

                .page-nap-tien tr th {
                    float: left;
                    width: 20%;
                    text-align: left;
                    vertical-align: middle;
                    min-height: 1px;
                    font-weight: bold;
                }

                .page-nap-tien tr td {
                    float: left;
                    width: 80%;
                    text-align: left;
                    margin-bottom: 10px;
                }

                    .page-nap-tien tr td textarea {
                        min-height: 150px;
                        width: 100%;
                        border: solid 1px #e1e1e1;
                        padding: 10px;
                    }

        .table-panel-main table td {
            text-align: center;
        }

        .pane-primary .heading {
            background-color: #366136 !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">

        <div class="page-wrap">
            <asp:Panel ID="pnMobile" runat="server" Visible="false">
                <div class="page-body">
                    <div class="all donhang hoante">
                        <h2 class="title_page">NẠP TIỀN TỆ</h2>
                        <div class="content_page">
                            <p class="title_onpage">YÊU CẦU NẠP TIỀN TỆ</p>
                            <p class="user">
                                <a class="item-user"><i class="fa fa-user"></i></a>
                                <asp:Literal ID="ltrusername" runat="server"></asp:Literal>
                            </p>
                            <div class="content_create_order">
                                <div class="bottom_order">
                                    <ul>
                                        <li>
                                            <p>
                                                Tham gia từ:
                                            <asp:Literal runat="server" ID="ltrJoin"></asp:Literal>
                                            </p>
                                        </li>
                                    </ul>
                                    <ul>
                                        <li>Số tiền:</li>
                                        <li>
                                            <telerik:RadNumericTextBox runat="server" CssClass="input_control" Skin="MetroTouch"
                                                ID="pAmount" NumberFormat-DecimalDigits="3" Value="0"
                                                NumberFormat-GroupSizes="3" Width="100%">
                                            </telerik:RadNumericTextBox>
                                            <%--<input class="input_control" type="text" name="" placeholder="0.000"></li>--%>
                                    </ul>
                                    <ul>
                                        <li>Ghi chú: </li>
                                        <li>
                                            <asp:TextBox ID="txtNote" runat="server" CssClass="input_control message" TextMode="MultiLine"></asp:TextBox>
                                            <%--<textarea class="" type="text" name=""></textarea>--%>
                                        </li>
                                    </ul>
                                </div>
                                <p class="btn_order">
                                    <asp:Button ID="btnSend" runat="server" Text="GỬI YÊU CẦU" CssClass="btn_ordersp" OnClick="btnSend_Click" />
                                    <%--<a class="" href="#">GỬI YÊU CẦU</a>--%>
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

        <asp:Button ID="btnclear" runat="server" OnClick="btnclear_Click" Style="display: none;" />
    </main>
    <asp:HiddenField ID="hdfTradeID" runat="server" />


    <telerik:RadScriptBlock ID="sc" runat="server">
        <script type="text/javascript">
            function deleteTrade(ID) {
                var r = confirm("Bạn muốn hủy yêu cầu?");
                if (r == true) {
                    $("#<%= hdfTradeID.ClientID %>").val(ID);
                    $("#<%= btnclear.ClientID %>").click();
                } else {
                }
            }
        </script>
    </telerik:RadScriptBlock>

</asp:Content>
