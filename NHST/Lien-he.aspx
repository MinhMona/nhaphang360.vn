<%@ Page Title="" Language="C#" MasterPageFile="~/1688Master.Master" AutoEventWireup="true" CodeBehind="Lien-he.aspx.cs" Inherits="NHST.Lien_he" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main>
        <div id="primary" class="index">
            <section id="sec-contact" class="sec sec-padd-50">
                <div class="container">
                    <h3 class="sec-tit text-center">
                        <span class="sub">Liên hệ</span>
                    </h3>
                    <div class="primary-form">
                        <div class="cont-wp clear black">
                            <aside class="side cont b500">
                                <h4 class="">NHẬP HÀNG 360 Xin Chân Thành Cám Ơn Bạn!</h4>
                                <p>Xin chân thành cám ơn bạn đã ghé thăm website của chúng tôi. Chúng tôi xin chúc bạn một ngày may mắn, tốt lành</p>
                                <ul class="contact">
                                    <asp:Literal ID="ltrContact" runat="server"></asp:Literal>
                                    <%-- <li>
                                        <span class="lbl">Điện thoại:</span>
                                        <span class="txt"><a href="tel:0126 922 0162">0126 922 0162 </a></span>
                                    </li>
                                    <li>
                                        <span class="lbl">Điện thoại:</span>
                                        <span class="txt"><a href="tel:0126 922 0162">0126 922 0162 </a></span>
                                    </li>
                                    <li>
                                        <span class="lbl">Email:</span>
                                        <span class="txt"><a href="mailto:giaodich1688@gmail.com">giaodich1688@gmail.com </a></span>
                                    </li>--%>
                                </ul>

                            </aside>
                            <aside class="side form">
                                <h4>Liên hệ</h4>
                                <div class="main-form">
                                    <div class="form-row">
                                        <div class="field">
                                            <asp:TextBox ID="txtFullname" CssClass="f-control required" placeholder="Họ và tên" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rq" runat="server" ControlToValidate="txtFullname" ErrorMessage="Vui lòng điền họ và tên"
                                                ForeColor="Red" Display="Dynamic" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                    <div class="form-row">
                                        <div class="field">
                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="f-control required" placeholder="Email">
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmail" ErrorMessage="Vui lòng điền email"
                                                ForeColor="Red" Display="Dynamic" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" ControlToValidate="txtEmail" ForeColor="Red"
                                                ErrorMessage="Sai định dạng Email" ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"
                                                ValidationGroup="a" Display="Dynamic" />
                                        </div>
                                    </div>
                                    <div class="form-row">
                                        <div class="field">
                                            <asp:TextBox ID="txtPhone" runat="server" CssClass="f-control required" placeholder="Số điện thoại">
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPhone" ErrorMessage="Vui lòng điền số điện thoại"
                                                ForeColor="Red" Display="Dynamic" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-row">
                                        <div class="field">
                                            <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" CssClass="f-control required" placeholder="Lời nhắn"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="submit">
                                        <asp:Button ID="btnRegister" runat="server" CssClass="main-btn hover submit-btn" Text="Gửi liên hệ"
                                            OnClick="btnRegister_Click" ValidationGroup="a" />
                                    </div>

                                </div>
                                <!--main-form-->

                            </aside>
                        </div>
                    </div>
                </div>
            </section>
        </div>
    </main>
</asp:Content>
