<%@ Page Title="" Language="C#" MasterPageFile="~/ClientMaster.Master" AutoEventWireup="true" CodeBehind="Thanh-toan.aspx.cs" Inherits="NHST.Thanh_toan" %>

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
                        <div class="step active">
                            <div class="step-img">
                                <img src="/App_Themes/NHST/images/order-step-2.png" alt="">
                            </div>
                            <h4 class="title">Chọn địa chỉ nhận hàng</h4>
                        </div>
                        <div class="step ">
                            <div class="step-img">
                                <img src="/App_Themes/NHST/images/order-step-3.png" alt="">
                            </div>
                            <h4 class="title">Đặt cọc và kết đơn</h4>
                        </div>
                    </div>
                </div>
                <div class="sec checkout-sec">
                    <div class="checkout-left">
                        <h4 class="feat-tt">Thông tin tài khoản</h4>
                        <div class="order-addinfo">
                            <div>
                                <div class="form-row">
                                    <div class="lb">Họ tên</div>
                                    <asp:TextBox ID="txt_Fullname" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-row">
                                    <div class="lb">Địa chỉ</div>
                                    <asp:TextBox ID="txt_Address" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-row">
                                    <div class="lb">Email</div>
                                    <asp:TextBox ID="txt_Email" runat="server" ReadOnly="true" CssClass="form-control">></asp:TextBox>
                                </div>
                                <div class="form-row">
                                    <div class="lb">Số điện thoại</div>
                                    <asp:TextBox ID="txt_Phone" runat="server" ReadOnly="true" CssClass="form-control">></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <h4 class="feat-tt">Địa chỉ giao hàng</h4>
                        <div class="order-addinfo">
                            <div>
                                <div class="form-row">
                                    <div class="lb">Họ tên</div>
                                    <asp:TextBox ID="txt_DFullname" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rq1" ControlToValidate="txt_DFullname" runat="server" ErrorMessage="Không được để rỗng." ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-row">
                                    <div class="lb">Địa chỉ</div>
                                    <asp:TextBox ID="txt_DAddress" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txt_DAddress" runat="server" ErrorMessage="Không được để rỗng." ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-row">
                                    <div class="lb">Email</div>
                                    <asp:TextBox ID="txt_DEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txt_DEmail" runat="server" ErrorMessage="Không được để rỗng." ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-row">
                                    <div class="lb">Số điện thoại</div>
                                    <asp:TextBox ID="txt_DPhone" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txt_DPhone" runat="server" ErrorMessage="Không được để rỗng." ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-row btn-row">
                                <asp:CheckBox ID="chk_DK" runat="server" />
                                Tôi đồng ý với các <a href="/chinh-sach-5/dieu-khoan-dat-hang-15" style="color: blue;" target="_blank">điều khoản đặt hàng</a> của Nhập Hàng Siêu Tốc
                                <%--<a href="#" class="right btn pill-btn primary-btn">HOÀN TẤT</a>--%>
                            </div>
                            <div class="form-row btn-row">
                                <asp:Label ID="lblCheckckd" runat="server" Text="Vui lòng xác nhận trước khi hoàn tất đơn hàng." ForeColor="Red" Visible="false"></asp:Label>
                            </div>
                            <div class="form-row btn-row">
                                <a href="/gio-hang" class="left hl-txt link"><i class="fa fa-long-arrow-left"></i>Quay lại</a>
                                <asp:Button ID="btn_saveOrder" runat="server" OnClick="btn_saveOrder_Click" Text="HOÀN TẤT" CssClass="right btn pill-btn primary-btn" />
                                <%--<a href="#" class="right btn pill-btn primary-btn">HOÀN TẤT</a>--%>
                            </div>
                        </div>
                    </div>
                    <div class="checkout-right">
                        <asp:Literal ID="ltr_pro" runat="server"></asp:Literal>
                        <%--<div class="order-detail">
                            <table>
                                <tr class="borderbtm">
                                    <td colspan="2">
                                        <div class="thumb-product">
                                            <div class="pd-img">
                                                <img src="http://antien.vn/temp/-uploaded-tai%20nghe_tai-nghe-khong-day-bluetooth-beats-studio-wireless-gia-re-L7_thumb_1200x1200.jpg" alt=""><span class="badge">2</span></div>
                                            <div class="info">
                                                <a href="#">Tai nghe Dr. Beat màu đỏ,chỉnh hãng mỹ</a>
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <strong>5.000.000vnđ</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Subtotal</td>
                                    <td></td>
                                    <td><strong>5.000.000vnđ</strong></td>
                                </tr>
                                <tr>
                                    <td>Shipping</td>
                                    <td></td>
                                    <td><strong>5.000.000vnđ</strong></td>
                                </tr>
                                <tr class="borderbtm">
                                    <td>taxed</td>
                                    <td></td>
                                    <td><strong>5.000.000vnđ</strong></td>
                                </tr>
                                <tr>
                                    <td style="color: #959595; text-transform: uppercase"><strong>Tổng tiền</strong></td>
                                    <td></td>
                                    <td><strong class="hl-txt">5.000.000vnđ</strong></td>
                                </tr>
                            </table>
                        </div>--%>
                        <div class="form-row">
                            <div class="lb">Nhận hàng tại:</div>
                            <asp:DropDownList ID="ddlReceivePlace" runat="server" CssClass="form-control">
                                <asp:ListItem Value="Kho Hà Nội" Text="Kho Hà Nội"></asp:ListItem>
                                <asp:ListItem Value="Kho Hồ Chí Minh" Text="Kho Hồ Chí Minh"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>
