<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Pay-Order.aspx.cs" Inherits="NHST.Admin.Pay_Order" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
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
                        <h3 class="panel-title semi-text text-uppercase">Thanh toán đơn hàng</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group col-md-12">
                                    Mã đơn hàng
                                    <br />
                                    <asp:Label ID="lblMainOrderID" runat="server" CssClass="form-control" BackColor="LightGray"></asp:Label>
                                </div>
                                <div class="form-group col-md-12">
                                    Tổng hóa đơn
                                    <br />
                                    <asp:Label ID="lblTotalPriceVND" runat="server" CssClass="form-control" BackColor="LightGray"></asp:Label>
                                </div>
                                <div class="form-group col-md-12">
                                    Người đặt hàng
                                    <br />
                                    <asp:Label ID="lblUsername" runat="server" CssClass="form-control" BackColor="LightGray"></asp:Label>
                                </div>
                                <div class="form-group col-md-12">
                                    Loại thanh toán
                                    <br />
                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control select2" onchange="showprice()">
                                        <%--<asp:ListItem Value="2" Text="Đặt cọc"></asp:ListItem>
                                        <asp:ListItem Value="9" Text="Thanh toán"></asp:ListItem>--%>
                                    </asp:DropDownList>
                                </div>

                                <div id="pndeposit" class="form-group col-md-12" style="display: none;">
                                    Số tiền phải đặt cọc
                                    <br />
                                    <asp:Label ID="lblAmountDeposit" runat="server" CssClass="form-control" BackColor="LightGray"></asp:Label>
                                </div>
                                <div id="pnPayall" class="form-group col-md-12" style="display: none;">
                                    Số tiền phải thanh toán
                                    <br />
                                    <asp:Label ID="lblMusPay" runat="server" CssClass="form-control" BackColor="LightGray"></asp:Label>
                                </div>
                                <%--<div class="form-group col-md-12">
                                    Phương thức thanh toán
                                    <br />
                                    <asp:DropDownList ID="ddlPaymentMethod" runat="server" CssClass="form-control select2" onchange="showprice()">
                                        <asp:ListItem Value="1" Text="Trực tiếp"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Ví điện tử"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>--%>

                                <div class="form-group col-md-12">
                                    Số tiền
                                    <br />
                                    <telerik:RadNumericTextBox runat="server" CssClass="form-control" Skin="MetroTouch"
                                        ID="pAmount" MinValue="0" NumberFormat-DecimalDigits="0"
                                        NumberFormat-GroupSizes="3" Width="100%" placeholder="Tiền thanh toán">
                                    </telerik:RadNumericTextBox>
                                </div>
                                <div class="form-group col-md-12">
                                    Nội dung
                                <br />
                                    <telerik:RadEditor runat="server" ID="pContent" Width="100%"
                                        Height="600px" ToolsFile="~/FilesResources/ToolContent.xml" Skin="Metro"
                                        DialogHandlerUrl="~/Telerik.Web.UI.DialogHandler.axd" AutoResizeHeight="True">
                                        <ImageManager ViewPaths="~/Uploads/Images" UploadPaths="~/Uploads/Images" DeletePaths="~/Uploads/Images" />
                                    </telerik:RadEditor>
                                </div>
                                <div class="form-group no-margin">
                                    <asp:Button ID="btnPay" runat="server" Text="Thanh toán" CssClass="btn btn-success btn-block small-btn"
                                        OnClick="btnPay_Click" />
                                    <asp:Button ID="btnback" runat="server" Text="Chi tiết đơn hàng" CssClass="btn btn-success btn-block small-btn"
                                        OnClick="btnback_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hdfshow" runat="server" />
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
        $(document).ready(function () {
            var show = $("#<%=hdfshow.ClientID%>").val();
            $("#" + show).show();
        });
            function showprice() {
                var val = $("#<%= ddlStatus.ClientID%>").val();
                if (val == 2) {
                    $("#pndeposit").show();
                    $("#pnPayall").hide();
                }
                else {
                    $("#pndeposit").hide();
                    $("#pnPayall").show();
                }
            }
    </script>

</asp:Content>
