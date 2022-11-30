<%@ Page Title="" Language="C#" MasterPageFile="~/ClientMasterNew.Master" AutoEventWireup="true" CodeBehind="thanh-toan-ho.aspx.cs" Inherits="NHST.thanh_toan_ho" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .row-item {
            float: left;
            width: 100%;
            clear: both;
            margin: 10px 0;
            padding: 0px 10%;
        }

        .row-left {
            float: left;
            width: 15%;
        }

        .row-right {
            float: left;
            width: 82%;
            margin-left: 20px;
        }

        .rowfull {
            float: left;
            margin-right: 20px;
            width: 100%;
            text-transform: uppercase;
            margin: 5px 0;
        }

        .left-rowfull {
            float: left;
            width: 20%;
            margin-right: 10px;
        }

        .right-rowfull {
            float: left;
            width: 75%;
        }

        .form-control {
            float: left;
            width: 100%;
        }

        textarea.form-control {
            min-height: 100px;
        }

        .text-align-center {
            text-align: center;
        }

        .addordercode {
            padding: 0 10px;
            margin: 20px 0;
            background: url(/App_Themes/NewUI/images/icon-plus.png) center left no-repeat;
        }

            .addordercode a {
                padding-left: 30px;
            }

        .float-right {
            float: right;
        }

        .border-ra-5px {
            border-radius: 8px;
            -moz-border-radius: 8px;
            -webkit-border-radius: 8px;
        }

        .width-custom {
            width: 850px;
            margin: 0 auto;
            float: none;
        }

        .border-top {
            border-top: solid 1px #333;
            padding-top: 10px;
        }

        .border-bottom {
            border-bottom: solid 1px #333;
            padding-bottom: 10px;
        }

        .itemaddmore {
            float: left;
            width: 100%;
            clear: both;
        }

        .border-ra-bg {
            border-radius: 8px;
            -moz-border-radius: 8px;
            -webkit-border-radius: 8px;
            border: solid 1px #ddd;
            background: #fdfdfd;
            padding: 10px;
            color:#000;
        }

        .lblstrongcolor {
            color: #ad0d12;
        }

        .custom-border-padding {
            border-top: solid 1px #ccc;
            padding-top: 30px;
        }

        .rowhalf {
            float: left;
            width: 45%;
        }

        .label-field {
            float: left;
            width: 35%;
            line-height:40px;
        }

        .textbox-field {
            float: left;
            width: 60%;
        }

        .rowpart {
            float: left;
            width: 10%;
        }

        .nomin-width {
            min-width: unset !important;
            width: 100%;
            padding: 0 !important;
        }

        .form-confirm-send {
            float: left;
            width: 100%;
            margin: 50px 0;
        }

        .table-panel-main table td {
            text-align: center;
        }

        .btn.pill-btn {
            margin-right: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="grid-row">
            <div class="grid-col" id="main-col-wrap">
                <div class="feat-row grid-row">
                    <div class="grid-col-50 grid-row-center">
                        <article class="pane-primary">
                            <div class="heading">
                                <h3 class="lb">Gửi yêu cầu thanh toán hộ</h3>
                            </div>
                            <div class="cont">
                                <div class="inner">
                                    <div class="form-row marbot2">
                                        <p class="red" style="color: red; font-weight: bold; font-size: 15px;">
                                            Lưu ý:
											<br />
                                            Yêu cầu không đúng thực tế, yêu cầu sẽ bị huỷ và hoàn tiền.
											<br />
                                            Yêu cầu sẽ tính theo tỉ giá lúc thực hiện giao dịch.
                                        </p>
                                    </div>
                                    <div class="form-row marbot1">
                                        Tên đăng nhập / Nickname
                                    </div>
                                    <div class="form-row marbot2">
                                        <strong class="lblstrongcolor">
                                            <asp:Literal ID="ltrIfn" runat="server"></asp:Literal></strong>
                                        
                                    </div>
                                    <div class="form-row marbot2">
                                        <div class="itemaddmore  border-bottom">
                                            <div class="itemyeuau row-item">
                                                <div class="rowhalf">
                                                    <div class="label-field">Giá tiền (Tệ):</div>
                                                    <div class="textbox-field">
                                                        <input class="txtDesc2 form-control border-ra-bg" oninput="sumtotalprice()" value="0" />
                                                    </div>
                                                </div>
                                                <div class="rowhalf">
                                                    <div class="label-field">Nội dung:</div>
                                                    <div class="textbox-field">
                                                        <input class="txtDesc1 form-control border-ra-bg" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-row marbot1">
                                        <a href="javascript:;" onclick="addMoreRequest()" class="btn btn-bg-submit" style="float:right">
                                            <i class="fa fa-plus"></i> Thêm hóa đơn thanh toán hộ</a>
                                    </div>
                                    <div class="form-row marbot1">
                                        Tổng tiền (Tệ)
                                    </div>
                                    <div class="form-row marbot2">
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control border-ra-bg" Skin="MetroTouch"
                                            ID="pAmount" NumberFormat-DecimalDigits="0" Value="0" Enabled="false"
                                            NumberFormat-GroupSizes="3" Width="100%">
                                        </telerik:RadNumericTextBox>
                                    </div>

                                    <div class="form-row marbot1">
                                        Tổng tiền (VNĐ)
                                    </div>
                                    <div class="form-row marbot2">
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control border-ra-bg" Skin="MetroTouch"
                                            ID="rVND" NumberFormat-DecimalDigits="0" Value="0"
                                            NumberFormat-GroupSizes="3" Width="100%" Enabled="false">
                                        </telerik:RadNumericTextBox>
                                    </div>

                                    <div class="form-row marbot1">
                                        Tỉ giá
                                    </div>
                                    <div class="form-row marbot2">
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control border-ra-bg" Skin="MetroTouch"
                                            ID="rTigia" NumberFormat-DecimalDigits="0" Value="0"
                                            NumberFormat-GroupSizes="3" Width="100%" Enabled="false">
                                        </telerik:RadNumericTextBox>
                                    </div>
                                    <div class="form-row marbot1">
                                        Ghi chú: 
                                    </div>
                                    <div class="form-row marbot2">
                                        <asp:TextBox ID="txtNote" runat="server" CssClass="form-control border-ra-bg" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <div class="form-row no-margin center-txt">
                                        <asp:Button ID="btnSend" runat="server" Text="GỬI" CssClass="submit-btn " OnClick="btnSend_Click" Style="display: none" />
                                        <a href="javascript:;" class="pill-btn btn order-btn main-btn hover submit-btn border-ra-5px" onclick="sendRequest()">Gửi yêu cầu</a>
                                    </div>
                                    <div class="form-row marbot2">
                                        <p class="red" style="color: red; font-weight: bold; font-size: 15px;">
                                            <img src="/App_Themes/VMT/images/TKNH.jpg" alt="" />
                                        </p>
                                        <p style="color: #000; font-weight: bold;">
                                            Chuyển khoản để lại nội dung: username - tổng số tệ.
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </article>
                    </div>
                </div>
                <div class="feat-row grid-row">
                    <div class="grid-col-50 grid-row-center">
                        <article class="pane-primary">
                            <div class="heading">
                                <h3 class="lb">Lịch sử yêu cầu</h3>
                            </div>
                            <div class="cont">
                                <div class="inner">
                                    <div class="form-row marbot2">
                                        <span style="display: block; clear: both; margin: 10px 0; color: red; width: 100%; text-align: center;">Nếu còn số dư trong tài khoản vui lòng bấm thanh toán để yêu cầu được xử lý nhanh hơn.
                                        </span>
                                    </div>
                                    <div class="form-row marbot2">
                                        <div class="table-rps table-responsive">
                                            <table class="customer-table mar-top1 full-width normal-table">
                                                <thead>
                                                    <tr>
                                                        <th>Ngày gửi</th>
                                                        <th>Tổng tiền (¥)</th>
                                                        <th>Tổng tiền (VNĐ)</th>
                                                        <th>Tỉ giá</th>
                                                        <th>Trạng thái</th>
                                                        <th></th>
                                                        <th></th>
                                                        <th></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Literal ID="ltr" runat="server" EnableViewState="false"></asp:Literal>
                                                </tbody>
                                            </table>
                                        </div>

                                        <div class="tbl-footer clear">
                                            <div class="pagenavi fl">
                                                <%this.DisplayHtmlStringPaging1();%>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </article>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hdfTradeID" runat="server" />
        <asp:HiddenField ID="hdflist" runat="server" />
        <asp:HiddenField ID="hdfAmount" runat="server" />
        <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Style="display: none" />
        <asp:Button ID="btnPayment" runat="server" OnClick="btnPayment_Click" Style="display: none" />
    </main>
    <script type="text/javascript">
        function deleteTrade(ID) {
            var r = confirm("Bạn muốn hủy yêu cầu này?");
            if (r == true) {
                $("#<%=hdfTradeID.ClientID%>").val(ID);
                $("#<%=btnCancel.ClientID%>").click();
            }
            else {

            }
        }
        function paymoney_old(ID) {
            var r = confirm("Bạn muốn thanh toán yêu cầu này?");
            if (r == true) {
                $("#<%=hdfTradeID.ClientID%>").val(ID);
                $("#<%=btnPayment.ClientID%>").click();
            }
            else {

            }

        }
        function paymoney(obj, ID) {
            var r = confirm("Bạn muốn thanh toán yêu cầu này?");
            if (r == true) {
                obj.removeAttr("onclick");
                $("#<%=hdfTradeID.ClientID%>").val(ID);
                $("#<%=btnPayment.ClientID%>").click();
            }
            else {

            }

        }
        function sendRequest() {
            var listyeucau = "";
            var total = 0;
            $(".itemyeuau").each(function () {
                var des1 = $(this).find(".txtDesc1").val();
                var des2 = $(this).find(".txtDesc2").val();
                listyeucau += des1 + "," + des2 + "|";
            });
            var amount = $("#<%= pAmount.ClientID %>").val();
            $("#<%= hdfAmount.ClientID %>").val(amount);
            $("#<%= hdflist.ClientID %>").val(listyeucau);
            $("#<%= btnSend.ClientID %>").click();
        }
        function addMoreRequest() {
            var html = "";
            html += "<div class=\"itemyeuau row-item custom-border-padding\">";
            html += "<div class=\"rowhalf \"> <div class=\"label-field\">Giá tiền (Tệ):</div><div class=\"textbox-field\"><input class=\"txtDesc2 form-control border-ra-bg\" oninput=\"sumtotalprice()\" value=\"0\"/></div></div>";
            html += "<div class=\"rowhalf\"> <div class=\"label-field\">Nội dung:</div><div class=\"textbox-field\"><input class=\"txtDesc1 form-control border-ra-bg\"/></div></div>";
            html += "<div class=\"rowpart\"> <a href=\"javascript:;\" class=\"btn btn-bg-close\" onclick=\"deleteitem($(this))\"><i class=\"fa fa-close\"></i></a></div>";
            html += "</div>";
            $(".itemaddmore").append(html);
        }
        function sumtotalprice() {
            var total = 0;
            var check = false;
            $(".txtDesc2").each(function () {
                if ($(this).val().indexOf(',') > -1) {
                    check = true;
                }
            });
            if (check == false) {
                $(".txtDesc2").each(function () {
                    var price = parseFloat($(this).val());
                    total += price;
                });
                $("#<%=pAmount.ClientID%>").val(total);
                returnTigia(total);
            }
            else {
                $(".txtDesc2").each(function () {
                    if ($(this).val().indexOf(',') > -1) {
                        $(this).val($(this).val().replace(',', ''));
                    }
                });
                alert('Vui lòng không nhập dấu phẩy vào giá tiền');

            }
        }
        function deleteitem(obj) {
            obj.parent().parent().remove();
            sumtotalprice();
        }
        function returnTigia(totalprice) {

            $.ajax({
                type: "POST",
                url: "/thanh-toan-ho.aspx/getCurrency",
                data: "{totalprice:'" + totalprice + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = msg.d;

                    if (data != "0") {
                        $("#<%=rTigia.ClientID%>").val(data);
                        var vnd = data * totalprice;
                        //var formne = formatThousands(vnd,'.');
                        var formne = numberWithCommas(vnd);
                        $("#<%=rVND.ClientID%>").val(formne);
                    }
                },
                error: function (xmlhttprequest, textstatus, errorthrow) {
                    //alert('lỗi checkend');
                }
            });
        }
        function numberWithCommas(x) {
            var parts = x.toString().split(".");
            parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
            return parts.join(".");
        }
        var formatThousands = function (n, dp) {
            var s = '' + (Math.floor(n)), d = n % 1, i = s.length, r = '';
            while ((i -= 3) > 0) { r = ',' + s.substr(i, 3) + r; }
            return s.substr(0, i + 3) + r +
              (d ? '.' + Math.round(d * Math.pow(10, dp || 2)) : '');
        };
    </script>
</asp:Content>
