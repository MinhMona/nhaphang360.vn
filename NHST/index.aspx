<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="NHST.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1, user-scalable=yes">
    <meta name="format-detection" content="telephone=no">
    <meta name="description" content="Hệ thống website và phần mềm nhập hàng, đặt hàng Trung Quốc từ Taobao, 1688, Tmall. Giải pháp hoàn thiện cho các doanh nghiệp chuyên ship hàng Quảng Châu">
    <meta name="keywords" content="">
    <meta property="og:title" content="">
    <meta property="og:type" content="website">
    <meta property="og:url" content="http://websitenhaphang.com/">
    <meta property="og:image" content="OG.jpg">
    <meta property="og:site_name" content="Thiết kế website phần mềm nhập hàng Trung Quốc có extension/add-on chrome, coccoc, firefox">
    <meta property="og:description" content="Hệ thống website và phần mềm nhập hàng, đặt hàng Trung Quốc từ Taobao, 1688, Tmall. Giải pháp hoàn thiện cho các doanh nghiệp chuyên ship hàng Quảng Châu">

    <title>Thiết kế website phần mềm nhập hàng Trung Quốc có extension/add-on chrome, coccoc, firefox</title>

    <link rel="shortcut icon" href="logo.png" type="image/x-icon">

    <link rel="stylesheet" href="/App_Themes/DemoIndex/css/style.css" media="all">
    <link rel="stylesheet" href="/App_Themes/DemoIndex/css/responsive.css" media="all">
    <link rel="stylesheet" href="/App_Themes/DemoIndex/css/mon.css" media="all">
    <script src="/App_Themes/DemoIndex/js/jquery-1.9.1.min.js"></script>
    <style>
        #header {
            background-color: #252525;
        }

        .faq {
            margin-top: 70px;
        }

        .categories1 {
            position: absolute;
            left: 0;
            width: 250px;
            z-index: 2;
            top: 200px;
        }

            .categories1 li {
                display: block;
                border: solid 1px #e1e1e1;
                margin-top: -1px;
                background-color: #f8f8f8;
            }

            .categories1 a {
                position: relative;
                display: block;
                overflow: hidden;
                padding: 10px 15px;
                -webkit-font-smoothing: antialiased;
                -moz-osx-font-smoothing: grayscale;
                white-space: nowrap;
                text-overflow: ellipsis;
                color: #000;
                font-weight: 500;
                -webkit-transition: background 0.2s, padding 0.2s;
                -moz-transition: background 0.2s, padding 0.2s;
                transition: background 0.2s, padding 0.2s;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <header id="header">
            <div class="all">

                <div class="logo">
                    <a href="#">
                        <img src="/App_Themes/DemoIndex/images/logo-white.png" alt="Website nhập hàng Trung Quốc by Mona-Media"></a>
                </div>
                <div class="hd-right">
                    <a href="tel:01269220162" class="ct-link"><i class="fa fa-phone"></i>
                        <span class="txt">0126 - 922 - 0162</span></a>
                </div>

                <div class="navbar-toggle"><span class="icon-bar"></span><span class="icon-bar"></span><span class="icon-bar"></span></div>
                <div class="nav-wrap right" id="main-nav">
                    <div class="nav-overlay"></div>
                    <ul class="nav-ul">
                        <li><a href="https://mona-media.com/">Tìm hiểu Mona Media</a></li>
                        <li><a href="/dang-nhap">Trang demo</a></li>
                        <li><a href="#">Extension/Add-on demo</a></li>
                        <li><a href="#">Bộ video nghiệp vụ</a></li>
                        <li><a href="http://websitenhaphang.com/" class="hl-link">Đặt hàng</a></li>
                    </ul>
                </div>


            </div>
        </header>
        <main id="main-wrap">
            <section class="sec sec-faq">
                <div class="all">
                    <section class="faq">
                        <ul class="categories">
                            <li><a class="selected" href="#basics">Giao diện trang chủ</a></li>
                            <li><a href="#mobile">Giao diện người dùng</a></li>
                            <li><a href="#account">Giao diện quản trị</a></li>
                            <%--<li><a href="#basics" onclick="linktosignin()">Đăng nhập demo</a></li>--%>
                        </ul>
                        <ul class="categories1">
                            <li><a href="#dangnhap" onclick="linktosignin()">Đăng nhập demo</a></li>
                        </ul>
                        <!-- categories -->
                        <div class="faq-items">
                            <ul id="basics" class="faq-group">
                                <li class="faq-title">
                                    <h2>Những phần sẽ có trên một trang chủ nhập hàng</h2>
                                </li>
                                <li>
                                    <a class="trigger" href="#0">Thông tin logo, slogan, HOTLINE, các quicklink giúp khách hàng thuận tiện thao tác</a>
                                    <div class="faq-content">
                                        <p>
                                            <img src="/App_Themes/DemoIndex/images/tab1.png" alt="" />
                                        </p>
                                    </div>
                                    <!-- faq-content -->
                                </li>

                                <li>
                                    <a class="trigger" href="#0">Slider tùy ý thay đổi nội dung và hình ảnh</a>
                                    <div class="faq-content">
                                        <p>
                                            <img src="/App_Themes/DemoIndex/images/tab2.png" alt="" />
                                        </p>
                                    </div>
                                    <!-- faq-content -->
                                </li>

                                <li>
                                    <a class="trigger" href="#0">Link download các extension/add-on</a>
                                    <div class="faq-content">
                                        <p>
                                            <img src="/App_Themes/DemoIndex/images/tab3.png" alt="" />
                                        </p>
                                    </div>
                                    <!-- faq-content -->
                                </li>

                                <li>
                                    <a class="trigger" href="#0">Thanh tìm kiếm sản phẩm nhanh
                                    </a>
                                    <div class="faq-content">
                                        <p>
                                            Khách hàng có thể nhập vào tiếng việt và hệ thống tự động tìm kiếm trên Taobao, Tmall, 1688 bằng tiếng Trung.
                                        </p>
                                        <p>
                                            Nhập từ khóa tìm kiếm và chọn trang muốn tìm.
                                            <img src="/App_Themes/DemoIndex/images/tab4.png" alt="" />
                                        </p>
                                        <p>
                                            Kết quả tìm kiếm trên trang đã chọn.
                                            <img src="/App_Themes/DemoIndex/images/tab5.png" alt="" />
                                        </p>
                                    </div>
                                    <!-- faq-content -->
                                </li>
                                <li>
                                    <a class="trigger" href="#0">Thanh pastelink nhanh
                                    </a>
                                    <div class="faq-content">
                                        <p>
                                            Giúp khách hàng có thêm lựa chọn đặt hàng nhanh bằng cách paste link sản phẩm muốn đặt vào thanh này và add vào giỏ hàng
                                            <img src="/App_Themes/DemoIndex/images/tab6.png" alt="" />
                                        </p>
                                    </div>
                                </li>
                                <li>
                                    <a class="trigger" href="#0">Danh sách danh mục sản phẩm
                                    </a>
                                    <div class="faq-content">
                                        <p>
                                            Danh sách danh mục sản phẩm, sản phẩm đáng chú ý nhất Taobao + Kèm link đi đến sản phẩm đó ở Taobao, 1688, Tmall để khách hàng lựa chọn 
                                            <img src="/App_Themes/DemoIndex/images/tab7.png" alt="" />
                                        </p>
                                    </div>
                                </li>
                                <li>
                                    <a class="trigger" href="#0">Bản đồ kho bãi và chi nhánh của doanh nghiệp
                                    </a>
                                    <div class="faq-content">
                                        <p>
                                            Bản đồ kho bãi và chi nhánh của doanh nghiệp
                                            <img src="/App_Themes/DemoIndex/images/tab8.png" alt="" />
                                        </p>
                                    </div>
                                </li>
                                <li>
                                    <a class="trigger" href="#0">Mô tả quy trình đặt hàng bằng giao diện trực quan
                                    </a>
                                    <div class="faq-content">
                                        <p>
                                            Mô tả quy trình đặt hàng bằng giao diện trực quan
                                            <img src="/App_Themes/DemoIndex/images/tab9.png" alt="" />
                                        </p>
                                    </div>
                                </li>
                                <li>
                                    <a class="trigger" href="#0">Thông tin liên hệ, thông tin chuyển khoản
                                    </a>
                                    <div class="faq-content">
                                        <p>
                                            Thông tin liên hệ, thông tin chuyển khoản
                                            <img src="/App_Themes/DemoIndex/images/tab10.png" alt="" />
                                        </p>
                                    </div>
                                </li>
                                <li>
                                    <a class="trigger" href="#0">Popup thông báo
                                    </a>
                                    <div class="faq-content">
                                        <p>
                                            Popup thay đổi thông tin tùy ý giúp gửi các thông điệp quan trọng gấp tới khách hàng
                                            <img src="/App_Themes/DemoIndex/images/tab11.png" alt="" />
                                        </p>
                                    </div>
                                </li>
                            </ul>
                            <ul id="mobile" class="faq-group">
                                <li class="faq-title">
                                    <h2>Tài khoản demo vào trang giao diện khách hàng/người dùng</h2>
                                </li>
                                <li>
                                    <a class="trigger" href="#0">Tài khoản/đường dẫn</a>
                                    <div class="faq-content">
                                        <p>
                                            Username: demo1<br>
                                            Password: demo1<br>
                                            <a href="http://nhaphangdemo.monamedia.net/dang-nhap" target="_blank">http://nhaphangdemo.monamedia.net/dang-nhap</a>
                                        </p>
                                    </div>
                                    <!-- faq-content -->
                                </li>
                            </ul>
                            <ul id="account" class="faq-group">
                                <li class="faq-title">
                                    <h2>Giao diện tài khoản quản trị</h2>
                                </li>
                                <li>
                                    <a class="trigger" href="#0">Chỉ dành cho các khách hàng đã đăng ký mới xem được tài khoản này</a>
                                    <div class="faq-content">
                                        <p>
                                            <iframe width="560" height="315" src="https://www.youtube.com/embed/O6QY9RpPwME" frameborder="0" allow="autoplay; encrypted-media" allowfullscreen></iframe>
                                        </p>
                                    </div>
                                    <!-- faq-content -->
                                </li>
                            </ul>
                        </div>
                        <!-- faq-items -->
                        <a href="#0" class="cd-close-panel">Close</a>
                    </section>
                    <!-- faq -->
                </div>
            </section>

        </main>
        <footer id="footer">
            <div class="all">
                NHẬP HÀNG 360
       
            </div>
        </footer>

        <a href="javascript:;" class="scroll-top-link" id="scroll-top"><i class="fa fa-angle-up"></i></a>

        <script src="/App_Themes/DemoIndex/js/slick/slick.min.js"></script>
        <script src="/App_Themes/DemoIndex/js/magnific-popup/jquery.magnific-popup.min.js"></script>
        <script src="/App_Themes/DemoIndex/js/master.js"></script>
        <script>
            jQuery(document).ready(function () {
                //        new WOW().init();

                $('#contactne').on('submit', function (e) {
                    e.preventDefault();
                    var $this = $(this);
                    var $sent_btn = $this.find('#sendbtn');
                    $sent_btn.addClass('sendding').html('Process <i class="fa fa-spinner fa-pulse fa-fw"></i>');

                    var fname = $('input[name="name"]').val();
                    var fmail = $('input[name="email"]').val();
                    var fphone = $('input[name="phone"]').val();
                    var fcontent = $('textarea[name="content"]').val();
                    var data = {
                        fname: fname,
                        fmail: fmail,
                        fcontent: fcontent,
                        fphone: fphone
                    };

                    $.ajax({
                        url: '/sentmail/mail.php',
                        type: "POST",
                        data: data,
                        cache: false,
                        dataType: 'json',
                        success: function (result) {
                            if (result.type == 'success') {
                                $.magnificPopup.open({
                                    items: {
                                        src: '<div class="message" id="message-pop"><div class="check">&#10004;</div><p>Success</p><p>Check your email for a booking confirmation. We will see you soon!</p><a href="javascript:;" class="btn primary-btn back" onclick="return $.magnificPopup.close();">OK</a></div>', // can be a HTML string, jQuery object, or CSS selector
                                        type: 'inline'
                                    },
                                    showCloseBtn: false,
                                    callbacks: {
                                        open: function () {
                                            var $popC = this.content;

                                            setTimeout(function () {
                                                $popC.addClass('comein');
                                                $popC.find('.check').addClass('scaledown');
                                            }, 1);
                                        },
                                        close: function () {
                                            var $popC = this.content;

                                            $popC.removeClass('comein');
                                            $popC.find('.check').removeClass('scaledown');

                                        }

                                    }
                                });
                                $('#contactne input, #contactne textarea').val('');

                            } else {
                                alert(result.message);
                            }
                        },
                    }).always(function () {
                        $sent_btn.removeClass('sendding').html('Xem demo');
                    });

                });
                $('.call-popup-ytvideo').on('click', function (e) {
                    e.preventDefault();
                    var ytid = $(this).attr('data-yt-id');
                    ytid = ytid != '' ? ytid : 'O6QY9RpPwME';
                    $.magnificPopup.open({
                        items: {
                            src: '<div class="popup-cont"></div>', // can be a HTML string, jQuery object, or CSS selector
                        },
                        mainClass: 'video-popup',
                        callbacks: {
                            open: function () {
                                var $popC = this.content;
                                var htmlString = '<div class="video-holder"><iframe width="560" height="315" src="https://www.youtube.com/embed/' + ytid + '?autoplay=1" frameborder="0" allow="autoplay; encrypted-media" allowfullscreen></iframe></div>';
                                $popC.prepend(htmlString);

                            },
                            close: function () {
                                var $popC = this.content;
                                $popC.html('');
                            }
                        }
                    });

                });


                if ($('#slider-duan').length) {
                    var rows = 1;
                    if ($(window).innerWidth() < 600) {
                        rows = 1;
                    }
                    $('#slider-duan').slick({
                        dots: false,
                        arrows: false,
                        infinite: true,
                        speed: 300,
                        rows: rows,
                        slidesToShow: 1,
                        slidesToScroll: 1,
                        centerMode: true,
                        variableWidth: true,
                        centerPadding: '0px',
                        responsive: [
                            {
                                breakpoint: 768,
                                settings: {
                                    variableWidth: false,

                                }
                            }
                        // You can unslick at a given breakpoint now by adding:
                        // settings: "unslick"
                        // instead of a settings object
                        ]
                    });
                }
            });
            function linktosignin() {
                window.location.href = "/dang-nhap";
            }
        </script>
    </form>
</body>
</html>
