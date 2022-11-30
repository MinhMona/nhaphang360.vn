<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ProductEdit.aspx.cs" Inherits="NHST.Admin.ProductEdit" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainTit" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="main" runat="server">
    <asp:Panel runat="server" ID="Parent">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-white">
                    <div class="panel-heading">
                        <h3 class="panel-title semi-text text-uppercase">Chỉnh sửa giá sản phẩm</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group marbot1">
                                    Tên sản phẩm
                                </div>
                                <div class="form-group marbot2">
                                    <asp:Label ID="lblBrandname" runat="server" CssClass="form-control has-validate"></asp:Label>
                                </div>
                                <div class="form-group marbot1">
                                    Giá sản phẩm CYN (<i class="fa fa-yen"></i>)
                                </div>
                                <div class="form-group marbot2">
                                    <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                        ID="pProductPriceOriginal" NumberFormat-DecimalDigits="2"
                                        NumberFormat-GroupSizes="3" Width="100%">
                                    </telerik:RadNumericTextBox>
                                    <span class="error-info-show">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="pProductPriceOriginal" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </span>
                                </div>
                                <div class="form-group marbot1">
                                    Giá mua thực tế (<i class="fa fa-yen"></i>)
                                </div>
                                <div class="form-group marbot2">
                                    <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                        ID="pRealPrice" NumberFormat-DecimalDigits="2"
                                        NumberFormat-GroupSizes="3" Width="100%">
                                    </telerik:RadNumericTextBox>
                                    <span class="error-info-show">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="pRealPrice" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </span>
                                </div>
                                <div class="form-group marbot1">
                                    Số lượng
                                </div>
                                <div class="form-group marbot2">
                                    <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                        ID="pQuanity" NumberFormat-DecimalDigits="0" MinValue="0"
                                        NumberFormat-GroupSizes="3" Width="100%">
                                    </telerik:RadNumericTextBox>
                                    <span class="error-info-show">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="pQuanity" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </span>
                                </div>
                                <div class="form-group marbot1">
                                    Ghi chú riêng sản phẩm
                                </div>
                                <div class="form-group marbot2">
                                    <asp:TextBox ID="txtproducbrand" runat="server" CssClass="form-control width-notfull"></asp:TextBox>
                                </div>
                                <div class="form-group marbot1">
                                    Trạng thái
                                </div>
                                <div class="form-group marbot2">
                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="1">Còn hàng</asp:ListItem>
                                        <asp:ListItem Value="2">Hết hàng</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group no-margin">
                                    <asp:Button ID="btncreateuser" runat="server" Text="Cập nhật" CssClass="btn btn-success btn-block small-btn"
                                        OnClick="btncreateuser_Click" />
                                    <asp:Literal ID="ltrback" runat="server"></asp:Literal>
                                    <%--<a href="javascript:;" onclick="createuser()" class="btn btn-success btn-block small-btn right-btn">Tạo tài khoản</a>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hdfcurrent" runat="server" />
    </asp:Panel>
    <telerik:RadAjaxManager ID="rAjax" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSave">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Parent" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadScriptBlock ID="rc" runat="server">
        <script type="text/javascript">
            function keypress(e) {
                var keypressed = null;
                if (window.event) {
                    keypressed = window.event.keyCode; //IE
                }
                else {
                    keypressed = e.which; //NON-IE, Standard
                }
                if (keypressed < 48 || keypressed > 57) {
                    if (keypressed == 8 || keypressed == 127) {
                        return;
                    }
                    return false;
                }
            }
            function isEmpty(str) {
                return !str.replace(/^\s+/g, '').length; // boolean (`true` if field is empty)
            }
            var currency = $("#<%=hdfcurrent.ClientID%>").val();
            function price(type) {
                var shipfeendt = $("#<%= pProductPriceOriginal.ClientID%>").val();
                var shipfeevnd = $("#<%= pRealPrice.ClientID%>").val();
                if (type == "vnd") {
                    if (isEmpty(shipfeevnd) != true) {
                        var ndt = shipfeevnd / currency;
                        $("#<%= pProductPriceOriginal.ClientID%>").val(ndt);
                    }
                    else {
                        $("#<%= pProductPriceOriginal.ClientID%>").val(0);
                        $("#<%= pRealPrice.ClientID%>").val(0);
                    }
                }
                else {
                    if (!isEmpty(shipfeendt)) {
                        var vnd = shipfeendt * currency;
                        $("#<%= pRealPrice.ClientID%>").val(vnd);
                    }
                    else {
                        $("#<%= pProductPriceOriginal.ClientID%>").val(0);
                        $("#<%= pRealPrice.ClientID%>").val(0);
                    }
                }
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
