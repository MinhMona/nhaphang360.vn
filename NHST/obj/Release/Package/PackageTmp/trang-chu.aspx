<%@ Page Title="" Language="C#" MasterPageFile="~/ClientMaster.Master" AutoEventWireup="true" CodeBehind="trang-chu.aspx.cs" Inherits="NHST.trang_chu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="sec panel-sec">
            <img src="/App_Themes/NHST/images/home-panel.png" alt="" class="bg">
            <div class="inner">
                <div class="caption">
                    <p class="captlv1">
                        <img src="/App_Themes/NHST/images/capt-txt.png" alt=""></p>
                    <p>
                        <a href="/bang-gia" class="btn primary-btn pill-btn">XEM BẢNG GIÁ</a>
                        <a href="/gioi-thieu" class="btn secondary-btn pill-btn">VỀ CHÚNG TÔI</a>
                    </p>
                </div>

            </div>
            <div class="sec-decor" id="truck-decor">
                <img src="/App_Themes/NHST/images/truck.png" alt="" class="">
            </div>
        </div>
        <div class="sec about-sec">

            <div class="all">
                <div class="main">

                    <div class="sec-tt">
                        <h2 class="tt-txt">VỀ <span class="hl-txt">DỊCH VỤ</span></h2>
                        <p class="deco">
                            <img src="/App_Themes/NHST/images/title-deco.png" alt=""></p>
                    </div>
                    <div class="sec-spec">
                        <p class="inner">One of the earliest activities we engaged in when we first got into astronomy is the same one we like to show our children just as soon as their excitement about the night sky begins to surface.</p>
                    </div>
                    <ul class="service-ul">
                        <li>
                            <div class="img">
                                <img src="/App_Themes/NHST/images/service-1.png" alt=""></div>
                            <div class="info">
                                <div class="title"><strong>Vận chuyển hàng không</strong></div>
                                <p class="spec">Of all of the celestial bodies that capture our attention and fascination as astronomers, none has a greater.</p>
                            </div>
                        </li>
                        <li>
                            <div class="img">
                                <img src="/App_Themes/NHST/images/service-2.png" alt=""></div>
                            <div class="info">
                                <div class="title"><strong>Vận chuyển nội địa</strong></div>
                                <p class="spec">Of all of the celestial bodies that capture our attention and fascination as astronomers, none has a greater.</p>
                            </div>
                        </li>
                        <li>
                            <div class="img">
                                <img src="/App_Themes/NHST/images/service-3.png" alt=""></div>
                            <div class="info">
                                <div class="title"><strong>Vận chuyển quốc tế</strong></div>
                                <p class="spec">Of all of the celestial bodies that capture our attention and fascination as astronomers, none has a greater.</p>
                            </div>
                        </li>
                        <li>
                            <div class="img">
                                <img src="/App_Themes/NHST/images/service-4.png" alt=""></div>
                            <div class="info">
                                <div class="title"><strong>Giao hàng tận nơi</strong></div>
                                <p class="spec">Of all of the celestial bodies that capture our attention and fascination as astronomers, none has a greater.</p>
                            </div>
                        </li>
                        <li>
                            <div class="img">
                                <img src="/App_Themes/NHST/images/service-5.png" alt=""></div>
                            <div class="info">
                                <div class="title"><strong>Bảo mật tuyệt đối</strong></div>
                                <p class="spec">Of all of the celestial bodies that capture our attention and fascination as astronomers, none has a greater.</p>
                            </div>
                        </li>
                        <li>
                            <div class="img">
                                <img src="/App_Themes/NHST/images/service-6.png" alt=""></div>
                            <div class="info">
                                <div class="title"><strong>Cho thuê kho bãi</strong></div>
                                <p class="spec">Of all of the celestial bodies that capture our attention and fascination as astronomers, none has a greater.</p>
                            </div>
                        </li>
                    </ul>
                </div>
            </div>

        </div>
        <div class="sec register-step-sec">
            <div class="all">
                <div class="main">
                    <div class="sec-tt">
                        <h2 class="tt-txt">Hướng dẫn <span class="hl-txt">Đăng ký</span></h2>
                        <p class="deco">
                            <img src="/App_Themes/NHST/images/title-deco.png" alt=""></p>
                    </div>
                    <div class="sec-spec">
                        <p class="inner">One of the earliest activities we engaged in when we first got into astronomy is the same one we like to show our children just as soon as their excitement about the night sky begins to surface.</p>
                    </div>
                    <div class="steps register-steps">
                        <div class="step active current" data-toggle="register-steps">
                            <div class="step-img">
                                <img src="/App_Themes/NHST/images/register-step-1.png" alt=""></div>
                            <h4 class="title">Đăng kí tài khoản</h4>
                        </div>
                        <div class="step " data-toggle="register-steps">
                            <div class="step-img">
                                <img src="/App_Themes/NHST/images/register-step-2.png" alt=""></div>
                            <h4 class="title">Cài đặt công cụ mua hàng</h4>
                        </div>
                        <div class="step " data-toggle="register-steps">
                            <div class="step-img">
                                <img src="/App_Themes/NHST/images/register-step-3.png" alt=""></div>
                            <h4 class="title">Chọn hàng và thêm hàng vào giỏ</h4>
                        </div>
                        <div class="step " data-toggle="register-steps">
                            <div class="step-img">
                                <img src="/App_Themes/NHST/images/register-step-4.png" alt=""></div>
                            <h4 class="title">Gửi đơn đặt hàng</h4>
                        </div>
                        <div class="step " data-toggle="register-steps">
                            <div class="step-img">
                                <img src="/App_Themes/NHST/images/register-step-5.png" alt=""></div>
                            <h4 class="title">Đặt cọc tiền hàng</h4>
                        </div>
                        <div class="step " data-toggle="register-steps">
                            <div class="step-img">
                                <img src="/App_Themes/NHST/images/register-step-6.png" alt=""></div>
                            <h4 class="title">Nhận hàng và thanh toán</h4>
                        </div>
                    </div>
                </div>
            </div>
            <div class="step-inner">
                <div class="all">
                    <div class="main">
                        <div class="slider-wrap">
                            <div class="slider-cont" id="step_register_slider">
                                <div class="slider-item">
                                    <div class="inner">
                                        <div class="img">
                                            <img src="/App_Themes/NHST/images/register-img/dang-ky.png" alt=""></div>
                                        <div class="info">
                                            <div class="title">ĐĂNG KÍ TÀI KHOẢN</div>
                                            <p class="spec">Đăng ký tài khoản đơn giản, xác nhận bằng tin nhắn điện thoải của bạn.</p>
                                        </div>
                                    </div>
                                </div>
                                <div class="slider-item">
                                    <div class="inner">
                                        <div class="img">
                                            <img src="/App_Themes/NHST/images/register-img/cai-dat.png" alt=""></div>
                                        <div class="info">
                                            <div class="title">Cài đặt công cụ mua hàng</div>
                                            <p class="spec">Cài đặt extension cho chrome của Vận chuyển đa quốc gia để mua hàng dễ dàng ngay tại website của taobao.com, 1688.com, tmall.com</p>
                                        </div>
                                    </div>
                                </div>
                                <div class="slider-item">
                                    <div class="inner">
                                        <div class="img">
                                            <img src="/App_Themes/NHST/images/register-img/chon-hang.png" alt=""></div>
                                        <div class="info">
                                            <div class="title">Chọn hàng và thêm hàng vào giỏ</div>
                                            <p class="spec">Sử dụng extension, chọn món hàng bạn muốn tại website TQ, thêm vào giỏ hàng dễ dàng với 1 nút bấm.</p>
                                        </div>
                                    </div>
                                </div>
                                <div class="slider-item">
                                    <div class="inner">
                                        <div class="img">
                                            <img src="/App_Themes/NHST/images/register-img/gui-don-hang.png" alt=""></div>
                                        <div class="info">
                                            <div class="title">Gửi đơn hàng</div>
                                            <p class="spec">Điền các thông tin liên quan như địa chỉ giao hàng, yêu cầu đóng gỗ...etc và gửi đơn hàng đến chúng tôi qua 1 nút kết thúc.</p>
                                        </div>
                                    </div>
                                </div>
                                <div class="slider-item">
                                    <div class="inner">
                                        <div class="img">
                                            <img src="/App_Themes/NHST/images/register-img/dat-coc.png" alt=""></div>
                                        <div class="info">
                                            <div class="title">Đặt cọc tiền hàng</div>
                                            <p class="spec">Chi phí đặt cọc được website tính ra sẵn, bạn có thể chuyển khoản cho admin theo cú pháp đã định sẵn</p>
                                        </div>
                                    </div>
                                </div>
                                <div class="slider-item">
                                    <div class="inner">
                                        <div class="img">
                                            <img src="/App_Themes/NHST/images/register-img/nhan-hang.png" alt=""></div>
                                        <div class="info">
                                            <div class="title">Nhận hàng và thanh toán</div>
                                            <p class="spec">Hàng sẽ chuyển đến tận tay của bạn qua hệ thống giao hàng của Vận chuyển đa quốc gia</p>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>


                    </div>
                </div>
            </div>
        </div>
        <div class="sec param-sec">
            <div class="all">
                <div class="main">
                    <ul class="param-ul">
                        <li>
                            <div class="img">
                                <img src="/App_Themes/NHST/images/param-img-1.png" alt=""></div>
                            <div class="info">
                                <div class="title"><strong>48</strong></div>
                                <p class="spec">Năm kinh nghiệm</p>
                            </div>
                        </li>
                        <li>
                            <div class="img">
                                <img src="/App_Themes/NHST/images/param-img-2.png" alt=""></div>
                            <div class="info">
                                <div class="title"><strong>12,456</strong></div>
                                <p class="spec">Khách Hàng</p>
                            </div>
                        </li>
                        <li>
                            <div class="img">
                                <img src="/App_Themes/NHST/images/param-img-3.png" alt=""></div>
                            <div class="info">
                                <div class="title"><strong>8,560</strong></div>
                                <p class="spec">Đơn hàng</p>
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="sec testimonial-sec overflow-y-sec">
            <div class="all">
                <div class="main">
                    <div class="sec-tt">
                        <h2 class="tt-txt">KHách hàng  nói gì về  <span class="hl-txt">CHúng tôi</span></h2>
                        <p class="deco">
                            <img src="/App_Themes/NHST/images/title-deco.png" alt=""></p>
                    </div>
                    <div class="slider-wrap">
                        <div class="slider-cont slider-quote" id="slider-quote">
                            <div class="slider-item">
                                <div class="quote-box">
                                    <div class="img">
                                        <img src="/App_Themes/NHST/images/avata_thumb.png" alt=""></div>
                                    <div class="info">
                                        <div class="title"><strong>MONA MEDIA</strong>/<span class="hl-txt">Designer</span></div>
                                        <div class="spec">
                                            <blockquote>
                                                <p>
                                                    One of the earliest activities we engaged in when we first got into astronomy is the same one we like to show our children.
