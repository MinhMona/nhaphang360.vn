<%@ Page Title="Chi tiết sản phẩm" Language="C#" MasterPageFile="~/manager/adminMasterNew.Master" AutoEventWireup="true" CodeBehind="ProductEdit.aspx.cs" Inherits="NHST.manager.ProductEdit" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="main" class="main-full">
        <div class="row">
            <div class="content-wrapper-before bg-dark-gradient"></div>
            <div class="page-title">
                <div class="card-panel">
                    <h4 class="title no-margin" style="display: inline-block;">Chi tiết sản phẩm</h4>
                    <div class="right-action">
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
            <div class="list-staff col s12 section">
                <div class="rp-detail card-panel row">
                    <div class="col s12 m12 l6">
                        <div class="row">
                            <div class="input-field col s12 m12">
                                <asp:TextBox runat="server" ID="txtTitleOrigin" type="text" class="validate" placeholder="" BackColor="LightGray" Enabled="false"></asp:TextBox>
                                <label for="pd_name">Tên sản phẩm</label>
                            </div>
                            <div class="input-field col s12 m6">
                                <asp:TextBox runat="server" ID="pQuanity" TextMode="Number" placeholder="" class="validate"></asp:TextBox>
                                <label for="pd_count">Số lượng</label>
                            </div>

                            <div class="input-field col s12 m6">
                                <asp:TextBox runat="server" ID="pProductPriceOriginal"  TextMode="Number" class="validate" placeholder=""></asp:TextBox>
                                <label for="pd_price">Giá sản phẩm CYN (¥)</label>
                            </div>
                            <div class="input-field col s12 m6" style="display:none">
                                <asp:TextBox runat="server" ID="pRealPrice"  TextMode="Number" class="validate" placeholder=""></asp:TextBox>
                                <label for="pd_realprice">Giá mua thực tế (¥)</label>
                            </div>

                            <div class="input-field col s12">
                                <asp:TextBox ID="txtproducbrand" runat="server" TextMode="SingleLine" placeholder=""></asp:TextBox>
                                <label for="rp_textarea">Ghi chú sản phẩm</label>
                            </div>
                            <div class="input-field col s12  ">
                                 <asp:DropDownList ID="ddlStatus" runat="server">
                                                <asp:ListItem Value="1">Còn hàng</asp:ListItem>
                                                <asp:ListItem Value="2">Hết hàng</asp:ListItem>
                                </asp:DropDownList>
                                <label>Trạng thái </label>
                            </div>
                        </div>
                        <div class="action-btn mt-3">
                              <asp:Button ID="btncreateuser" runat="server" Text="Cập nhật" CssClass="btn primary-btn"
                                                OnClick="btncreateuser_Click" />
                                            <asp:Literal ID="ltrback" runat="server"></asp:Literal>
                           
                        </div>
                    </div>

                </div>
            </div>
        </div>

    </div>
    <asp:HiddenField ID="hdfcurrent" runat="server" />
    <!-- END: Page Main-->
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
