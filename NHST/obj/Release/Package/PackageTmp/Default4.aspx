<%@ Page Language="C#" MasterPageFile="~/MinhAnMaster.Master" AutoEventWireup="true" CodeBehind="Default4.aspx.cs" Inherits="NHST.Default2" %>

<asp:Content runat="server" ContentPlaceHolderID="head"></asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <main class="main">
        <div class="search-tracking-section sec wow fadeIn" data-wow-duration="1s" data-wow-delay="0s">
            <div class="box-img">
                <img src="/App_Themes/MinhAn/images/truck.jpg" alt="">
            </div>
            <div class="search-tracking-tab tab-wrapper">
                <div class="search-tracking-nav">
                    <ul>
                        <li class="tab-link" data-tab="#search">
                            <div class="icon">
                                <img src="/App_Themes/MinhAn/images/search.png" alt="">
                                <img src="/App_Themes/MinhAn/images/search-active.png" alt="" class="img-active">
                            </div>
                            Tìm kiếm sản phẩm
                        </li>
                        <li class="tab-link" data-tab="#tracking">
                            <div class="icon">
                                <img src="/App_Themes/MinhAn/images/tracking.png" alt="">
                                <img src="/App_Themes/MinhAn/images/tracking-active.png" alt="" class="img-active">
                            </div>
                            Tra cứu mã vận đơn
                        </li>
                    </ul>
                    <a href="#" class="main-btn"><i class="fa fa-cog" aria-hidden="true"></i>Cài đặt công cụ</a>
                </div>
                <div class="search-tracking-content-wrapper">
                    <div id="search" class="search-tracking-content tab-content">
                        <select class="select" id="brand-source">
                            <option value="taobao">TAOBAO</option>
                            <option value="tmall">Tmall</option>
                            <option value="1688">1688</option>
                        </select>
                        <asp:TextBox runat="server" ID="txtSearch" CssClass="input txtsearchfield" placeholder="Tìm kiếm sản phẩm"></asp:TextBox>
                        <%--   <input class="input" type="text" placeholder="Tìm kiếm sản phẩm">--%>
                        <a href="javascript:;" onclick="searchProduct()" class="search-btn"><i class="fa fa-search" aria-hidden="true"></i></a>
                        <asp:Button ID="btnSearch" runat="server"
                            OnClick="btnSearch_Click" Style="display: none"
                            OnClientClick="document.forms[0].target = '_blank';" UseSubmitBehavior="false" />
                    </div>
                    <div id="tracking" class="search-tracking-content tab-content">
                        <input id="txtMVD" class="input" type="text" placeholder="Nhập sản phẩm">
                        <a href="javascript:;" onclick="searchCode()" class="search-btn"><i class="fa fa-search" aria-hidden="true"></i></a>
                    </div>
                </div>
            </div>
        </div>
        <section class="step-section sec">
            <div class="container wow zoomIn" data-wow-duration="1s" data-wow-delay="0s">
                <h2 class="main-title">Quy trình đặt hàng</h2>
                <div class="step-list">
                    <div class="columns">
                        <asp:Literal runat="server" ID="ltrStep1"></asp:Literal>
                        <%-- <div class="column">
                            <div class="step-item">
                                <div class="icon">
                                    <img src="/App_Themes/MinhAn/images/step-1.png" alt="">
                                </div>
                                <p class="title">Bước 1</p>
                                <p class="desc">Lorem ipsum dolor sit amet, consectetur adipiscing elit</p>
                            </div>
                        </div>
                        <div class="column">
                            <div class="step-item">
                                <div class="icon">
                                    <img src="/App_Themes/MinhAn/images/step-2.png" alt="">
                                </div>
                                <p class="title">Bước 2</p>
                                <p class="desc">Lorem ipsum dolor sit amet, consectetur adipiscing elit</p>
                            </div>
                        </div>
                        <div class="column">
                            <div class="step-item">
                                <div class="icon">
                                    <img src="/App_Themes/MinhAn/images/step-3.png" alt="">
                                </div>
                                <p class="title">Bước 3</p>
                                <p class="desc">Lorem ipsum dolor sit amet, consectetur adipiscing elit</p>
                            </div>
                        </div>
                        <div class="column">
                            <div class="step-item">
                                <div class="icon">
                                    <img src="/App_Themes/MinhAn/images/step-4.png" alt="">
                                </div>
                                <p class="title">Bước 3</p>
                                <p class="desc">Lorem ipsum dolor sit amet, consectetur adipiscing elit</p>
                            </div>
                        </div>
                        <div class="column">
                            <div class="step-item">
                                <div class="icon">
                                    <img src="/App_Themes/MinhAn/images/step-5.png" alt="">
                                </div>
                                <p class="title">Bước 5</p>
                                <p class="desc">Lorem ipsum dolor sit amet, consectetur adipiscing elit</p>
                            </div>
                        </div>--%>
                    </div>
                </div>
                <div class="step-btn-box">
                    <a href="/chuyen-muc/huong-dan/huong-dan-tao-don-hang-thong-qua-cong-cu-dat-hang" target="_blank" class="main-btn more-btn">Tìm hiểu thêm</a>
                    <a href="/dang-ky" target="_blank" class="main-btn register-btn">Đăng ký ngay</a>
                </div>
            </div>
        </section>
        <section class="service-section sec">
            <div class="container">
                <h2 class="main-title">Service</h2>
                <div class="service-content-wrapper">
                    <div class="service-list">
                        <asp:Literal runat="server" ID="ltrService"></asp:Literal>
                        <%--  <div class="service-item wow fadeInLeft" data-wow-duration="1s" data-wow-delay="0s">
                            <div class="service-icon">
                                <img src="/App_Themes/MinhAn/images/icon-1.png" alt="">
                            </div>
                            <p class="service-title">Đặt hàng Taobao,1688,tmall</p>
                            <p class="service-desc">
                                Lorem ipsum dolor sit amet, consectetur adipiscing elitsed do
                                eiusmod tempor incididunt ut labore et dolore magna aliqua.
                            </p>
                        </div>
                        <div class="service-item wow fadeInRight" data-wow-duration="1s" data-wow-delay="0s">
                            <div class="service-icon">
                                <img src="/App_Themes/MinhAn/images/icon-2.png" alt="">
                            </div>
                            <p class="service-title">Giao dịch tiền tệ</p>
                            <p class="service-desc">
                                Lorem ipsum dolor sit amet, consectetur adipiscing elitsed do
                                eiusmod tempor incididunt ut labore et dolore magna aliqua.
                            </p>
                        </div>
                        <div class="service-item wow fadeInLeft" data-wow-duration="1s" data-wow-delay=".2s">
                            <div class="service-icon">
                                <img src="/App_Themes/MinhAn/images/icon-3.png" alt="">
                            </div>
                            <p class="service-title">Dịch vụ thanh toán hộ</p>
                            <p class="service-desc">
                                Lorem ipsum dolor sit amet, consectetur adipiscing elitsed do
                                eiusmod tempor incididunt ut labore et dolore magna aliqua.
                            </p>
                        </div>
                        <div class="service-item wow fadeInRight" data-wow-duration="1s" data-wow-delay=".2s">
                            <div class="service-icon">
                                <img src="/App_Themes/MinhAn/images/icon-4.png" alt="">
                            </div>
                            <p class="service-title">Dịch vụ ký gửi hàng hoá</p>
                            <p class="service-desc">
                                Lorem ipsum dolor sit amet, consectetur adipiscing elitsed do
                                eiusmod tempor incididunt ut labore et dolore magna aliqua.
                            </p>
                        </div>
                        <div class="service-item wow fadeInLeft" data-wow-duration="1s" data-wow-delay=".4s">
                            <div class="service-icon">
                                <img src="/App_Themes/MinhAn/images/icon-5.png" alt="">
                            </div>
                            <p class="service-title">Vận chuyển, xuất nhập khẩu chính ngạch 2 chiều</p>
                            <p class="service-desc">
                                Lorem ipsum dolor sit amet, consectetur adipiscing elitsed do
                                eiusmod tempor incididunt ut labore et dolore magna aliqua.
                            </p>
                        </div>
                        <div class="service-item wow fadeInRight" data-wow-duration="1s" data-wow-delay=".4s">
                            <div class="service-icon">
                                <img src="/App_Themes/MinhAn/images/icon-6.png" alt="">
                            </div>
                            <p class="service-title">Tìm nguồn hàng, máy móc, dây truyền sản xuất tại Trung Quốc </p>
                            <p class="service-desc">
                                Lorem ipsum dolor sit amet, consectetur adipiscing elitsed do
                                eiusmod tempor incididunt ut labore et dolore magna aliqua.
                            </p>
                        </div>--%>
                    </div>
                    <div class="service-bg wow zoomIn" data-wow-duration="1s" data-wow-delay="0s">
                        <img src="/App_Themes/MinhAn/images/service-bg.png" alt="">
                    </div>
                </div>
            </div>
        </section>
        <section class="customer-benefit-section sec">
            <div class="container">
                <h2 class="main-title wow fadeIn" data-wow-duration="1s" data-wow-delay="0s">Quyền lợi khách hàng</h2>
                <p class="section-intro wow fadeIn" data-wow-duration="1s" data-wow-delay="0s">
                    You have finished building your own website. You have introduced your company
                    and presented your products and services. You have added...
                </p>
                <div class="benefit-list">
                    <div class="columns">
                        <asp:Literal runat="server" ID="ltrQL1"></asp:Literal>
                        <%--  <div class="column wow fadeInRight" data-wow-duration="1s" data-wow-delay="0s">
                            <div class="benefit-item">
                                <div class="benefit-icon">
                                    <img src="/App_Themes/MinhAn/images/bn-icon.png" alt="">
                                </div>
                                <p class="benefit-title">Khách hàng thân thiết</p>
                                <p class="benefit-desc">
                                    Like the majority of sales people, I visit a huge number of
                                    clients and prospects every month, some end up...
                                </p>
                                <a href="#" class="readmore-btn">Xem thêm <i class="fa fa-arrow-circle-right"
                                    aria-hidden="true"></i></a>
                            </div>
                        </div>
                        <div class="column wow fadeInRight" data-wow-duration="1s" data-wow-delay=".2s">
                            <div class="benefit-item">
                                <div class="benefit-icon">
                                    <img src="/App_Themes/MinhAn/images/bn-icon-2.png" alt="">
                                </div>
                                <p class="benefit-title">Ưu đãi theo sản lượng</p>
                                <p class="benefit-desc">
                                    Like the majority of sales people, I visit a huge number of
                                    clients and prospects every month, some end up...
                                </p>
                                <a href="#" class="readmore-btn">Xem thêm <i class="fa fa-arrow-circle-right"
                                    aria-hidden="true"></i></a>
                            </div>
                        </div>
                        <div class="column wow fadeInRight" data-wow-duration="1s" data-wow-delay=".4s">
                            <div class="benefit-item">
                                <div class="benefit-icon">
                                    <img src="/App_Themes/MinhAn/images/bn-icon-3.png" alt="">
                                </div>
                                <p class="benefit-title">Marketing & bán hàng</p>
                                <p class="benefit-desc">
                                    Like the majority of sales people, I visit a huge number of
                                    clients and prospects every month, some end up...
                                </p>
                                <a href="#" class="readmore-btn">Xem thêm <i class="fa fa-arrow-circle-right"
                                    aria-hidden="true"></i></a>
                            </div>
                        </div>--%>
                    </div>
                </div>
            </div>
        </section>
        <div class="info-section sec">
            <div class="container wow zoomIn" data-wow-duration="1s" data-wow-delay="0s">
                <div class="info-list">
                    <div class="columns">
                        <div class="column">
                            <div class="info-item">
                                <p class="title">Địa chỉ:</p>
                                <asp:Literal ID="ltrAddress" runat="server"></asp:Literal>
                            </div>
                        </div>
                        <div class="column">
                            <div class="info-item">
                                <p class="title">SỐ điện thoại:</p>
                                <asp:Literal ID="ltrHotline" runat="server"></asp:Literal>
                            </div>
                        </div>
                        <div class="column">
                            <div class="info-item">
                                <p class="title">Email:</p>
                                <asp:Literal ID="ltrEmail" runat="server"></asp:Literal>
                            </div>
                        </div>
                        <div class="column">
                            <div class="info-item">
                                <p class="title">GIờ làm việc:</p>
                                <asp:Literal ID="ltrTimeWork" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="map-section sec">
            <iframe
                src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d89973.99884337359!2d106.68236339019145!3d10.786877403112605!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x31752ed189fa855d%3A0xf63e15bfce46baef!2sC%C3%B4ng%20ty%20TNHH%20-%20MONA%20MEDIA!5e0!3m2!1svi!2s!4v1568821941466!5m2!1svi!2s"
                frameborder="0" style="border: 0;" allowfullscreen=""></iframe>
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

        function searchCode() {
            var code = $("#txtMVD").val();
            if (isEmpty(code)) {
                alert('Vui lòng nhập mã vận đơn.');
            }
            else {
                $.ajax({
                    type: "POST",
                    url: "/theo-doi-mvd.aspx/getInfo",
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
            background: #961b4e;
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
            background: #961b4e;
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
                background: #6692a5;
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
