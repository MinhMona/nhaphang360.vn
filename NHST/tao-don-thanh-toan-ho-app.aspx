<%@ Page Title="Tạo đơn thanh toán hộ app" Language="C#" MasterPageFile="~/App.Master" AutoEventWireup="true" CodeBehind="tao-don-thanh-toan-ho-app.aspx.cs" Inherits="NHST.tao_don_thanh_toan_ho_app" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="/App_Themes/App/css/style-NA.css" media="all">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">

        <div class="page-wrap">
            <asp:Panel ID="pnMobile" runat="server" Visible="false">
                <div class="page-body">
                    <div class="all donhang">
                        <h2 class="title_page">YÊU CẦU THANH TOÁN HỘ</h2>
                        <div class="content_page">
                            <p class="title_onpage">GỬI YÊU CẦU THANH TOÁN HỘ</p>
                            <p class="note">
                                Lưu ý:<br>
                                Yêu cầu không đúng thực tế, yêu cầu sẽ bị hủy và hoàn tiền.
                            <br>
                                Yêu cầu sẽ tính theo tỉ giá lúc thực hiện giao dịch.
                            </p>
                            <p class="user">
                                <a class="item-user"><i class="fa fa-user"></i></a>
                                <asp:Literal ID="ltrIfn" runat="server"></asp:Literal>
                            </p>
                            <div class="content_create_order">
                                <div class="itemaddmore">
                                    <div class="top_pay itemyeuau">
                                        <ul>
                                            <li class="right_item">
                                                <input class="input_control txtDesc2" type="number" oninput="sumtotalprice()" step="0.01" min="0" value="0">
                                            </li>
                                            <li class="right_item">
                                                <input class="input_control txtDesc1" type="text" placeholder="Nội dung">
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="group_btn">
                                    <p class="btn_new">
                                        <a class="btn_addnew" onclick="addMoreRequest()"><i class="icon_addnew fa fa-plus"></i>THÊM HÓA ĐƠN THANH TOÁN HỘ</a>
                                    </p>
                                </div>
                                <div class="bottom_order">
                                    <ul>
                                        <li>Tổng tiền (Tệ):</li>
                                        <li>
                                            <telerik:RadNumericTextBox runat="server" CssClass="input_control" Skin="MetroTouch"
                                                ID="pAmount" NumberFormat-DecimalDigits="0" Value="0" Enabled="false"
                                                NumberFormat-GroupSizes="3" Width="100%">
                                            </telerik:RadNumericTextBox>
                                        </li>
                                    </ul>
                                    <ul>
                                        <li>Tổng tiền (VNĐ):</li>
                                        <li>
                                            <telerik:RadNumericTextBox runat="server" CssClass="input_control" Skin="MetroTouch"
                                                ID="rVND" NumberFormat-DecimalDigits="0" Value="0"
                                                NumberFormat-GroupSizes="3" Width="100%" Enabled="false">
                                            </telerik:RadNumericTextBox>
                                        </li>
                                    </ul>
                                    <ul>
                                        <li>Tỉ giá:</li>
                                        <li>
                                            <telerik:RadNumericTextBox runat="server" CssClass="input_control" Skin="MetroTouch"
                                                ID="rTigia" NumberFormat-DecimalDigits="0" Value="0"
                                                NumberFormat-GroupSizes="3" Width="100%" Enabled="false">
                                            </telerik:RadNumericTextBox>
                                        </li>
                                    </ul>
                                    <ul>
                                        <li>Ghi chú: </li>
                                        <li>
                                            <asp:TextBox ID="txtNote" runat="server" CssClass="input_control message" TextMode="MultiLine"></asp:TextBox>
                                        </li>
                                    </ul>
                                </div>
                                <p class="btn_order">
                                    <asp:Button ID="btnSend" runat="server" Text="GỬI" CssClass="submit-btn " OnClick="btnSend_Click" Style="display: none" />
                                    <%--<a href="javascript:;" class="btn_ordersp" onclick="sendRequest()">Gửi yêu cầu</a>--%>
                                    <a href="javascript:;" onclick="sendRequest($(this))" class="btn_ordersp">Gửi yêu cầu</a>
                                </p>
                            </div>
                            <p style="color: #000; font-weight: bold; padding: 10px 20px 10px 10px;">
                                Chuyển khoản để lại nội dung: username - tổng số tệ.
                            </p>
                            <%--   <img class="img_tk" src="images/tk.png">--%>
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





        <asp:HiddenField ID="hdfTradeID" runat="server" />
        <asp:HiddenField ID="hdflist" runat="server" />
        <asp:HiddenField ID="hdfAmount" runat="server" />
    </main>
    <script type="text/javascript">


        function sendRequest(obj) {
            var r = confirm("Xác nhận gửi yêu cầu!");
            if (r == true) {
                obj.removeAttr("onclick");
                var listyeucau = "";
                var total = 0;
                $(".itemyeuau").each(function () {
                    var des1 = $(this).find(".txtDesc1").val();
                    var des2 = $(this).find(".txtDesc2").val();
                    listyeucau += des1 + "*" + des2 + "|";
                });
                var amount = $("#<%= pAmount.ClientID %>").val();
                $("#<%= hdfAmount.ClientID %>").val(amount);
                $("#<%= hdflist.ClientID %>").val(listyeucau);
                $("#<%= btnSend.ClientID %>").click();
            }
        }
        function addMoreRequest() {
            var html = "";

            html += " <div class=\"top_pay itemyeuau\">";
            html += "   <ul>";
            html += "    <li class=\"right_item\">";
            html += "<input class=\"input_control txtDesc2\" type=\"number\" value=\"0\" name=\"\" oninput=\"sumtotalprice()\" step=\"0.01\" min=\"0\">";
            html += "       </li>";
            html += "    <li class=\"right_item\">";
            html += "    <input class=\"input_control txtDesc1\" type=\"text\" name=\"\" placeholder=\"Nội dung\">";
            html += "      </li>";
            html += "   <div class=\"button_delete\"><a onclick=\"deleteitem($(this))\">";
            html += "        <img src=\"/App_Themes/App/images/icon-remove-red.png\"></a></div>";
            html += "   </ul>";
            html += "   </div>";
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
            obj.parent().parent().parent().remove();
            sumtotalprice();
        }
        function returnTigia(totalprice) {

            $.ajax({
                type: "POST",
                url: "/tao-don-thanh-toan-ho-app.aspx/getCurrency",
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
