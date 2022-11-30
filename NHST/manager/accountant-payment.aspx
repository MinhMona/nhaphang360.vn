<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="accountant-payment.aspx.cs" Inherits="NHST.manager.accountant_payment" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel runat="server" ID="Parent">
        <main id="main-wrap">
            <div class="grid-row">
                <div class="grid-col" id="main-col-wrap">
                    <div class="feat-row grid-row">
                        <div class="grid-col-50 grid-row-center">
                            <article class="pane-primary">
                                <div class="heading">
                                    <h3 class="lb">Thanh toán đơn hàng</h3>
                                </div>
                                <div class="cont">
                                    <div class="inner">
                                        <div class="form-row marbot1">
                                            Username
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-row marbot1">
                                            Mã đơn hàng
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:TextBox ID="txtMainOrderID" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-row marbot1">
                                            Số tiền
                                        </div>
                                        <div class="form-row marbot2">
                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control" Skin="MetroTouch"
                                                ID="pAmount" NumberFormat-DecimalDigits="0" MinValue="0"
                                                NumberFormat-GroupSizes="3" Width="100%" placeholder="Số tiền muốn rút" Value="100000">
                                            </telerik:RadNumericTextBox>
                                        </div>
                                                                              
                                        <div class="form-row no-margin center-txt">
                                            <%--<asp:Button ID="btncreateuser" runat="server" Text="Cập nhật" CssClass="btn primary-btn"
                                                OnClick="btncreateuser_Click" />--%>
                                            <a href="javascript:;" class="btn primary-btn" onclick="payOrder()">Thanh toán</a>
                                            <a href="javascript:;" class="btn primary-btn" onclick="printReceipt()">Xuất hóa đơn</a>
                                        </div>
                                    </div>
                                    <div class="print-bill-abc"></div>
                                </div>
                            </article>
                        </div>
                    </div>
                </div>
            </div>
        </main>
        
    </asp:Panel>
    <telerik:RadAjaxManager ID="rAjax" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btncreateuser">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Parent" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <script type="text/javascript">
        function payOrder()
        {
            var id = $("#<%=txtMainOrderID.ClientID%>").val();
            swal
	        (
		        {
		            title: 'Thông báo',
		            text: 'Thanh hoán đơn hàng: ' + id + ' thành công',
		            type: 'success'
		        }
		        //function () { window.location.replace(window.location.href); }
	         );
        }
        function VoucherSourcetoPrint(source) {
            var r = "<html><head><link rel=\"stylesheet\" href=\"/App_Themes/AdminNew/css/style-p.css\" type=\"text/css\"/><script>function step1(){\n" +
                    "setTimeout('step2()', 10);}\n" +
                    "function step2(){window.print();window.close()}\n" +
                    "</scri" + "pt></head><body onload='step1()'>\n" +
                    "" + source + "</body></html>";
            return r;
        }
        function VoucherPrint(source) {
            Pagelink = "about:blank";
            var pwa = window.open(Pagelink, "_new");
            pwa.document.open();
            pwa.document.write(VoucherSourcetoPrint(source));
            pwa.document.close();
        }
        function printReceipt()
        {
            var id = $("#<%=txtMainOrderID.ClientID%>").val();
            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1; //January is 0!
            var yyyy = today.getFullYear();

            if (dd < 10) {
                dd = '0' + dd
            }

            if (mm < 10) {
                mm = '0' + mm
            }

            today = dd + '/' + mm + '/' + yyyy;
            var html = "";
            html += "<div class=\"print-bill\">";
            html += "   <div class=\"top\">";
            html += "       <div class=\"left\">";
            html += "           <span class=\"company-info\">MONA MEDIA</span>";
            html += "           <span class=\"company-info\">Địa chỉ: 319-C16 Lý Thường Kiệt, P.15, Q.11, Tp.HCM</span>";
            html += "       </div>";
            html += "       <div class=\"right\">";
            html += "           <span class=\"bill-num\">Mẫu số 01 - TT</span>";
            html += "           <span class=\"bill-promulgate-date\">(Ban hành theo Thông tư số 133/2016/TT-BTC ngày 26/8/2016 của Bộ Tài chính)</span>";
            html += "       </div>";
            html += "   </div>";
            html += "   <div class=\"bill-title\">";
            html += "       <h1>PHIẾU THU</h1>";
            html += "       <span class=\"bill-date\">" + today + " </span>";
            html += "   </div>";
            html += "   <div class=\"bill-content\">";
            html += "       <div class=\"bill-row\">";
            html += "           <label class=\"row-name\">Họ và tên người nộp tiền: </label>";
            html += "           <label class=\"row-info\">Vy Nguyễn Khánh Hùng</label>";
            html += "       </div>";
            html += "       <div class=\"bill-row\">";
            html += "           <label class=\"row-name\">Địa chỉ: </label>";
            html += "           <label class=\"row-info\">319-C16 Lý Thường Kiệt, P.15, Q.11, Tp.HCM</label>";
            html += "       </div>";
            html += "       <div class=\"bill-row\">";
            html += "           <label class=\"row-name\">Lý do nộp: </label>";
            html += "           <label class=\"row-info\">Thanh toán đơn hàng " + id + "</label>";
            html += "       </div>";
            html += "       <div class=\"bill-row\">";
            html += "           <label class=\"row-name\">Số tiền: </label>";
            html += "           <label class=\"row-info\">12.000.000</label>";
            html += "       </div>";
            html += "       <div class=\"bill-row\">";
            html += "           <label class=\"row-name\">Bằng chữ: </label>";
            html += "           <label class=\"row-info\">Mười hai triệu đồng</label>";
            html += "       </div>";            
            html += "   </div>";
            html += "   <div class=\"bill-footer\">";
            html += "       <div class=\"bill-row-two\">";
            html += "           <strong>Người xuất hóa đơn</strong>";
            html += "           <span class=\"note\">(Ký, họ tên)</span>";
            html += "       </div>";
            html += "       <div class=\"bill-row-two\">";
            html += "           <strong>Người nộp tiền</strong>";
            html += "           <span class=\"note\">(Ký, họ tên)</span>";
            html += "       </div>";
            html += "   </div>";
            html += "</div>";
            VoucherPrint(html);
            //$(".print-bill-abc").html(html);
        }
    </script>
</asp:Content>
