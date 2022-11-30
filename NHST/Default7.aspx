<%@ Page Language="C#" MasterPageFile="~/ThuongHaiOrderMaster.Master" AutoEventWireup="true" CodeBehind="Default7.aspx.cs" Inherits="NHST.Default4" %>

<asp:Content runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <main class="main-wrap">
        <div class="main-banner">
            <div class="container">
                <div class="content-main-banner">
                    <h4 class="title-small wow fadeInRight" data-wow-delay=".2s" data-wow-duration="1s">CHÚNG TÔI
                    </h4>
                    <h3 class="title-big wow fadeInRight" data-wow-delay=".4s" data-wow-duration="1s">CHUYÊN BUÔN BÁN NHẬP HÀNG TRUNG QUỐC
                    </h3>
                    <div class="tab-wrapper-main wow zoomIn" data-wow-delay=".6s" data-wow-duration="1s">
                        <nav class="c-tab__nav">
                            <ul>
                                <li data-tab="01" class="active">TÌM KIẾM</li>
                                <li data-tab="02">TRACKING</li>
                            </ul>
                        </nav>
                        <div class="c-tab__bottom">
                            <div class="c-tab__content 01 active">
                                <div class="box-select">
                                    <div class="icon-dropdown">
                                        <i class="fa fa-angle-down"></i>
                                    </div>
                                    <select class="fcontrol" id="brand-source">
                                        <option value="taobao">TAOBAO</option>
                                        <option value="tmall">Tmall</option>
                                        <option value="1688">1688</option>
                                    </select>
                                </div>
                                <div class="search-box">
                                    <asp:TextBox type="text" runat="server" ID="txtSearch" class="fcontrol f-input" placeholder="Nhập sản phẩm tìm kiếm"></asp:TextBox>
                                    <%--<input type="text" placeholder="Nhập sản phẩm tìm kiếm" class="fcontrol f-input">--%>
                                    <div class="absolute-btn">
                                        <%-- <a href="#" class="btn btn-search">Tìm kiếm</a>--%>
                                        <a href="javascript:" onclick="searchProduct()" class="btn btn-search">Tìm kiếm</a>
                                        <asp:Button ID="btnSearch" runat="server"
                                            OnClick="btnSearch_Click" Style="display: none"
                                            OnClientClick="document.forms[0].target = '_blank';" UseSubmitBehavior="false" />
                                    </div>
                                </div>
                            </div>
                            <div class="c-tab__content 02">
                                <div class="search-box">
                                    <input type="text" id="txtMVD" class="fcontrol f-input" placeholder="Nhập mã vận đơn">
                                    <%-- <input type="text" placeholder="Nhập sản phẩm tìm kiếm" class="fcontrol f-input">--%>
                                    <div class="absolute-btn">
                                        <a href="javascript:;" onclick="searchCode()" class="btn btn-search">Tìm kiếm</a>
                                        <%--<a href="#" class="btn btn-search">Tìm kiếm</a>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="extension-setting wow fadeInRight" data-wow-delay=".8s" data-wow-duration="1s">
                        <div class="extension-item">
                            <h5 class="title-extension">Cài đặt công cụ đặt hàng
                            </h5>
                            <div class="btn-extension">
                                <a href="https://chrome.google.com/webstore/detail/c%C3%B4ng-c%E1%BB%A5-%C4%91%E1%BA%B7t-h%C3%A0ng-c%E1%BB%A7a-th%C6%B0%C6%A1/cbggkombblkfiingfcjbfklhbafnnkdo/related" class="btn-main btn bg-brow"><i class="fa fa-chrome"></i>Chrome</a>
                                <a href="https://chrome.google.com/webstore/detail/c%C3%B4ng-c%E1%BB%A5-%C4%91%E1%BA%B7t-h%C3%A0ng-c%E1%BB%A7a-th%C6%B0%C6%A1/cbggkombblkfiingfcjbfklhbafnnkdo/related" class="btn-main btn bg-brow"><i class="fa fa-chrome"></i>Cốc Cốc</a>
                            </div>
                        </div>
                        <div class="extension-item">
                            <h5 class="title-extension">App đặt hàng trên mobile
                            </h5>
                            <div class="btn-extension">
                                <a href="#">
                                    <img src="/App_Themes/ThuongHaiOrder/images/gg.png" alt=""></a>
                                <a href="#">
                                    <img src="/App_Themes/ThuongHaiOrder/images/apstore.png" alt=""></a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <section class="sec sec-number">
            <div class="container">
                <div class="table-number wow zoomIn" data-wow-delay=".2s" data-wow-duration="1s">
                    <div class="columns">
                        <div class="colum">
                            <div class="content">
                                <div class="number counting" data-count="6">
                                    6 năm
                                </div>
                                <h5 class="txt">năm kinh nghiệm khách hàng
                                </h5>
                            </div>
                        </div>
                        <div class="colum">
                            <div class="content">
                                <div class="number counting" data-count="9117">
                                    9,117 
                                </div>
                                <h5 class="txt">khách hàng sử dụng
                                </h5>
                            </div>
                        </div>
                        <div class="colum">
                            <div class="content">
                                <div class="number counting" data-count="101936">
                                   101,936 
                                </div>
                                <h5 class="txt">đơn đặt hàng
                                </h5>
                            </div>
                        </div>
                        <div class="colum">
                            <div class="content">
                                <div class="number counting" data-count="6602">
                                    6,602 
                                </div>
                                <h5 class="txt">đánh giá khách hàng
                                </h5>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section class="sec sec-servises">
            <div class="container">
                <div class="main-title wow fadeInRight" data-wow-delay=".2s" data-wow-duration="1s">
                    <h3 class="title">Dịch vụ
                    </h3>
                    <p class="desc">
                    </p>
                </div>
                <div class="table-services">
                    <div class="columns">
                        <asp:Literal runat="server" ID="ltrService"></asp:Literal>
                    </div>
                </div>
            </div>
        </section>
        <section class="sec sec-tutorial">
            <div class="container">
                <div class="table-tutorial">
                    <div class="columns">
                        <div class="colum left">
                            <div class="content-left wow fadeInLeft" data-wow-delay=".2s" data-wow-duration="1s">
                                <div class="c-tab__tutorial__content">
                                    <div class="main-title cus">
                                        <h3 class="title cus">Hướng dẫn đặt hàng
                                        </h3>
                                    </div>
                                    <asp:Literal runat="server" ID="ltrStep2"></asp:Literal>
                                </div>
                            </div>
                        </div>
                        <div class="colum center">
                            <div class="content-center wow fadeInUp" data-wow-delay=".4s" data-wow-duration="1s">
                                <div class="box-img">
                                    <img src="/App_Themes/ThuongHaiOrder/images/img-video.png" alt="">
                                    <a href="#test-popup" class="icon-play open-popup-link">
                                        <i class="fa fa-play" aria-hidden="true"></i>
                                    </a>
                                </div>
                            </div>
                        </div>
                        <div class="colum right">
                            <div class="content-right wow fadeInRight" data-wow-delay=".6s" data-wow-duration="1s">
                                <nav class="c-tab__tutorial">
                                    <ul>
                                        <asp:Literal runat="server" ID="ltrStep1"></asp:Literal>
                                    </ul>
                                </nav>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section class="sec sec-benefits">
            <div class="container">
                <div class="table-benefits">
                    <div class="columns">
                        <div class="colum left wow zoomInLeft" data-wow-delay=".2s" data-wow-duration="1s">
                            <div class="box-img">
                                <img src="/App_Themes/ThuongHaiOrder/images/thung.png" alt="">
                            </div>
                        </div>
                        <div class="colum right wow zoomInRight" data-wow-delay=".2s" data-wow-duration="1s">
                            <div class="main-title cus">
                                <h3 class="title prev-left">Quyền lợi khách hàng
                                </h3>
                            </div>
                            <div class="content">
                                <asp:Literal runat="server" ID="ltrBenefits"></asp:Literal>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section class="sec sec-contact">
            <div class="container isner">
                <div class="bg-map">
                    <img src="/App_Themes/ThuongHaiOrder/images/map.png" alt="">
                    <div class="icon-check">
                        <img src="/App_Themes/ThuongHaiOrder/images/check.png" alt="">
                    </div>
                </div>
                <div class="table-contact">
                    <div class="main-title cus color-fff">
                        <h3 class="title cus wow fadeInRight" data-wow-delay=".2s" data-wow-duration="1s">Liên hệ chúng tôi
                        </h3>
                    </div>
                    <div class="box-contact wow zoomIn" data-wow-delay=".4s" data-wow-duration="1s">
                        <div class="columns">
                            <div class="colum">
                                <div class="content-ct">
                                    <div class="icon">
                                        <img src="/App_Themes/ThuongHaiOrder/images/ico-1.png" alt="">
                                    </div>
                                    <h4 class="txt">Địa chỉ
                                    </h4>
                                    <asp:Literal runat="server" ID="ltrAddress"></asp:Literal>
                                    <%--<p class="desc">
                                        319 Lý Thường Kiệt, Phường 15,Quận 11, Tp.hcm
                                    </p>--%>
                                </div>
                            </div>
                            <div class="colum">
                                <div class="content-ct">
                                    <div class="icon">
                                        <img src="/App_Themes/ThuongHaiOrder/images/ico-2.png" alt="">
                                    </div>
                                    <h4 class="txt">Email contact:
                                    </h4>
                                    <asp:Literal runat="server" ID="ltrEmail"></asp:Literal>
                                    <%-- <p class="desc">
                                        <a href="mailto:admin@nhaphang.com">admin@nhaphang.com</a>
                                    </p>--%>
                                </div>
                            </div>
                            <div class="colum">
                                <div class="content-ct">
                                    <div class="icon">
                                        <img src="/App_Themes/ThuongHaiOrder/images/ico-3.png" alt="">
                                    </div>
                                    <h4 class="txt">HOTLINE:
                                    </h4>
                                    <asp:Literal runat="server" ID="ltrHotline"></asp:Literal>
                                    <%--<p class="desc">
                                        <a href="tel:01269220234">0126 922 0234</a>
                                    </p>--%>
                                </div>
                            </div>
                            <div class="colum">
                                <div class="content-ct">
                                    <div class="icon">
                                        <img src="/App_Themes/ThuongHaiOrder/images/ico-4.png" alt="">
                                    </div>
                                    <h4 class="txt">GIỜ HOẠT ĐỘNG:
                                    </h4>
                                    <asp:Literal runat="server" ID="ltrTimeWork"></asp:Literal>
                                    <%--<p class="desc">
                                        08:00 AM - 17:00 PM 
                                    </p>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </main>

    <asp:HiddenField ID="hdfWebsearch" runat="server" />
    <script type="text/javascript">
        $(document).ready(function () {
            $('.txtSearch').on("keypress", function (e) {
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
        function searchCode() {
            var code = $("#txtMVD").val();
            if (isEmpty(code)) {
                alert('Vui lòng nhập mã vận đơn.');
            }
            else {
                $.ajax({
                    type: "POST",
                    url: "/Default.aspx/getInfo",
                    data: "{ordecode:'" + code + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d != "null") {
                            //var data = JSON.parse(msg.d);
                            var title = "Thông tin mã vận đơn";
                            var content = msg.d;
                            var email = "";
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
                            fr += "         <div class=\"content1\" style=\"width:75%;margin-left:11%\">";
                            fr += content;
                            fr += "         </div>";
                            fr += "         <div class=\"content2\">";
                            fr += "             <a href=\"javascript:;\" class=\"btn btn-close\" onclick='close_popup_ms()'>Đóng</a>";
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
            }

        }
    </script>


    <script>
        jQuery(document).ready(function () {
            new WOW().init();

            var iniTab = $('.list-guide-nav li.active .tabswap-btn').attr('src-navtab');
            if (!!iniTab) {
                $('.guide-ct-nav ' + iniTab).show().siblings().hide();
                $('.list-guide-nav').on('click', '.tabswap-btn', function (e) {
                    e.preventDefault();
                    var srcTab = $(this).attr('src-navtab');
                    $(this).parent().addClass('active').siblings().removeClass('active');
                    $('.guide-ct-nav ' + srcTab).fadeIn().siblings().hide();
                })
            }
            $('.banner-home').slick({
                infinite: true,
                speed: 300,
                slidesToShow: 1,
                adaptiveHeight: true,
                arrows: false
            });

        });
    </script>
    <script>
        $(".acc-info-btn").on('click', function (e) {
            e.preventDefault();
            $(".status-mobile").addClass("open");
            $(".overlay-status-mobile").show();
        });

        $(".overlay-status-mobile").on('click', function () {
            $(".status-mobile").removeClass("open");
            $(this).hide();
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
                /*background: url("/App_Themes/bee-order/images/close_button.png") no-repeat scroll 0 0 rgba(0, 0, 0, 0);*/
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
            background: #FECD06;
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
            background: #dc4d88;
            color: #fff;
            margin: 10px 5px;
            text-transform: none;
            padding: 10px 20px;
            width: 15%;
        }

            .btn.btn-close:hover {
                background: #752d4b;
            }

        .btn.btn-close-full {
            float: right;
            background: #7bb1c7;
            color: #fff;
            margin: 10px 5px;
            text-transform: none;
            padding: 10px 20px;
            width: 30%;
        }

            .btn.btn-close-full:hover {
                background: #FECD06;
            }


        @media screen and (max-width: 768px) {
            #popup_content_home {
                left: 10%;
                width: 80%;
            }

            .content1 {
                overflow: auto;
                height: 300px;
            }
        }

        @media screen and (max-width: 1300px) {
            .btn.btn-close-full {
                width: 45%;
            }

            .btn.btn-close-full {
                width: 50%;
            }
        }

        @media screen and (max-width: 1200px) {
            .btn.btn-close-full {
                width: 50%;
            }
        }

        @media screen and (max-width: 1070px) {
            .btn.btn-close-full {
                width: 55%;
            }
        }

        @media screen and (max-width: 911px) {
            .btn.btn-close-full {
                width: 60%;
            }
        }

        @media screen and (max-width: 768px) {
            .btn.btn-close-full {
                width: 100%;
            }
        }
        @media screen and (max-width: 768px) {
            .btn.btn-close {
                width: 100%;
            }
        }
    </style>
</asp:Content>
