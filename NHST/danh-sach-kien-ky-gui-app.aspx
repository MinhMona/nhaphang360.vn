<%@ Page Language="C#" MasterPageFile="~/App.Master" AutoEventWireup="true" CodeBehind="danh-sach-kien-ky-gui-app.aspx.cs" Inherits="NHST.danh_sach_kien_ky_gui_app" %>

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
                            <h1 class="page-title">ĐƠN HÀNG VẬN CHUYỂN</h1>
                            <div class="frow flex-group">
                                <asp:TextBox runat="server" ID="txtOrderTransactionCode" CssClass="fcontrol" placeholder="Mã vận đơn"></asp:TextBox>
                            </div>
                            <div class="frow">
                                <div class="ip-with-sufix">
                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="fcontrol">
                                        <asp:ListItem Value="-1" Text="Tất cả trạng thái"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Đơn hàng mới"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Đã về kho TQ"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="Đã về kho đích"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="Đã yêu cầu"></asp:ListItem>
                                        <asp:ListItem Value="6" Text="Đã nhận hàng"></asp:ListItem>
                                        <asp:ListItem Value="0" Text="Đơn hàng hủy"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <asp:Button ID="btnSear" runat="server" CssClass="btn primary-btn fw-btn" OnClick="btnSear_Click" Text="Tìm kiếm" />
                            <a href="javascript:;" id="exportselected" onclick="requestexportselect()" style="display: none; margin-top: 10px" class="btn primary-btn fw-btn">Yêu cầu xuất kho các kiện đã chọn</a>
                            <a href="javascript:;" style="margin-top: 10px" onclick="requestoutstockAll()" class="btn primary-btn fw-btn">Yêu cầu xuất kho tất cả kiện</a>
                        </div>
                    </div>

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

    <asp:HiddenField ID="hdfListID" runat="server" />
    <asp:HiddenField ID="hdfShippingType" runat="server" />
    <asp:HiddenField ID="hdfNote" runat="server" />
    <asp:HiddenField runat="server" ID="hdfListShippingVN" />
    <asp:Button ID="btnPayExport" UseSubmitBehavior="false" runat="server" OnClick="btnPayExport_Click" Style="display: none" />
    <asp:Button ID="btnThanhToanTaiKho" UseSubmitBehavior="false" runat="server" OnClick="btnThanhToanTaiKho_Click" Style="display: none" />

    <script>
        $(document).ready(function () {
            var table = $('.order-list-info .table');
            $('tr[data-action="other"] td label').hide();
            var listCb = table.find('tbody tr td label input[type="checkbox"]');


            $('.collapase_header .collapse').on('click', function () {
                $(this).toggleClass('active');
                $(this).parent().next().slideToggle();

            });

        });

        function isEmpty(str) {
            return !str.replace(/^\s+/g, '').length; // boolean (`true` if field is empty)
        }

        var formatThousands = function (n, dp) {
            var s = '' + (Math.floor(n)), d = n % 1, i = s.length, r = '';
            while ((i -= 3) > 0) { r = ',' + s.substr(i, 3) + r; }
            return s.substr(0, i + 3) + r + (d ? '.' + Math.round(d * Math.pow(10, dp || 2)) : '');
        };


        function selectdeposit() {
            var check = false;
            $(".chk-deposit").each(function () {
                if ($(this).is(':checked')) {
                    check = true;
                }
            });
            if (check == true) {
                $("#exportselected").show();
                $("#exportselected1").show();
            }
            else {
                $("#exportselected").hide();
                $("#exportselected1").hide();
            }

        }


        function requestexportselect() {
            var count = 0;
            var html = "";
            $(".chk-deposit").each(function () {
                if ($(this).is(':checked')) {
                    html += $(this).attr("data-id") + "|";
                    count++;
                }
            });
            if (count > 0) {
                    //$("#<%= hdfListID.ClientID%>").val(html);
                $.ajax({
                    type: "POST",
                    url: "/danh-sach-kien-ky-gui-app.aspx/exportSelectedAll",
                    data: "{listID:'" + html + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var ret = msg.d;
                        if (ret != "0") {
                            var c = confirm('Bạn muốn xuất kho tất cả các kiện đã chọn?');
                            if (c == true) {
                                var ret = msg.d;
                                var data = ret.split(':');
                                var status = data[0];
                                var wallet = data[1];
                                var walletstr = formatThousands(wallet, 0);
                                var totalCount = data[2];
                                var totalWeight = data[3];
                                var totalWeightPriceCYN = data[4];
                                var totalWeightPriceVND = data[5];
                                var totalWeightPriceVNDstr = formatThousands(totalWeightPriceVND, 0);
                                var feeOutStockCYN = data[6]
                                var feeOutStockVND = data[7];
                                var feeOutStockVNDstr = formatThousands(feeOutStockVND, 0);
                                var totalPriceCYN = data[8];
                                var totalPriceVND = data[9];
                                var totalPriceVNDstr = formatThousands(totalPriceVND, 0);
                                var listID = data[10];
                                var TotalSensoredFeeVND = data[11];
                                var TotalSensoredFeeVNDstr = formatThousands(TotalSensoredFeeVND, 0);
                                var TotalAdditionFeeVND = data[12];
                                var TotalAdditionFeeVNDstr = formatThousands(TotalAdditionFeeVND, 0);
                                if (status == 1) {
                                    $("#<%=hdfListID.ClientID%>").val(listID);
                                    var button = "<a href=\"javascript:;\" class=\"btn btn-close pill-btn primary-btn main-btn hover\" onclick=\"paytoexport($(this))\">Thanh toán</a>";
                                    button += "             <a href=\"javascript:;\" class=\"btn btn-close pill-btn primary-btn main-btn hover\" onclick='ThanhToanTaiKho($(this))'>Thanh toán tại kho</a>";
                                    var html = "";
                                    html += "<div class=\"popup-row\">";
                                    html += "   <p>Tổng số mã xuất kho của quý khách  : <strong>" + totalCount
                                        + "</strong></p>";
                                    html += "</div>";
                                    html += "<div class=\"popup-row\">";
                                    html += "<p>Tổng số kg xuất kho: " + totalWeight
                                        + " kg - <strong>" + totalWeightPriceVNDstr + "</strong> VNĐ.</p>";
                                    html += "</div>";
                                    html += "<div class=\"popup-row\" style=\"display:none\">";
                                    html += "<p>Phí hải quan xuất kho cố định: <strong>" + feeOutStockVNDstr + "</strong> VNĐ.</p>";
                                    html += "</div>";
                                    html += "<div class=\"popup-row\" style=\"display:none\">";
                                    html += "<p>Phí hải quan xuất kho cố định: <strong>" + feeOutStockVNDstr + "</strong> VNĐ.</p>";
                                    html += "</div>";
                                    html += "<div class=\"popup-row\">";
                                    html += "<p>Cước vật tư: <strong>" + TotalSensoredFeeVNDstr + "</strong> VNĐ.</p>";
                                    html += "</div>";
                                    html += "<div class=\"popup-row\">";
                                    html += "<p>PP hàng đặc biệt: <strong>" + TotalAdditionFeeVNDstr + "</strong> VNĐ.</p>";
                                    html += "</div>";

                                    html += "<div class=\"popup-row\">";
                                    html += "<p>Tổng số tiền xuất kho của quý khách: <strong>" + totalPriceVNDstr + "</strong> VNĐ.</p>";
                                    html += "</div>";
                                    html += "<div class=\"popup-row\">";
                                    html += "<span class=\"popuprow-left\">Hình thức vận chuyển trong nước:</span>";
                                    var s = $("#<%=hdfListShippingVN.ClientID%>").val();
                                    var lists = s.split('|');
                                    if (lists.length - 1 > 0) {
                                        html += "<select class=\"form-control popuprow-right shippingtypevn\">";
                                        html += "   <option value=\"0\">---Chọn phương thức vận chuyển---</option>";
                                        for (var i = 0; i < lists.length - 1; i++) {
                                            var item = lists[i].split(':');
                                            var sID = item[0];
                                            var sName = item[1];
                                            html += "<option value=\"" + sID + "\">" + sName + "</option>";
                                        }
                                        html += "</select>";
                                    }
                                    html += "</div>";
                                    html += "<div class=\"popup-row\">";
                                    html += "<span class=\"popuprow-left\">Ghi chú:</span>";
                                    html += "<input class=\"form-control requestnote popuprow-right\" placeholder=\"Ghi chú\"/>";
                                    html += "</div>";
                                    html += "<div class=\"popup-row\">";
                                    html += "<span class=\"popuperror\" style=\"color:red\"></span>";
                                    html += "</div>";
                                    showPopup("Thanh toán xuất kho", html, button);
                                }
                                else {
                                    $("#<%=hdfListID.ClientID%>").val(listID);
                                    var leftmoney = data[13];
                                    var leftmoneystr = formatThousands(leftmoney, 0);
                                    var button = "";
                                    button += "             <a href=\"javascript:;\" class=\"btn btn-close pill-btn primary-btn main-btn hover\" onclick='ThanhToanTaiKho($(this))'>Thanh toán tại kho</a>";
                                    var html = "";
                                    html += "<div class=\"popup-row\">";
                                    html += "   <p>Tổng số mã xuất kho của quý khách  : <strong>" + totalCount
                                        + "</strong></p>";
                                    html += "</div>";
                                    html += "<div class=\"popup-row\">";
                                    html += "<p>Tổng số kg xuất kho: " + totalWeight
                                        + " kg - <strong>" + totalWeightPriceVNDstr + "</strong> VNĐ.</p>";
                                    html += "</div>";
                                    html += "<div class=\"popup-row\" style=\"display:none\">";
                                    html += "<p>Phí hải quan xuất kho cố định: <strong>" + feeOutStockVNDstr + "</strong> VNĐ.</p>";
                                    html += "</div>";
                                    html += "<div class=\"popup-row\">";
                                    html += "<p>Cước vật tư: <strong>" + TotalSensoredFeeVNDstr + "</strong> VNĐ.</p>";
                                    html += "</div>";
                                    html += "<div class=\"popup-row\">";
                                    html += "<p>PP hàng đặc biệt: <strong>" + TotalAdditionFeeVNDstr + "</strong> VNĐ.</p>";
                                    html += "</div>";
                                    html += "<div class=\"popup-row\">";
                                    html += "<p>Tổng số tiền xuất kho của quý khách: <strong>" + totalPriceVNDstr + "</strong> VNĐ.</p>";
                                    html += "</div>";
                                    html += "<div class=\"popup-row\">";
                                    //html += "<p>Số tiền trong tài khoản của quý khách : <strong>" + walletstr + "</strong> VNĐ. Bạn còn thiếu <strong>" + strrechargeMore + "</strong> VNĐ.</p>";
                                    html += "<p>Số tiền trong tài khoản của quý khách : <strong>" + walletstr + "</strong> VNĐ.</p>";
                                    html += "</div>";
                                    html += "<div class=\"popup-row\">";
                                    html += "<p>Quý khách còn thiếu <strong>" + leftmoneystr + "</strong> VNĐ để xuất kho thành công.</p>";
                                    html += "</div>";
                                    html += "<div class=\"popup-row\">";
                                    html += "<span class=\"popuprow-left\">Hình thức vận chuyển trong nước:</span>";
                                    var s = $("#<%=hdfListShippingVN.ClientID%>").val();
                                    var lists = s.split('|');
                                    if (lists.length - 1 > 0) {
                                        html += "<select class=\"form-control popuprow-right shippingtypevn\">";
                                        html += "   <option value=\"0\">---Chọn phương thức vận chuyển---</option>";
                                        for (var i = 0; i < lists.length - 1; i++) {
                                            var item = lists[i].split(':');
                                            var sID = item[0];
                                            var sName = item[1];
                                            html += "<option value=\"" + sID + "\">" + sName + "</option>";
                                        }
                                        html += "</select>";
                                    }
                                    html += "</div>";
                                    html += "<div class=\"popup-row\">";
                                    html += "<span class=\"popuprow-left\">Ghi chú:</span>";
                                    html += "<input class=\"form-control requestnote popuprow-right\" placeholder=\"Ghi chú\"/>";
                                    html += "</div>";
                                    html += "<div class=\"popup-row\">";
                                    html += "<span class=\"popuperror\" style=\"color:red\">Để nạp thêm tiền vào tài khoản, vui lòng truy cập <a href=\"/chuyen-muc/huong-dan/nap-tien-vao-tai-khoan\" target=\"_blank\"><strong style=\"font-size:20px;\">TẠI ĐÂY</strong></a>.</span>";
                                    html += "</div>";
                                    showPopup("Thanh toán xuất kho", html, button);
                                }
                            }
                        }
                        else {
                            alert('Hiện tại không đơn thích hợp để yêu cầu xuất kho.');
                        }
                    },
                    error: function (xmlhttprequest, textstatus, errorthrow) {
                        //alert('lỗi checkend');
                    }
                });
            }
            else {
                alert('Vui lòng chọn kiện bạn muốn yêu cầu xuất kho');
            }
        }
        function requestoutstockAll() {
            $.ajax({
                type: "POST",
                url: "/danh-sach-kien-ky-gui-app.aspx/exportAll",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var ret = msg.d;
                    if (ret != "0") {
                        var c = confirm('Bạn muốn xuất kho tất cả các kiện?');
                        if (c == true) {
                            var ret = msg.d;
                            var data = ret.split(':');
                            var status = data[0];
                            var wallet = data[1];
                            var walletstr = formatThousands(wallet, 0);
                            var totalCount = data[2];
                            var totalWeight = data[3];
                            var totalWeightPriceCYN = data[4];
                            var totalWeightPriceVND = data[5];
                            var totalWeightPriceVNDstr = formatThousands(totalWeightPriceVND, 0);
                            var feeOutStockCYN = data[6]
                            var feeOutStockVND = data[7];
                            var feeOutStockVNDstr = formatThousands(feeOutStockVND, 0);
                            var totalPriceCYN = data[8];
                            var totalPriceVND = data[9];
                            var totalPriceVNDstr = formatThousands(totalPriceVND, 0);
                            var listID = data[10];
                            var totalAdditionFeeCYN = data[12];
                            var totalAdditionFeeVND = data[13];
                            var totalAdditionFeeVNDstr = formatThousands(totalAdditionFeeVND, 0);
                            var TotalSensoredFeeVND = data[16];
                            var TotalSensoredFeeVNDstr = formatThousands(TotalSensoredFeeVND, 0);
                            if (status == 1) {
                                $("#<%=hdfListID.ClientID%>").val(listID);
                                var button = "<a href=\"javascript:;\" class=\"btn btn-close pill-btn primary-btn main-btn hover\" onclick=\"paytoexport($(this))\">Thanh toán</a>";
                                button += "             <a href=\"javascript:;\" class=\"btn btn-close pill-btn primary-btn main-btn hover\" onclick='ThanhToanTaiKho($(this))'>Thanh toán tại kho</a>";
                                var html = "";
                                html += "<div class=\"popup-row\">";
                                html += "   <p>Tổng số mã xuất kho của quý khách  : <strong>" + totalCount
                                    + "</strong></p>";
                                html += "</div>";
                                html += "<div class=\"popup-row\">";
                                html += "<p>Tổng số kg xuất kho: " + totalWeight
                                    + " kg. <strong>" + totalWeightPriceVNDstr + "</strong> VNĐ.</p>";
                                html += "</div>";
                                html += "<div class=\"popup-row\" style=\"display:none\">";
                                html += "<p>Phí hải quan xuất kho cố định: <strong>" + feeOutStockVNDstr + "</strong> VNĐ.</p>";
                                html += "</div>";
                                html += "<div class=\"popup-row\">";
                                html += "<p>Cước vật tư: <strong>" + totalAdditionFeeVNDstr + "</strong> VNĐ.</p>";
                                html += "</div>";
                                html += "<div class=\"popup-row\">";
                                html += "<p>PP hàng đặc biệt: <strong>" + TotalSensoredFeeVNDstr + "</strong> VNĐ.</p>";
                                html += "</div>";
                                html += "<div class=\"popup-row\">";
                                html += "<p>Tổng số tiền xuất kho của quý khách: <strong>" + totalPriceVNDstr + "</strong> VNĐ.</p>";
                                html += "</div>";
                                html += "<div class=\"popup-row\">";
                                html += "<span class=\"popuprow-left\">Hình thức vận chuyển trong nước:</span>";
                                var s = $("#<%=hdfListShippingVN.ClientID%>").val();
                                var lists = s.split('|');
                                if (lists.length - 1 > 0) {
                                    html += "<select class=\"form-control popuprow-right shippingtypevn\">";
                                    html += "   <option value=\"0\">---Chọn phương thức vận chuyển---</option>";
                                    for (var i = 0; i < lists.length - 1; i++) {
                                        var item = lists[i].split(':');
                                        var sID = item[0];
                                        var sName = item[1];
                                        html += "<option value=\"" + sID + "\">" + sName + "</option>";
                                    }
                                    html += "</select>";
                                }
                                html += "</div>";
                                html += "<div class=\"popup-row\">";
                                html += "<span class=\"popuprow-left\">Ghi chú:</span>";
                                html += "<input class=\"form-control requestnote popuprow-right\" placeholder=\"Ghi chú\"/>";
                                html += "</div>";
                                html += "<div class=\"popup-row\">";
                                html += "<span class=\"popuperror\" style=\"color:red\"></span>";
                                html += "</div>";
                                showPopup("Thanh toán xuất kho", html, button);
                            }
                            else {
                                $("#<%=hdfListID.ClientID%>").val(listID);
                                var leftmoney = data[11];
                                var leftmoneystr = formatThousands(leftmoney, 0);
                                var button = "";
                                button += "             <a href=\"javascript:;\" class=\"btn btn-close pill-btn primary-btn main-btn hover\" onclick='ThanhToanTaiKho($(this))'>Thanh toán tại kho</a>";
                                var html = "";
                                html += "<div class=\"popup-row\">";
                                html += "   <p>Tổng số mã xuất kho của quý khách  : <strong>" + totalCount
                                    + "</strong></p>";
                                html += "</div>";
                                html += "<div class=\"popup-row\">";
                                html += "<p>Tổng số kg xuất kho: " + totalWeight
                                    + " kg - <strong>" + totalWeightPriceVNDstr + "</strong> VNĐ.</p>";
                                html += "</div>";
                                html += "<div class=\"popup-row\" style=\"display:none\">";
                                html += "<p>Phí hải quan xuất kho cố định: <strong>" + feeOutStockVNDstr + "</strong> VNĐ.</p>";
                                html += "</div>";
                                html += "<div class=\"popup-row\">";
                                html += "<p>Cước vật tư: <strong>" + totalAdditionFeeVNDstr + "</strong> VNĐ.</p>";
                                html += "</div>";
                                html += "<div class=\"popup-row\">";
                                html += "<p>PP hàng đặc biệt: <strong>" + TotalSensoredFeeVNDstr + "</strong> VNĐ.</p>";
                                html += "</div>";
                                html += "<div class=\"popup-row\">";
                                html += "<p>Tổng số tiền xuất kho của quý khách: <strong>" + totalPriceVNDstr + "</strong> VNĐ.</p>";
                                html += "</div>";
                                html += "<div class=\"popup-row\">";
                                //html += "<p>Số tiền trong tài khoản của quý khách : <strong>" + walletstr + "</strong> VNĐ. Bạn còn thiếu <strong>" + strrechargeMore + "</strong> VNĐ.</p>";
                                html += "<p>Số tiền trong tài khoản của quý khách : <strong>" + walletstr + "</strong> VNĐ.</p>";
                                html += "</div>";
                                html += "<div class=\"popup-row\">";
                                html += "<p>Quý khách còn thiếu <strong>" + leftmoneystr + "</strong> VNĐ để xuất kho thành công.</p>";
                                html += "</div>";
                                html += "<div class=\"popup-row\">";
                                html += "<span class=\"popuprow-left\">Hình thức vận chuyển trong nước:</span>";
                                var s = $("#<%=hdfListShippingVN.ClientID%>").val();
                                var lists = s.split('|');
                                if (lists.length - 1 > 0) {
                                    html += "<select class=\"form-control popuprow-right shippingtypevn\">";
                                    html += "   <option value=\"0\">---Chọn phương thức vận chuyển---</option>";
                                    for (var i = 0; i < lists.length - 1; i++) {
                                        var item = lists[i].split(':');
                                        var sID = item[0];
                                        var sName = item[1];
                                        html += "<option value=\"" + sID + "\">" + sName + "</option>";
                                    }
                                    html += "</select>";
                                }
                                html += "</div>";
                                html += "<div class=\"popup-row\">";
                                html += "<span class=\"popuprow-left\">Ghi chú:</span>";
                                html += "<input class=\"form-control requestnote popuprow-right\" placeholder=\"Ghi chú\"/>";
                                html += "</div>";
                                html += "<div class=\"popup-row\">";
                                html += "<span class=\"popuperror\" style=\"color:red\">Để nạp thêm tiền vào tài khoản, vui lòng truy cập <a href=\"/chuyen-muc/huong-dan/nap-tien\" target=\"_blank\"><strong style=\"font-size:20px;\">TẠI ĐÂY</strong></a>.</span>";
                                html += "</div>";
                                showPopup("Thanh toán xuất kho", html, button);
                            }
                        }
                    }
                    else {
                        alert('Hiện tại không đơn thích hợp để yêu cầu xuất kho.');
                    }
                },
                error: function (xmlhttprequest, textstatus, errorthrow) {
                    //alert('lỗi checkend');
                }
            });
        }



        function rejectOrder(obj, id) {
            var c = confirm('Bạn muốn hủy yêu cầu này?');
            if (c) {
                var button = "<a href=\"javascript:;\" class=\"btn btn-close pill-btn primary-btn main-btn hover\" onclick=\"agreeCancel($(this),'" + id + "')\">Xác nhận</a>";
                var html = "";
                html += "<div class=\"popup-row\">";
                html += "   <span class=\"popuprow-left\">Lý do hủy đơn:</span>";
                html += "   <input class=\"form-control requestcancelnote popuprow-right\" placeholder=\"Lý do hủy đơn\"/>";
                html += "</div>";
                html += "<div class=\"popup-row\">";
                html += "   <span class=\"popuperror\" style=\"color:red\"></span>";
                html += "</div>";
                showPopup("Hủy đơn VCH", html, button);
            }
        }
        function agreeCancel(obj, id) {
            var note = $(".requestcancelnote").val();
            if (isEmpty(note) != true) {
                obj.removeAttr("onclick");
                $.ajax({
                    type: "POST",
                    url: "/danh-sach-kien-ky-gui-app.aspx/rejectOrder",
                    data: "{id:'" + id + "',cancelnote:'" + note + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var ret = msg.d;
                        if (ret == 1) {
                            close_popup_ms();
                            swal
                                (
                                {
                                    title: 'Thông báo',
                                    text: 'Hủy đơn hàng: ' + id + ' thành công',
                                    type: 'success'
                                },
                                function () { window.location.replace(window.location.href); }
                                );
                        }
                        else {
                            swal
                                (
                                {
                                    title: 'Thông báo',
                                    text: 'Có lỗi trong quá trình hủy đơn, vui lòng thử lại sau.',
                                    type: 'errpr'
                                },
                                function () { window.location.replace(window.location.href); }
                                );
                        }
                        $(".popuperror").html("");
                    },
                    error: function (xmlhttprequest, textstatus, errorthrow) {
                        //alert('lỗi checkend');
                    }
                });
            }
            else {
                $(".popuperror").html("Vui lòng nhập lý do hủy đơn.");
            }
        }
        function viewdetail(obj) {

        }

        function paytoexport(obj) {
            var shippingtype = parseFloat($(".shippingtypevn").val());
            var note = $(".requestnote").val();
            if (shippingtype > 0) {
                obj.removeAttr("onclick");
                $("#<%=hdfNote.ClientID%>").val(note);
                $("#<%=hdfShippingType.ClientID%>").val(shippingtype);
                $("#<%=btnPayExport.ClientID%>").click();
                $(".popuperror").html("");
            }
            else {
                $(".popuperror").html("Vui lòng chọn hình thức vận chuyển.");
            }
        }

        function ThanhToanTaiKho(obj) {
            var shippingtype = parseFloat($(".shippingtypevn").val());
            var note = $(".requestnote").val();
            if (shippingtype > 0) {
                obj.removeAttr("onclick");
                $("#<%=hdfNote.ClientID%>").val(note);
                $("#<%=hdfShippingType.ClientID%>").val(shippingtype);
                $("#<%=btnThanhToanTaiKho.ClientID%>").click();
                $(".popuperror").html("");
            }
            else {
                $(".popuperror").html("Vui lòng chọn hình thức vận chuyển.");
            }
        }



        //Khu vực popup
        function keyclose_ms(e) {
            if (e.keyCode == 27) {
                close_popup_ms();
            }
        }
        function close_popup_ms() {
            $("#pupip_home").animate({ "opacity": 0 }, 400);
            $("#bg_popup_home").animate({ "opacity": 0 }, 400);
            setTimeout(function () {
                $("#pupip_home").remove();
                $(".zoomContainer").remove();
                $("#bg_popup_home").remove();
                $('body').css('overflow', 'auto').attr('onkeydown', '');
                $('form').css('overflow', 'auto');
            }, 500);
        }
        function showPopup(title, content, button) {
            var obj = $('form');
            $(obj).css('overflow', 'hidden');
            $(obj).attr('onkeydown', 'keyclose_ms(event)');
            var bg = "<div id='bg_popup_home'></div>";
            var fr = "<div id='pupip_home' class=\"columns-container1\">" +
                "  <div class=\"center_column col-xs-12 col-sm-5\" id=\"popup_content_home\">";
            fr += "<div class=\"popup_header\">";
            fr += title;
            fr += "<a style='cursor:pointer;right:5px;' onclick='close_popup_ms()' class='close_message'></a>";
            fr += "</div>";
            fr += "     <div class=\"changeavatar\">";
            fr += "         <div class=\"content1\">";
            fr += content;
            fr += "         </div>";
            fr += "         <div class=\"content2\">";
            fr += "             <a href=\"javascript:;\" class=\"btn btn-close pill-btn primary-btn main-btn hover\" onclick='close_popup_ms()'>Đóng</a>";

            //fr += "             <a href=\"javascript:;\" class=\"btn btn-close\" onclick=\"addOrderShopCode('" + shopID + "', '" + MainOrderID + "')\">Thêm</a>";
            fr += button;
            fr += "         </div>";
            fr += "     </div>";
            fr += "<div class=\"popup_footer\">";
            //fr += "<span class=\"float-right\">" + email + "</span>";
            fr += "</div>";
            fr += "   </div>";
            fr += "</div>";
            $(bg).appendTo($(obj)).show().animate({ "opacity": 0.7 }, 800);
            $(fr).appendTo($(obj));
            setTimeout(function () {
                $('#pupip').show().animate({ "opacity": 1, "top": 20 + "%" }, 200);
                $("#bg_popup").attr("onclick", "close_popup_ms()");
            }, 1000);
            $('select').formSelect();
        }





    </script>

    <style>
        .vermid-tecenter {
            vertical-align: middle !important;
            text-align: center;
        }

        .popup-row {
            float: left;
            width: 100%;
            clear: both;
            /*margin: 10px 0;*/
        }

        .popuprow-left {
            float: left;
      
        }

        .popuprow-right {
            float: left;
           
        }

        .content1 {
            float: left;
            width: 100%;
        }

        .content2 {
            float: left;
            width: 100%;
            border-top: 1px solid #eee;
            clear: both;
            margin-top: 10px;
        }

        .ordercodes {
            width: 100%;
        }

        .ordercode {
            float: left;
            width: 100%;
            clear: both;
            margin-bottom: 10px;
        }

            .ordercode .item-element {
                float: left;
                width: 33%;
                padding: 0 10px;
            }

        .addordercode {
            padding: 0 10px;
            margin: 20px 0;
            background: url('/App_Themes/NewUI/images/icon-plus.png') center left no-repeat;
        }

            .addordercode a {
                padding-left: 30px;
            }

        .btn.btn-close {
            float: right;
            background: #29aae1;
            color: #fff;
            margin: 10px 5px;
            text-transform: none;
            /*padding: 0 20px;*/
            font-weight: bold;
            text-transform: uppercase;
        }

        #bg_popup_home {
            position: fixed;
            width: 100%;
            height: 100%;
            background-color: #333;
            opacity: 0.7;
            filter: alpha(opacity=70);
            left: 0px;
            top: 0px;
            z-index: 999999999;
            opacity: 0;
            filter: alpha(opacity=0);
        }

        #popup_ms_home {
            background: #fff;
            border-radius: 0px;
            box-shadow: 0px 2px 10px #fff;
            float: left;
            position: fixed;
            width: 735px;
            z-index: 10000;
            left: 50%;
            margin-left: -370px;
            top: 200px;
            opacity: 0;
            filter: alpha(opacity=0);
            height: 360px;
        }

            #popup_ms_home .popup_body {
                border-radius: 0px;
                float: left;
                position: relative;
                width: 735px;
            }

            #popup_ms_home .content {
                /*background-color: #487175;     border-radius: 10px;*/
                margin: 12px;
                padding: 15px;
                float: left;
            }

            #popup_ms_home .title_popup {
                /*background: url("../images/img_giaoduc1.png") no-repeat scroll -200px 0 rgba(0, 0, 0, 0);*/
                color: #ffffff;
                font-family: Arial;
                font-size: 24px;
                font-weight: bold;
                height: 35px;
                margin-left: 0;
                margin-top: -5px;
                padding-left: 40px;
                padding-top: 0;
                text-align: center;
            }

            #popup_ms_home .text_popup {
                color: #fff;
                font-size: 14px;
                margin-top: 20px;
                margin-bottom: 20px;
                line-height: 20px;
            }

                #popup_ms_home .text_popup a.quen_mk, #popup_ms_home .text_popup a.dangky {
                    color: #FFFFFF;
                    display: block;
                    float: left;
                    font-style: italic;
                    list-style: -moz-hangul outside none;
                    margin-bottom: 5px;
                    margin-left: 110px;
                    -webkit-transition-duration: 0.3s;
                    -moz-transition-duration: 0.3s;
                    transition-duration: 0.3s;
                }

                    #popup_ms_home .text_popup a.quen_mk:hover, #popup_ms_home .text_popup a.dangky:hover {
                        color: #8cd8fd;
                    }

            #popup_ms_home .close_popup {
                background: url("/App_Themes/Camthach/images/close_button.png") no-repeat scroll 0 0 rgba(0, 0, 0, 0);
                display: block;
                height: 28px;
                position: absolute;
                right: 0px;
                top: 5px;
                width: 26px;
                cursor: pointer;
                z-index: 10;
            }

        #popup_content_home {
             height: auto;
            position: fixed;
            background-color: #fff;
            top: 5%;
            z-index: 999999999;
            left: 3%;
            border-radius: 10px;
            -moz-border-radius: 10px;
            -webkit-border-radius: 10px;
            width: 95%;
            padding: 20px;
        }

        #popup_content_home {
            padding: 0;
        }

        .popup_header, .popup_footer {
            float: left;
            width: 100%;
            background: #29aae1;
            padding: 10px;
            position: relative;
            color: #fff;
        }

        .popup_header {
            font-weight: bold;
            font-size: 16px;
            text-transform: uppercase;
        }

        .close_message {
            top: 10px;
            background: url('/App_Themes/1688/images/close_button.png') no-repeat;
        }

        .changeavatar {
            padding: 10px;
            margin: 5px 0;
            float: left;
            width: 100%;
        }

        .spackage-row {
            float: left;
            width: 100%;
        }
    </style>

    <style>
        .pane-primary .heading {
            background-color: #366136 !important;
        }

        .btn.payment-btn {
            background-color: #3f8042;
            color: white;
            padding: 10px;
            height: 40px;
            width: 49%;
        }

        .btn.cancel-btn {
            background-color: #f84a13;
            color: white;
            padding: 10px;
            height: 40px;
            width: 49%;
        }

        .pagenavi {
            float: right;
            margin-top: 20px;
        }

            .pagenavi a,
            .pagenavi span {
                width: 30px;
                height: 35px;
                line-height: 40px;
                text-align: center;
                color: #959595;
                font-weight: bold;
                background: #f8f8f8;
                display: inline-block;
                font-weight: bold;
                margin-right: 1px;
            }

                .pagenavi .current,
                .pagenavi a:hover {
                    background: #ea1f28;
                    color: #fff;
                }

        .pagenavi {
            float: right;
            margin-top: 20px;
        }

            .pagenavi a,
            .pagenavi span {
                width: 30px;
                height: 35px;
                line-height: 40px;
                text-align: center;
                color: #959595;
                font-weight: bold;
                background: #f8f8f8;
                display: inline-block;
                font-weight: bold;
                margin-right: 1px;
            }

                .pagenavi .current,
                .pagenavi a:hover {
                    background: #ea1f28;
                    color: #fff;
                }

        .filters {
            background: #ebebeb;
            border: 1px solid #e1e1e1;
            font-weight: bold;
            padding: 20px;
            margin-bottom: 20px;
        }

            /*.page.orders-list .filters .lbl {
            padding-right: 50px;
        }*/

            .filters ul li {
                display: inline-block;
                text-align: center;
                padding-right: 2px;
            }

            .filters ul li {
                padding-right: 4px;
            }

        select.form-control {
            appearance: none;
            -webkit-appearance: none;
            -moz-appearance: none;
            -ms-appearance: none;
            -o-appearance: none;
            background: #fff url(/App_Themes/NHST/images/icon-select.png) no-repeat right 15px center;
            padding-right: 25px;
            padding-left: 15px;
            line-height: 22px;
        }
    </style>

    <script type="text/javascript">


        $(".btn_seemore").click(function () {
            if ($(this).parents().parents().children(".table_pay").css("display") == "none")
                $(this).parents().parents().children(".table_pay").css("display", "");
            else
                $(this).parents().parents().children(".table_pay").css("display", "none");
        });


        $('.navbar-toggle').on('click', function (e) {
            $(this).toggleClass('open');
            $('body').toggleClass('menuin');
        });
        $('.nav-overlay').on('click', this, function (e) {
            $('.navbar-toggle').trigger('click');
        });
        $('.dropdown').on('click', '.dropdown-toggle', function (e) {

            var $this = $(this);
            var parent = $this.parent('.dropdown');
            var submenu = parent.find('.sub-menu-wrap');
            parent.toggleClass('open').siblings().removeClass('open');
            e.stopPropagation();

            submenu.click(function (e) {
                e.stopPropagation();
            });


        });
        $('body,html').on('click', function () {

            if ($('.dropdown').hasClass('open')) {

                $('.dropdown').removeClass('open');
            }
        });
        $(document).on('click', '.block-toggle', function (e) {
            e.preventDefault();
            var target = $(this).attr('href');
            if (!target) return;
            $(target).slideToggle();
        });
    </script>

</asp:Content>
