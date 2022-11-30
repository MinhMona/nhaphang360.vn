<%@ Page Language="C#" MasterPageFile="~/nhaphangazMaster.Master" AutoEventWireup="true" CodeBehind="Default5.aspx.cs" Inherits="NHST.Default3" %>

<asp:Content runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <main class="main">
        <div class="home-banner-section sec" style="background-image: url(/App_Themes/NHAZ/images/banner.png);">
            <div class="container">
                <h1 class="home-banner-title wow fadeInDown" data-wow-duration="1.5s">
                    <p class="sub-title">HỢP TÁC</p>
                    <p class="title">VƯƠN ĐẾN THÀNH CÔNG</p>
                </h1>
                <div class="home-banner-setup-ext wow fadeInUp" data-wow-duration="1.5s" data-wow-delay="">
                    <p class="ext-title">CÀI ĐẶT CÔNG CỤ</p>
                    <div class="ext-btn-wrapper">
                        <a href="https://chrome.google.com/webstore/detail/c%C3%B4ng-c%E1%BB%A5-%C4%91%E1%BA%B7t-h%C3%A0ng-nh%E1%BA%ADp-h%C3%A0n/emafphkkcamjdcgmmffppomdnjkikjij" target="_blank" class="main-btn chrome-btn"><i class="fa fa-chrome" aria-hidden="true"></i>CHROME</a>
                        <a href="https://chrome.google.com/webstore/detail/c%C3%B4ng-c%E1%BB%A5-%C4%91%E1%BA%B7t-h%C3%A0ng-nh%E1%BA%ADp-h%C3%A0n/emafphkkcamjdcgmmffppomdnjkikjij" target="_blank" class="main-btn coccoc-btn"><i class="fa fa-chrome" aria-hidden="true"></i>CỐC CỐC</a>
                    </div>
                    <p class="ext-title">APP QUẢN LÝ MOBLIE</p>
                    <div class="ext-btn-wrapper cus">
                        <a href="https://play.google.com/store/apps/details?id=com.appteamvn.NhapHangAZ" target="_blank" class="main-btn cus chrome-btn-1 btn-app cus">
                            <img src="/App_Themes/NHAZ/images/chplay.png" /></a>
                        <a href="https://apps.apple.com/us/app/id1508255000" target="_blank" class="main-btn cus coccoc-btn-1 btn-app cus">
                            <img src="/App_Themes/NHAZ/images/apple.png" /></a>
                    </div>
                </div>
            </div>
        </div>
        <section class="tracking-section sec">
            <div class="container">
                <div class="tracking-decor">
                    <div class="wow slideInRight" data-wow-duration="3s">
                       <marquee scrollamount="40"><img src="/App_Themes/NHAZ/images/truck.png" alt=""></marquee>
                    </div>
                </div>
                <div class="main-title-box text-center">
                    <h2 class="main-title">TRACKING MÃ VẬN ĐƠN</h2>
                </div>
                <div class="tracking-wrapper wow zoomIn">
                    <div class="tracking-desc"></div>
                    <div id="tracking" class="tracking-form">
                        <input id="txtMVD" type="text" class="f-control txtsearchfield" placeholder="Nhập mã vận đơn">
                        <a href="javascript:;" onclick="searchCode()" class="main-btn">Tìm kiếm</a>
                    </div>
                </div>
            </div>
        </section>
        <section class="services-section sec">
            <div class="container wow fadeInLeft">
                <div class="main-title-box main-title-box-2">
                    <h2 class="main-title">DỊCH VỤ</h2>
                </div>
                <div class="services-wrapper">
                    <div class="columns">
                        <asp:Literal runat="server" ID="ltrService"></asp:Literal>
                    </div>
                </div>
            </div>
        </section>
        <section class="benefits-section sec">
            <div class="container wow fadeInRight">
                <div class="main-title-box main-title-box-2">
                    <h2 class="main-title">QUYỀN LỢI KHÁCH HÀNG</h2>
                </div>
                <div class="benefits-wrapper">
                    <div class="columns">
                        <asp:Literal runat="server" ID="ltrBenefits"></asp:Literal>
                    </div>
                </div>
            </div>
        </section>
        <section class="guide-section sec">
            <div class="container wow fadeInLeft">
                <div class="main-title-box main-title-box-2">
                    <h2 class="main-title">HƯỚNG DẪN MUA HÀNG</h2>
                </div>
                <div class="guide-tab-wrapper tab-wrapper">
                    <ul class="tab-link-nav">
                        <asp:Literal runat="server" ID="ltrStep1"></asp:Literal>
                    </ul>
                    <div class="tab-content-wrapper">
                        <asp:Literal runat="server" ID="ltrStep2"></asp:Literal>
                    </div>
                </div>
            </div>
        </section>
        <section class="experience-section sec">
            <div class="container wow zoomIn">
                <div class="main-title-box main-title-box-2">
                    <h2 class="main-title">KINH NGHIỆM MUA HÀNG</h2>
                </div>
                <div class="experience-wrapper">
                    <div class="columns">
                        <div class="column">
                            <div class="experience-item">
                                <a href="#" class="experience-title">CHỌN SIZE QUẦN ÁO KHI ORDER TRUNG QUỐC</a>
                            </div>
                        </div>
                        <div class="column">
                            <div class="experience-item">
                                <a href="#" class="experience-title">THƯƠNG HIỆU THỂ THAO SALE HOT TRÊN TMALL</a>
                            </div>
                        </div>
                        <div class="column">
                            <div class="experience-item">
                                <a href="#" class="experience-title">SĂN MỸ PHẨM HÀNG HIỆU TRÊN TMALL DỊP SALE 8/3</a>
                            </div>
                        </div>
                        <div class="column">
                            <div class="experience-item">
                                <a href="#" class="experience-title">SALE TMALL 20-70% NHÂN NGÀY 8-3</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section class="feedback-section sec">
            <div class="container wow fadeInUp">
                <div class="main-title-box main-title-box-2">
                    <h2 class="main-title">KHÁCH HÀNG NÓI VỀ CHÚNG TÔI</h2>
                </div>
                <div class="feedback-wrapper">
                    <div class="columns">
                        <div class="column">
                            <div class="feedback-item">
                                <div class="feedback-content">Many small businesses don’t get success they want from advertising due to availability of very little resources. The results are simply flat due ..</div>
                                <div class="feedback-author">
                                    <div class="author-avatar">
                                        <img src="/App_Themes/NHAZ/images/avatar.png" alt="">
                                    </div>
                                    <p class="author-name">Nguyên Văn A</p>
                                </div>
                            </div>
                        </div>
                        <div class="column">
                            <div class="feedback-item">
                                <div class="feedback-content">Many small businesses don’t get success they want from advertising due to availability of very little resources. The results are simply flat due ..</div>
                                <div class="feedback-author">
                                    <div class="author-avatar">
                                        <img src="/App_Themes/NHAZ/images/avatar.png" alt="">
                                    </div>
                                    <p class="author-name">Nguyên Văn A</p>
                                </div>
                            </div>
                        </div>
                        <div class="column">
                            <div class="feedback-item">
                                <div class="feedback-content">Many small businesses don’t get success they want from advertising due to availability of very little resources. The results are simply flat due ..</div>
                                <div class="feedback-author">
                                    <div class="author-avatar">
                                        <img src="/App_Themes/NHAZ/images/avatar.png" alt="">
                                    </div>
                                    <p class="author-name">Nguyên Văn A</p>
                                </div>
                            </div>
                        </div>
                        <div class="column">
                            <div class="feedback-item">
                                <div class="feedback-content">Many small businesses don’t get success they want from advertising due to availability of very little resources. The results are simply flat due ..</div>
                                <div class="feedback-author">
                                    <div class="author-avatar">
                                        <img src="/App_Themes/NHAZ/images/avatar.png" alt="">
                                    </div>
                                    <p class="author-name">Nguyên Văn A</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <div class="partner-section sec">
            <div class="container wow fadeIn">
                <ul class="partner-nav">
                    <li>
                        <a href="https://world.taobao.com/" target="_blank">
                            <img src="/App_Themes/NHAZ/images/partner-1.png" alt=""></a></li>
                    <li>
                        <a href="https://www.tmall.com/" target="_blank">
                            <img src="/App_Themes/NHAZ/images/partner-2.png" alt=""></a></li>
                    <li>
                        <a href="https://intl.alipay.com/" target="_blank">
                            <img src="/App_Themes/NHAZ/images/partner-3.png" alt=""></a></li>
                    <li>
                        <a href="https://www.1688.com/" target="_blank">
                            <img src="/App_Themes/NHAZ/images/partner-4.png" alt=""></a></li>
                </ul>
            </div>
        </div>
    </main>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.txtsearchfield').on("keypress", function (e) {
                if (e.keyCode == 13) {
                    searchCode();
                }
            });
        });
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
            background: #ff8201;
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
            background: #ff8201;
            color: #fff;
            margin: 10px 5px;
            text-transform: none;
            padding: 10px 20px;
        }

            .btn.btn-close:hover {
                background: #f8d486;
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
                background: #c9dae1;
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
    </style>
    <style>
        .ext-btn-wrapper.cus {
            display: flex;
            flex-wrap: wrap;
            justify-content: center;
        }

        .btn-app.cus {
            width: 15% !important;
            background: transparent !important;
            padding: 0 !important;
        }

            .btn-app.cus img {
                width: 150px;
                height: 50px;
            }

    </style>
</asp:Content>
