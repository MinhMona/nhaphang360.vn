<%@ Page Title="" Language="C#" MasterPageFile="~/ClientMaster.Master" AutoEventWireup="true" CodeBehind="chi-tiet-trang.aspx.cs" Inherits="NHST.chi_tiet_trang" %>

<%--<%@ Register Src="~/Including/uMenuSidebar.ascx" TagName="uMenuSidebar" TagPrefix="uc" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="sec sidebar-page-sec">
            <div class="all">
                <div class="main">
                    <div class="breadcrumb">
                        <asp:Literal ID="ltrBrea" runat="server"></asp:Literal>
                        <%--<ul>
                            <li><a href="#">Trang chủ</a></li>
                            <li>-</li>
                            <li class="current"><a href="#">Biểu phí</a></li>
                        </ul>--%>
                    </div>
                    <%--<uc:uMenuSidebar ID="uMenuSidebar1" runat="server" />--%>
                    <aside class="side-bar-cont">
                        <asp:Literal ID="ltrMenu" runat="server"></asp:Literal>
                    <%--<div class="panel on">
                            <div class="panel-heading">
                                BIỂU PHÍ HÀNG ORDER<div class="indicator right"></div>
                            </div>
                            <div class="panel-body" style="display: block">
                                <ul class="side-nav-ul">
                                    <li><a href="#">Tiền hàng trên web</a></li>
                                    <li><a href="#">Phí ship Trung Quốc</a></li>
                                    <li><a href="#">Phí dịch vụ mua hàng</a></li>
                                    <li><a href="#">Phí vận chuyển cân nặng</a></li>
                                    <li><a href="#">Phí kiểm đếm hàng hoá </a></li>
                                    <li><a href="#">Phí đóng gỗ</a></li>
                                    <li><a href="#">Phí ship hàng tận nhà</a></li>
                                </ul>
                            </div>
                        </div>
                        <div class="panel">
                            <div class="panel-heading">
                                CHÍNH SÁCH<div class="indicator right"></div>
                            </div>
                            <div class="panel-body">
                                <ul class="side-nav-ul">
                                    <li><a href="#">Tiền hàng trên web</a></li>
                                    <li><a href="#">Phí ship Trung Quốc</a></li>
                                    <li><a href="#">Phí dịch vụ mua hàng</a></li>
                                    <li><a href="#">Phí vận chuyển cân nặng</a></li>
                                    <li><a href="#">Phí kiểm đếm hàng hoá </a></li>
                                    <li><a href="#">Phí đóng gỗ</a></li>
                                    <li><a href="#">Phí ship hàng tận nhà</a></li>
                                </ul>
                            </div>
                        </div>--%>
                    </aside>

                    <div class="main-cont">
                        <div class="sec-tt">
                            <h1 class="tt-txt">
                                <asp:Label ID="lblTitle" runat="server"></asp:Label></h1>
                            <p class="deco">
                                <img src="/App_Themes/NHST/images/title-deco.png" alt="">
                            </p>
                        </div>
                        <div class="post-cont wow fadeInUp" data-wow-duration="1s" data-wow-delay=".6s">
                            <asp:Literal ID="ltrContent" runat="server"></asp:Literal>
                            <%--<div class="table-wrap">
                                <table>
                                    <tr>
                                        <th></th>
                                        <th colspan="2">Giải thích</th>
                                    </tr>
                                    <tr>
                                        <td>Chuyển phát nhanh thông thường</td>
                                        <td>Kg đầu dựa vào quy định của nhà cung cấp trên trang Taobao hoặc Alibaba thông thường là 10 tệ/kg đầu
                                        </td>
                                        <td>Kg tiếp theo 8 tệ</td>
                                    </tr>
                                    <tr>
                                        <td>Chuyển phát nhanh thông thường</td>
                                        <td>Kg đầu dựa vào quy định của nhà cung cấp trên trang Taobao hoặc Alibaba thông thường là 10 tệ/kg đầu
                                        </td>


                                    </tr>
                                </table>
                            </div>
                            <div class="article-block">
                                <h1>heading 1</h1>
                                <h2>heading 2</h2>
                                <h3>heading 3</h3>
                                <h4>heading 4</h4>
                                <h5>heading 5</h5>
                                <h6>heading 6</h6>
                                <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Libero suscipit fuga expedita aperiam quidem laborum, sint unde iure earum distinctio.</p>
                                <p>
                                    <img src="/App_Themes/NHST/images/logo.png" alt="">
                                </p>
                                <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Libero suscipit fuga expedita aperiam quidem laborum, sint unde iure earum distinctio.</p>
                            </div>
                            <div class="panels">
                                <div class="panel">
                                    <div class="panel-heading hl-txt">
                                        1. It is important to find the best dentist to do the restorative work on your teeth ?
                                        <div class="indicator"></div>
                                    </div>
                                    <div class="panel-body">
                                        Lorem ipsum dolor sit amet, consectetur adipisicing elit. Fugiat, blanditiis maiores dolore fugit iure quidem expedita doloribus! Assumenda modi ducimus obcaecati quasi, nobis ea, itaque, veritatis vel commodi corporis, ex?
                                   
                                    </div>
                                </div>
                                <div class="panel">
                                    <div class="panel-heading hl-txt">2. I am sure that the overall appearance of your ?</div>
                                    <div class="panel-body">
                                        Lorem ipsum dolor sit amet, consectetur adipisicing elit. Fugiat, blanditiis maiores dolore fugit iure quidem expedita doloribus! Assumenda modi ducimus obcaecati quasi, nobis ea, itaque, veritatis vel commodi corporis, ex?
                                   
                                    </div>
                                </div>
                                <div class="panel">
                                    <div class="panel-heading hl-txt">3. Teeth is very important to you and thus you will not make any compromise on seeking the best treatment ?</div>
                                    <div class="panel-body">
                                        Lorem ipsum dolor sit amet, consectetur adipisicing elit. Fugiat, blanditiis maiores dolore fugit iure quidem expedita doloribus! Assumenda modi ducimus obcaecati quasi, nobis ea, itaque, veritatis vel commodi corporis, ex?
                                   
                                    </div>
                                </div>
                            </div>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>
