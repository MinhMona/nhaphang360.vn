<%@ Page Title="" Language="C#" MasterPageFile="~/dqgMaster.Master" AutoEventWireup="true" CodeBehind="danh-sach-san-pham.aspx.cs" Inherits="NHST.danh_sach_san_pham" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main>
        <div id="primary" class="index">
            <section id="firm-services" class="sec sec-padd-50">
                <div class="container">
                    <h3 class="sec-tit text-center">
                        <span class="sub">Sản phẩm</span>
                    </h3>
                    <div class="primary-form">
                        <ul class="its use-cols">
                            <asp:Literal ID="ltrProductHot" runat="server" EnableViewState="false"></asp:Literal>
                            <%--<li class="it col">
                                <div class="inner">
                                    <aside class="tmb">
                                        <img src="/App_Themes/vcdqg/images/product1.jpg" alt="" />
                                    </aside>
                                    <aside class="cont">
                                        <h4 class="web">Taobao.com</h4>
                                        <h5 class="tit">Áo khoác, đồ thể thao</h5>
                                    </aside>
                                </div>
                            </li>
                            <li class="it col">
                                <div class="inner">
                                    <aside class="tmb">
                                        <img src="/App_Themes/vcdqg/images/product2.jpg" alt="" />
                                    </aside>
                                    <aside class="cont">
                                        <h4 class="web">Taobao.com</h4>
                                        <h5 class="tit">Áo khoác, đồ thể thao</h5>
                                    </aside>
                                </div>
                            </li>
                            <li class="it col">
                                <div class="inner">
                                    <aside class="tmb">
                                        <img src="/App_Themes/vcdqg/images/product3.jpg" alt="" />
                                    </aside>
                                    <aside class="cont">
                                        <h4 class="web">Taobao.com</h4>
                                        <h5 class="tit">Áo khoác, đồ thể thao</h5>
                                    </aside>
                                </div>
                            </li>
                            <li class="it col">
                                <div class="inner">
                                    <aside class="tmb">
                                        <img src="/App_Themes/vcdqg/images/product4.jpg" alt="" />
                                    </aside>
                                    <aside class="cont">
                                        <h4 class="web">Taobao.com</h4>
                                        <h5 class="tit">Áo khoác, đồ thể thao</h5>
                                    </aside>
                                </div>
                            </li>
                            <li class="it col">
                                <div class="inner">
                                    <aside class="tmb">
                                        <img src="/App_Themes/vcdqg/images/product5.jpg" alt="" />
                                    </aside>
                                    <aside class="cont">
                                        <h4 class="web">Taobao.com</h4>
                                        <h5 class="tit">Áo khoác, đồ thể thao</h5>
                                    </aside>
                                </div>
                            </li>
                            <li class="it col">
                                <div class="inner">
                                    <aside class="tmb">
                                        <img src="/App_Themes/vcdqg/images/product6.jpg" alt="" />
                                    </aside>
                                    <aside class="cont">
                                        <h4 class="web">Taobao.com</h4>
                                        <h5 class="tit">Áo khoác, đồ thể thao</h5>
                                    </aside>
                                </div>
                            </li>

                            <li class="it col">
                                <div class="inner">
                                    <aside class="tmb">
                                        <img src="/App_Themes/vcdqg/images/product1.jpg" alt="" />
                                    </aside>
                                    <aside class="cont">
                                        <h4 class="web">Taobao.com</h4>
                                        <h5 class="tit">Áo khoác, đồ thể thao</h5>
                                    </aside>
                                </div>
                            </li>
                            <li class="it col">
                                <div class="inner">
                                    <aside class="tmb">
                                        <img src="/App_Themes/vcdqg/images/product2.jpg" alt="" />
                                    </aside>
                                    <aside class="cont">
                                        <h4 class="web">Taobao.com</h4>
                                        <h5 class="tit">Áo khoác, đồ thể thao</h5>
                                    </aside>
                                </div>
                            </li>
                            <li class="it col">
                                <div class="inner">
                                    <aside class="tmb">
                                        <img src="/App_Themes/vcdqg/images/product3.jpg" alt="" />
                                    </aside>
                                    <aside class="cont">
                                        <h4 class="web">Taobao.com</h4>
                                        <h5 class="tit">Áo khoác, đồ thể thao</h5>
                                    </aside>
                                </div>
                            </li>
                            <li class="it col">
                                <div class="inner">
                                    <aside class="tmb">
                                        <img src="/App_Themes/vcdqg/images/product4.jpg" alt="" />
                                    </aside>
                                    <aside class="cont">
                                        <h4 class="web">Taobao.com</h4>
                                        <h5 class="tit">Áo khoác, đồ thể thao</h5>
                                    </aside>
                                </div>
                            </li>
                            <li class="it col">
                                <div class="inner">
                                    <aside class="tmb">
                                        <img src="/App_Themes/vcdqg/images/product5.jpg" alt="" />
                                    </aside>
                                    <aside class="cont">
                                        <h4 class="web">Taobao.com</h4>
                                        <h5 class="tit">Áo khoác, đồ thể thao</h5>
                                    </aside>
                                </div>
                            </li>
                            <li class="it col">
                                <div class="inner">
                                    <aside class="tmb">
                                        <img src="/App_Themes/vcdqg/images/product6.jpg" alt="" />
                                    </aside>
                                    <aside class="cont">
                                        <h4 class="web">Taobao.com</h4>
                                        <h5 class="tit">Áo khoác, đồ thể thao</h5>
                                    </aside>
                                </div>
                            </li>--%>

                        </ul>
                        <div class="services-list clearfix">
                            <asp:Literal ID="ltrList" runat="server" EnableViewState="false"></asp:Literal>
                            <div class="pagination">
                                <%this.DisplayHtmlStringPaging1();%>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </div>
    </main>

</asp:Content>
