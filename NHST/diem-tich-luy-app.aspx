<%@ Page Title="Điểm tích lũy app" Language="C#" MasterPageFile="~/App.Master" AutoEventWireup="true" CodeBehind="diem-tich-luy-app.aspx.cs" Inherits="NHST.diem_tich_luy_app" %>

<asp:Content runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <main id="main-wrap">
        <div class="page-wrap">
            <asp:Panel ID="pnMobile" runat="server" Visible="false">
                <div class="page-body">
                    <div class="all">
                        <div class="white-nooffset-cont account-page">
                            <style>
                                .account-opts-wrap .opts-row .lb {
                                    width: 205px;
                                }
                            </style>
                            <div class="trophy-heading">
                                <img src="/App_Themes/App/images/icon-trophy.png" alt="" class="caption-img">
                                <div class="membership-process">
                                    <p class="lb">Level</p>
                                    <p class="lv">
                                        <i class="fa fa-star hl-txt"></i>
                                        <asp:Label runat="server" ID="lbvip"></asp:Label>
                                    </p>

                                    <div class="bar">
                                        <asp:Literal runat="server" ID="fillVip"></asp:Literal>
                                    </div>
                                    <%--<p class="lb"><i class="fa fa-usd hl-txt"></i>Thêm <span class="hl-txt">150K</span> lên VIP 2</p>--%>
                                </div>
                            </div>
                            <div class="account-opts-wrap trophy-opt">
                                <div class="opts-row">
                                    <p class="lb gray-txt">
                                        <span class="icon">
                                            <img src="/App_Themes/App/images/icon-tag-gray.png" alt=""></span> Đơn thành công
                                    </p>
                                    <p class="txt">
                                        <asp:Literal runat="server" ID="ltrOrderS"></asp:Literal>
                                    </p>
                                </div>
                                <div class="opts-row">
                                    <p class="lb gray-txt">
                                        <span class="icon">
                                            <img src="/App_Themes/App/images/icon-tag-gray.png" alt=""></span> Tổng tiền
                                    </p>
                                    <p class="txt">
                                        <asp:Literal runat="server" ID="ltrMoneyS"></asp:Literal>
                                        VNĐ
                                    </p>
                                </div>
                                <div class="opts-row">
                                    <p class="lb gray-txt">
                                        <span class="icon">
                                            <img src="/App_Themes/App/images/icon-tag-gray.png" alt=""></span> Level
                                    </p>
                                    <p class="txt">
                                        <asp:Literal runat="server" ID="ltrLevel"></asp:Literal>
                                    </p>
                                </div>
                            </div>
                            <div class="gray-seperator"></div>
                            <div class="account-opts-wrap trophy-opt">
                                <div class="opts-row">
                                    <p class="lb gray-txt">
                                        <span class="icon">
                                            <img src="/App_Themes/App/images/icon-tag-gray.png" alt=""></span> Chiết khấu phí mua hàng
                                    </p>
                                    <p class="txt">
                                        <span class="btn primary-btn pill-btn">
                                            <asp:Literal runat="server" ID="ltrBuy"></asp:Literal></span>
                                    </p>
                                </div>
                                <div class="opts-row">
                                    <p class="lb gray-txt">
                                        <span class="icon">
                                            <img src="/App_Themes/App/images/icon-tag-gray.png" alt=""></span> Chiết khấu phí vận chuyển
                                    </p>
                                    <p class="txt">
                                        <span class="btn primary-btn pill-btn">
                                            <asp:Literal runat="server" ID="ltrTrans"></asp:Literal></span>
                                    </p>
                                </div>
                                <div class="opts-row">
                                    <p class="lb gray-txt">
                                        <span class="icon">
                                            <img src="/App_Themes/App/images/icon-tag-gray.png" alt=""></span> Tiền đặt cọc tối thiểu
                                    </p>
                                    <p class="txt">
                                        <span class="btn primary-btn pill-btn">
                                            <asp:Literal runat="server" ID="ltrDep"></asp:Literal></span>
                                    </p>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
                <div class="page-bottom-toolbar">
                </div>
            </asp:Panel>
            <asp:Panel ID="pnShowNoti" runat="server" Visible="false">
                <div class="page-body">
                    <div class="heading-search">
                        <h4 class="page-title">Bạn vui lòng đăng xuất và đăng nhập lại!</h4>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </main>

    <style>
        .pane-primary .heading {
            background-color: #0086da !important;
        }

        .btn.payment-btn {
            background-color: #3f8042;
            color: white;
            padding: 10px;
            height: 40px;
            width: 49%;
        }

        .btn.cancel-btn {
            background-color: #f84a13;
            color: white;
            padding: 10px;
            height: 40px;
            width: 49%;
        }
    </style>

    <script type="text/javascript">

</script>

</asp:Content>

