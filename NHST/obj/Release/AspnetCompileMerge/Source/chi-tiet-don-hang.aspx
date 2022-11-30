<%@ Page Title="" Language="C#" MasterPageFile="~/ClientMaster.Master" AutoEventWireup="true" CodeBehind="chi-tiet-don-hang.aspx.cs" Inherits="NHST.chi_tiet_don_hang" %>

<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<%@ Import Namespace="MB.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="sec order-detail-sec">
            <div class="all">
                <div class="main">
                    <div class="sec step-sec">
                        <div class="sec-tt">
                            <h2 class="tt-txt">Chi tiết đơn hàng</h2>
                            <p class="deco">
                                <img src="/App_Themes/NHST/images/title-deco.png" alt="">
                            </p>
                        </div>
                    </div>
                    <div class="order-panels mar-bot2 color-white">
                        <asp:Label ID="ltr_info" runat="server" Visible="false" CssClass="inforshow"></asp:Label>
                    </div>
                    <div class="order-panels mar-bot2">
                        <a href="javascript:;" onclick="printDiv()" class="btn pill-btn primary-btn admin-btn">In đơn hàng</a>
                    </div>
                    <div class="order-panels">
                        <asp:Literal ID="ltr_OrderFee_UserInfo" runat="server"></asp:Literal>
                    </div>
                    <div class="order-panels">
                        <div class="order-panel">
                            <div class="title">Đánh giá đơn hàng</div>
                            <ul class="list-comment">
                                <asp:Literal ID="ltr_comment" runat="server"></asp:Literal>
                                <%--<li class="item">
                                    <div class="item-left">
                                        <span class="avata circle">
                                            <img src="/App_Themes/NHST/images/user-icon.png" width="100%" />
                                        </span>
                                    </div>
                                    <div class="item-right">
                                        <strong class="item-username">Phương Nguyễn</strong>
                                        <span class="item-date">22/12/2015 13:55</span>
                                        <p class="item-comment">
                                            Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. 
                                        </p>
                                    </div>
                                </li>
                                <li class="item">
                                    <span class="item-left avata circle">
                                        <img src="/App_Themes/NHST/images/icon.png" width="100%" />
                                    </span>
                                    <div class="item-right">
                                        <strong class="item-username">Phương Nguyễn</strong>
                                        <span class="item-date">22/12/2015 13:55</span>
                                        <p class="item-comment">
                                            Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. 
                                        </p>
                                    </div>
                                </li>--%>
                            </ul>
                            <asp:Panel ID="pn_sendcomment" runat="server">
                                <div class="bottom comment-bottom">
                                    <div class="comment-input">
                                        <div class="comment-input-left">
                                            <asp:Literal ID="ltr_currentUserImg" runat="server"></asp:Literal>

                                        </div>
                                        <div class="comment-input-right">
                                            <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rq" runat="server" ControlToValidate="txtComment" ForeColor="Red" ErrorMessage="Không để trống nội dung."></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <asp:Button ID="btnSend" runat="server" Text="Gửi đánh giá" CssClass="btn pill-btn primary-btn" OnClick="btnSend_Click" />

                                </div>
                            </asp:Panel>
                        </div>
                        <asp:Literal ID="ltr_AddressReceive" runat="server"></asp:Literal>
                    </div>
                    <div class="order-panel print3">
                        <div class="title">Danh sách sản phẩm</div>
                        <div class="cont clear">
                            <div class="tbl-product-wrap">
                                <table class="tb-product">
                                    <tr>
                                        <th class="pro">Sản phẩm</th>
                                        <th class="pro">Thuộc tính</th>
                                        <th class="qty">Số lượng</th>
                                        <th class="price">Đơn giá (¥)</th>
                                        <th class="price">Đơn giá (VNĐ)</th>
                                        <th class="price">Ghi chú riêng sản phẩm</th>
                                        <th class="price">Trạng thái</th>
                                        <%--<th class="price">Phí bội chi</th>--%>
                                        <%--  <th class="price">Tổng tiền</th>
                                    <th class="price">Tiền đã cọc</th>
                                    <th class="date">Ngày đặt hàng</th>
                                    <th class="status">Trạng thái đơn hàng</th>--%>
                                    </tr>
                                    <asp:Literal ID="ltrProducts" runat="server"></asp:Literal>
                                    <%--<asp:Repeater ID="rpt" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="pro">
                                                <div class="thumb-product">
                                                    <div class="pd-img">
                                                        <a href="<%#Eval("link_origin") %>" target="_blank">
                                                            <img src="<%#Eval("image_origin") %>" alt="">
                                                        </a>
                                                    </div>
                                                    <div class="info">
                                                        <a href="<%#Eval("link_origin") %>" target="_blank"><%#Eval("brand") %>
                                                        </a>
                                                    </div>
                                                </div>
                                            </td>
                                             <td class="pro">
                                               <%#Eval("property") %>
                                            </td>
                                            <td class="qty"><%#Eval("quantity") %>
                                            </td>
                                            <td class="price">
                                                <p class="">

                                                    <%#string.Format("{0:N0}", Convert.ToDouble(Eval("price_origin"))*Convert.ToDouble(Eval("CurrentCNYVN"))) %> đ</p>
                                            </td>                                          
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>--%>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="order-panels">
                        <asp:Literal ID="ltrOrderFee" runat="server"></asp:Literal>
                    </div>
                    <div class="order-panel print5">
                        <div class="title">Lịch sử thanh toán </div>
                        <div class="cont">
                            <table class="tb-product">
                                <tr>
                                    <th class="pro">Ngày thanh toán</th>
                                    <th class="pro">Loại thanh toán</th>
                                    <th class="pro">Hình thức thanh toán</th>
                                    <th class="qty">Số tiền</th>
                                </tr>
                                <asp:Repeater ID="rptPayment" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="pro">
                                                <%#Eval("CreatedDate","{0:dd/MM/yyyy}") %>
                                            </td>
                                            <td class="pro">
                                                <%# PJUtils.ShowStatusPayHistory( Eval("Status").ToString().ToInt()) %>
                                            </td>
                                            <td class="pro">
                                                <%#Eval("Type").ToString() == "1"?"Trực tiếp":"Ví điện tử" %>
                                            </td>
                                            <td class="qty"><%#Eval("Amount","{0:N0}") %> VNĐ
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:Literal ID="ltrpa" runat="server"></asp:Literal>
                            </table>
                        </div>
                    </div>
                    <asp:Panel ID="pn" runat="server" Visible="false">
                        <div class="order-panels">
                            <a class="btn pill-btn primary-btn" href="javascript:;" onclick="cancelOrder()">Hủy đơn hàng</a>
                            <asp:Button ID="btn_cancel" runat="server" CssClass="btn pill-btn primary-btn" CausesValidation="false" Text="Hủy đơn hàng" Style="display: none;" OnClick="btn_cancel_Click" />
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnthanhtoan" runat="server" Visible="false">
                        <div class="order-panels">
                            <a class="btn pill-btn primary-btn" href="javascript:;" onclick="payallorder()">Thanh toán</a>
                            <asp:Button ID="btnPayAll" runat="server" CssClass="btn pill-btn primary-btn" CausesValidation="false" Text="Thanh toán" Style="display: none;" OnClick="btnPayAll_Click" />
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
        <div id="printcontent" class="printdetail" style="display: none;">
        </div>
    </main>
    <asp:Button ID="btnDeposit" runat="server" CssClass="btn pill-btn primary-btn" Style="display: none" CausesValidation="false" Text="Đặt cọc" OnClick="btnDeposit_Click" />
    <script type="text/javascript">
        function printDiv() {
            var html = "";

            $('link').each(function () { // find all <link tags that have
                if ($(this).attr('rel').indexOf('stylesheet') != -1) { // rel="stylesheet"
                    html += '<link rel="stylesheet" href="' + $(this).attr("href") + '" />';
                }
            });
            html += '<body onload="window.focus(); window.print()">' + $("#printcontent").html() + '</body>';
            var w = window.open("", "print");
            if (w) { w.document.write(html); w.document.close() }
        }
        $(document).ready(function () {
            $(".print1").clone().appendTo(".printdetail");
            $(".print2").clone().appendTo(".printdetail");
            $(".print3").clone().appendTo(".printdetail");
            $(".print4").clone().appendTo(".printdetail");
            $(".print5").clone().appendTo(".printdetail");
        });
        function payallorder() {
            var r = confirm("Bạn muốn thanh toán đơn hàng này?");
            if (r == true) {
                $("#<%= btnPayAll.ClientID%>").click();
            }
            else {
            }
        }
        function cancelOrder() {
            var r = confirm("Bạn muốn hủy đơn hàng này?");
            if (r == true) {
                $("#<%= btn_cancel.ClientID%>").click();
            }
            else {
            }
        }
        function depositOrder() {
            var r = confirm("Bạn muốn đặt cọc đơn hàng này?");
            if (r == true) {
                $("#<%= btnDeposit.ClientID%>").click();
            }
            else {
            }
        }
        function PrintDiv() {
            var contents = document.getElementById("dvContents").innerHTML;
            var frame1 = document.createElement('iframe');
            frame1.name = "frame1";
            frame1.style.position = "absolute";
            frame1.style.top = "-1000000px";
            document.body.appendChild(frame1);
            var frameDoc = frame1.contentWindow ? frame1.contentWindow : frame1.contentDocument.document ? frame1.contentDocument.document : frame1.contentDocument;
            frameDoc.document.open();
            frameDoc.document.write('<html><head><title>DIV Contents</title>');
            frameDoc.document.write('</head><body>');
            frameDoc.document.write(contents);
            frameDoc.document.write('</body></html>');
            frameDoc.document.close();
            setTimeout(function () {
                window.frames["frame1"].focus();
                window.frames["frame1"].print();
                document.body.removeChild(frame1);
            }, 500);
            return false;
        }
    </script>
</asp:Content>
