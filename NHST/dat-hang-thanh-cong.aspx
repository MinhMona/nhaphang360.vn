<%@ Page Title="" Language="C#" MasterPageFile="~/ClientMaster.Master" AutoEventWireup="true" CodeBehind="dat-hang-thanh-cong.aspx.cs" Inherits="NHST.dat_hang_thanh_cong" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="all">
            <div class="main">
                <div class="sec step-sec">
                    <div class="sec-tt">
                        <h2 class="tt-txt">Đơn hàng</h2>
                        <p class="deco">
                            <img src="/App_Themes/NHST/images/title-deco.png" alt="">
                        </p>
                    </div>
                    <div class="steps">
                        <div class="step ">
                            <div class="step-img">
                                <img src="/App_Themes/NHST/images/order-step-1.png" alt="">
                            </div>
                            <h4 class="title">Giỏ hàng</h4>
                        </div>
                        <div class="step ">
                            <div class="step-img">
                                <img src="/App_Themes/NHST/images/order-step-2.png" alt="">
                            </div>
                            <h4 class="title">Chọn địa chỉ nhận hàng</h4>
                        </div>
                        <div class="step active">
                            <div class="step-img">
                                <img src="/App_Themes/NHST/images/order-step-3.png" alt="">
                            </div>
                            <h4 class="title">Đặt cọc và kết đơn</h4>
                        </div>
                    </div>
                </div>
                <div class="clear"></div>
                <div class="sec gray-area">
                    <div class="sec-tt">
                        <h2 class="tt-txt text-italic">Đặt cọc và kết đơn</h2>
                        <p class="deco">
                            <img src="/App_Themes/NHST/images/title-deco.png" alt="">
                        </p>
                        ok
                    </div>
                </div>
            </div>
        </div>
    </main>

</asp:Content>
