<%@ Page Title="" Language="C#" MasterPageFile="~/vantaihoakieuMaster.Master" AutoEventWireup="true" CodeBehind="Default3.aspx.cs" Inherits="NHST.Default19" %>
<%@ Register Src="~/UC/uc_Banner.ascx" TagName="Banner" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        
        <uc:Banner ID="Banner1" runat="server" />
        <div class="find-product wow zoomIn" style="background-image: url(/App_Themes/vantaihoakieu/images/searchbg.jpg)">
            <div class="all title-left sec">
                <div class="find-prd-wrap wow zoomIn">
                    <div class="title clear">
                        <h3 class="hd">Tìm kiếm sản phẩm</h3>
                        <p class="ct" style="display:none">
                            You have finished building your own website. You have introduced your company and
                        presented your products and services. You have added...
                        </p>
                    </div>
                    <div class="search-form">
                        <div class="select-form">
                            <select class="fcontrol" id="brand-source">
                                <option value="taobao" data-image="/App_Themes/vantaihoakieu/images/hdsearch-item-taobao.png">TAOBAO</option>
                                <option value="tmall" data-image="/App_Themes/vantaihoakieu/images/hdsearch-item-tmall.png">Tmall</option>
                                <option value="1688" data-image="/App_Themes/vantaihoakieu/images/hdsearch-item-1688.png">1688</option>
                            </select>
                        </div>
                        <div class="input-form">
                            <%--<input type="text" class="fcontrol" placeholder="Nhập link sản phẩm">--%>
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="fcontrol txtsearchfield" placeholder="Tìm kiếm sản phẩm"></asp:TextBox>
                        </div>
                        <%--<a href="#" class="submit-form">Tìm kiếm</a>--%>
                        <a href="javascript:;" onclick="searchProduct()" class="submit-form" style="line-height:25px;height:40px;">Tìm kiếm</a>
                        <asp:Button ID="btnSearch" runat="server"
                            OnClick="btnSearch_Click" Style="display: none"
                            OnClientClick="document.forms[0].target = '_blank';" UseSubmitBehavior="false" />
                    </div>
                </div>
                <div class="find-prd-right">
                    <div class="numver-experience">
                        <div class="number">
                            06 <div class="text-experience"> NĂM <br> KINH NGHIỆM </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="services-index sec" style="background-image: url(/App_Themes/vantaihoakieu/images/bg-services.jpg)">
            <div class="plane-img">
                <div class="img">
                    <img id="air-plane" src="/App_Themes/vantaihoakieu/images/plane.png" alt="">
                </div>
            </div>
            <div class="all">
                <div class="services-index-left">
                    <div class="gate-plane-top"></div>
                    <div class="gate-plane-left"></div>
                    <div class="gate-plane-right"></div>
                </div>
                <div class="services-index-right">
                    <div class="title clear">
                        <h3 class="hd">Dịch vụ</h3>
                    </div>
                    <ul class="list-right clear wow fadeInUp">
                        <asp:Literal ID="ltrService" runat="server"></asp:Literal>
                       <%-- <li>
                            <div class="item-right-customer clear">
                                <div class="img">
                                    <a href="#">
                                        <img src="/App_Themes/vantaihoakieu/images/icon-services.png">
                                    </a>
                                </div>
                                <div class="info">
                                    <a class="title">Chuyển tiền, nạp tiền Alipay, Wechat</a>
                                    <p>In today’s net-savvy world it has become common for any...</p>
                                </div>
                                <div class="btn-seemore">
                                    <a href="#">Xem thêm <span class="icon"><i class="fa fa-arrow-circle-right"></i></span>
                                    </a>
                                </div>
                            </div>
                        </li>
                        <li>
                            <div class="item-right-customer clear">
                                <div class="img">
                                    <a href="#">
                                        <img src="/App_Themes/vantaihoakieu/images/icon-services.png">
                                    </a>
                                </div>
                                <div class="info">
                                    <a class="title">Chuyển tiền, nạp tiền Alipay, Wechat</a>
                                    <p>In today’s net-savvy world it has become common for any... </p>
                                </div>
                                <div class="btn-seemore">
                                    <a href="#">Xem thêm <span class="icon"><i class="fa fa-arrow-circle-right"></i></span>
                                    </a>
                                </div>
                            </div>
                        </li>
                        <li>
                            <div class="item-right-customer clear">
                                <div class="img">
                                    <a href="#">
                                        <img src="/App_Themes/vantaihoakieu/images/icon-services.png">
                                    </a>
                                </div>
                                <div class="info">
                                    <a class="title">Chuyển tiền, nạp tiền Alipay, Wechat</a>
                                    <p>In today’s net-savvy world it has become common for any...</p>
                                </div>
                                <div class="btn-seemore">
                                    <a href="#">Xem thêm <span class="icon"><i class="fa fa-arrow-circle-right"></i></span>
                                    </a>
                                </div>
                            </div>
                        </li>
                        <li>
                            <div class="item-right-customer clear">
                                <div class="img">
                                    <a href="#">
                                        <img src="/App_Themes/vantaihoakieu/images/icon-services.png">
                                    </a>
                                </div>
                                <div class="info">
                                    <a class="title">Chuyển tiền, nạp tiền Alipay, Wechat</a>
                                    <p>In today’s net-savvy world it has become common for any... </p>
                                </div>
                                <div class="btn-seemore">
                                    <a href="#">Xem thêm <span class="icon"><i class="fa fa-arrow-circle-right"></i></span>
                                    </a>
                                </div>
                            </div>
                        </li>
                        <li>
                            <div class="item-right-customer clear">
                                <div class="img">
                                    <a href="#">
                                        <img src="/App_Themes/vantaihoakieu/images/icon-services.png">
                                    </a>
                                </div>
                                <div class="info">
                                    <a class="title">Chuyển tiền, nạp tiền Alipay, Wechat</a>
                                    <p>In today’s net-savvy world it has become common for any... </p>
                                </div>
                                <div class="btn-seemore">
                                    <a href="#">Xem thêm <span class="icon"><i class="fa fa-arrow-circle-right"></i></span>
                                    </a>
                                </div>
                            </div>
                        </li>
                        <li>
                            <div class="item-right-customer clear">
                                <div class="img">
                                    <a href="#">
                                        <img src="/App_Themes/vantaihoakieu/images/icon-services.png">
                                    </a>
                                </div>
                                <div class="info">
                                    <a class="title">Chuyển tiền, nạp tiền Alipay, Wechat</a>
                                    <p>In today’s net-savvy world it has become common for any... </p>
                                </div>
                                <div class="btn-seemore">
                                    <a href="#">Xem thêm <span class="icon"><i class="fa fa-arrow-circle-right"></i></span>
                                    </a>
                                </div>
                            </div>
                        </li>--%>
                    </ul>
                </div>
            </div>
        </div>
        <div class="step-register sec" style="background-image: url(/App_Themes/vantaihoakieu/images/home.jpg)">
            <div class="title clear center-txt bolt">
                <h3 class="hd">Hướng dẫn đặt hàng</h3>
            </div>
            <div class="sec-align tab_menu  wow slideInDown">
                <div class="customer_right">
                    <div class="all">
                        <div class="menu-intruction clear">
                            <ul class="nav nav-tabs ">
                                <asp:Literal ID="ltrStep1" runat="server"></asp:Literal>
                                <%--<li class="active">
                                    <a class="name_btn" data-toggle="tab" href="#menu1">
                                        <span class="icon">
                                            <img src="/App_Themes/vantaihoakieu/images/icon-step2.png">
                                        </span>
                                        Đăng ký tài khoản</a>
                                </li>
                                <li><a class="name_btn" data-toggle="tab" href="#menu2">
                                    <span class="icon">
                                        <img src="/App_Themes/vantaihoakieu/images/icon-step2.png">
                                    </span>
                                    Cài đặt công cụ</a></li>
                                <li><a class="name_btn" data-toggle="tab" href="#menu3">
                                    <span class="icon">
                                        <img src="/App_Themes/vantaihoakieu/images/icon-step3.png">
                                    </span>
                                    Thêm vào giỏ</a></li>
                                <li><a class="name_btn" data-toggle="tab" href="#menu4">
                                    <span class="icon">
                                        <img src="/App_Themes/vantaihoakieu/images/icon-step4.png">
                                    </span>
                                    Gửi đơn hàng</a></li>
                                <li><a class="name_btn" data-toggle="tab" href="#menu5">
                                    <span class="icon">
                                        <img src="/App_Themes/vantaihoakieu/images/icon-step5.png">
                                    </span>
                                    Đặt cọc tiền hàng</a></li>
                                <li><a class="name_btn" data-toggle="tab" href="#menu6">
                                    <span class="icon">
                                        <img src="/App_Themes/vantaihoakieu/images/icon-step6.png">
                                    </span>
                                    Nhận & thanh toán</a></li>--%>
                            </ul>
                        </div>
                        <div class="tab-content">
                            <asp:Literal ID="ltrStep2" runat="server"></asp:Literal>
                            <%--<div id="menu1" class="tab-pane fade in active">
                                <div class="sec-align">
                                    <div class="nav-tabswap-ct">
                                        <div class="guide">
                                            <div class="step-guide-ct">
                                                <div class="ct">
                                                    <h4 class="hd">Đăng ký tài khoản</h4>
                                                    <p>
                                                        Sometimes this is the irony of promoting your businesses products and
                                                    services, because for a fact that you want to make your business
                                                    recognizable and earn more sales of course you need to come up with
                                                    attractive prints that will make you stand out. And since you want
                                                    to be
                                                    competitive you
                                                    want to come up with a material that will make you stand out and
                                                    recognizable. We all know that
                                                    </p>
                                                    <div class="btn-seemore">
                                                        <a href="#">Xem thêm</span>
                                                    </a>
                                                    </div>
                                                </div>
                                                <div class="img">
                                                    <img src="/App_Themes/vantaihoakieu/images/iMac.png" alt="">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="menu2" class="tab-pane fade">
                                <div class="sec-align">
                                    <div class="nav-tabswap-ct">
                                        <div class="guide">
                                            <div class="step-guide-ct">
                                                <div class="ct">
                                                    <h4 class="hd">Đăng ký tài khoản</h4>
                                                    <p>
                                                        Sometimes this is the irony of promoting your businesses products and
                                                    services, because for a fact that you want to make your business
                                                    recognizable and earn more sales of course you need to come up with
                                                    attractive prints that will make you stand out. And since you want
                                                    to be
                                                    competitive you
                                                    want to come up with a material that will make you stand out and
                                                    recognizable. We all know that
                                                    </p>
                                                    <div class="btn-seemore">
                                                        <a href="#">Xem thêm</span>
                                                    </a>
                                                    </div>
                                                </div>
                                                <div class="img">
                                                    <img src="/App_Themes/vantaihoakieu/images/iMac.png" alt="">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="menu3" class="tab-pane fade">
                                <div class="sec-align">
                                    <div class="nav-tabswap-ct">
                                        <div class="guide">
                                            <div class="step-guide-ct">
                                                <div class="ct">
                                                    <h4 class="hd">Đăng ký tài khoản</h4>
                                                    <p>
                                                        Sometimes this is the irony of promoting your businesses products and
                                                    services, because for a fact that you want to make your business
                                                    recognizable and earn more sales of course you need to come up with
                                                    attractive prints that will make you stand out. And since you want
                                                    to be
                                                    competitive you
                                                    want to come up with a material that will make you stand out and
                                                    recognizable. We all know that
                                                    </p>
                                                    <div class="btn-seemore">
                                                        <a href="#">Xem thêm</span>
                                                    </a>
                                                    </div>
                                                </div>
                                                <div class="img">
                                                    <img src="/App_Themes/vantaihoakieu/images/iMac.png" alt="">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="menu4" class="tab-pane fade">
                                <div class="sec-align">
                                    <div class="nav-tabswap-ct">
                                        <div class="guide">
                                            <div class="step-guide-ct">
                                                <div class="ct">
                                                    <h4 class="hd">Đăng ký tài khoản</h4>
                                                    <p>
                                                        Sometimes this is the irony of promoting your businesses products and
                                                    services, because for a fact that you want to make your business
                                                    recognizable and earn more sales of course you need to come up with
                                                    attractive prints that will make you stand out. And since you want
                                                    to be
                                                    competitive you
                                                    want to come up with a material that will make you stand out and
                                                    recognizable. We all know that
                                                    </p>
                                                    <div class="btn-seemore">
                                                        <a href="#">Xem thêm</span>
                                                    </a>
                                                    </div>
                                                </div>
                                                <div class="img">
                                                    <img src="/App_Themes/vantaihoakieu/images/iMac.png" alt="">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="menu5" class="tab-pane fade">
                                <div class="sec-align">
                                    <div class="nav-tabswap-ct">
                                        <div class="guide">
                                            <div class="step-guide-ct">
                                                <div class="ct">
                                                    <h4 class="hd">Đăng ký tài khoản</h4>
                                                    <p>
                                                        Sometimes this is the irony of promoting your businesses products and
                                                    services, because for a fact that you want to make your business
                                                    recognizable and earn more sales of course you need to come up with
                                                    attractive prints that will make you stand out. And since you want
                                                    to be
                                                    competitive you
                                                    want to come up with a material that will make you stand out and
                                                    recognizable. We all know that
                                                    </p>
                                                    <div class="btn-seemore">
                                                        <a href="#">Xem thêm</span>
                                                    </a>
                                                    </div>
                                                </div>
                                                <div class="img">
                                                    <img src="/App_Themes/vantaihoakieu/images/iMac.png" alt="">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="menu6" class="tab-pane fade">
                                <div class="sec-align">
                                    <div class="nav-tabswap-ct">
                                        <div id="guide-1">
                                            <div class="step-guide-ct">
                                                <div class="ct">
                                                    <h4 class="hd">Đăng ký tài khoản</h4>
                                                    <p>
                                                        Sometimes this is the irony of promoting your businesses products and
                                                    services, because for a fact that you want to make your business
                                                    recognizable and earn more sales of course you need to come up with
                                                    attractive prints that will make you stand out. And since you want
                                                    to be
                                                    competitive you
                                                    want to come up with a material that will make you stand out and
                                                    recognizable. We all know that
                                                    </p>
                                                    <div class="btn-seemore">
                                                        <a href="#">Xem thêm</span>
                                                    </a>
                                                    </div>
                                                </div>
                                                <div class="img">
                                                    <img src="/App_Themes/vantaihoakieu/images/iMac.png" alt="">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="right_customer sec">
            <div class="all">
                <div class="group-right-customer clear">
                    <div class="right-customer-left">
                        <div class="img-bout">
                            <img src="/App_Themes/vantaihoakieu/images/bout.jpg">
                        </div>
                    </div>
                    <div class="right-customer-right">
                        <div class="title_services">
                            <div class="title clear">
                                <h3 class="hd">Quyền lợi khách hàng</h3>
                            </div>
                        </div>
                        <div class="sec-align">
                            <div class="customer_right">
                                <ul class="container-col3 wow lightSpeedIn"
                                    style="visibility: visible; animation-name: lightSpeedIn;">
                                    <asp:Literal ID="ltrQL1" runat="server"></asp:Literal>
                                    <%--<li class="col3__item">
                                        <div class="info-card">
                                            <div class="img">
                                                <img src="/App_Themes/vantaihoakieu/images/iconright1.png" alt="">
                                            </div>
                                            <div class="ct">
                                                <h3 class="hd"><a href="#">Khách hàng thân thiết</a></h3>
                                                <p>
                                                    You have finished building your own website. You have intro duced your
                                                company and presented You have finished building your own website. You
                                                have intro duced your company and presented
                                                </p>
                                            </div>
                                        </div>
                                    </li>
                                    <li class="col3__item">
                                        <div class="info-card">
                                            <div class="img">
                                                <img src="/App_Themes/vantaihoakieu/images/iconright2.png" alt="">
                                            </div>
                                            <div class="ct">
                                                <h3 class="hd"><a href="#">Ưu đãi theo sản lượng</a></h3>
                                                <p>
                                                    You have finished building your own website. You have intro duced your
                                                company and presented You have finished building your own website. You
                                                have intro duced your company and presented
                                                </p>
                                            </div>
                                        </div>
                                    </li>
                                    <li class="col3__item">
                                        <div class="info-card">
                                            <div class="img">
                                                <img src="/App_Themes/vantaihoakieu/images/iconright3.png" alt="">
                                            </div>
                                            <div class="ct">
                                                <h3 class="hd"><a href="#">Marketing &amp; bán hàng</a></h3>
                                                <p>
                                                    You have finished building your own website. You have intro duced your
                                                company and presented You have finished building your own website. You
                                                have intro duced your company and presented
                                                </p>
                                            </div>
                                        </div>
                                    </li>--%>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="contact-index sec">
            <div class="all">
                <div class="group-right-customer clear">
                    <div class="right-customer-left">
                        <div class="title clear">
                            <h3 class="hd">Liên hệ chúng tôi</h3>
                        </div>
                        <div class="group-contact-index clear">
                            <div class="item-contact-index">
                                <div class="name">
                                    <p>ĐỊA CHỈ</p>
                                </div>
                                <div class="value">
                                    <asp:Literal ID="ltrAddress" runat="server"></asp:Literal>
                                </div>
                            </div>
                            <div class="item-contact-index">
                                <div class="name">
                                    <p>HOTLINE</p>
                                </div>
                                <div class="value">
                                    <asp:Literal ID="ltrHotline" runat="server"></asp:Literal>
                                </div>
                            </div>
                            <div class="item-contact-index">
                                <div class="name">
                                    <p>EMAIL CONTACT</p>
                                </div>
                                <div class="value">
                                    <asp:Literal ID="ltrEmail" runat="server"></asp:Literal>
                                </div>
                            </div>
                            <div class="item-contact-index">
                                <div class="name">
                                    <p>GIỜ HOẠT ĐỘNG</p>
                                </div>
                                <div class="value">
                                    <asp:Literal ID="ltrTimeWork" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="right-customer-right">
                        <div class="sec-align">
                            <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3722.9920062339165!2d105.80006561476415!3d21.072981485974474!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x3135aac3aa8657dd%3A0x40d529f0ccb8b7cc!2zU-G7kSA1OCwgMjc1IMSQxrDhu51uZyBOZ3V54buFbiBIb8OgbmcgVMO0biwgWHXDom4gVOG6o28sIFThu6sgTGnDqm0sIEjDoCBO4buZaSwgVmnhu4d0IE5hbQ!5e0!3m2!1svi!2s!4v1552447773901" width="100%" height="300" frameborder="0" style="border: 0" allowfullscreen></iframe>
                            <%--<iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3919.4479477511036!2d106.6526192143304!3d10.776962992321142!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x31752ed189fa855d%3A0xf63e15bfce46baef!2sC%C3%B4ng+ty+TNHH+-+MONA+MEDIA!5e0!3m2!1sen!2s!4v1544080307506"
                                width="100%" height="300" frameborder="0" style="border: 0" allowfullscreen></iframe>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
    <asp:HiddenField ID="hdfWebsearch" runat="server" />
    <script type="text/javascript">
        $(document).ready(function () {
            $('.txtsearchfield').on("keypress", function (e) {
                if (e.keyCode == 13) {
                    searchProduct();
                }
            });
        });

        function searchProduct() {
            var web = $("#brand-source").val();
            $("#<%=hdfWebsearch.ClientID%>").val(web);
            $("#<%=btnSearch.ClientID%>").click();
        }
    </script>
    <script type="text/javascript">
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
            }, 500);
        }
        function closeandnotshow() {
            $.ajax({
                type: "POST",
                url: "/Default.aspx/setNotshow",
                //data: "{ID:'" + id + "',UserName:'" + username + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    close_popup_ms();
                },
                error: function (xmlhttprequest, textstatus, errorthrow) {
                    alert('lỗi');
                }
            });

        }
        $(document).ready(function () {
            $.ajax({
                type: "POST",
                url: "/Default.aspx/getPopup",
                //data: "{ID:'" + id + "',UserName:'" + username + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d != "null") {
                        var data = JSON.parse(msg.d);
                        var title = data.NotiTitle;
                        var content = data.NotiContent;
                        var email = data.NotiEmail;
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
                        fr += "<a href=\"javascript:;\" class=\"btn btn-close-full\" onclick='closeandnotshow()'>Đóng & không hiện thông báo</a>";
                        fr += "<a href=\"javascript:;\" class=\"btn btn-close\" onclick='close_popup_ms()'>Đóng</a>";
                        fr += "         </div>";
                        fr += "     </div>";
                        fr += "<div class=\"popup_footer\">";
                        fr += "<span class=\"float-right\">" + email + "</span>";
                        fr += "</div>";
                        fr += "   </div>";
                        fr += "</div>";
                        $(bg).appendTo($(obj)).show().animate({ "opacity": 0.7 }, 800);
                        $(fr).appendTo($(obj));
                        setTimeout(function () {
                            $('#pupip').show().animate({ "opacity": 1, "top": 20 + "%" }, 200);
                            $("#bg_popup").attr("onclick", "close_popup_ms()");
                        }, 1000);
                    }
                },
                error: function (xmlhttprequest, textstatus, errorthrow) {
                    alert('lỗi');
                }
            });
        });
    </script>
    <style>
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
            top: 15%;
            z-index: 999999999;
            left: 25%;
            border-radius: 10px;
            -moz-border-radius: 10px;
            -webkit-border-radius: 10px;
            width: 50%;
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
        }

        .changeavatar {
            padding: 10px;
            margin: 5px 0;
            float: left;
            width: 100%;
        }

        .float-right {
            float: right;
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

        .btn.btn-close {
            float: right;
            background: #29aae1;
            color: #fff;
            margin: 10px 5px;
            text-transform: none;
            padding: 10px 20px;
        }

            .btn.btn-close:hover {
                background: #1f85b1;
            }

        .btn.btn-close-full {
            float: right;
            background: #7bb1c7;
            color: #fff;
            margin: 10px 5px;
            text-transform: none;
            padding: 10px 20px;
        }

            .btn.btn-close-full:hover {
                background: #6692a5;
            }
    </style>
</asp:Content>
