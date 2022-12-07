<%@ Page Language="C#" MasterPageFile="~/UserMasterNew.Master" AutoEventWireup="true" CodeBehind="chi-tiet-don-hang.aspx.cs" Inherits="NHST.chi_tiet_don_hang" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<%@ Import Namespace="MB.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .bronze.darken-2 {
            background: #e6cb78;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="main">
        <div class="row">
            <div class="content-wrapper-before blue-grey lighten-5"></div>
            <div class="col s12">
                <div class="container">
                    <div class="all">
                        <div class="card-panel mt-3 no-shadow">
                            <div class="row">
                                <div class="col s12">
                                    <div class="page-title mt-2 center-align">
                                        <h4>
                                            <asp:Literal runat="server" ID="ltrMainOrderID"></asp:Literal></h4>
                                    </div>
                                    <div class="waitting" style="display: none">
                                        <div class="all">
                                            <div class="wait__hd">
                                                <asp:Literal ID="ltrstep" runat="server"></asp:Literal>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="order-panels" style="display: none">
                                    <asp:Literal ID="ltrOrderFee" runat="server"></asp:Literal>
                                </div>
                                <div class="col s12 map-view">
                                    <div class="map-container">
                                        <div class="map-view-custom" id="map"></div>
                                        <div class="map-view-info">
                                            <div class="view-info-wrap">
                                                <asp:Literal runat="server" ID="ltrSmallInfo"></asp:Literal>
                                                <%--   <div class="info-top">
                                                    <span class="bill">Mã vận đơn: <span class="bold black-text code">445111235</span></span>
                                                    <span class="status incoming">Mới đặt</span>
                                                </div>
                                                <div class="info-top">
                                                    <span class="bill">Mã vận đơn: <span class="bold black-text code">445111235</span></span>
                                                    <span class="status incoming">Đã về kho TQ</span>
                                                </div>
                                                <div class="info-top">
                                                    <span class="bill">Mã vận đơn: <span class="bold black-text code">445111235</span></span>
                                                    <span class="status incoming">Đã nhận hàng tại kho đích</span>
                                                </div>--%>
                                                <div class="info-bottom">
                                                    <div class="from-to-location">
                                                        <asp:Literal runat="server" ID="ltrTQ"></asp:Literal>

                                                        <div class="icon">
                                                            <i class="material-icons">arrow_forward</i>
                                                        </div>
                                                        <asp:Literal runat="server" ID="ltrVN"></asp:Literal>

                                                    </div>
                                                    <asp:Literal runat="server" ID="ltrExpectedDate"></asp:Literal>
                                                    <%-- <div class="arrival-info">
                                                        <div class="arrival-note">
                                                            <p>Dự kiến tới nơi</p>
                                                        
                                                        </div>
                                                        <div class="arrival-date">
                                                            <span class="time">25/10/2019</span>
                                                        </div>
                                                    </div>--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="transport-wrap" style="display: none">
                                        <div class="transport-info">
                                            <!-- 3 trang thai:  new , being-transport ,transported -->
                                            <ul class="transport-list active being-transport">
                                                <li class="transport-item">
                                                    <div class="trans-status">
                                                        <span class="lb status">Đang vận chuyển</span>
                                                    </div>
                                                </li>
                                                <li class="transport-item">
                                                    <div class="trans-icons">
                                                        <div class="rectangle">
                                                            <i class="material-icons">airplanemode_active</i>
                                                        </div>
                                                    </div>
                                                </li>
                                                <li class="transport-item">
                                                    <div class="trans-time">
                                                        <span class="lb time">27/09/2019 10:30 AM</span>
                                                    </div>
                                                </li>

                                                <li class="transport-item">
                                                    <div class="trans-note">
                                                        <p>Earth fully zoomed out, and larger zoom levels zoom in at a higher resolution. Specify zoom level as an integer.</p>
                                                    </div>
                                                </li>
                                            </ul>
                                            <ul class="transport-list active transported">
                                                <li class="transport-item">
                                                    <div class="trans-status">
                                                        <span class="lb status">Đã về kho đích</span>
                                                    </div>
                                                </li>
                                                <li class="transport-item">
                                                    <div class="trans-icons">
                                                        <div class="rectangle">
                                                            <i class="material-icons">airplanemode_active</i>
                                                        </div>
                                                    </div>
                                                </li>
                                                <li class="transport-item">
                                                    <div class="trans-time">
                                                        <span class="lb time">27/09/2019 10:30 AM</span>
                                                    </div>
                                                </li>

                                                <li class="transport-item">
                                                    <div class="trans-note">
                                                        <p>The initial resolution at which to display the map is set by the zoom property, as an integer.</p>
                                                    </div>
                                                </li>
                                            </ul>
                                            <ul class="transport-list active new">
                                                <li class="transport-item">
                                                    <div class="trans-status">
                                                        <span class="lb status">Mới đặt hàng</span>
                                                    </div>
                                                </li>
                                                <li class="transport-item">
                                                    <div class="trans-icons">
                                                        <div class="rectangle">
                                                            <i class="material-icons">airplanemode_active</i>
                                                        </div>
                                                    </div>
                                                </li>
                                                <li class="transport-item">
                                                    <div class="trans-time">
                                                        <span class="lb time">27/09/2019 10:30 AM</span>
                                                    </div>
                                                </li>

                                                <li class="transport-item">
                                                    <div class="trans-note">
                                                        <p>The initial resolution at which to display the map is set by the zoom property, where zoom 0 corresponds to a map of the Earth fully zoomed out, and larger zoom levels zoom in at a higher resolution. Specify zoom level as an integer.</p>
                                                    </div>
                                                </li>
                                            </ul>
                                            <ul class="transport-list">
                                                <li class="transport-item">
                                                    <div class="trans-status">
                                                        <span class="lb status">In transit</span>
                                                    </div>
                                                </li>
                                                <li class="transport-item">
                                                    <div class="trans-icons">
                                                        <div class="rectangle">
                                                            <i class="material-icons">airplanemode_active</i>
                                                        </div>
                                                    </div>
                                                </li>
                                                <li class="transport-item">
                                                    <div class="trans-time">
                                                        <span class="lb time">27/09/2019 10:30 AM</span>
                                                    </div>
                                                </li>

                                                <li class="transport-item">
                                                    <div class="trans-note">
                                                        <p>The initial resolution at which to display the map is set by the zoom property, where zoom 0 corresponds to a map of the Earth fully zoomed out, and larger zoom levels zoom in at a higher resolution. Specify zoom level as an integer.</p>
                                                    </div>
                                                </li>
                                            </ul>
                                            <ul class="transport-list">
                                                <li class="transport-item">
                                                    <div class="trans-status">
                                                        <span class="lb status">In transit</span>
                                                    </div>
                                                </li>
                                                <li class="transport-item">
                                                    <div class="trans-icons">
                                                        <div class="rectangle">
                                                            <i class="material-icons">airplanemode_active</i>
                                                        </div>
                                                    </div>
                                                </li>
                                                <li class="transport-item">
                                                    <div class="trans-time">
                                                        <span class="lb time">27/09/2019 10:30 AM</span>
                                                    </div>
                                                </li>

                                                <li class="transport-item">
                                                    <div class="trans-note">
                                                        <p>The initial resolution at which to display the map is set by the zoom property, where zoom 0 corresponds to a map of the Earth fully zoomed out, and larger zoom levels zoom in at a higher resolution. Specify zoom level as an integer.</p>
                                                    </div>
                                                </li>
                                            </ul>
                                            <ul class="transport-list">
                                                <li class="transport-item">
                                                    <div class="trans-status">
                                                        <span class="lb status">In transit</span>
                                                    </div>
                                                </li>
                                                <li class="transport-item">
                                                    <div class="trans-icons">
                                                        <div class="rectangle">
                                                            <i class="material-icons">airplanemode_active</i>
                                                        </div>
                                                    </div>
                                                </li>
                                                <li class="transport-item">
                                                    <div class="trans-time">
                                                        <span class="lb time">27/09/2019 10:30 AM</span>
                                                    </div>
                                                </li>

                                                <li class="transport-item">
                                                    <div class="trans-note">
                                                        <p>The initial resolution at which to display the map is set by the zoom property, where zoom 0 corresponds to a map of the Earth fully zoomed out, and larger zoom levels zoom in at a higher resolution. Specify zoom level as an integer.</p>
                                                    </div>
                                                </li>
                                            </ul>
                                            <ul class="transport-list">
                                                <li class="transport-item">
                                                    <div class="trans-status">
                                                        <span class="lb status">In transit</span>
                                                    </div>
                                                </li>
                                                <li class="transport-item">
                                                    <div class="trans-icons">
                                                        <div class="rectangle">
                                                            <i class="material-icons">airplanemode_active</i>
                                                        </div>
                                                    </div>
                                                </li>
                                                <li class="transport-item">
                                                    <div class="trans-time">
                                                        <span class="lb time">27/09/2019 10:30 AM</span>
                                                    </div>
                                                </li>

                                                <li class="transport-item">
                                                    <div class="trans-note">
                                                        <p>The initial resolution at which to display the map is set by the zoom property, where zoom 0 corresponds to a map of the Earth fully zoomed out, and larger zoom levels zoom in at a higher resolution. Specify zoom level as an integer.</p>
                                                    </div>
                                                </li>
                                            </ul>
                                            <ul class="transport-list">
                                                <li class="transport-item">
                                                    <div class="trans-status">
                                                        <span class="lb status">In transit</span>
                                                    </div>
                                                </li>
                                                <li class="transport-item">
                                                    <div class="trans-icons">
                                                        <div class="rectangle">
                                                            <i class="material-icons">airplanemode_active</i>
                                                        </div>
                                                    </div>
                                                </li>
                                                <li class="transport-item">
                                                    <div class="trans-time">
                                                        <span class="lb time">27/09/2019 10:30 AM</span>
                                                    </div>
                                                </li>

                                                <li class="transport-item">
                                                    <div class="trans-note">
                                                        <p>The initial resolution at which to display the map is set by the zoom property, where zoom 0 corresponds to a map of the Earth fully zoomed out, and larger zoom levels zoom in at a higher resolution. Specify zoom level as an integer.</p>
                                                    </div>
                                                </li>
                                            </ul>
                                            <ul class="transport-list">
                                                <li class="transport-item">
                                                    <div class="trans-status">
                                                        <span class="lb status">In transit</span>
                                                    </div>
                                                </li>
                                                <li class="transport-item">
                                                    <div class="trans-icons">
                                                        <div class="rectangle">
                                                            <i class="material-icons">airplanemode_active</i>
                                                        </div>
                                                    </div>
                                                </li>
                                                <li class="transport-item">
                                                    <div class="trans-time">
                                                        <span class="lb time">27/09/2019 10:30 AM</span>
                                                    </div>
                                                </li>

                                                <li class="transport-item">
                                                    <div class="trans-note">
                                                        <p>The initial resolution at which to display the map is set by the zoom property, where zoom 0 corresponds to a map of the Earth fully zoomed out, and larger zoom levels zoom in at a higher resolution. Specify zoom level as an integer.</p>
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>

                                </div>
                                <div class="order-detail-wrap col s12 section account-sticky mt-2">
                                    <div class="row">
                                        <div class="col s12">
                                            <div class="summary-detail mb-5">
                                                <div class="card-panel z-depth-3">
                                                    <div class="title-header bg-dark-gradient  mb-1">
                                                        <h6 class="white-text ">Tổng quan đơn hàng</h6>
                                                    </div>
                                                    <div class="row">
                                                        <asp:Literal runat="server" ID="ltrOverView"></asp:Literal>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col s12">
                                                            <div class="order-action">
                                                                <div class="display-flex">
                                                                    <div class="action-left">
                                                                        <asp:Literal ID="ltrbtndeposit" runat="server"></asp:Literal><asp:Button ID="btnDeposit" runat="server" CssClass="btn pill-btn primary-btn" Style="display: none" UseSubmitBehavior="false" CausesValidation="false" Text="Đặt cọc" OnClick="btnDeposit_Click" />
                                                                        <asp:Panel ID="pnthanhtoan" runat="server" Visible="false">
                                                                            <a href="javascript:;" class="btn btn-checkout" onclick="payallorder()">Thanh toán</a>
                                                                            <asp:Literal runat="server" ID="ltrYCG"></asp:Literal>
                                                                            <asp:Button ID="btnPayAll" runat="server" CssClass="btn pill-btn primary-btn main-btn hover" UseSubmitBehavior="false" CausesValidation="false" Text="Thanh toán" Style="display: none;" OnClick="btnPayAll_Click" />
                                                                        </asp:Panel>

                                                                    </div>
                                                                    <div class="action-right">

                                                                        <asp:Literal runat="server" ID="ltrCancel"></asp:Literal>
                                                                        <asp:Button ID="btn_cancel" runat="server" CssClass="btn pill-btn primary-btn main-btn hover" UseSubmitBehavior="false" CausesValidation="false" Text="Hủy đơn hàng" Style="display: none;" OnClick="btn_cancel_Click" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <%--    <div class="card-panel">
                                                <div id="order-code" class="section scrollspy mvc-list">
                                                    <div class="title-header bg-dark-gradient">
                                                        <h6 class="white-text ">Mã đơn hàng</h6>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col s12 input-field">
                                                            <asp:TextBox runat="server" Enabled="false" ID="txtMainOrderCode"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="clearfix"></div>
                                                </div>
                                            </div>--%>
                                            <div class="card-panel">
                                                <div id="mvc-list" class="section scrollspy mvc-list">
                                                    <div class="title-header bg-dark-gradient">
                                                        <h6 class="white-text ">Danh sách mã vận đơn</h6>
                                                    </div>
                                                    <div class="responsive-tb">
                                                        <table class="table    highlight bordered  centered   ">
                                                            <thead>
                                                                <tr>
                                                                    <th>Mã vận đơn</th>
                                                                    <th>Cân nặng (kg)</th>
                                                                    <th>Kích thước </br> (D x R x C)</th>
                                                                    <th>Cân quy đổi (kg)</th>
                                                                    <th>Cân tính tiền (kg)</th>
                                                                    <th class="tb-date">Ghi chú</th>
                                                                    <th>Trạng thái</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody class="list-product">
                                                                <asp:Literal runat="server" ID="ltrSmallPackages"></asp:Literal>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card-panel">
                                                <div class="row">
                                                    <div class="col s12 m6">
                                                        <div id="fee-total" class="scrollspy fee-total">
                                                            <div class="title-header bg-dark-gradient">
                                                                <h6 class="white-text ">Thông tin đơn hàng</h6>
                                                            </div>
                                                            <div class="child-fee">
                                                                <div class="title-subheader grey lighten-2">
                                                                    <p class="black-text no-margin font-weight-700">
                                                                        Phí đơn
                                                                    hàng
                                                                    </p>
                                                                </div>
                                                                <div class="content-panel">
                                                                    <div class="fee-wrap">
                                                                        <ul class="list-total m-0">
                                                                            <asp:Literal runat="server" ID="ltrService"></asp:Literal>
                                                                        </ul>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="child-fee">
                                                                <div class="title-subheader grey lighten-2">
                                                                    <p class="black-text no-margin font-weight-700">
                                                                        Total
                                                                    </p>
                                                                </div>
                                                                <div class="content-panel">
                                                                    <div class="fee-wrap">
                                                                        <ul class="list-total m-0">
                                                                            <asp:Literal runat="server" ID="ltrTotal"></asp:Literal>
                                                                        </ul>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="col s12 m6">
                                                        <div class="title-header bg-dark-gradient">
                                                            <h6 class="white-text ">Thông tin người đặt hàng</h6>
                                                        </div>
                                                        <div class="order-owner">
                                                            <table class="table">
                                                                <tbody>
                                                                    <asp:Literal runat="server" ID="ltrBuyerInfo"></asp:Literal>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>

                                                </div>

                                            </div>
                                            <div class="card-panel">
                                                <div id="order-list" class="section scrollspy order-list account-site">
                                                    <div class="title-header bg-dark-gradient">
                                                        <h6 class="white-text ">Danh sách sản phẩm 
                                                            <span><a href="javascript:;" class="btn btnExcel" style="margin-left: 20px; background-color:green">Xuất Excel</a>
                                                            </span>
                                                        </h6>
                                                    </div>
                                                    <div class="order-item">
                                                        <div class="left-info">
                                                            <div class="order-main">
                                                                <asp:Literal runat="server" ID="ltrProducts"></asp:Literal>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <asp:Button runat="server" ID="btnExcel" UseSubmitBehavior="false" Style="display: none" OnClick="btnExcel_Click" />


                                            <div class="card-panel">
                                                <div id="historypay-list" class="section scrollspy history-list">
                                                    <div class="title-header bg-dark-gradient">
                                                        <h6 class="white-text ">Lịch sử thanh toán</h6>
                                                    </div>
                                                    <div class="child-fee">
                                                        <div class="content-panel">
                                                            <div class="responsive-tb">
                                                                <table class="table    highlight bordered  centered   ">
                                                                    <thead>
                                                                        <tr>
                                                                            <th class="tb-date">Ngày thanh toán</th>
                                                                            <th class="tb-date">Loại thanh toán</th>
                                                                            <th class="tb-date">Hình thức thanh toán</th>
                                                                            <th class="tb-date">Số tiền</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody class="list-product">
                                                                        <asp:Repeater ID="rptPayment" runat="server">
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td class="bold black-text"><%#Eval("CreatedDate","{0:dd/MM/yyyy HH:mm}") %></td>
                                                                                    <td><%# PJUtils.ShowStatusPayHistoryUserNew( Eval("Status").ToString().ToInt()) %></td>
                                                                                    <td><%#Eval("Type").ToString() == "1"?"Trực tiếp":"Ví điện tử" %></td>
                                                                                    <td><%#Eval("Amount","{0:N0}") %> VNĐ</td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                        <asp:Literal runat="server" ID="ltrHistoryPay"></asp:Literal>
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>


                                            <div class="card-panel" style="display: none">
                                                <div id="history-list" class="section scrollspy history-list">
                                                    <div class="title-header bg-dark-gradient">
                                                        <h6 class="white-text ">Lịch sử thay đổi</h6>
                                                    </div>
                                                    <div class="child-fee">
                                                        <div class="content-panel">
                                                            <div class="responsive-tb">
                                                                <table class="table highlight bordered  centered ">
                                                                    <thead>
                                                                        <tr>
                                                                            <th class="tb-date">Thời gian</th>
                                                                            <th class="tb-date">Nội dung</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody class="list-product">
                                                                        <asp:Repeater ID="rptHistoryOrderChange" runat="server">
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td class="bold black-text"><%#Eval("CreatedDate","{0:dd/MM/yyyy HH:mm}") %></td>
                                                                                    <td><%#Eval("HistoryContent") %></td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                        <asp:Literal runat="server" ID="ltrHistory"></asp:Literal>
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
                    </div>
                    <div id="contact-chat" class="section chat-fixed hidden">
                        <div class="row">
                            <div class="col s12">
                                <div class="title-header bg-dark-gradient">
                                    <h6 class="white-text ">
                                        <asp:Literal runat="server" ID="ltrChat"></asp:Literal>
                                        <span
                                            class="material-icons right">expand_more</span></h6>
                                </div>
                                <div class="customer-chat">
                                    <div class="chat-wrapper">
                                        <div class="chat-content-area animate fadeUp">
                                            <!-- Chat content area -->
                                            <div class="chat-area">
                                                <div class="chats">
                                                    <div class="chats" id="ContactCustomer">
                                                        <asp:Literal ID="ltrComment" runat="server"></asp:Literal>
                                                    </div>
                                                </div>
                                            </div>
                                            <!--/ Chat content area -->

                                            <!-- Chat footer <-->
                                            <div class="chat-footer">
                                                <div class="preview-upload" id="imgUpload">
                                                </div>
                                                <div class="chat-input">
                                                    <input id="txtComment" placeholder="Type message here.." class="message" />
                                                    <%-- <asp:TextBox runat="server" ID="txtComment" type="text" placeholder="Type message here.."
                                                                class="message"></asp:TextBox>--%>
                                                    <asp:FileUpload runat="server" ID="IMGUpLoadToUS" class="upload-img" type="file" onchange="chatFileUploaded(this)" AllowMultiple="true" title=""></asp:FileUpload>
                                                    <a href="javascript:;" class="upload-file-chat">
                                                        <i class="material-icons">attach_file</i></a>
                                                    <a id="sendnotecomment" class="btn waves-effect waves-light send" onclick="SendMessage()">Send</a>
                                                </div>
                                            </div>
                                            <div class="writebox">
                                                <span class="info-show"></span>
                                            </div>
                                            <!--/ Chat footer -->
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

    <div id="addPackage" class="modal modal-small add-package">
        <div class="modal-hd">
            <span class="right"><i class="material-icons modal-close right-align">clear</i></span>
            <h4 class="no-margin center-align">Yêu cầu giao hàng</h4>
        </div>
        <div class="modal-bd">
            <div class="row">
                <div class="input-field col s12">
                    <asp:TextBox runat="server" ID="txtFullName" placeholder="" class="validate" data-type="text-only"></asp:TextBox>
                    <label for="txtFullName">Họ tên</label>
                </div>
                <div class="input-field col s12 m6">
                    <asp:TextBox runat="server" ID="txtPhone" placeholder="" class="validate" data-type="text-only"></asp:TextBox>
                    <label for="txtPhone" class="active">Số điện thoại</label>
                </div>
                <div class="input-field col s12 m6">
                    <asp:TextBox ID="txtAddress" runat="server" class="validate" placeholder="" data-type="text-only"></asp:TextBox>
                    <label for="txtAddress">Địa chỉ</label>
                </div>

                <div class="input-field col s12 m12">
                    <asp:TextBox ID="txtNote" runat="server" class="validate" placeholder="" data-type="text-only"></asp:TextBox>
                    <label for="txtNote">Ghi chú</label>
                </div>
            </div>
        </div>
        <div class="modal-ft">
            <div class="ft-wrap center-align">
                <a href="javascript:;" onclick="YCGSelected()" class="modal-action btn modal-close waves-effect waves-green mr-2 submit-button">Yêu cầu</a>
                <a href="javascript:;" class="modal-action btn orange darken-2 modal-close waves-effect waves-green ml-2">Hủy</a>
            </div>

        </div>
    </div>

    <asp:Button runat="server" ID="btnYCG" OnClick="btnYCG_Click" UseSubmitBehavior="false" Style="display: none" />
    <asp:HiddenField ID="hdfCommentText" runat="server" />
    <asp:HiddenField ID="hdfShopID" runat="server" />
    <asp:HiddenField ID="hdfOrderID" runat="server" />
    <asp:HiddenField ID="hdfID" runat="server" />
    <asp:HiddenField runat="server" ID="hdfLoadMap" />
    <!-- END: Page Main-->

    <script src="https://maps.googleapis.com/maps/api/js?v=3&key=AIzaSyA-jM_HB6qmua59KRiq2eF6NgKEPr4SumU"></script>
    <!-- <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyA-jM_HB6qmua59KRiq2eF6NgKEPr4SumU&callback=initMap"
    async defer></script> -->
    <script src="/App_Themes/UserNew45/assets/js/markerwithlabel_packed.js"></script>
    <script src="/App_Themes/UserNew45/assets/js/custom/chat.js"></script>
    <script>

        $('.btnExcel').click(function () {
            $('#<%=btnExcel.ClientID%>').click();
        });

        var array2 = $('#<%=hdfLoadMap.ClientID%>').val();
        console.log(array2);
        var data = JSON.parse(array2);
        console.log(data);

        //var wh = [];
        //for (var i = 0; i < data.length; i++) {
        //    var item = data[i];
        //    whs = {
        //        name: item.name,
        //        lat: item.lat,
        //        lng: item.lng

        //    };
        //    wh.push(whs);
        //}
        //console.log(wh);


        var map;
        var warehouses = data;
        //var warehouses = [{
        //    name: 'Kho Hà Nội',
        //    lat: 21.027763,
        //    lng: 105.834160,
        //    package: [
        //        {
        //            code: '33323212',
        //            status: 'Đã về kho đích',
        //            classColor: 'transported'
        //        },
        //        {
        //            code: '65477214',
        //            status: 'Đang vận chuyển',
        //            classColor: 'being-transport'
        //        },
        //        {
        //            code: '39594574',
        //            status: 'Mới tạo',
        //            classColor: 'new'
        //        }
        //    ]
        //}, {
        //    name: 'Kho Nanning TQ',
        //    lat: 22.821930,
        //    lng: 108.318100,
        //    package: [
        //        {
        //            code: '221141',
        //            status: 'Đã về kho đích',
        //            classColor: 'transported'
        //        },
        //        {
        //            code: '7878984',
        //            status: 'Đang vận chuyển',
        //            classColor: 'being-transport'
        //        }
        //    ]
        //}, {
        //    name: 'Kho Hồ Chí Minh',
        //    lat: 10.7553411,
        //    lng: 106.4150407,
        //    package: [
        //        {
        //            code: '33323212',
        //            status: 'Đã về kho đích',
        //            classColor: 'transported'
        //        },
        //        {
        //            code: '65477214',
        //            status: 'Đang vận chuyển',
        //            classColor: 'being-transport'
        //        },
        //        {
        //            code: '39594574',
        //            status: 'Mới tạo',
        //            classColor: 'new'
        //        }, {
        //            code: '65477214',
        //            status: 'Đang vận chuyển',
        //            classColor: 'being-transport'
        //        },
        //        {
        //            code: '39594574',
        //            status: 'Mới tạo',
        //            classColor: 'new'
        //        }
        //    ]
        //}];
        function initMap() {


            map = new google.maps.Map(document.getElementById('map'), {
                center: { lat: parseFloat(warehouses[0].lat), lng: parseFloat(warehouses[0].lng) },
                zoom: 6,
                zoomControl: true
            });
            //Huong chuyen dong

            var lineSymbol = {
                path: google.maps.SymbolPath.CIRCLE,
                scale: 5,
                strokeColor: 'red'
            };

            // Create the polyline and add the symbol to it via the 'icons' property.


            var fromPos = document.getElementById('js-map-from');
            var toPos = document.getElementById('js-map-to');
            var fromToPosArr = [
                {
                    lat: parseFloat(fromPos.getAttribute('data-lat')),
                    lng: parseFloat(fromPos.getAttribute('data-lng')),
                },
                {
                    lat: parseFloat(toPos.getAttribute('data-lat')),
                    lng: parseFloat(toPos.getAttribute('data-lng'))
                }
            ];
            var transporting = new google.maps.Polyline({
                path: fromToPosArr,
                geodesic: true,
                strokeColor: "#0b444c",
                strokeWeight: 4,
                icons: [{
                    icon: lineSymbol,
                    offset: '100%'
                }],

                map: map
            });
            var line = new google.maps.Polyline({
                path: fromToPosArr,
                geodesic: true,
                strokeColor: "#FF0000",
                strokeOpacity: 0.3,
                strokeWeight: 4,
                map: map
            });
            animateCircle(transporting);

            setMarkerAll(map);
        }


        function animateCircle(line) {
            var count = 0;
            window.setInterval(function () {
                count = (count + 1) % 200;

                var icons = line.get('icons');
                icons[0].offset = (count / 2) + '%';
                line.set('icons', icons);
            }, 20);
        }
        function setMarkerAll(map) {
            for (var i = 0; i < warehouses.length; i++) {
                var data = warehouses[i];
                setMarkers(map, data, i)
            }
        }
        function setMarkers(map, data, i) {
            var contentString = this.buildHtmlInfoWindow(data);

            var length = 0;
            if (data.package != null)
                length = data.package.length;

            var infowindow = new google.maps.InfoWindow({
                maxWidth: 300
            });
            infowindow.setContent(contentString);
            // Adds markers to the map.
            // Define image icon
            var image = {
                url: '/App_Themes/UserNew45/assets/images/icon/warehouse.png',
                // This marker is 24 pixels wide by 24 pixels high.
                size: new google.maps.Size(24, 24),
                // The origin for this image is (0, 0).
                origin: new google.maps.Point(0, 0),
                // The anchor for this image is the base of the flagpole at (32, 0).
                anchor: new google.maps.Point(24, 5),
                labelOrigin: new google.maps.Point(0, -5)
            };
            // Shapes define the clickable region of the icon. The type defines an HTML
            // <area> element 'poly' which traces out a polygon as a series of X,Y points.
            // The final coordinate closes the poly by connecting to the first coordinate.
            var shape = {
                coords: [1, 1, 1, 20, 18, 20, 18, 1],
                type: 'poly'
            };

            var marker = new MarkerWithLabel({
                position: { lat: parseFloat(data.lat), lng: parseFloat(data.lng) },
                map: map,
                animation: google.maps.Animation.DROP,
                icon: image,
                shape: shape,
                title: data.name,
                zIndex: i,
                labelContent: length.toString(),
                labelAnchor: new google.maps.Point(0, -5),
                labelClass: "label-count",
                // your desired CSS class
                labelInBackground: true
                // label: {
                // text: data.package.length.toString(),
                // color: "#eb3a44",
                // fontSize: "16px",
                // fontWeight: "bold"
                // }
            });
            marker.addListener('mouseover', function () {
                infowindow.open(map, marker);
            });
            marker.addListener('mouseout', function () {
                infowindow.close(map, marker);
            });
        }


        function buildHtmlInfoWindow(data) {
            var listPackage = [];
            if (data.package != null) {
                for (var i = 0; i < data.package.length; i++) {
                    var package = '<div class="package">' +
                        'Mã <span class="bold red-text code">' + data.package[i].code + '</span>'
                    '</div>';
                    listPackage.push(package);
                }
            }


            var joinString = listPackage.join(' ');

            var content = '<div class="content">' +
                '<p class="name-warehouse">' + data.name + '</p>' +
                '<p>Các mã vận đơn hiện có:</p>' +
                joinString +
                '</div>';



            return content;
        }
        google.maps.event.addDomListener(window, 'load', initMap);
    </script>
    <script>

        $(document).ready(function () {
            $('#txtComment').on("keypress", function (e) {
                if (e.keyCode == 13) {
                    SendMessage();
                }
            });
        });

        $(function () {
            var chat = $.connection.chatHub;
            chat.client.broadcastMessage = function (uid, id, message) {
                var u = $("#<%= hdfID.ClientID%>").val();
                if (uid != u) {
                    var OrderID = $("#<%= hdfOrderID.ClientID%>").val();
                    if (id == OrderID) {
                        $("#ContactCustomer").append(message);
                        loadchat();
                        if ($("#contact-chat").hasClass("hidden")) {
                            let $noti = $('<div class="toast-noti-fixed teal darken-1"><p><span>Bạn có 1 tin nhắn mới từ <span>Hỗ trợ</span></span><a href="javascript:;" class="view-message" data-mess-id="#contact-chat">Xem</a></p></div>');
                            $('body').append($noti);
                            setTimeout(function () {
                                $noti.fadeOut('slow', function () {
                                    $(this).remove();
                                })
                            }, 3000);
                            setTimeout(function () {
                                $('.toast-noti-fixed').addClass('show');
                            }, 100);
                        }


                    }
                }
            };
            $.connection.hub.start().done(function () {

            });
        });
        function SendMessage() {
            var data = new FormData();
            var orderID = $("#<%=hdfOrderID.ClientID%>").val();
            var comment = $("#txtComment").val();
            var files = $("#<%=IMGUpLoadToUS.ClientID%>").get(0).files;
            var url = "";
            var real = "";
            if (files.length > 0) {
                for (var i = 0; i < files.length; i++) {
                    data.append(files[i].name, files[i]);
                }
                data.append("comment", comment);
                data.append("orderID", orderID);
                $.ajax({
                    url: '/HandlerCS.ashx',
                    type: 'POST',
                    data: data,
                    cache: false,
                    contentType: false,
                    processData: false,
                    success: function (file) {
                        if (file.length > 0) {
                            file.forEach(function (data, index) {
                                url += data.name + "|";
                                real += data.realname + "|";
                            });
                            $("#<%=IMGUpLoadToUS.ClientID%>").replaceWith($("#<%=IMGUpLoadToUS.ClientID%>").val('').clone(true));
                            sendmessagetous(orderID, comment, url, real);

                        }
                    },
                    error: function (e) {
                        console.log(e)
                    }
                });

            }
            else {
                sendmessagetous(orderID, comment, url, real);
            }
        }
        function depositOrder() {
            var r = confirm("Bạn muốn đặt cọc đơn hàng này?");
            if (r == true) {
                $("#<%= btnDeposit.ClientID%>").click();
            }
            else {
            }
        }

        function cancelOrder() {
            var r = confirm("Bạn muốn hủy đơn hàng này?");
            if (r == true) {
                $("#<%= btn_cancel.ClientID%>").click();
            }
            else {
            }
        }

        function payallorder() {
            var r = confirm("Bạn muốn thanh toán đơn hàng này?");
            if (r == true) {
                $("#<%= btnPayAll.ClientID%>").click();
            }
            else {
            }
        }

        function YCGSelected() {
            var c = confirm('Bạn muốn yêu cầu giao hàng tất cả đơn hàng đã chọn?');
            if (c == true) {
                var fname = $("#<%=txtFullName.ClientID%>").val();
                var phone = $("#<%=txtPhone.ClientID%>").val();
                var address = $("#<%=txtAddress.ClientID%>").val();
                if (!isEmpty(fname) && !isEmpty(phone) && !isEmpty(address)) {
                    $("#<%=btnYCG.ClientID%>").click();
                }
                else {
                    alert('Vui lòng nhập đầy đủ thông tin');
                }
            }
        }

        function isEmpty(str) {
            return !str.replace(/^\s+/g, '').length; // boolean (`true` if field is empty)
        }
        function sendmessagetous(orderID, comment, url, real) {
            if (isEmpty(comment) && url == "") {
                $(".info-show").html("Vui lòng điền nội dung.").attr("style", "color:red");
            }
            else {
                $(".info-show").html("Đang cập nhật...").attr("style", "color:blue");
                $.ajax({
                    type: "POST",
                    url: "/chi-tiet-don-hang.aspx/PostComment",
                    data: "{commentext:'" + comment + "',shopid:'" + orderID + "',urlIMG:'" + url + "',real:'" + real + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var data = JSON.parse(msg.d);
                        if (data != null) {
                            var dataComment = data.Comment;
                            $("#ContactCustomer").append(dataComment);
                            $("#imgUpload").html("");
                            $(".info-show").html("");
                            $("#txtComment").val("");
                            $('select').formSelect();
                            loadchat();
                        }
                        else {
                            $("#imgUpload").html("");
                            $(".info-show").html("Có lỗi trong quá trình gửi, vui lòng thử lại sau.").attr("style", "color:red");
                        }
                    },
                    error: function (xmlhttprequest, textstatus, errorthrow) {
                        console.log('lỗi checkend');
                    }
                });
            }

        }
        $(window).load(function () {
            var window_height = $('.chat-area').height();
            var document_height = $('.chats').height();
            $('.chat-area').animate({
                scrollTop: window_height + document_height
            }, 1);
            $(".materialboxed").materialbox({
                inDuration: 200,
                onOpenStart: function (element) {
                    $(element).parents('.chat-area.ps').attr('style', 'overflow:visible !important;');
                },
                onCloseStart: function (element) {
                    $(element).parents('.chat-area.ps').attr('style', '');
                }
            });
        });


        function loadchat() {
            var window_height = $('.chat-area').height();
            var document_height = $('.chats').height();
            $('.chat-area').animate({
                scrollTop: window_height + document_height
            }, 1);
            $(".materialboxed").materialbox({
                inDuration: 200,
                onOpenStart: function (element) {
                    $(element).parents('.chat-area.ps').attr('style', 'overflow:visible !important;');
                },
                onCloseStart: function (element) {
                    $(element).parents('.chat-area.ps').attr('style', '');
                }
            });
        }


        $(document).ready(function () {
            $('#contact-chat .title-header').on('click', function () {
                $(this).parents('#contact-chat').toggleClass('hidden');
            });

            if ($(".customer-chat .chat-area").length > 0) {
                var ps_chat_area = new PerfectScrollbar('.customer-chat .chat-area', {
                    theme: "dark"
                });

            }
            var ps_chat_area = new PerfectScrollbar('.map-view .transport-wrap', {
                theme: "dark",
                wheelSpeed: 0.2
            });
            $('.upload-file-chat').on('click', function () {
                $(this).siblings('.upload-img').trigger('click');
            });

        });
    </script>

</asp:Content>
