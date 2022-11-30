<%@ Page Language="C#" MasterPageFile="~/UserMasterNew.Master" AutoEventWireup="true" CodeBehind="gui-yeu-cau-thanh-toan-ho.aspx.cs" Inherits="NHST.gui_yeu_cau_thanh_toan_ho" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="main">
        <div class="row">
            <div class="content-wrapper-before blue-grey lighten-5"></div>
            <div class="col s12">
                <div class="container">
                    <div class="all">
                        <div class="card-panel mt-3 no-shadow">
                            <div class="row">
                                <div class="col s12">
                                    <div class="page-title mt-2 center-align">
                                        <h4>GỞI YÊU CẦU THANH TOÁN HỘ</h4>
                                    </div>
                                </div>
                                <div class="col s12 mt-2">
                                    <div class="order-list-info">
                                        <div class="total-info mb-2 create-product">
                                            <div class="row section">
                                                <div class="col s12">
                                                    <div class="order-row">
                                                        <div class="left-fixed">
                                                            <p class="txt">Username:</p>
                                                        </div>
                                                        <div class="right-content">
                                                            <div class="row">
                                                                <div class="input-field col s12">
                                                                    <asp:TextBox runat="server" ID="txtUsername" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="order-row top-justify">
                                                        <div class="left-fixed">
                                                            <p class="txt">Hóa đơn thanh toán hộ:</p>
                                                        </div>
                                                        <div class="right-content">

                                                            <div class="list-order">
                                                                <asp:Literal runat="server" ID="ltrTTH"></asp:Literal>
                                                                <div class="row button-wrap mb-2">
                                                                    <div class="col s12">
                                                                        <a href="javascript:;" class="btn add-order valign-wrapper left" style="display: flex;"><i class="material-icons">add</i><span>Thêm yêu cầu</span></a>
                                                                    </div>
                                                                </div>

                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="order-row">
                                                        <div class="left-fixed">
                                                            <p class="txt">Tổng tiền Tệ (¥):</p>
                                                        </div>
                                                        <div class="right-content">
                                                            <div class="row">
                                                                <div class="input-field col s12">
                                                                    <telerik:RadNumericTextBox runat="server" Skin="MetroTouch"
                                                                        ID="pAmount" NumberFormat-DecimalDigits="0" Value="0" Enabled="false"
                                                                        NumberFormat-GroupSizes="3" Width="100%">
                                                                    </telerik:RadNumericTextBox>
                                                                    <label class="active">Tệ (¥)</label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="order-row">
                                                        <div class="left-fixed">
                                                            <p class="txt">Tổng tiền (VNĐ):</p>
                                                        </div>
                                                        <div class="right-content">
                                                            <div class="row">
                                                                <div class="input-field col s12">
                                                                    <telerik:RadNumericTextBox runat="server" Skin="MetroTouch"
                                                                        ID="rVND" NumberFormat-DecimalDigits="0" Value="0"
                                                                        NumberFormat-GroupSizes="3" Width="100%" Enabled="false">
                                                                    </telerik:RadNumericTextBox>
                                                                    <label class="active">Việt Nam Đồng (VNĐ)</label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="order-row">
                                                        <div class="left-fixed">
                                                            <p class="txt">Tỉ giá:</p>
                                                        </div>
                                                        <div class="right-content">
                                                            <div class="row">
                                                                <div class="input-field col s12">
                                                                    <telerik:RadNumericTextBox runat="server" Skin="MetroTouch"
                                                                        ID="rTigia" NumberFormat-DecimalDigits="0" Value="0"
                                                                        NumberFormat-GroupSizes="3" Width="100%" Enabled="false">
                                                                    </telerik:RadNumericTextBox>
                                                                    <label class="active">Tỉ giá</label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="order-row">
                                                        <div class="left-fixed">
                                                            <p class="txt">Ghi chú:</p>
                                                        </div>
                                                        <div class="right-content">
                                                            <div class="row">
                                                                <div class="input-field col s12">
                                                                    <asp:TextBox ID="txtNote" runat="server" CssClass="materialize-textarea" TextMode="MultiLine"></asp:TextBox>
                                                                    <label for="textarea2">Nội dung</label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="float-right mt-2">
                                                        <asp:Button ID="btnSend" runat="server" Text="GỬI" CssClass="submit-btn " OnClick="btnSend_Click" Style="display: none" />
                                                        <a href="javascript:;" onclick="sendRequest()" class="btn create-order">Gởi yêu cầu</a>
                                                    </div>
                                                    <div class="clearfix"></div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>

                    </div>

                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdflist" runat="server" />
    <asp:HiddenField ID="hdfAmount" runat="server" />
    <script>
        $(document).ready(function () {
            $('.add-order').on('click', function () {
                var listOrder = $('.right-content .list-order .button-wrap');
                var html = ` <div class="row order-wrap slide-up itemyeuau" data-id="0">
                        <div class="input-field col s12 m6">
                           <input type="text" class="txtDesc2" oninput="sumtotalprice()" value="0">
                           <label class="active">Giá tiền (tệ)</label>
                        </div>
                        <div class="input-field col s12 m6">
                           <input type="text" class="txtDesc1">
                           <label>Nội dung</label>
                        </div>                       
                        <a href='javascript:;' class="remove-order tooltipped" data-position="top" data-tooltip="Xóa"><i class="material-icons">remove_circle</i></a>                       
                        
                     </div>   `;
                listOrder.before(html);

                $('.tooltipped')
                    .tooltip({
                        trigger: 'manual'
                    })
                    .tooltip('show');
            });
            $('.list-order').on('click', '.remove-order', function () {
                if ($(this).parents('.list-order').find('.order-wrap').length > 1) {
                    $(this).parent().fadeOut(function () {
                        $(this).remove();
                        sumtotalprice();
                    });
                }

            });
        });

        function sendRequest() {
            var listyeucau = "";
            var total = 0;
            debugger;
            $(".itemyeuau").each(function () {
                var id = $(this).attr("data-id");
                var des1 = $(this).find(".txtDesc1").val();
                var des2 = $(this).find(".txtDesc2").val();
                listyeucau += id + "," + des1 + "," + des2 + "|";
            });
            var amount = $("#<%= pAmount.ClientID %>").val();
            $("#<%= hdfAmount.ClientID %>").val(amount);
            $("#<%= hdflist.ClientID %>").val(listyeucau);
            $("#<%= btnSend.ClientID %>").click();
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

        function returnTigia(totalprice) {

            $.ajax({
                type: "POST",
                url: "/gui-yeu-cau-thanh-toan-ho.aspx/getCurrency",
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
