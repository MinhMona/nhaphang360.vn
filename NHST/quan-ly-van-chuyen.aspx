<%@ Page Title="" Language="C#" MasterPageFile="~/1688MasterLogined.Master" AutoEventWireup="true" CodeBehind="quan-ly-van-chuyen.aspx.cs" Inherits="NHST.quan_ly_van_chuyen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main>
        <div id="primary" class="page index">
            <section id="firm-services" class="sec sec-padd-50">
                <div class="container">
                    <h3 class="sec-tit text-center">
                        <span class="sub">Quản lý ký gửi</span>
                    </h3>
                    <div class="primary-form">
                        <div class="main-content clear">
                            <div class="search-table">
                                <aside class="filters">
                                    <ul>
                                        <li class="lbl"><b class="black">Lọc tìm kiếm</b></li>
                                        <li>
                                            <asp:TextBox ID="txtNote" runat="server" CssClass="form-control"></asp:TextBox>
                                        </li>
                                        <li class="submit">
                                            <asp:Button ID="btnFilter" runat="server" CssClass="btn main-btn hover" Text="TÌM KIẾM" OnClick="btnFilter_Click" />
                                        </li>
                                    </ul>
                                </aside>

                                <div class="res-table">
                                    <table class="trans table">
                                        <tr>
                                            <th></th>
                                            <th>Mã vận đơn
                                            </th>
                                            <th>Trọng lượng
                                            </th>
                                            <th>Kho
                                            </th>
                                            <th>Ghi chú
                                            </th>
                                            <th>Tốc độ
                                            </th>
                                            <th>Trạng thái nhận hàng
                                            </th>
                                            <th>Trạng thái thanh toán
                                            </th>
                                        </tr>
                                        <asp:Literal ID="ltrorderlist" runat="server" EnableViewState="false"></asp:Literal>
                                    </table>
                                </div>

                                <div class="tbl-footer clear">
                                    <div class="subtotal fr">
                                        <asp:Literal ID="ltrTotal" runat="server"></asp:Literal>
                                    </div>
                                    <div class="pagenavi fl">
                                        <%this.DisplayHtmlStringPaging1();%>
                                    </div>
                                </div>
                                <!--tbl-footer-->
                            </div>
                            <!--search-table-->
                        </div>
                    </div>
                </div>
            </section>
        </div>
        <asp:HiddenField ID="hdfSenddate" runat="server" />
        <asp:HiddenField ID="hdfPrice" runat="server" />
        <asp:Button ID="btnPay" runat="server" OnClick="btnPay_Click" Style="display: none" />
        <asp:Button ID="btnAcceptreceive" runat="server" OnClick="btnAcceptreceive_Click" Style="display: none" />
    </main>
    <script type="text/javascript">
        function payallfordate(sendate, price) {
            var r = confirm("Bạn đồng ý thanh toán cho ngày: " + sendate);
            if (r == true) {
                $("#<%=hdfSenddate.ClientID%>").val(sendate);
                $("#<%=hdfPrice.ClientID%>").val(price);
                $("#<%=btnPay.ClientID%>").click();
            } else {

            }
        }
        function acceptreceived(sendate) {
            var r = confirm("Bạn đồng ý xác nhận đã nhận hàng cho ngày: " + sendate);
            if (r == true) {
                $("#<%=hdfSenddate.ClientID%>").val(sendate);

                $("#<%=btnAcceptreceive.ClientID%>").click();
            } else {

            }
        }
        function show_messageorder(content) {
            var obj = $('body');
            $(obj).css('overflow', 'hidden');
            $(obj).attr('onkeydown', 'keyclose_ms(event)');
            var bg = "<div id='bg_popup'></div>";
            var fr = "<div id='pupip' class=\"columns-container1\"><div class=\"container\" id=\"columns\"><div class='row'>" +
                     "  <div class=\"center_column col-xs-12 col-sm-5\" id=\"popup_content\"><a style='cursor:pointer;right:5px;' onclick='close_popup_ms()' class='close_message'></a>";
            fr += "     <div class=\"changeavatar\">";
            fr += "         <label class=\"lbl\" style=\"float:left;width:30%;\">Ghi chú: </label>";
            fr += "         <div class=\"content\" style=\"float:left;width:70%;text-align:left\">";
            fr += content;
            fr += "         </div>";
            fr += "     </div>";
            fr += "   </div>";

            fr += "</div></div></div>";
            $(bg).appendTo($(obj)).show().animate({ "opacity": 0.7 }, 800);
            $(fr).appendTo($(obj));
            setTimeout(function () {
                $('#pupip').show().animate({ "opacity": 1, "top": 20 + "%" }, 200);
                $("#bg_popup").attr("onclick", "close_popup_ms()");
            }, 1000);
        }
        function keyclose_ms(e) {
            if (e.keyCode == 27) {
                close_popup_ms();
            }
        }
        function close_popup_ms() {
            $("#pupip").animate({ "opacity": 0 }, 400);
            $("#bg_popup").animate({ "opacity": 0 }, 400);
            setTimeout(function () {
                $("#pupip").remove();
                $(".zoomContainer").remove();
                $("#bg_popup").remove();
                $('body').css('overflow', 'auto').attr('onkeydown', '');
            }, 500);
        }

        function viewnote(id) {
            $.ajax({
                type: "POST",
                url: "/quan-ly-van-chuyen.aspx/getNote",
                data: "{ID:'" + id + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = msg.d;
                    if (data != "0") {
                        show_messageorder(data);
                    }
                },
                error: function (xmlhttprequest, textstatus, errorthrow) {
                    alert('lỗi checkend');
                }
            });
        }
    </script>
    <style>
        .page .filters {
            background: #ebebeb;
            border: 1px solid #e1e1e1;
            font-weight: bold;
            padding: 20px;
            margin-bottom: 20px;
        }

        ul {
            margin: 0;
            padding: 0;
        }

            ul li {
                list-style: none;
            }

        .page .filters ul li {
            display: inline-block;
            text-align: center;
            padding-right: 25px;
            color: #2a363b;
        }

        .black {
            color: #2a363b;
        }

        input, select {
            border: 1px solid #e1e1e1;
            background: #fff;
            padding: 10px;
            height: 40px;
            line-height: 20px;
            color: #000;
            display: block;
            width: 100%;
            border-radius: 0;
        }

        .page .filters input {
            width: 265px;
        }
    </style>
</asp:Content>
