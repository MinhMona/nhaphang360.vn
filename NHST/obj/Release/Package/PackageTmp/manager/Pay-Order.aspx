<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMasterNew.Master" AutoEventWireup="true" CodeBehind="Pay-Order.aspx.cs" Inherits="NHST.manager.Pay_Order" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <div id="main" class="main-full">
  <div class="row">
      <div class="content-wrapper-before bg-dark-gradient"></div>
      <div class="page-title">
         <div class="card-panel">
            <h4 class="title no-margin" style="display:inline-block;">Thanh toán đơn hàng #<asp:Label ID="lbOrderID" runat="server" CssClass="form-control" ></asp:Label></h4>
         </div>
      </div>
      <div class="checkout-info-wrap col s12 m12 l6">
         <div class="card-panel">

        
            <div class="order-row">
               <div class="left-fixed">
                     <span>Mã đơn hàng:</span>
               </div>
               <div class="right-content">
                     <span class="black-text font-weight-500"><asp:Label ID="lblMainOrderID" runat="server" CssClass="form-control" ></asp:Label></span>
               </div>
            </div>
            <div class="order-row">
               <div class="left-fixed">
                     <span>Tổng hóa đơn:</span>
               </div>
               <div class="right-content">
                     <span class="black-text font-weight-500"><asp:Label ID="lblTotalPriceVND" runat="server" CssClass="form-control" ></asp:Label> VNĐ</span>
               </div>
            </div>
            <div class="order-row">
               <div class="left-fixed">
                     <span>Người đặt hàng:</span>
               </div>
               <div class="right-content">
                     <span class="black-text font-weight-500"><asp:Label ID="lblUsername" runat="server" CssClass="form-control"></asp:Label></span>
               </div>
            </div>
            <div class="order-row">
               <div class="left-fixed">
                     <span>Loại thanh toán:</span>
               </div>
               <div class="right-content">
                     <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control select2" onchange="showprice()">
                     </asp:DropDownList>
                     
               </div>
            </div>
            <div class="order-row">
               <div class="left-fixed">
                     <span>Số tiền phải đặt cọc:</span>
               </div>
               <div class="right-content">
               <span class="black-text font-weight-500"><asp:Label ID="lblAmountDeposit" runat="server" CssClass="form-control" ></asp:Label> VNĐ</span>
               </div>
            </div>
            <div class="order-row">
               <div class="left-fixed">
                     <span>Số tiền phải thanh toán:</span>
               </div>
               <div class="right-content">
               <span class="black-text font-weight-500"><asp:Label ID="lblMusPay" runat="server" CssClass="form-control"></asp:Label> VNĐ</span>
               </div>
            </div>
            <div class="order-row">
               <div class="left-fixed">
                     <span>Phương thức thanh toán:</span>
               </div>
               <div class="right-content">
                    <asp:DropDownList ID="ddlPaymentMethod" runat="server" CssClass="form-control select2" onchange="showprice()">
                                                <asp:ListItem Value="2" Text="Ví điện tử"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Trực tiếp"></asp:ListItem>
                                            </asp:DropDownList>
                     
               </div>
            </div>
            <div class="order-row">
               <div class="left-fixed">
                     <span>Số tiền thanh toán:</span>
               </div>
               <div class="right-content">                  
                       <telerik:RadNumericTextBox runat="server" Skin="MetroTouch" BorderStyle="Solid" BorderColor="Black"
                                                ID="pAmount" MinValue="0" NumberFormat-DecimalDigits="0"
                                                NumberFormat-GroupSizes="3" Width="100%">
                                            </telerik:RadNumericTextBox>                                
               </div>
            </div>
            <div class="order-row">
               <div class="left-fixed">
                     <span>Nội dung thanh toán:</span>
               </div>
               <div class="right-content">
                   <asp:TextBox runat="server" TextMode="MultiLine" id="pContent1" class="materialize-textarea"></asp:TextBox>                 
               </div>
            </div>
            <hr class="mt-4"/>
            <div class="action-btn mt-4">
                <asp:Button ID="btnPay" runat="server" Text="Thanh toán" CssClass="btn" OnClick="btnPay_Click" />
                <asp:Button ID="btnback" runat="server" Text="Chi tiết đơn hàng" CssClass="btn primary-btn" OnClick="btnback_Click" />
            </div>
         </div>
      </div>
  </div>
</div>
        <asp:HiddenField ID="hdfshow" runat="server" />
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
