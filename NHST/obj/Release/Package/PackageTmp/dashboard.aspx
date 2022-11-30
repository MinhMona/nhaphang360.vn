<%@ Page Language="C#" MasterPageFile="~/UserMasterNew.Master" AutoEventWireup="true" CodeBehind="dashboard.aspx.cs" Inherits="NHST.dashboard" %>

<asp:Content runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="main">
        <div class="row">
            <div class="content-wrapper-before blue-grey lighten-5"></div>
            <div class="col s12">
                <div class="container">
                    <div class="all">
                        <div class="user-dash-wrap">
                            <div class="row">
                                <div class="col s12">
                                    <div class="page-title mt-2 center-align">
                                        <h4>Dashboard</h4>
                                    </div>
                                </div>
                                <div class="col s12 m6 l12">
                                    <a class="black-text" href="/danh-sach-don-hang?t=1">
                                        <div class="card z-depth-2 gradient-45deg-amber-amber faq-card">
                                            <div class="card-left">
                                                <i class="material-icons dp48 white-text">nature_people</i>
                                                <h6 class="font-weight-500 black-text">Mua hàng hộ</h6>
                                            </div>
                                            <div class="card-right center-align">
                                                <h4 class="white-text">
                                                    <asp:Literal runat="server" ID="ltrTotalOrder"></asp:Literal></h4>
                                                <p class="font-weight-500">Đơn hàng</p>
                                            </div>
                                        </div>
                                    </a>
                                </div>

                                <div class="col s12 m6 l3" style="display:none;">
                                    <a class="black-text" href="/danh-sach-don-hang?t=3">
                                        <div class="card z-depth-2 gradient-45deg-amber-amber faq-card">
                                            <div class="card-left">
                                                <i class="material-icons dp48 white-text">nature_people</i>
                                                <h6 class="font-weight-500 black-text">Mua hàng hộ khác</h6>
                                            </div>
                                            <div class="card-right center-align">
                                                <h4 class="white-text">
                                                    <asp:Literal runat="server" ID="ltrOrderOther"></asp:Literal></h4>
                                                <p class="font-weight-500">Đơn hàng</p>
                                            </div>
                                        </div>
                                    </a>
                                </div>


                                <div class="col s12 m6 l3" style="display:none;">
                                    <a class="black-text" href="/danh-sach-kien-yeu-cau-ky-gui">
                                        <div class="card z-depth-2 gradient-45deg-purple-amber faq-card">

                                            <div class="card-left">
                                                <i class="material-icons dp48 white-text">next_week</i>
                                                <h6 class="font-weight-500 black-text">Vận chuyển ký gửi</h6>
                                            </div>
                                            <div class="card-right center-align">
                                                <h4 class="white-text">
                                                    <asp:Literal runat="server" ID="ltrTotalTran"></asp:Literal></h4>
                                                <p class="font-weight-500">Đơn hàng</p>
                                            </div>
                                        </div>
                                    </a>
                                </div>
                                <div class="col s12 m6 l3" style="display:none;">
                                    <a class="black-text" href="/danh-sach-thanh-toan-ho">
                                        <div class="card z-depth-2 gradient-45deg-cyan-light-green faq-card">

                                            <div class="card-left">
                                                <i class="material-icons dp48 white-text">payment</i>
                                                <h6 class="font-weight-500 black-text">Thanh toán hộ</h6>
                                            </div>
                                            <div class="card-right center-align">
                                                <h4 class="white-text">
                                                    <asp:Literal runat="server" ID="ltrPayhelp"></asp:Literal></h4>
                                                <p class="font-weight-500">Đơn hàng</p>
                                            </div>
                                        </div>
                                    </a>
                                </div>
                            </div>
                            <div class="row section">
                                <div class="col s12">
                                    <div class="card-panel">
                                        <h5 class="black-text mb-2">Đơn hàng đặt hàng hộ gần đây</h5>
                                        <div class="responsive-tb">
                                            <table class="table    highlight bordered  centered">
                                                <thead>
                                                    <tr>
                                                        <th>ID</th>
                                                        <th>Tổng tiền</th>
                                                        <th>Số tiền phải cọc</th>
                                                        <th>Số tiền đã cọc</th>
                                                        <th class="tb-date">Ngày đặt</th>
                                                        <th>Trạng thái</th>
                                                        <th>Thao tác</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Literal runat="server" ID="ltrListOrder"></asp:Literal>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>

                            </div>


                            <div class="row section" style="display:none;">
                                <div class="col s12">
                                    <div class="card-panel">
                                        <h5 class="black-text mb-2">Đơn hàng đặt hàng hộ khác gần đây</h5>
                                        <div class="responsive-tb">
                                            <table class="table    highlight bordered  centered">
                                                <thead>
                                                    <tr>
                                                        <th>ID</th>
                                                        <th>Tổng tiền</th>
                                                        <th>Số tiền phải cọc</th>
                                                        <th>Số tiền đã cọc</th>
                                                        <th class="tb-date">Ngày đặt</th>
                                                        <th>Trạng thái</th>
                                                        <th>Thao tác</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Literal runat="server" ID="ltrListOther"></asp:Literal>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>

                            </div>






                            <div class="row section" style="display:none;">
                                <div class="col s12">
                                    <div class="card-panel">
                                        <h5 class="black-text mb-2">Đơn hàng thanh toán hộ gần đây</h5>
                                        <div class="responsive-tb">
                                            <table class="table highlight bordered  centered">
                                                <thead>
                                                    <tr>
                                                        <th>ID</th>
                                                        <th class="tb-date">Ngày gửi</th>
                                                        <th>Tổng tiền (¥)</th>
                                                        <th>Tổng tiền (VNĐ)</th>
                                                        <th>Tỉ giá</th>
                                                        <th>Trạng thái</th>
                                                        <th>Thao tác</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Literal runat="server" ID="ltrListPayHelp"></asp:Literal>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>

                            </div>

                            <div class="row section" style="display:none;">
                                <div class="col s12">
                                    <div class="card-panel">
                                        <h5 class="black-text mb-2">Đơn hàng vận chuyển hộ gần đây</h5>
                                        <div class="responsive-tb">
                                            <table class="table    highlight bordered  centered">
                                                <thead>
                                                    <tr>
                                                        <th>ID</th>
                                                        <th>Mã vận đơn</th>
                                                        <th>Trọng lượng</th>
                                                        <th>Trạng thái</th>
                                                        <th class="tb-date">Ngày đặt</th>
                                                        <th>Thao tác</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Literal runat="server" ID="ltrListTran"></asp:Literal>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
