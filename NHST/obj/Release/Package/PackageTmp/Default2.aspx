<%@ Page Title="" Language="C#" MasterPageFile="~/ClientMaster.Master" AutoEventWireup="true" CodeBehind="Default2.aspx.cs" Inherits="NHST.Default1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="sec panel-sec wow fadeIn" data-wow-duration=".5s" data-wow-delay="0s">
            <img src="/App_Themes/NHST/images/home-panel.png" alt="" class="bg">
            <div class="inner">
                <div class="caption">
                    <p class="captlv1 wow fadeInDown" data-wow-duration="1s" data-wow-delay=".5s">
                        <img src="/App_Themes/NHST/images/capt-txt.png" alt="">
                    </p>
                    <p>
                        <%--<a href="/bieu-phi-hang-order-1/tien-hang-tren-web-5" class="btn primary-btn pill-btn wow fadeInLeft">CHROME</a>
                        <a href="/gioi-thieu-4/ve-chung-toi-14" class="btn secondary-btn pill-btn wow fadeInRight">CỐC CỐC</a>--%>
                        <a href="https://chrome.google.com/webstore/detail/c%C3%B4ng-c%E1%BB%A5-%C4%91%E1%BA%B7t-h%C3%A0ng-c%E1%BB%A7a-nh%E1%BA%ADp/kbnmcpogajoimjgahfjniokjlhhgfknd" class="btn primary-btn pill-btn" target="_blank"><i class="fa fa-chrome" style="margin-right:10px"></i>CHROME</a>
                        <a href="https://chrome.google.com/webstore/detail/c%C3%B4ng-c%E1%BB%A5-%C4%91%E1%BA%B7t-h%C3%A0ng-c%E1%BB%A7a-nh%E1%BA%ADp/kbnmcpogajoimjgahfjniokjlhhgfknd" class="btn secondary-btn pill-btn" target="_blank"><i class="fa fa-chrome" style="margin-right: 10px"></i>CỐC CỐC</a>
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

                    <div class="sec-tt wow fadeInUp" data-wow-duration="1s" data-wow-delay="0s">
                        <h2 class="tt-txt">VỀ <span class="hl-txt">DỊCH VỤ</span></h2>
                        <p class="deco">
                            <img src="/App_Themes/NHST/images/title-deco.png" alt="">
                        </p>
                    </div>
                    <div class="sec-spec">
                        <p class="inner">Vận chuyển đa quốc gia là giải pháp nhập hàng tối ưu cho quý khách. Chúng tôi mang lại cho khách hàng nguồn hàng phong phú với mức giá cực rẻ.</p>
                    </div>
                    <ul class="service-ul">
                        <li>
                            <div class="img wow bounceIn" data-wow-duration="1s" data-wow-delay="0s">
                                <img src="/App_Themes/NHST/images/service-1.png" alt="">
                            </div>
                            <div class="info wow fadeInUp" data-wow-duration="1s" data-wow-delay="0s">
                                <div class="title"><strong>ĐẶT HÀNG, SHIP HÀNG QUỐC TẾ</strong></div>
                                <p class="spec">Đặt hàng từ tất cả các website TMĐT Trung Quốc như taobao, 1688, tmall. </p>
                            </div>
                        </li>
                        <li>
                            <div class="img wow bounceIn" data-wow-duration="1s" data-wow-delay=".3s">
                                <img src="/App_Themes/NHST/images/service-2.png" alt="">
                            </div>
                            <div class="info wow fadeInUp" data-wow-duration="1s" data-wow-delay=".3s">
                                <div class="title"><strong>CHUYỂN HÀNG 2 CHIỀU TRUNG – VIỆT</strong></div>
                                <p class="spec">Vận chuyển hàng hóa từ khắp các tỉnh thành của Trung Quốc về Việt Nam. 
                                 </p>
                            </div>
                        </li>
                        <li>
                            <div class="img wow bounceIn" data-wow-duration="1s" data-wow-delay=".6s">
                                <img src="/App_Themes/NHST/images/service-3.png" alt="">
                            </div>
                            <div class="info wow fadeInUp" data-wow-duration="1s" data-wow-delay=".6s">
                                <div class="title"><strong>CHUYỂN TIỀN, NẠP TIỀN ALIPAY, WECHAT</strong></div>
                                <p class="spec">Thanh toán đơn hàng, nạp tiền tài khoản ngân hàng hoặc Alipay, Wechat, QQ pay.</p>
                            </div>
                        </li>
                        <li>
                            <div class="img wow bounceIn" data-wow-duration="1s" data-wow-delay="0s">
                                <img src="/App_Themes/NHST/images/service-4.png" alt="">
                            </div>
                            <div class="info wow fadeInUp" data-wow-duration="1s" data-wow-delay="0s">
                                <div class="title"><strong>HỖ TRỢ VÀ TƯ VẤN CHUYÊN NGHIỆP</strong></div>
                                <p class="spec">Tư vấn miễn phí. Hỗ trợ tìm kiếm nguồn hàng, hỗ trợ kiểm đếm hàng hóa.</p>
                            </div>
                        </li>
                        <li>
                            <div class="img wow bounceIn" data-wow-duration="1s" data-wow-delay=".3s">
                                <img src="/App_Themes/NHST/images/service-5.png" alt="">
                            </div>
                            <div class="info wow fadeInUp" data-wow-duration="1s" data-wow-delay=".3s">
                                <div class="title"><strong>DỄ DÀNG QUẢN LÝ ĐƠN HÀNG</strong></div>
                                <p class="spec">Hệ thống ví điện tử giúp giao dịch nhanh chóng, tích điểm nhận nhiều ưu đãi.</p>
                            </div>
                        </li>
                        <li>
                            <div class="img wow bounceIn" data-wow-duration="1s" data-wow-delay=".6s">
                                <img src="/App_Themes/NHST/images/service-6.png" alt="">
                            </div>
                            <div class="info wow fadeInUp" data-wow-duration="1s" data-wow-delay=".6s">
                                <div class="title"><strong>GIAO HÀNG TẬN NƠI</strong></div>
                                <p class="spec">Ngồi nhà nhập hàng kinh doanh, tiết kiệm thời gian và chi phí hơn trước.</p>
                            </div>
                        </li>
                    </ul>
                </div>
            </div>

        </div>
        <div class="sec register-step-sec">
            <div class="all">
                <div class="main">
                    <div class="sec-tt wow fadeInUp" data-wow-duration="1s" data-wow-delay="0s">
                        <h2 class="tt-txt">Hướng dẫn <span class="hl-txt">Đăng ký</span></h2>
                        <p class="deco">
                            <img src="/App_Themes/NHST/images/title-deco.png" alt="">
                        </p>
                    </div>
                    <div class="sec-spec">
                        <p class="inner">Nhập hàng kinh doanh dễ dàng chỉ với vài thao tác.</p>
                    </div>
                    <div class="steps register-steps wow fadeInUp" data-wow-duration="1s" data-wow-delay="0s">
                        <div class="step active current" data-toggle="register-steps">
                            <div class="step-img">
                                <img src="/App_Themes/NHST/images/register-step-1.png" alt="">
                            </div>
                            <h4 class="title">Đăng kí tài khoản</h4>
                        </div>
                        <div class="step " data-toggle="register-steps">
                            <div class="step-img">
                                <img src="/App_Themes/NHST/images/register-step-2.png" alt="">
                            </div>
                            <h4 class="title">Cài đặt công cụ mua hàng</h4>
                        </div>
                        <div class="step " data-toggle="register-steps">
                            <div class="step-img">
                                <img src="/App_Themes/NHST/images/register-step-3.png" alt="">
                            </div>
                            <h4 class="title">Chọn hàng và thêm hàng vào giỏ</h4>
                        </div>
                        <div class="step " data-toggle="register-steps">
                            <div class="step-img">
                                <img src="/App_Themes/NHST/images/register-step-4.png" alt="">
                            </div>
                            <h4 class="title">Gửi đơn đặt hàng</h4>
                        </div>
                        <div class="step " data-toggle="register-steps">
                            <div class="step-img">
                                <img src="/App_Themes/NHST/images/register-step-5.png" alt="">
                            </div>
                            <h4 class="title">Đặt cọc tiền hàng</h4>
                        </div>
                        <div class="step " data-toggle="register-steps">
                            <div class="step-img">
                                <img src="/App_Themes/NHST/images/register-step-6.png" alt="">
                            </div>
                            <h4 class="title">Nhận hàng và thanh toán</h4>
                        </div>
                    </div>
                </div>
            </div>
            <div class="step-inner wow fadeInUp" data-wow-duration="1s" data-wow-delay="0s">
                <div class="all">
                    <div class="main">
                        <div class="slider-wrap">
                            <div class="slider-cont" id="step_register_slider">
                                <div class="slider-item">
                                    <div class="inner">
                                        <div class="img">
                                            <img src="/App_Themes/NHST/images/register-img/dang-ky.png" alt="">
                                        </div>
                                        <div class="info">
                                            <div class="title">ĐĂNG KÍ TÀI KHOẢN</div>
                                            <p class="spec">Nhập các thông tin cá nhân bắt buộc vào ô, lưu ý nhập chính xác các thông tin để đảm bảo cho việc quản lí đơn hàng và nhận hàng của bạn.</p>
                                        </div>
                                    </div>
                                </div>
                                <div class="slider-item">
                                    <div class="inner">
                                        <div class="img">
                                            <img src="/App_Themes/NHST/images/register-img/cai-dat.png" alt="">
                                        </div>
                                        <div class="info">
                                            <div class="title">Cài đặt công cụ mua hàng</div>
                                            <p class="spec">Click vào cài đặt công cụ đặt hàng của Vận chuyển đa quốc gia Công cụ hỗ trợ đặt hàng các website taobao, tmall, 1688.</p>
                                        </div>
                                    </div>
                                </div>
                                <div class="slider-item">
                                    <div class="inner">
                                        <div class="img">
                                            <img src="/App_Themes/NHST/images/register-img/chon-hang.png" alt="">
                                        </div>
                                        <div class="info">
                                            <div class="title">Chọn hàng và thêm hàng vào giỏ</div>
                                            <p class="spec">Truy cập vào các trang mua sắm Taobao.com, Tmall.com, 1688.com … chọn hàng và thêm hàng vào giỏ.</p>
                                        </div>
                                    </div>
                                </div>
                                <div class="slider-item">
                                    <div class="inner">
                                        <div class="img">
                                            <img src="/App_Themes/NHST/images/register-img/gui-don-hang.png" alt="">
                                        </div>
                                        <div class="info">
                                            <div class="title">Gửi đơn hàng</div>
                                            <p class="spec">Quay lại website alivietnam.com và kiểm tra giỏ hàng Click vào “Gửi đơn hàng” để tạo đơn hàng,chờ xác nhận đặt hàng thành công.</p>
                                        </div>
                                    </div>
                                </div>
                                <div class="slider-item">
                                    <div class="inner">
                                        <div class="img">
                                            <img src="/App_Themes/NHST/images/register-img/dat-coc.png" alt="">
                                        </div>
                                        <div class="info">
                                            <div class="title">Đặt cọc tiền hàng</div>
                                            <p class="spec">Kiểm tra đơn hàng và đặt cọc tiền hàng qua hình thức chuyển khoản hoặc trực tiếp tại các văn phòng giao dịch gần nhất của Vận chuyển đa quốc gia</p>
                                        </div>
                                    </div>
                                </div>
                                <div class="slider-item">
                                    <div class="inner">
                                        <div class="img">
                                            <img src="/App_Themes/NHST/images/register-img/nhan-hang.png" alt="">
                                        </div>
                                        <div class="info">
                                            <div class="title">Nhận hàng và thanh toán</div>
                                            <p class="spec">Quý khách nhận được thông báo hàng về Việt Nam. Quý khách thanh toán số tiền còn  thiếu qua hình thức chuyển khoản hoặc trực tiếp. Sau khi thanh toán quý khách hàng có thể yêu cầu ship hàng hoặc trực tiếp nhận hàng tại kho của Vận chuyển đa quốc gia</p>
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
                        <li class="wow fadeInLeft" data-wow-duration="1s" data-wow-delay="0s">
                            <div class="img">
                                <img src="/App_Themes/NHST/images/param-img-1.png" alt="">
                            </div>
                            <div class="info">
                                <div class="title"><strong>5</strong></div>
                                <p class="spec">Năm kinh nghiệm</p>
                            </div>
                        </li>
                        <li class="wow bounceIn" data-wow-duration="1s" data-wow-delay=".5s">
                            <div class="img">
                                <img src="/App_Themes/NHST/images/param-img-2.png" alt="">
                            </div>
                            <div class="info">
                                <div class="title"><strong>12,456</strong></div>
                                <p class="spec">Khách Hàng</p>
                            </div>
                        </li>
                        <li class="wow fadeInRight" data-wow-duration="1s" data-wow-delay="0s">
                            <div class="img">
                                <img src="/App_Themes/NHST/images/param-img-3.png" alt="">
                            </div>
                            <div class="info">
                                <div class="title"><strong>85,600</strong></div>
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
                    <div class="sec-tt wow fadeInUp" data-wow-duration="1s" data-wow-delay="0s">
                        <h2 class="tt-txt">Quyền lợi  <span class="hl-txt">khách hàng</span></h2>
                        <p class="deco">
                            <img src="/App_Themes/NHST/images/title-deco.png" alt="">
                        </p>
                    </div>                 
                    <div class="slider-wrap wow fadeInUp customer-benefits" data-wow-duration="1s" data-wow-delay="0s" style="visibility: visible; animation-duration: 1s; animation-delay: 0s; animation-name: fadeInUp;">
                        <div class="customer-benefit-content">
                            <i class="fa fa-compress "></i>
                            <div class="clearfix"></div>
                            <h4 class="title mar-bot1"><a href="/bieu-phi-hang-order-1/cap-do-thanh-vien-24">Khách hàng thân thiết</a></h4>
                            <div class="clearfix"></div>
                            <span style="display:block;min-height:100px">Tích điểm thành viên từ mỗi đơn hàng và nhận được ưu đãi khác cho các đơn hàng sau.
                            </span>
                            <div class="clearfix mar-bot2"></div>
                            <a href="/bieu-phi-hang-order-1/cap-do-thanh-vien-24" class="btn primary-btn pill-btn wow fadeInLeft" style="visibility: visible; animation-name: fadeInLeft;">Xem thêm</a>
                        </div>
                        <div class="customer-benefit-content">
                            <i class="fa fa-trophy"></i>
                            <div class="clearfix"></div>
                            <h4 class="title mar-bot1"><a href="/bieu-phi-hang-order-1/cap-do-thanh-vien-24">Ưu đãi theo sản lượng tháng.</a></h4>
                            <div class="clearfix"></div>
                            <span style="display:block;min-height:100px">Hoàn phí vận chuyển theo sản lượng đặt hàng theo từng tháng.</span>
                            <div class="clearfix mar-bot2"></div>
                            <a href="/bieu-phi-hang-order-1/cap-do-thanh-vien-24" class="btn primary-btn pill-btn wow fadeInLeft" style="visibility: visible; animation-name: fadeInLeft;">Xem thêm</a>
                        </div>
                        <div class="customer-benefit-content">
                            <i class="fa fa-diamond"></i>
                            <div class="clearfix"></div>
                            <h4 class="title mar-bot1"><a href="https://www.facebook.com/nhaphang1688taobao">Marketing &amp; Bán hàng</a></h4>
                            <div class="clearfix"></div>
                            <span style="display:block;min-height:100px">Hỗ trợ Quý Khách kết nối tới các giải pháp Marketing &amp; bán hàng, cung cấp hệ sinh thái đầy đủ từ nhập hàng tới bán hàng và phân phối.</span>
                            <div class="clearfix mar-bot2"></div>
                            <a href="https://www.facebook.com/nhaphang1688taobao" class="btn primary-btn pill-btn wow fadeInLeft" style="visibility: visible; animation-name: fadeInLeft;">Chương trình hợp tác</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="sec contact-sec overflow-y-sec">
            <div class="all">
                <div class="main">

                    <div class="sec-tt wow fadeInUp" data-wow-duration="1s" data-wow-delay="0s">
                        <h2 class="tt-txt">Thông tin <span class="hl-txt">liên hệ</span></h2>
                        <p class="deco">
                            <img src="/App_Themes/NHST/images/title-deco.png" alt="">
                        </p>
                    </div>
                    <%--<div class="sec-spec">
                        <p class="inner">One of the earliest activities we engaged in when we first got into astronomy is the same one we like to</p>
                    </div>--%>
                    <ul class="activity-ul">
                        <li class="hvr-icon-push wow fadeInUp" data-wow-duration="1s" data-wow-delay="0s">
                            <asp:Literal ID="ltr_Email" runat="server"></asp:Literal>

                        </li>
                        <li class="hvr-icon-push wow fadeInUp" data-wow-duration="1s" data-wow-delay=".3s">
                            <a class="img"><i class="fa fa-clock-o"></i></a>
                            <a class="info">
                                <div class="title"><strong>Giờ hoạt động</strong></div>
                                <p>
                                    <asp:Literal ID="ltr_time" runat="server"></asp:Literal></p>
                            </a>
                        </li>
                        <li class="hvr-icon-push wow fadeInUp" data-wow-duration="1s" data-wow-delay=".6s">
                            <asp:Literal ID="ltr_Hotline" runat="server"></asp:Literal>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="map-wrap" id="map-canvas">
            </div>
        </div>
    </main>
</asp:Content>
