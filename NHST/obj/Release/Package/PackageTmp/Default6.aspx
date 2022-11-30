<%@ Page Language="C#" MasterPageFile="~/nhaphangazMasterNew.Master" AutoEventWireup="true" CodeBehind="Default6.aspx.cs" Inherits="NHST.DefaultX" %>

<asp:Content runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <main id="main-wrap">
        <div class="group-banner">
            <div class="banner-entry">
                <div class="all">
                    <h1 class="hd" style="color:white">Chúng tôi
        nhanh chóng &
      chuyên nghiệp</h1>
                    <span class="border-bot-title"></span>
                    <div class="group-tool">
                        <div class="tool-item">
                            <a href="https://chrome.google.com/webstore/detail/c%C3%B4ng-c%E1%BB%A5-%C4%91%E1%BA%B7t-h%C3%A0ng-c%E1%BB%A7a-bee/ifehddcalafgneefdkdmopfkplgcjjjd" target="_blank"><i class="fa fa-chrome" aria-hidden="true"></i>CHROME</a>
                            <a href="https://chrome.google.com/webstore/detail/c%C3%B4ng-c%E1%BB%A5-%C4%91%E1%BA%B7t-h%C3%A0ng-c%E1%BB%A7a-bee/ifehddcalafgneefdkdmopfkplgcjjjd" target="_blank"><i class="fa fa-chrome" aria-hidden="true"></i>CỐC CỐC</a>
                        </div>
                    </div>
                </div>
            </div>
            <div class="issue-number">
                <div class="all">
                    <div class="group-bot-banner">
                        <ul class="list-bot-banner">
                            <li>
                                <div class="item-issue-banner">
                                    <div class="img">
                                        <img src="/App_Themes/ThuongHaiOrder/images/iconisue1.png">
                                    </div>
                                    <div class="info">
                                        <h3>05</h3>
                                        <p>Năm kinh nghiệm</p>
                                    </div>
                                </div>
                            </li>
                            <li>
                                <div class="item-issue-banner">
                                    <div class="img">
                                        <img src="/App_Themes/ThuongHaiOrder/images/iconisue2.png">
                                    </div>
                                    <div class="info">
                                        <h3>12,345</h3>
                                        <p>Khách hàng</p>
                                    </div>
                                </div>
                            </li>
                            <li>
                                <div class="item-issue-banner">
                                    <div class="img">
                                        <img src="/App_Themes/ThuongHaiOrder/images/iconisue3.png">
                                    </div>
                                    <div class="info">
                                        <h3>85,302</h3>
                                        <p>Đơn hàng</p>
                                    </div>
                                </div>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="banner-home">
                <div class="banner-slide">
                    <div class="banner" style="background-image: url(/App_Themes/ThuongHaiOrder/images/banner.png);">
                    </div>
                </div>
                <div class="banner-slide">
                    <div class="banner" style="background-image: url(/App_Themes/ThuongHaiOrder/images/background-world.png);">
                    </div>
                </div>
            </div>
        </div>
        <div class="background-world">
            <div class="sec sec-search">
                <div class="all">
                    <h3 class="title-sec"><span class="title">TÌM KIẾM SẢN PHẨM</span></h3>
                    <div class="search-form">
                        <div class="select-form">
                            <select class="fcontrol" id="brand-source">
                                <option value="taobao" data-image="./images/hdsearch-item-taobao.png">TAOBAO</option>
                                <option value="tmall" data-image="./images/hdsearch-item-tmall.png">Tmall</option>
                                <option value="1688" data-image="./images/hdsearch-item-1688.png">1688</option>
                            </select>


                        </div>
                        <div class="input-form">
                            <%--<input type="text" class="fcontrol" placeholder="Nhập link sản phẩm">--%>
                            <asp:TextBox type="text" runat="server" ID="txtSearch" class="fcontrol" placeholder="Tìm kiếm sản phẩm"></asp:TextBox>
                            <%--<a href="javascript:;" onclick="searchProduct()" class="main-btn"><i class="fa fa-search" aria-hidden="true"></i></a>--%>
                        </div>
                        <a href="javascript:" onclick="searchProduct()" class="submit-form">Tìm kiếm</a>
                        <asp:Button ID="btnSearch" runat="server"
                            OnClick="btnSearch_Click" Style="display: none"
                            OnClientClick="document.forms[0].target = '_blank';" UseSubmitBehavior="false" />
                    </div>
                </div>
            </div>
            <div class="sec services wow fadeInUp">
                <div class="all">
                    <div class="title">
                        <h3 class="title-sec center"><span class="tree-title left"></span><span class="title">VỀ DỊCH VỤ</span> <span class="tree-title right"></span></h3>
                    </div>
                    <div class="messgae-title">
                        <p>
                            Bee Order là giải pháp nhập hàng tối ưu cho quý khách. Chúng tôi mang lại cho khách hàng nguồn hàng phong phú với mức giá cực rẻ.
                        </p>
                    </div>
                    <div class="group-services">
                        <div class="services-home">
                            <ul class="list-services">
                                <asp:Literal runat="server" ID="ltrService"></asp:Literal>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--quy trình đặt hàng--%>
        <div class="sec step-oreder">
            <div class="all">
                <div class="title">
                    <h3 class="title-sec center"><span class="tree-title left"></span><span class="title">QUY TRÌNH ĐẶT HÀNG</span> <span class="tree-title right"></span></h3>
                </div>
                <div class="guide-wrap">
                    <div class="guide-nav wow zoomInLeft">
                        <ul class="list-guide-nav list-nav-tabswap">
                            <asp:Literal runat="server" ID="ltrStep1"></asp:Literal>
                        </ul>

                    </div>

                    <div class="guide-content guide-ct-nav wow rotateInUpRight">
                        <asp:Literal runat="server" ID="ltrStep2"></asp:Literal>
                    </div>
                    <%--<div class="guide-content guide-ct-nav wow rotateInUpRight">
                        <asp:Literal runat="server" ID="ltrStep2"></asp:Literal>
                    </div>  --%>
                </div>
            </div>
        </div>
        <div class="sec sec-right-custommer wow fadeInUp">
            <div class="all">
                <div class="title">
                    <h3 class="title-sec center"><span class="tree-title left"></span><span class="title">Quyền lợi khách hàng</span> <span class="tree-title right"></span></h3>
                </div>
                <ul class="list-right-cus">
                    <asp:Literal runat="server" ID="ltrBenefits"></asp:Literal>
                </ul>
            </div>
        </div>
        <div class="contact-map">
            <div class="all">
                <div class="iframe-google-map">
                    <%--<iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3919.4479477511036!2d106.6526192143304!3d10.776962992321142!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x31752ed189fa855d%3A0xf63e15bfce46baef!2sC%C3%B4ng+ty+TNHH+-+MONA+MEDIA!5e0!3m2!1sen!2s!4v1544080307506" width="100%" height="400" frameborder="0" style="border: 0" allowfullscreen></iframe>--%>
                    <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d878.8533014209997!2d105.81701028834094!3d21.01465909909062!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x3135ab63352767b7%3A0xea695bc797671e22!2zMTY1IFRow6FpIEjDoCwgTMOhbmcgSOG6oSwgxJDhu5FuZyDEkGEsIEjDoCBO4buZaSwgVmnhu4d0IE5hbQ!5e0!3m2!1svi!2s!4v1601523511186!5m2!1svi!2s" width="100%" height="400" frameborder="0" style="border: 0;" allowfullscreen=""></iframe>
                </div>
                <div class="contact-bot-map">

                    <div class="item-bot-map">
                        <div class="img">
                            <a href="#">
                                <i class="fa fa-envelope"></i>
                            </a>
                            <%-- <asp:Literal runat="server" ID="ltrLogoEn"></asp:Literal>--%>
                        </div>
                        <div class="info">
                            <h3 class="title">EMAIL CONTACT</h3>
                            <p class="desc">tfdfhggdqkgtslct</p>
                            <%-- <asp:Literal runat="server" ID="ltrEmailEn"></asp:Literal>--%>
                        </div>
                    </div>
                    <div class="item-bot-map">
                        <div class="img">
                            <a href="#">
                                <i class="fa fa-phone"></i>
                            </a>
                        </div>
                        <div class="info">
                            <h3 class="title">GIỜ HOẠT ĐỘNG</h3>
                            <p class="desc">08:30 am - 17:30 pm</p>
                        </div>
                    </div>
                    <div class="item-bot-map">
                        <div class="img">
                            <a href="#">
                                <i class="fa fa-clock-o"></i>
                            </a>
                        </div>
                        <div class="info">
                            <h3 class="title">HOTLINE</h3>
                            <a href="#" class="desc">0969636602</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
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
            background: #ea8c51;
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
        }

            .btn.btn-close-full:hover {
                background: #ea8c51;
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
</asp:Content>