That is the fun of finding constellations. But finding constellations and using them to navigate the sky is a discipline that goes back virtually to the dawn of man. In fact, we have cave pictures to show that the more primitive of human societies could “see pictures” in the sky and ascribe to them significance.
                                                </p>
                                            </blockquote>
                                            <p>
                                                <span class="ratting">
                                                    <span><i class="fa fa-star"></i><i class="fa fa-star"></i><i class="fa fa-star"></i><i class="fa fa-star"></i><i class="fa fa-star"></i></span>
                                                    <span class="rate" style="width: 23%"><i class="fa fa-star"></i><i class="fa fa-star"></i><i class="fa fa-star"></i><i class="fa fa-star"></i><i class="fa fa-star"></i></span>
                                                </span>
                                            </p>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- /slider-item -->
                            <div class="slider-item">
                                <div class="quote-box">
                                    <div class="img">
                                        <img src="/App_Themes/NHST/images/avata_thumb.png" alt=""></div>
                                    <div class="info">
                                        <div class="title"><strong>MONA MEDIA</strong>/<span class="hl-txt">Designer</span></div>
                                        <div class="spec">
                                            <blockquote>
                                                <p>
                                                    One of the earliest activities we engaged in when we first got into astronomy is the same one we like to show our children.
That is the fun of finding constellations. But finding constellations and using them to navigate the sky is a discipline that goes back virtually to the dawn of man. In fact, we have cave pictures to show that the more primitive of human societies could “see pictures” in the sky and ascribe to them significance.
                                                </p>
                                            </blockquote>
                                            <p>
                                                <span class="ratting">
                                                    <span><i class="fa fa-star"></i><i class="fa fa-star"></i><i class="fa fa-star"></i><i class="fa fa-star"></i><i class="fa fa-star"></i></span>
                                                    <span class="rate" style="width: 23%"><i class="fa fa-star"></i><i class="fa fa-star"></i><i class="fa fa-star"></i><i class="fa fa-star"></i><i class="fa fa-star"></i></span>
                                                </span>
                                            </p>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- /slider-item -->
                            <div class="slider-item">
                                <div class="quote-box">
                                    <div class="img">
                                        <img src="/App_Themes/NHST/images/avata_thumb.png" alt=""></div>
                                    <div class="info">
                                        <div class="title"><strong>MONA MEDIA</strong>/<span class="hl-txt">Designer</span></div>
                                        <div class="spec">
                                            <blockquote>
                                                <p>
                                                    One of the earliest activities we engaged in when we first got into astronomy is the same one we like to show our children.
