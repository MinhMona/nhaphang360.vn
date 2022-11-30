<%@ Page Title="Nạp tiền VNĐ" Language="C#" MasterPageFile="~/manager/adminMasterNew.Master" AutoEventWireup="true" CodeBehind="UserWallet.aspx.cs" Inherits="NHST.manager.UserWallet" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="main" class="main-full">
        <div class="row">
            <div class="content-wrapper-before bg-dark-gradient"></div>
            <div class="page-title">
                <div class="card-panel">
                    <h4 class="title no-margin" style="display: inline-block;">Nạp tiền VNĐ</h4>
                    <div class="clearfix"></div>
                </div>
            </div>
            <div class="list-staff col s12 m8 l4 xl4 section">
                <div class="rp-detail card-panel row">
                    <div class="col s12">
                        <div class="row pb-2 border-bottom-1 ">
                            <div class="input-field col s12">
                                <asp:TextBox runat="server" ID="rp_username" type="text" class="validate" value="" disabled></asp:TextBox>
                                <label for="rp_username">Username</label>
                            </div>
                            <div class="input-field col s12">
                                <%-- <asp:TextBox runat="server" ID="rp_vnd" type="text" class="validate" TextMode="Number"></asp:TextBox>--%>
                                <telerik:RadNumericTextBox runat="server" Skin="MetroTouch"
                                    ID="pAmount" NumberFormat-DecimalDigits="0" Value="0"
                                    NumberFormat-GroupSizes="3" Width="100%">
                                </telerik:RadNumericTextBox>
                               <%-- <label for="rp_vnd">Số tiền (VNĐ)</label>--%>
                            </div>
                            <div class="input-field col s12">
                                <asp:TextBox runat="server" ID="rp_textarea"
                                    class="materialize-textarea">Nạp tiền</asp:TextBox>
                                <label for="rp_textarea">Nội dung</label>
                            </div>
                            <div class="input-field col s12">
                                <asp:DropDownList runat="server" ID="ddlBank" DataTextField="BankInfo" CssClass="icons"
                                    DataValueField="ID">
                                </asp:DropDownList>
                                <label>Ngân hàng</label>
                            </div>
                            <div class="input-field col s12">
                                <asp:DropDownList runat="server" ID="ddlStatus">
                                    <asp:ListItem Value="1" Selected="True">Chưa duyệt</asp:ListItem>
                                    <asp:ListItem Value="2">Đã duyệt </asp:ListItem>
                                </asp:DropDownList>
                                <label>Trạng thái</label>
                            </div>

                            <div class="input-field col s12" style="display:none;">
                                <div class="switch status-func">
                                    <span class="mr-2">Tiền vay: </span>
                                    <label>
                                        <asp:TextBox runat="server" onclick="CheckStatus()" ID="txtStatus" type="checkbox"></asp:TextBox>
                                        <span class="lever"></span>
                                    </label>
                                </div>
                            </div>
                            <asp:HiddenField runat="server" ID="hdfStatus" Value="0" />
                        </div>
                        <div class="row section mt-2">
                            <div class="col s12">
                                <asp:Button ID="Button1" runat="server" Text="Nạp tiền" CssClass="btn" OnClick="btncreateuser_Click" />
                                <asp:Literal ID="ltr" runat="server" EnableViewState="false"></asp:Literal>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        $(document).ready(function () {
            M.textareaAutoResize($('#rp_textarea'));
        });

        function CheckStatus() {
            var a = $('#<%=hdfStatus.ClientID%>').val();
            if (a == '0') {
                $('#<%=hdfStatus.ClientID%>').val('1');
             }
             else {

                 $('#<%=hdfStatus.ClientID%>').val('0');
            }
            console.log($('#<%=hdfStatus.ClientID%>').val());
        }
    </script>
</asp:Content>

