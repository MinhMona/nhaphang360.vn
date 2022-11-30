<%@ Page Language="C#" MasterPageFile="~/UserMasterNew.Master" AutoEventWireup="true" CodeBehind="gio-hang-thanh-toan-ho.aspx.cs" Inherits="NHST.gio_hang_thanh_toan_ho" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <style>
        .yellow-gold.darken-2 {
            background-color: #e87e04 !important;
        }

        .bronze.darken-2 {
            background: #e6cb78;
        }
    </style>
</asp:Content>
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
                                        <h4>Danh sách yêu cầu thanh toán hộ tạm</h4>
                                    </div>
                                </div>
                                <div class="col s12">
                                    <div class="order-list-info">
                                        <div class="total-info mb-2 pb-10">

                                            <div class="row section mt-1">
                                                <div class="col s12">

                                                    <div class="responsive-tb">
                                                        <table class="table   highlight bordered  centered bordered mt-2">
                                                            <thead>
                                                                <tr>
                                                                    <th class="center-checkbox">
                                                                        <%-- <label>
                                                                            <input type="checkbox" class="selected-all">
                                                                            <span></span>
                                                                        </label>--%>
                                                                    </th>
                                                                    <th>ID</th>
                                                                    <th>Số tệ</th>
                                                                    <th>Nội dung</th>
                                                                    <th>Tiền tạm tính (VNĐ)</th>
                                                                    <th class="tb-date">Ngày gửi</th>
                                                                    <th>Thao tác</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:Literal ID="ltr" runat="server" EnableViewState="false"></asp:Literal>
                                                            </tbody>
                                                        </table>
                                                    </div>

                                                    <div class="pagi-table float-right mt-2">
                                                        <%this.DisplayHtmlStringPaging1();%>
                                                    </div>
                                                    <div class="clearfix"></div>
                                                    <div class="result-select">
                                                        <div class="all">
                                                            <div class="fixed-noti">
                                                                <div class="hide-noti"></div>
                                                                <div class="noti-wrap">

                                                                    <div class="noti-item GiaoHang" style="display: none">
                                                                        <div class="info">
                                                                            <div class="noti-txt col">
                                                                                <p>
                                                                                    <span class="count"></span>
                                                                                    yêu cầu <span class="type"></span>
                                                                                </p>
                                                                            </div>
                                                                        </div>

                                                                        <div class="noti-action col">
                                                                            <a href="javascript:;" onclick="SubmitYC()" class="btn"><span
                                                                                class="type">Gửi</span> yêu cầu</a>
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
                        </div>

                    </div>

                </div>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hdfOrderID" runat="server" />
    <asp:HiddenField ID="hdfListOrderID" runat="server" />

    <script>
        $(document).ready(function () {
            var table = $('.order-list-info .table');
            $('tr[data-action="other"] td label').hide();
            var listCb = table.find('tbody tr td label input[type="checkbox"]');


            table.on('change', 'tbody input[type="checkbox"]', function (e) {
                var checkedList = table.find('tbody tr td label input[type="checkbox"]:checked');
                var type = $(this).parents('tr').attr('data-action');
                if (checkedList.length < 1) {
                    $('body').find('.result-select').removeClass('show');

                } else {
                    $('body').find('.result-select').addClass('show');
                }
                var listYC = table.find(
                    'tbody tr[data-action="GiaoHang"] td label input[type="checkbox"]:checked');
                var listCheckout = table.find(
                    'tbody tr[data-action="checkout"] td label input[type="checkbox"]:checked');
                $('.noti-item.GiaoHang').find('.count').text(listYC.length);
                $('.noti-item.checkout').find('.count').text(listCheckout.length);
                if (listYC.length < 1) {
                    $('.noti-item.GiaoHang').hide();
                } else {
                    $('.noti-item.GiaoHang').show();
                }
                if (listCheckout.length < 1) {
                    $('.noti-item.checkout').hide();
                } else {
                    var amount = 0;
                    listCheckout.each(function () {
                        amount += parseFloat($(this).attr('data-value'));
                    })
                    $(".totalpayselected").html(formatThousands(amount) + " VNĐ")
                    $('.noti-item.checkout').show();
                }
            })
            $('body').on('click', '.selected-all', function () {
                var checkStatus = this.checked;
                var listCb = table.find(
                    'tbody tr:not([data-action="other"]) td label input[type="checkbox"]');
                $(listCb).each(function () {
                    $(this).prop('checked', checkStatus);
                    $(this).trigger('change');
                });
            })

            $('.collapase_header .collapse').on('click', function () {
                $(this).toggleClass('active');
                $(this).parent().next().slideToggle();

            });

        });

        function SubmitYC() {
            var table = $('.order-list-info .table');
            var listYC = table.find(
                'tbody tr[data-action="GiaoHang"] td label input[type="checkbox"]:checked');
            if (listYC.length >= 1) {
                var list = "";
                listYC.each(function () {
                    //var id = $(this).attr('data-id');
                    list += $(this).attr("data-id") + "|";
                })
                window.location.assign("/gui-yeu-cau-thanh-toan-ho/all_" + list + "");
            }
        }

        function isEmpty(str) {
            return !str.replace(/^\s+/g, '').length; // boolean (`true` if field is empty)
        }

        var formatThousands = function (n, dp) {
            var s = '' + (Math.floor(n)), d = n % 1, i = s.length, r = '';
            while ((i -= 3) > 0) { r = ',' + s.substr(i, 3) + r; }
            return s.substr(0, i + 3) + r + (d ? '.' + Math.round(d * Math.pow(10, dp || 2)) : '');
        };

     <%--   function depositOrder(orderID) {
            var c = confirm('Bạn muốn đặt cọc đơn: ' + orderID);
            if (c == true) {
                $("#<%=hdfOrderID.ClientID%>").val(orderID);
                $("#<%=btnDeposit.ClientID%>").click();
            }
        }

        function payallorder(orderID) {
            var r = confirm("Bạn muốn thanh toán đơn hàng này?");
            if (r == true) {
                $("#<%=hdfOrderID.ClientID%>").val(orderID);
                $("#<%= btnPay.ClientID%>").click();
            }
            else {
            }
        }--%>


        function CheckYCG(ID) {
            $.ajax({
                type: "POST",
                url: "/danh-sach-don-hang.aspx/CheckYCGAll",
                data: "{MainOrderID:'" + ID + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = JSON.parse(msg.d);
                    if (data.length > 0) {
                        var totalPay = 0;
                        for (var i = 0; i < data.length; i++) {
                            totalPay += data[i].TotalPricePay;
                        }
                        $('.noti-item.GiaoHang').find('.count').text(data.length);
                        $('body').find('.result-select').addClass('show');
                        $('.noti-item.GiaoHang').show();
                    }
                    else {
                        var count = $('.noti-item.deposit').find('.count').text();
                        var count1 = $('.noti-item.checkout').find('.count').text();
                        $('.noti-item.GiaoHang').find('.count').text("0");
                        $(".totalpayselected").html("0 VNĐ")
                        if (count == 0 && count1 == 0) {
                            $('body').find('.result-select').removeClass('show');
                        }
                        $('.noti-item.checkout').hide();
                    }
                },
                error: function (xmlhttprequest, textstatus, errorthrow) {
                    //alert(errorthrow);
                }
            });
        }


    </script>
</asp:Content>
