<%@ Page Title="" Language="C#" MasterPageFile="~/TruongThanhMaster.Master" AutoEventWireup="true" CodeBehind="cong-cu.aspx.cs" Inherits="NHST.cong_cu1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .services {
            background: #f8f8f8;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="home">
        <section class="services clearfix">
            <div class="all">
                <div class="breakcrum">
                    <a href="/" class="brc-home"><i class="fa fa-home"></i>Trang chủ</a>
                    <span class="brc-seperate"><i class="fa fa-arrow-right" style="font-size: 10px;"></i></span>
                    Công Cụ Đặt Hàng
                </div>
                <h4 class="sec__title center-txt">Công cụ đặt hàng</h4>
                <div class="order-tool clearfix">
                    <div class="order-tool-left">
                        <img src="/App_Themes/pdv/assets/images/destop.png" alt="#">
                    </div>
                    <div class="order-tool-right">
                        <h3 class="order-title">ADDON Trường Thành Express SẼ GIÚP BẠN:</h3>
                        <p>1. Tiết kiệm thời gian và tăng cơ hội kinh doanh</p>
                        <p>2. Đặt hàng nhanh chóng, thuận tiện và chính xác</p>
                        <p>3. Form đặt hàng hiển thị sẵn khi vào trang chi tiết</p>
                        <p>4. Hỗ trợ dịch tự động từ tiếng Trung sang tiếng Việt</p>
                        <h3 class="order-title">SỬ DỤNG TRÊN TRÌNH DUYỆT CHROME & CỜ RÔM+ (CỐC CỐC)</h3>
                        <p>
                            Cài đặt nhanh chóng, hạn chế tối đa việc cài đặt lại
							Tự động cập nhật khi có phiên bản mới
                        </p>
                        <p><a href="#">Click để cài đặt</a></p>
                        <div class="tool-setting">
                            <a target="_blank" href="https://chrome.google.com/webstore/detail/công-cụ-đặt-hàng-của-trườ/blkppnlclihndokfgeanceljdemfffbf">
                                <img src="/App_Themes/pdv/assets/images/chrom-set.png" alt="#"></a>
                            <a target="_blank" href="https://chrome.google.com/webstore/detail/công-cụ-đặt-hàng-của-trườ/blkppnlclihndokfgeanceljdemfffbf">
                                <img src="/App_Themes/pdv/assets/images/cococ-set.png" alt="#"></a>
                        </div>
                    </div>
                    <div class="clearfix"></div>
                   <%-- <div class="tool-detail">
                        <p>Sau khi tạo được tài khoản trên Trường Thành Express , khách hàng cần phải cài đặt <span>CÔNG CỤ MUA HÀNG </span>thì mới có thể mua được hàng được trên trang web thương mại điện tử của Trung Quốc. Công cụ này có tính năng ước lượng được mức giá của món hàng và giúp bạn thêm vào rỏ hàng.</p>
                        <h3 class="tool-detail-title">Bước 1</h3>
                        <p>Cài đặt công cụ đặt hàng tự động cho trình duyệt Google Chorme hoặc Cốc Cốc </p>
                        <h3 class="tool-detail-title">Bước 2</h3>
                        <p>Khách hàng click vào dòng chữ <span>“THÊM VÀO CHROME”</span> như trong hình để tải công cụ về máy.</p>
                        <img src="/App_Themes/pdv/assets/images/tool1.jpg" alt="#">
                        <h3 class="tool-detail-title">Bước 3</h3>
                        <p>Tiếp tục click vào chữ <span>“ THÊM TIỆN ÍCH ”</span> như trong hình Và đợi trong vòng 3 giây để việc cài đặt hoàn tất.</p>
                        <img src="/App_Themes/pdv/assets/images/tool2.jpg" alt="#">
                        <p>Khi biểu tượng này xuất hiện ở góc phải của giao diện hoặc trong phần tiện ích thì bạn đã cài đặt thành công công cụ</p>
                        <img src="/App_Themes/pdv/assets/images/tool3.jpg" alt="#">
                    </div>
                    <div class="cmt">
                        <asp:Literal ID="ltrcomment" runat="server"></asp:Literal>
                    </div>--%>
                </div>
            </div>
        </section>
    </main>
    <style>
        .order-tool-left {
            width: 50%;
            float: left;
        }

            .order-tool-left img {
                width: 100%;
                min-height: 342px;
            }

        .order-tool-right {
            width: 50%;
            float: right;
            padding-left: 50px;
        }

        .order-title {
            font-size: 15px;
            color: #404040;
            text-transform: uppercase;
            font-weight: 700;
            margin: 7px 0;
        }

        .order-tool-right p {
            font-size: 15px;
            color: #404040;
            margin: 10px 0;
            width: 80%;
        }

            .order-tool-right p a {
                color: #d92027;
                font-weight: 700;
            }

        .tool-setting {
            margin-top: 40px;
        }

            .tool-setting a {
                float:left;
                padding-right: 20px;
            }

        .tool-detail {
            text-align: center;
            clear:both;
        }

            .tool-detail p {
                color: #3e3c3c;
                font-size: 15px;
                padding: 10px 0;
                line-height: 24px;
                text-align: left;
            }

                .tool-detail p span {
                    text-transform: uppercase;
                }

        .tool-detail-title {
            color: #d92027;
            text-transform: uppercase;
            font-size: 15px;
            font-weight: 700;
            text-align: left;
        }

        .cmt {
            margin-top: 30px;
            width: 100%;
            float: left;
            text-align: left;
        }

            .cmt img {
                width: 100%;
            }
    </style>
</asp:Content>