That is the fun of finding constellations. But finding constellations and using them to navigate the sky is a discipline that goes back virtually to the dawn of man. In fact, we have cave pictures to show that the more primitive of human societies could “see pictures” in the sky and ascribe to them significance.
                                                </p>
                                            </blockquote>
                                            <p>
                                                <span class="ratting">
                                                    <span><i class="fa fa-star"></i><i class="fa fa-star"></i><i class="fa fa-star"></i><i class="fa fa-star"></i><i class="fa fa-star"></i></span>
                                                    <span class="rate" style="width: 23%"><i class="fa fa-star"></i><i class="fa fa-star"></i><i class="fa fa-star"></i><i class="fa fa-star"></i><i class="fa fa-star"></i></span>
                                                </span>
                                            </p>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- /slider-item -->

                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="sec contact-sec overflow-y-sec">
            <div class="all">
                <div class="main">

                    <div class="sec-tt">
                        <h2 class="tt-txt">Thông tin <span class="hl-txt">liên hệ</span></h2>
                        <p class="deco">
                            <img src="/App_Themes/NHST/images/title-deco.png" alt=""></p>
                    </div>
                    <div class="sec-spec">
                        <p class="inner">One of the earliest activities we engaged in when we first got into astronomy is the same one we like to</p>
                    </div>
                    <ul class="activity-ul">
                        <li class="hvr-icon-push">
                            <a class="img" href="mailto:myinfo@mona-media.com"><i class=" fa fa-envelope"></i></a>
                            <a class="info" href="mailto:myinfo@mona-media.com">
                                <div class="title"><strong>Email Contact</strong></div>
                                <p>myinfo@mona-media.com</p>
                            </a>
                        </li>
                        <li class="hvr-icon-push">
                            <a class="img"><i class=" fa fa-clock-o"></i></a>
                            <a class="info">
                                <div class="title"><strong>Giờ hoạt động</strong></div>
                                <p>06:00 am - 21:00 pm</p>
                            </a>
                        </li>
                        <li class="hvr-icon-push">
                            <a class="img" href="tel:+84908555555"><i class="fa fa-phone"></i></a>
                            <a class="info" href="tel:+84908555555">
                                <div class="title"><strong>HOtline</strong></div>
                                <p>0908 - 555 -555</p>
                            </a>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="map-wrap" id="map-canvas">
            </div>
        </div>
    </main>
</asp:Content>
